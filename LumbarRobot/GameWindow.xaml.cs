using LumbarRobot.Common;
using LumbarRobot.Common.Enums;
using LumbarRobot.Communication;
using LumbarRobot.DAL;
using LumbarRobot.Data;
using LumbarRobot.ModulesCommon;
using LumbarRobot.Protocol;
using LumbarRobot.Services;
using ModulesCommon;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;


namespace LumbarRobot
{
    /// <summary>
    /// GameWindow.xaml 的交互逻辑
    /// </summary>
    public partial class GameWindow : Window 
    {
        public GameWindow()
        {
            InitializeComponent();
          
           
        }
        Thread GaReciveThread;

        private string _UserID;

        public string UserID
        {
            get { return _UserID; }
            set { _UserID = value; }
        }
        

        private String _GameIP;
        /// <summary>
        /// 游戏IP
        /// </summary>
        public String GameIP
        {
            get { return _GameIP; }
            set { _GameIP = value; }
        }

        private int _GameProt;
        /// <summary>
        /// 游戏端口号
        /// </summary>
        public int GameProt
        {
            get { return _GameProt; }
            set { _GameProt = value; }
        }



     
        /// <summary>
        /// 与游戏通信接口
        /// </summary>
        OSCHandler osc;
        /// <summary>
        /// 序例化与游戏
        /// </summary>
        TrajectoryInfo tinfo = new TrajectoryInfo();

        GamePram gp = new GamePram();

        int ReciveModel;

