using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Net;

using System.Threading;

using Newtonsoft.Json;
using ModulesCommon;


namespace LumbarRobot.ModulesCommon
{
    public class GlobalVar
    {
        Thread _receiverThread;
        private static Dictionary<string, ServerLog> servers;
        private static string serverIp = "127.0.0.1";//System.Configuration.ConfigurationSettings.AppSettings["ServerIP"].ToString();
       // private static string LeftView3D = System.Configuration.ConfigurationSettings.AppSettings["LeftView3D"].ToString();
       // private static string LeftView2D = System.Configuration.ConfigurationSettings.AppSettings["LeftView2D"].ToString();
       // private static string LeftCarma = System.Configuration.ConfigurationSettings.AppSettings["LeftCarma"].ToString();
       // public static string PScreenIndex = System.Configuration.ConfigurationSettings.AppSettings["PatientScreenIndex"].ToString();
       // private static string RightView3D = System.Configuration.ConfigurationSettings.AppSettings["RightView3D"].ToString();
       // private static string RightView2D = System.Configuration.ConfigurationSettings.AppSettings["RightView2D"].ToString();
       //private static string RightCarma = System.Configuration.ConfigurationSettings.AppSettings["RightCarma"].ToString();
       // public static string InitiatedByEyes = System.Configuration.ConfigurationSettings.AppSettings["InitiatedByEyes"].ToString();
        /// <summary>
        /// 是否显示 眼动按钮
        /// </summary>
        //public static bool IsShowEyesButton = Convert.ToBoolean(System.Configuration.ConfigurationSettings.AppSettings["IsShowEyesButton"].ToString());
        /// <summary>
        /// 角速度
        /// </summary>
        //public static int AngleVelocity =Convert.ToInt32(System.Configuration.ConfigurationSettings.AppSettings["AngleVelocity"].ToString());
        public static IPAddress serverIP = IPAddress.Parse(serverIp);
        public static int serverPort = Convert.ToInt32("111");//System.Configuration.ConfigurationSettings.AppSettings["ServerPort"].ToString());
        public static int listenPort = Convert.ToInt32("123");//System.Configuration.ConfigurationSettings.AppSettings["ListenPort"].ToString());
        //static ULTRobotController robotControl = null;
        private static string gameId;

        public static bool IsPlay = false;
        public static bool IsStop = false;

        public static  int MaxValue;

        public static int MinValue;

        
        private GlobalVar() { }
        /// <summary>
        /// 玩游戏手臂
        /// </summary>
        public static int GameSelectHand = 0;
        /// <summary>
        /// 当前所选游戏
        /// </summary>
        public static string GameId
        {
            get { return GlobalVar.gameId; }
            set { GlobalVar.gameId = value; }
        }
        /// <summary>
        /// 数据包发送
        /// </summary>
        /// <param name="metaData">数据对象的json字符串</param>
        /// <param name="cmdType">数据包类型</param>
        public static void SendCommand(string metaData, CommandType cmdType)
        {
            //ThreadPool.QueueUserWorkItem((x) =>
            //{
                List<string> list = new List<string>();
                list.Add(Convert.ToInt32(cmdType).ToString());
                list.Add(metaData);
                string SenderName = "ULTClient";
                OSCHandler.Instance.SendMessageToClient<string>(SenderName, "address/folder", list);
        
           // }, null);

        }

        //public static ULTRobotController RobotControl
        //{
        //    get { return robotControl; }
        //    set { robotControl = value; }
        //}

        static CMDClient cmdClient = new CMDClient(serverIP, serverPort, "None");

        public static CMDClient Client
        {
            get { return GlobalVar.cmdClient; }
            set { GlobalVar.cmdClient = value; }
        }


        static IDataCommunication dataCommunication = null;

        public static IDataCommunication DataCommunication
        {
            get { return dataCommunication; }
            set { dataCommunication = value; }
        }

        static DataCommunicationService dataService = null;

        public static DataCommunicationService DataService
        {
            get { return dataService; }
            set { dataService = value; }
        }


        //static UltrobotDataContext dataContext = new UltrobotDataContext();

        ///// <summary>
        ///// 数据上下文
        ///// </summary>
        //public static UltrobotDataContext DataContext
        //{
        //    get { return GlobalVar.dataContext; }
        //    set { GlobalVar.dataContext = value; }
        //}

        //public UltrobotDataContext DataContext1()
        //{
        //    return null;
        //}

        ///// <summary>
        ///// 
        ///// </summary>
        //static UserContext userContext = new UserContext();

        //public static UserContext UserContext
        //{
        //    get
        //    {
        //        return GlobalVar.userContext;
        //    }
        //    set { GlobalVar.userContext = value; }
        //}

        static int spaceTime = 10;


        static int changeValue = 10;
        /// <summary>
        /// 变化的档位
        /// </summary>
        public static int ChangeValue
        {
            get { return GlobalVar.changeValue; }
            set { GlobalVar.changeValue = value; }
        }


