using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using LumbarRobot.Common;
using Microsoft.Win32;
using System.Reflection;
using System.Windows.Navigation;
using LumbarRobot.ViewModels;
using LumbarRobot.DAL;
using LumbarRobot.Data;
using NHibernate.Cfg;
using NHibernate;
using NHibernate.Context;
using NHibernate.Linq;
using LumbarRobot.Services;
using LumbarRobot.Requests;
using LumbarRobot.Interactions;
using LumbarRobot.MyUserControl;


namespace LumbarRobot
{
    /// <summary>
    /// Login.xaml 的交互逻辑
    /// </summary>
    public partial class Login : Window
    {
        #region 属性
        public bool IsLogin = false;
        #endregion

        #region 通知消息
        private LumbarRobot.Services.LumbarRobotController.AlarmHandler AlarmEvent = null;
        /// <summary>
        /// 通知消息
        /// </summary>
        public GenericInteractionRequest<AlarmInfo> NotificationToAlarm { get; private set; }

        MyMessageBox_Login msgBox = null;
        #endregion

        #region 加载事件
        public Login()
        {
            InitializeComponent();
            NotificationToAlarm = new GenericInteractionRequest<AlarmInfo>();            
            DataContext = new LoginViewModel();
            PwdBox.KeyDown += (sender, e) => { if (e.Key == Key.Enter)logBtn_Click(null, null); };

        }
        #endregion

        #region 开机启动
        /// <summary> 
        /// 设置程序开机启动 
        /// 或取消开机启动 
        /// </summary> 
        /// <param name="started">设置开机启动，或者取消开机启动</param> 
        /// <param name="exeName">注册表中程序的名字</param> 
        /// <param name="path">开机启动的程序路径</param> 
        /// <returns>开启或则停用是否成功</returns> 
        public static bool runWhenStart(bool started, string exeName, string path)
        {
            RegistryKey key = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);//打开注册表子项 
            if (key == null)//如果该项不存在的话，则创建该子项 
            {
                key = Registry.LocalMachine.CreateSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run");
            }
            if (started == true)
            {
                try
                {
                    key.SetValue(exeName, path);//设置为开机启动 
                    key.Close();
                }
                catch
                {
                    return false;
                }
            }
            else
            {
                try
                {
                    key.DeleteValue(exeName);//取消开机启动 
                    key.Close();
                }
                catch
                {
                    return false;
                }
            }
            return true;
        }

        #endregion