        /// <summary>
        /// 初始化
        /// </summary>
        public void Init()
        {

            if(UserID!=null)
            {
                //var LeftRang = from eva in MySession.Query<EvaluteDetail>() where eva.PatientID == UserID && eva.ActionId==(int)EvaluateActionEnum.RotationRangeLeft orderby eva.EvaluteDate descending select eva;
                //if (LeftRang.Count() > 0)
                //{
                //    gp.RandMax = LeftRang.First().MaxV;
                //}
                //else
                //{
                //    MessageBox.Show("该患者没有评测记录，请返回评测");
                //    this.Game1.IsEnabled = false;
                //    return;
                //}
                //var RightRang = from eva in MySession.Query<EvaluteDetail>() where eva.PatientID == UserID && eva.ActionId == (int)EvaluateActionEnum.RotationRangeRight orderby eva.EvaluteDate descending select eva;
                //if (LeftRang.Count() > 0)
                //{
                //    gp.RanMin = RightRang.FirstOrDefault().MaxV;
                //}
                //else
                //{
                //    MessageBox.Show("该患者没有评测记录，请返回评测");
                //    this.Game1.IsEnabled = false;
                //    return;
                //}
                //var BendRang = from eva in MySession.Query<EvaluteDetail>() where eva.PatientID == UserID && eva.ActionId == (int)EvaluateActionEnum.RangeBend orderby eva.EvaluteDate descending select eva;
                //if (BendRang.Count()>0)
                //{
                //    gp.ProtrusiveMax = BendRang.FirstOrDefault().MaxV;
                //}else
                //{
                //    MessageBox.Show("该患者没有评测记录，请返回评测");
                //    this.Game1.IsEnabled = false;
                //    return;
                //}
                //var ProtrusiveRange = from eva in MySession.Query<EvaluteDetail>() where eva.PatientID == UserID && eva.ActionId == (int)EvaluateActionEnum.RangeProtrusive orderby eva.EvaluteDate descending select eva;
                //if (ProtrusiveRange.Count() > 0)
                //{
                //    gp.ProtrusiveMin = ProtrusiveRange.FirstOrDefault().MaxV;
                //}
                //else
                //{
                //    MessageBox.Show("该患者没有评测记录，请返回评测");
                //    this.Game1.IsEnabled = false;
                //    return;
                //}
                //this.MaxValue.Text = gp.ProtrusiveMax.ToString();
                //this.MinValue.Text = gp.ProtrusiveMin.ToString();
                
            }
            else
            {

                MessageBox.Show("请选择患者！");
                this.Close();
                return;
            }
            //GlobalVar.GameReturn+=GlobalVar_GameReturn;
            LumbarRobotController.RobotController.ControlCommand.DataPackRecieved += new DataPackReceivedHandler(DataCommunication_Move);
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
            if(args.ResponsePackageData.Code==ReciveModel)//屈伸
            {

                ThreadPool.QueueUserWorkItem((x) => {

                    if(ReciveModel==0x10)
                    {
                        tinfo.RunVlue=args.ResponsePackageData.RotationAngle;
                    
                    }
                    else
                    {
                        tinfo.RunVlue=args.ResponsePackageData.BendStretchAngle;
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
       
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            
            this.Close();
        }

        private void Game1_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string GamePath = System.Environment.CurrentDirectory;
                GamePath += @"\Conveyor3D.exe";
                if (GamePath != null)
                {
                    Process GameProcess = new Process();
                    GameProcess.StartInfo.FileName = GamePath;
                    GameProcess.Start();
                    
                }
                int PramValues=int.Parse(this.PramValue.Text);
                LumbarRobot.ModulesCommon.GlobalVar.MaxValue = int.Parse(this.MaxValue.Text);
                LumbarRobot.ModulesCommon.GlobalVar.MinValue = int.Parse(this.MinValue.Text);

                int tage = 0;
                if (ActionList.SelectedIndex == 0)
                {
                    ReciveModel = 0x10;
                    LumbarRobotController.RobotController.ControlCommand.BendStretchFree(PramValues, tage, LumbarRobot.ModulesCommon.GlobalVar.MaxValue, LumbarRobot.ModulesCommon.GlobalVar.MinValue);//设置力量第一个参数为力量设置屈伸模式
                }
                else if (ActionList.SelectedIndex == 1)
                {
                    ReciveModel = 0x11;
                    LumbarRobotController.RobotController.ControlCommand.RotationFree(PramValues, 0, LumbarRobot.ModulesCommon.GlobalVar.MaxValue, LumbarRobot.ModulesCommon.GlobalVar.MinValue); //第一个参数为力量 设置旋转模式
                }
                

                
            }
            catch (Exception ex)
            {

                MessageBox.Show("游戏起动失败！请联系管理员");
                
            }
            

        }

        private void MinAdd_Click(object sender, RoutedEventArgs e)
        {
            if(this.MinValue.Text==null)
            {

                this.MinValue.Text = "0";
            }
            if (Convert.ToInt32(this.MinValue.Text) < 100)
            {
                float minvalue = (Convert.ToInt32(this.MinValue.Text) + 1);
                this.MinValue.Text = minvalue.ToString();
                
            }
        }

        private void MinDiff_Click(object sender, RoutedEventArgs e)
        {
            if (this.MinValue.Text == null)
            {

                this.MinValue.Text = "0";
            }
            if (Convert.ToInt32(this.MinValue.Text) >0)
            {
                float minvalue = (Convert.ToInt32(this.MinValue.Text) - 1);
                this.MinValue.Text = minvalue.ToString();
               
            }
                
        }

        private void MaxDiff_Click(object sender, RoutedEventArgs e)
        {
            if (this.MaxValue.Text == null)
            {

                this.MaxValue.Text = "0";
            }
            if (Convert.ToInt32(this.MaxValue.Text) > 0)
            {
               float maxvalue= (Convert.ToInt32(this.MaxValue.Text) - 1);
                this.MaxValue.Text = maxvalue.ToString();
               
            }
        }

        private void MaxAdd_Click(object sender, RoutedEventArgs e)
        {
            if (this.MaxValue.Text == null)
            {

                this.MaxValue.Text = "0";
            }
            if (Convert.ToInt32(this.MaxValue.Text) < 100)
            {
                float maxvalue = (Convert.ToInt32(this.MaxValue.Text) + 1);
                this.MaxValue.Text = maxvalue.ToString();
             
            }
        }

        private void PramDiff_Click(object sender, RoutedEventArgs e)
        {
            if (this.PramValue.Text == null)
            {

                this.PramValue.Text = "0";
            }
            if (Convert.ToInt32(this.PramValue.Text) > 0)
            {
                float pram = (Convert.ToInt32(this.PramValue.Text) - 1);
                this.PramValue.Text = pram.ToString();
              
            }
        }

        private void PramAdd_Click(object sender, RoutedEventArgs e)
        {
            if (this.PramValue.Text == null)
            {

                this.PramValue.Text = "0";
            }
            if (Convert.ToInt32(this.PramValue.Text) < 100)
            {
                float pram = (Convert.ToInt32(this.PramValue.Text) + 1);
                this.PramValue.Text = pram.ToString();
            }
        }
    }

   public class GamePram
    {
       private float _BendStretchMax;
       /// <summary>
       /// 旋转最大值
       /// </summary>
       public float BendStretchMax
        {
            get { return _BendStretchMax; }
            set { _BendStretchMax = value; }
        }

        private float _BendStretchMin;
       /// <summary>
       /// 屈伸最小值
       /// </summary>
        public float BendStretchMin
        {
            get { return _BendStretchMin; }
            set { _BendStretchMin = value; }
        }

        private float _RotationMax;
       /// <summary>
       /// 旋转最大值
       /// </summary>
        public float RotationMax
        {
            get { return _RotationMax; }
            set { _RotationMax = value; }
        }

        private float _RotationMin;
       /// <summary>
       /// 旋转最小值
       /// </summary>
        public float RotationMin
        {
            get { return _RotationMin; }
            set { _RotationMin = value; }
        }
        
        
        
        
    }
}
