using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Windows;
using LumbarRobot.DAL;
using LumbarRobot.Common;
//using LumbarRobot.Services;
using Microsoft.Win32;
using LumbarRobot.Services;
using LumbarRobot.Communication;
using System.Threading;
using System.Globalization;
using System.Management;

namespace LumbarRobot
{
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo("zh-CN");
            Thread.CurrentThread.CurrentCulture = (CultureInfo)Thread.CurrentThread.CurrentCulture.Clone();
            Thread.CurrentThread.CurrentCulture.DateTimeFormat.ShortDatePattern = "yyyy-MM-dd";
            InitializeComponent();
        } 
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            MySession.Init(new NHibernate.Cfg.Configuration().Configure());

            log4net.Config.XmlConfigurator.Configure();

            this.DispatcherUnhandledException += new System.Windows.Threading.DispatcherUnhandledExceptionEventHandler(Application_DispatcherUnhandledException);
            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);

            this.Exit += new ExitEventHandler(App_Exit);

            //创建化控制器
            LumbarRobotController.RobotController = new LumbarRobotController();
            //创建数据通讯接口
            if (ConfigurationManager.AppSettings["CommunicationType"] != null)
            {
                if (ConfigurationManager.AppSettings["CommunicationType"].ToLower() == "cannet")
                {
                    if (ConfigurationManager.AppSettings["CanPort"] != null && ConfigurationManager.AppSettings["CanIP"] != null)
                    {
                        GlobalVar.DataCommunication = new CanNetDataCommunication(ConfigurationManager.AppSettings["CanIP"], int.Parse(ConfigurationManager.AppSettings["CanPort"]));

                        LumbarRobotController.RobotController.Init(GlobalVar.DataCommunication);
                    }
                }
                else
                {
                    GlobalVar.DataCommunication = new CanDataCommunication();

                    LumbarRobotController.RobotController.Init(GlobalVar.DataCommunication);
                }
            }
        }

        void App_Exit(object sender, ExitEventArgs e)
        {
            LumbarRobotController.RobotController.ControlCommand.QuickStopCmd();
            LumbarRobotController.RobotController.Close();

            MySession.DisposeContextSession();

            try
            {
                //记录开机时间
                var key = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Default);
                var subkey = key.OpenSubKey(@"software\dih\lumbarrobot\", true);
                if (subkey == null)
                {
                    subkey = key.CreateSubKey(@"software\dih\lumbarrobot\",RegistryKeyPermissionCheck.ReadWriteSubTree);
                }

                object sum = subkey.GetValue("SumTicks");
                long sumLong;

                if (sum != null && long.TryParse(sum.ToString(), out sumLong))
                {
                    subkey.SetValue("SumTicks", sumLong + DateTime.Now.Ticks - GlobalVar.StartTimeTicks);
                }
                else
                {
                    subkey.SetValue("SumTicks", DateTime.Now.Ticks - GlobalVar.StartTimeTicks);
                }
            }
            catch (Exception ee)
            {
                LogManager.LogException(ee);
            }

            LogManager.Log("关机时间：" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));

            System.Environment.Exit(0);
        }

        void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            LumbarRobotController.RobotController.ControlCommand.QuickStopCmd();
            LumbarRobotController.RobotController.Close();

            MySession.DisposeContextSession();

            Exception ex = e.ExceptionObject as Exception;
            LogManager.LogException(ex);
        }

        private void Application_DispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
         {
            LumbarRobotController.RobotController.ControlCommand.QuickStopCmd();
            Exception ex = e.Exception;
            LogManager.LogException(ex);
        }
    }
}
