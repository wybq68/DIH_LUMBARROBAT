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
using System.Windows.Navigation;
using System.Windows.Shapes;
using LumbarRobot.Services;
using System.Configuration;
using System.Threading;
using Newtonsoft.Json;
using LumbarRobot.Common;
using ModulesCommon;
using System.Diagnostics;
using LumbarRobot.Protocol;
using LumbarRobot.DAL;
using LumbarRobot.Data;
using LumbarRobot.Common.Enums;

namespace LumbarRobot
{
    /// <summary>
    /// GameView.xaml 的交互逻辑
    /// </summary>
    public partial class GameView : UserControl
    {
        int ReciveModel;
        Thread GaReciveThread;
        /// <summary>
        /// 与游戏通信接口
        /// </summary>
        OSCHandler osc;

       public GamePram gp = new GamePram();

       DataPackReceivedHandler recdata;
        /// <summary>
        /// 初始化
        /// </summary>
        public void Init()
        {
            recdata = new DataPackReceivedHandler(DataCommunication_Move);
            this.setControlMaxAngle.Value = Convert.ToInt32(gp.RotationMax);
            this.setControlMinAngle.Value = Convert.ToInt32(-gp.RotationMin);
            this.setControlMaxAngle.MaxValue = Convert.ToInt32(gp.RotationMax);
            this.setControlMinAngle.MinValue = Convert.ToInt32(-gp.RotationMin);
            //GlobalVar.GameReturn+=GlobalVar_GameReturn;
            LumbarRobotController.RobotController.ControlCommand.DataPackRecieved += recdata;
            //LumbarRobotController.RobotController.Move += DataCommunication_Move;
            osc = OSCHandler.Instance;
            int Gameport = int.Parse(ConfigurationManager.AppSettings["ListenPort"]);
            if (Gameport > 0)
            {
                if (osc.Servers.Count < 1)
                {
                    osc.Init(Gameport);
                    GaReciveThread = new Thread(new ThreadStart(LumbarRobot.ModulesCommon.GlobalVar.GetData));
                    GaReciveThread.Start();
                }
               

            }



        }


        /// <summary>
        /// 传输数据
        /// </summary>
        /// <param name="args"></param>
        void DataCommunication_Move(DataPackReceivedEventArgs args)
        {
            GameRunInfo tinfo = new GameRunInfo();
            if (args.ResponsePackageData.Code == ReciveModel)//屈伸
            {

                ThreadPool.QueueUserWorkItem((x) =>
                {

                if (ReciveModel == ResponseCodes.BendStretchData)
                    {
                        tinfo.RunVlue = args.ResponsePackageData.BendStretchAngle;

                    }
                    else
                    {
                        tinfo.RunVlue = args.ResponsePackageData.RotationAngle;
                    }
                    
                    string runMetaData = JsonConvert.SerializeObject(tinfo);
                    LumbarRobot.ModulesCommon.GlobalVar.SendCommand(runMetaData, LumbarRobot.ModulesCommon.CommandType.GameRunCommand);
                });


            }
            //else if (args.ResponsePackageData.Code == ReciveModel)//旋转
            //{
            //    ThreadPool.QueueUserWorkItem((x) =>
            //    {
            //        List<string> list = new List<string>();
            //        list.Add(args.ResponsePackageData.RotationAngle.ToString());
            //        list.Add(GetJson());
            //        osc.SendMessageToClient<string>("ULTClient", "address/folder", list);

            //    });
            //}
        }

        #region 事件
         public event EventHandler Close;
        #endregion

        #region 构造
        public GameView()
        {
            InitializeComponent();
        }
        #endregion

        #region 加载事件
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            ActionList.SelectedIndex = 1;
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
            LumbarRobotController.RobotController.Connected += new EventHandler(RobotController_Connected);
            Init();
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
        #endregion

        #region 返回
        private void rtnBtn_Click(object sender, RoutedEventArgs e)
        {
            LumbarRobotController.RobotController.ControlCommand.DataPackRecieved -= recdata;
            
                this.Close(sender, e);
          //this.Close();
            
        }
        #endregion

        #region 游戏事件
        private void BtnGame_Click(object sender, RoutedEventArgs e)
        {
            //最大值
            int MaxAngle=this.setControlMaxAngle.Value;
            //最小值
            int MinAngle = this.setControlMinAngle.Value;
            //力量
            int Force = this.setControlForce.Value;
            //模式
            int ModeId = ActionList.SelectedIndex;

            int PramValues = Force;
            LumbarRobot.ModulesCommon.GlobalVar.MaxValue = MaxAngle;
            LumbarRobot.ModulesCommon.GlobalVar.MinValue = MinAngle;
            int tage = 0;
            if (ModeId == 1)
            {
                ReciveModel = ResponseCodes.RotationData;
                LumbarRobotController.RobotController.ControlCommand.RotationFree(PramValues, 0, LumbarRobot.ModulesCommon.GlobalVar.MaxValue, LumbarRobot.ModulesCommon.GlobalVar.MinValue); //第一个参数为力量 设置旋转模式
                
            }
            else if (ModeId == 2)
            {
                ReciveModel = ResponseCodes.BendStretchData;
                LumbarRobotController.RobotController.ControlCommand.BendStretchFree(PramValues, tage, LumbarRobot.ModulesCommon.GlobalVar.MaxValue, LumbarRobot.ModulesCommon.GlobalVar.MinValue);//设置力量第一个参数为力量设置屈伸模式
            }

            ThreadPool.QueueUserWorkItem((x) => {

                try
                {
                    string GamePath = System.Environment.CurrentDirectory;
                    GamePath += @"\Conveyor3D.exe";
                    if (GamePath != null)
                    {
                        Process GameProcess = new Process();
                        GameProcess.StartInfo.CreateNoWindow = false;
                        GameProcess.StartInfo.FileName = GamePath;

                        GameProcess.Start();
                        IntPtr test = GameProcess.Handle;

                    }

                }
                catch (Exception ex)
                {
                    LogManager.LogException(ex);
                    MessageBox.Show("游戏起动失败！请联系管理员");

                }
            
            });
           
        }
        #endregion

        private void ActionList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ActionList.SelectedIndex == 1)
            {
                this.setControlMaxAngle.Value = Convert.ToInt32(gp.RotationMax);
                this.setControlMinAngle.Value = Convert.ToInt32(-gp.RotationMin);
                this.setControlMaxAngle.MaxValue = Convert.ToInt32(gp.RotationMax);
                this.setControlMinAngle.MinValue = Convert.ToInt32(-gp.RotationMin);
            }
            else if(ActionList.SelectedIndex==2)
            {
                this.setControlMaxAngle.Value = Convert.ToInt32(gp.BendStretchMax);
                this.setControlMinAngle.Value = Convert.ToInt32(-gp.BendStretchMin);
                this.setControlMaxAngle.MaxValue = Convert.ToInt32(gp.BendStretchMax);
                this.setControlMinAngle.MinValue = Convert.ToInt32(-gp.BendStretchMin);
            }
        }
    }
}