        #region 启动键盘
        private void txtCode_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            Machine.OpenTabtip();
        }

        private void PwdBox_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            //调用软键盘
            Machine.OpenTabtip();
        }
        #endregion

        #region 登录
        private void logBtn_Click(object sender, RoutedEventArgs e)
        {
            LumbarRobot.Services.LumbarRobotController.RobotController.ControlCommand.PauseCmd();

            var list = from od in MySession.Session.Query<Sysuserinfo>()
                       where od.UserCode == this.txtCode.Text.Trim() && od.SysPassWord == this.PwdBox.Password
                       select od;
            if (list != null && list.Count() > 0)
            {
                ModuleConstant.UserID = list.ToList()[0].Id;
                ModuleConstant.UserName = list.ToList()[0].UserName;
                IntPtr hwnd = WindowsAPI.FindWindow("IPTip_Main_Window", null);
                WindowsAPI.SendMessage(hwnd, WindowsAPI.WM_SYSCOMMAND, WindowsAPI.SC_CLOSE, 0);

                if (GlobalVar.MainWindows != null)
                {
                    (GlobalVar.MainWindows as MainWindow).Close();
                }
                MainWindow window = new MainWindow();
                GlobalVar.MainWindows = window;
                window.Show();
                this.Close();
            }
            else
            {
                if (MessageBox.Show("请输入正确的用户名和密码！", "提示信息", MessageBoxButton.OKCancel, MessageBoxImage.Error) == MessageBoxResult.OK)
                {
                }
            }
        }
        #endregion

        #region 加载Load事件
        private void Window_Closed(object sender, EventArgs e)
        {
            if (AlarmEvent != null)
            {
                LumbarRobotController.RobotController.Alarm -= AlarmEvent;
                AlarmEvent = null;
            }
        }

        private void Window_Unloaded(object sender, RoutedEventArgs e)
        {
            
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            

            if (LumbarRobotController.RobotController.IsConnected)
            {
                this.Light.light_0.Visibility = Visibility.Visible;
                this.Light.light_1.Visibility = Visibility.Collapsed;
            }
            else
            {
                this.Light.light_0.Visibility = Visibility.Collapsed;
                this.Light.light_1.Visibility = Visibility.Visible;
            }

            if (IsLogin)
            {
                Loading.Visibility = Visibility.Collapsed;
                this.SetVidicon.Visibility = Visibility.Collapsed;
                this.SpInitialise.Visibility = Visibility.Collapsed;
                this.BLogin.Visibility = Visibility.Visible;
                isCanClick = true;
            }

            LumbarRobotController.RobotController.Disconnected += new EventHandler(RobotController_Disconnected);
            LumbarRobotController.RobotController.Connected += new EventHandler(RobotController_Connected);

            ShowAlarmDialog();

            AlarmEvent = new LumbarRobotController.AlarmHandler(RobotController_Alarm);
            LumbarRobotController.RobotController.Alarm += AlarmEvent;
   
        }

        void RobotController_Connected(object sender, EventArgs e)
        {
            if (LumbarRobotController.RobotController.IsConnected)
            {
                Application.Current.Dispatcher.BeginInvoke(new Action(() =>
                {
                    this.Light.light_0.Visibility = Visibility.Visible;
                    this.Light.light_1.Visibility = Visibility.Collapsed;
                }));
            }
        }

        void RobotController_Disconnected(object sender, EventArgs e)
        {
            if (!LumbarRobotController.RobotController.IsConnected)
            {
                Application.Current.Dispatcher.BeginInvoke(new Action(() =>
                {
                    this.Light.light_0.Visibility = Visibility.Collapsed;
                    this.Light.light_1.Visibility = Visibility.Visible;
                }));
            }
        }
        #endregion

        #region 确定
        bool isCanClick = true;

        private void BtnCorrect_Click(object sender, RoutedEventArgs e)
        {
            lock (this)
            {
                if (!isCanClick) return;
                this.SetVidicon.Visibility = Visibility.Visible;
                //Loading.Visibility = Visibility.Visible;
                isCanClick = false;

                System.Threading.ThreadPool.QueueUserWorkItem((x) =>
                {
                    //暂停
                    LumbarRobotController.RobotController.ControlCommand.PauseCmd();
                    System.Threading.Thread.Sleep(200);
                    //homing
                    LumbarRobotController.RobotController.ControlCommand.Homing();

                    //等待homing结束
                    while (LumbarRobotController.RobotController.HomingAngle == 99)
                    {
                        System.Threading.Thread.Sleep(1);
                    }

                    Application.Current.Dispatcher.BeginInvoke(new Action(() =>
                    {
                        Loading.Visibility = Visibility.Collapsed;
                        this.SetVidicon.Visibility = Visibility.Collapsed;
                        this.SpInitialise.Visibility = Visibility.Collapsed;
                        this.BLogin.Visibility = Visibility.Visible;
                        isCanClick = true;

                    }));
                });

            }
        }
        #endregion

        #region 通知事件
        private void GetAlarmCallBack(AlarmInfo a)
        {
            //取消报警
            LumbarRobotController.RobotController.ControlCommand.ErrorReset();
        }

        private void CancelCallback()
        {

        }
        #endregion

        #region 显示警告对话框
        private void ShowAlarmDialog()
        {
            lock (this)
            {
                if (msgBox != null)
                {
                    return;
                }
                if (LumbarRobotController.RobotController.AlarmCode != 0)
                {
                    LumbarRobot.Services.LumbarRobotController.AlarmArgs args = new LumbarRobotController.AlarmArgs();
                    args.AlarmCode = LumbarRobotController.RobotController.AlarmCode;

                    msgBox = new MyMessageBox_Login();
                    msgBox.Width = 1366;
                    //msgBox.Height = 768;
                    msgBox.Top = 80;
                    msgBox.Left = 0;
                    msgBox.lblMsg.Text = "出现" + args.ToString() + "，如无异常，请点击确认，继续训练！";
                    msgBox.lblTitle.Text = "提示信息";
                    msgBox.Topmost = true;
                    msgBox.BtnIsEnable += new EventHandler(msgBox_BtnIsEnable);

                    msgBox.Show();
                }
            }
        }

        void msgBox_BtnIsEnable(object sender, EventArgs e)
        {
            LumbarRobotController.RobotController.ControlCommand.ErrorReset();
            if (msgBox != null)
            {
                msgBox.Close();
                msgBox = null;
            }

            System.Threading.Thread.Sleep(100);
            if (LumbarRobotController.RobotController.AlarmCode != 0)
            {
                ShowAlarmDialog();
            }
        }

        void RobotController_Alarm(LumbarRobotController.AlarmArgs e)
        {

            Application.Current.Dispatcher.Invoke(new Action(() =>
            {
                ShowAlarmDialog();
            }));
        }
        #endregion
       
    }
}