        /// <summary>
        /// 间隔时间
        /// 默认值 10秒
        /// </summary>
        public static int SpaceTime
        {
            get { return GlobalVar.spaceTime; }
            set { GlobalVar.spaceTime = value; }
        }

        private static long startTimeTicks;

        public static long StartTimeTicks
        {
            get { return startTimeTicks; }
            set
            {
                startTimeTicks = value;
            }
        }

        private static bool isShowDialog;

        public static bool IsShowDialog 
        {
            get { return isShowDialog; }
            set
            {
                isShowDialog = value;
                //GlobalVar.RobotControl.IsCanAlarmOrQuickStart = !value;
            }
        }

        private static MyLock dispatcherLock = new MyLock();

        public static MyLock DispatcherLock 
        {
            get { return dispatcherLock; } 
        }



        private static string _GameInitInfo;

        public static string GameInitInfo
        {
            get { return _GameInitInfo; }
            set { _GameInitInfo = value; }
        }


        /// <summary>
        /// Occurs when the network had been failed.
        /// </summary>
        public static event CameraMoveEventHandler CameraMove;
        /// <summary>
        /// Occurs when the network had been failed.
        /// </summary>
        public static event GameReturnEventHandler GameReturn;

        public static event EyeMoveInfonEventHandler EyeMove;
       
        public  static event StartInitEventHandler StartInit;

        public static void GetData()
        {

            while (true)
            {
                OSCHandler.Instance.UpdateLogs();
                servers = OSCHandler.Instance.Servers;

                foreach (KeyValuePair<string, ServerLog> item in servers)
                {

                    lock (item.Value.log)
                    {
                        if (item.Value.log.Count > 0)
                        {
                            try
                            {
                                if (item.Value.packets[0].Data.Count == 2)
                                {
                                  

                                    if ((CommandType)Convert.ToInt32(item.Value.packets[0].Data[0].ToString()) == CommandType.CameraCommand)
                                    {
                                        CarmaEventArgs arg = new CarmaEventArgs();
                                        arg.ComnandType = Convert.ToInt32(item.Value.packets[0].Data[0].ToString());
                                        CameraInfo info = JsonConvert.DeserializeObject<CameraInfo>(item.Value.packets[0].Data[1].ToString());
                                        //arg.Info = info;
                                        CameraMove(arg);
                                    }

                                    if ((CommandType)Convert.ToInt32(item.Value.packets[0].Data[0].ToString()) == CommandType.GameResultData)
                                    {
                                        GameReturnEventArgs arg = new GameReturnEventArgs();
                                        arg.ComnandType = Convert.ToInt32(item.Value.packets[0].Data[0].ToString());
                                        GameResultInfo info = JsonConvert.DeserializeObject<GameResultInfo>(item.Value.packets[0].Data[1].ToString());
                                       // arg.Info = info;
                                        GameReturn(arg);
                                    }
                                    if ((CommandType)Convert.ToInt32(item.Value.packets[0].Data[0].ToString()) == CommandType.EyesPlayCommand)
                                    {
                                        EyeMoveInfonEventArgs arg = new EyeMoveInfonEventArgs();
                                        arg.ComnandType = Convert.ToInt32(item.Value.packets[0].Data[0].ToString());
                                        MoveCommandInfo info = JsonConvert.DeserializeObject<MoveCommandInfo>(item.Value.packets[0].Data[1].ToString());
                                        EyeMove(arg);

                                    }


                                }
                                else if (item.Value.packets[0].Data.Count > 2)
                                {
                                    if (item.Value.packets[0].Data[2].ToString() == "OK")
                                    {
                                        GameInitializeInfo Initinfo = new GameInitializeInfo();

                                        Initinfo.Position_X_Min = MaxValue;
                                      
                                        Initinfo.Position_X_Max = MinValue;

                                        string initMetaData = JsonConvert.SerializeObject(Initinfo);
                                  
                                        if (!string.IsNullOrEmpty(initMetaData))
                                        {                                       
                                            // LogManager.Debug("ok----"+GameInitInfo);
                                            GlobalVar.SendCommand(initMetaData, CommandType.GameInitializeCommand);

                                        }

                                       

                                      
                                    }
                                    else if (item.Value.packets[0].Data[2].ToString() == "OKTrainning")
                                    {
                                        StartInitEventArgs s = new StartInitEventArgs();
                                        s.InitType = 1;
                                        //StartInit(s);
                                        //if (ModulesCommon.ModuleConstant.PatientId != "")
                                        //{
                                        //    TrajectoryInfo tinfo = new TrajectoryInfo();
                                        //    tinfo.IsClear = true; //清除
                                        //    tinfo.IsCrate = false;
                                        //    string clearStr = JsonConvert.SerializeObject(tinfo);
                                        //    SendCommand(clearStr, CommandType.CreateTrajectory);

                                        //}


                                    }


                                }
                            }
                            catch
                            {
 
                            }
                            item.Value.log.RemoveAt(0);
                            item.Value.packets.RemoveAt(0);
                        }
                    }
                }
                Thread.Sleep(10);
            }
        }

    }
}
