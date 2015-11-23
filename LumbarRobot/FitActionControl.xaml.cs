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
using System.Collections.ObjectModel;
using System.Windows.Threading;
using LumbarRobot.Services;
using LumbarRobot.Protocol;
using LumbarRobot.Enums;
using LumbarRobot.Common;
using LumbarRobot.MyUserControl;
using LumbarRobot.Data;
using LumbarRobot.DAL;
using LumbarRobot.Requests;
using LumbarRobot.Interactions;
using LumbarRobot.ViewModels;
using LumbarRobot.Common.Enums;
using System.Data;

namespace LumbarRobot
{
    /// <summary>
    /// ActionControl.xaml 的交互逻辑
    /// </summary>
    public partial class FitActionControl : UserControl
    {
        #region 变量
        /// <summary>
        /// 通知消息
        /// </summary>
        public GenericInteractionRequest<AlarmInfo> NotificationToAlarm { get; private set; }

        /// <summary>
        /// 评测结果集合
        /// </summary>
        public ObservableCollection<FitResultCount> FitResultCountList = null;

        public event EventHandler Close;
        private LumbarRobotController.MoveHandler Move = null;
        private EventHandler NextTime = null;
        private EventHandler StopAction = null;
        private EventHandler LimitSwitchChanged = null;
        private EventHandler PushRodChanged = null;
        private LumbarRobot.Services.LumbarRobotController.AlarmHandler AlarmEvent = null;
        private SetForceHandler SetForce = null;
        private EventHandler StartAction = null;
        private List<FitResult> FitResultList = null;
        private FitResult FitResult = null;
        /// <summary>
        /// 是否禁用按钮
        /// </summary>
        public bool IsMyFlag = false;

        #region 枚举
        private EvaluateActionEnum _action;
        /// <summary>
        /// 测试枚举
        /// </summary>
        public EvaluateActionEnum Action
        {
            get { return _action; }
            set 
            { 
                _action = value;
                this.CboMode.SelectedIndex = Convert.ToInt32(EnumHelper.GetEnumValueStr(Action));
            }
        }
        #endregion

        #region 枚举模式
        private EvaluateModeEnum _mode;
       /// <summary>
       /// 枚举模式
       /// </summary>
        public EvaluateModeEnum Mode
        {
            get { return _mode; }
            set 
            {
                _mode = value;
                if (value == EvaluateModeEnum.Range)
                {
                     setControlMaxAngle.Visibility = Visibility.Visible;
                     setControlForce.Visibility = Visibility.Collapsed;
                }
                else
                {
                    setControlMaxAngle.Visibility = Visibility.Collapsed;
                    setControlForce.Visibility = Visibility.Visible;
                }
            }
        }
        #endregion

        #region 初始位置
        
        private int _initialPosition;
        /// <summary>
        /// 初始位置
        /// </summary>
        public int InitialPosition
        {
            get 
            {
                return Convert.ToInt32(this.setControlPosition.Value); 
            }
            set { _initialPosition = value; }
        }
        #endregion

        #region 力量
      
        private int _power;
        /// <summary>
        /// 力量
        /// </summary>
        public int Power
        {
            get
            {
                return Convert.ToInt32(setControlForce.Value);
            }
            set { _power = value; }
        }
        #endregion

        #region 角度设置
      
        private int _angle;
        /// <summary>
        /// 角度设置
        /// </summary>
        public int Angle
        {
            get 
            { return Convert.ToInt32(setControlMaxAngle.Value); }
            set { _angle = value; }
        }
        #endregion

        #region 时间设置
        private int _time;
        /// <summary>
        /// 时间设置
        /// </summary>
        public int Time
        {
            get
            {
                return Convert.ToInt32(setControlTimes.Value);
            }
            set { _time = value; }
        }

        #endregion

        MyMessageBox msgBox = null;
        object _lock = new object();
        #endregion

        #region 构造
        public FitActionControl(ObservableCollection<ItemDemo> ActionList)
        {
            InitializeComponent();
            NotificationToAlarm = new GenericInteractionRequest<AlarmInfo>();
            ControlIsEnabled(true);
            this.playBtn.imgStop.IsEnabled = false; //停止禁止使用
            playBtn.PlayClick += new EventHandler(playBtn_PlayClick);
            playBtn.PauseClick += new EventHandler(playBtn_PauseClick);
            playBtn.StopClick += new EventHandler(playBtn_StopClick);
            
            txtPrompt.Text = "提示信息";
        }

        #endregion

        #region 返回
        
        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
             this.Close(sender, e);
        }

        void msgBox_BtnIsEnable(object sender, EventArgs e)
        {
            LumbarRobotController.RobotController.ControlCommand.ErrorReset();
            if (msgBox != null)
            {
                msgBox.Close();
                msgBox = null;
            }
          
            this.btnClearGeocoder.IsEnabled = true;
            this.btnActionFree.IsEnabled = true;
            LumbarRobotController.RobotController.Continue();

            System.Threading.Thread.Sleep(100);
            //if (LumbarRobotController.RobotController.AlarmCode != 0)
            //{
            ShowAlarmDialog();
            //}
        }
        #endregion

        #region stop
        private void Stop(bool isShowDialog = true)
        {
            var record = LumbarRobotController.RobotController.GetEvaluteResult();
            ControlIsEnabled(true);

            if (Move != null)
            {
                LumbarRobotController.RobotController.Move -= Move;
                Move = null;
            }

            if (NextTime != null)
            {
                LumbarRobotController.RobotController.NextTime -= NextTime;
                NextTime = null;
            }

            if (StopAction != null)
            {
                LumbarRobotController.RobotController.StopAction -= StopAction;
                StopAction = null;
            }

            if (SetForce != null)
            {
                LumbarRobotController.RobotController.SetForce -= SetForce;
                SetForce = null;
            }

            if (StartAction != null)
            {
                LumbarRobotController.RobotController.StartAction -= StartAction;
                StartAction = null;
            }

            SaveTrainRecord(record);

            //sinWpf1.ReSet();
            //eilelc21.Rest();
            LumbarRobotController.RobotController.ControlCommand.PauseCmd();
            txtPrompt.Text = "当前评测结束！";
            if (isShowDialog)
            {
                AlarmDialog dialog = new AlarmDialog();
                dialog.lblTitle.Text = "提示信息";
                dialog.lblMsg.Text = "当前评测结束！";
                dialog.Topmost = true;
                dialog.ShowDialog();
            }
        }

        #endregion

        #region 事件处理函数

        #region 停止
        /// <summary>
        /// 停止
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void playBtn_StopClick(object sender, EventArgs e)
        {
            this.playBtn.IsCanStop = false;
            this.playBtn.imgStop.IsEnabled = false;

            LumbarRobotController.RobotController.ControlCommand.PauseCmd();
            LumbarRobotController.RobotController.IsStop = true;
        }
        #endregion

        #region 暂停
        /// <summary>
        /// 暂停
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void playBtn_PauseClick(object sender, EventArgs e)
        {
            LumbarRobotController.RobotController.IsPause = true;
        }
        #endregion

        #region 开始
        /// <summary>
        /// 开始
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void playBtn_PlayClick(object sender, EventArgs e)
        {
            if (LumbarRobotController.RobotController.IsPause)
            {
                LumbarRobotController.RobotController.Continue();
            }
            else
            {
                SetParam();

                if (Action == EvaluateActionEnum.RangeBend || Action == EvaluateActionEnum.RangeProtrusive)
                {

                    //判断限位塞是否有效
                    var limitSwitchs = LumbarRobotController.RobotController.LimitSwitch;
                    bool minFlag = false;
                    bool maxFlag = false;
                    int minAngle = int.MinValue;
                    int maxAngle = int.MaxValue;
                    foreach (var ls in limitSwitchs)
                    {
                        var limitAngle = GetLimitSwitchAngle(ls);
                        if (LumbarRobotController.RobotController.CurrentBendStretchAngle - (LumbarRobotController.RobotController.HomingAngle - LumbarRobotController.RobotController.PushRodAngle) < limitAngle)
                        {
                            if (limitAngle < maxAngle)
                            {
                                maxAngle = limitAngle;
                            }
                            maxFlag = true;
                        }
                        else
                        {
                            if (limitAngle > minAngle)
                            {
                                minAngle = limitAngle;
                            }
                            minFlag = true;
                        }
                    }

                    if (!(minFlag && maxFlag))
                    {
                        playBtn.Init(1, 1);
                        AlarmDialog dialog = new AlarmDialog();
                        dialog.lblTitle.Text = "提示信息";
                        dialog.lblMsg.Text = "限位塞还未设置或设置错误！";
                        dialog.ShowDialog();
                        return;
                    }

                    //判断设置角度是否大于限位栓角度
                    if (LumbarRobotController.RobotController.IsConnected)
                    {
                        if (InitialPosition < minAngle)
                        {
                            playBtn.Init(1, 1);
                            AlarmDialog dialog = new AlarmDialog();
                            dialog.lblTitle.Text = "提示信息";
                            dialog.lblMsg.Text = "初始角度不能小于限位塞角度！";
                            dialog.ShowDialog();
                            return;
                        }

                        if (InitialPosition > maxAngle)
                        {
                            playBtn.Init(1, 1);
                            AlarmDialog dialog = new AlarmDialog();
                            dialog.lblTitle.Text = "提示信息";
                            dialog.lblMsg.Text = "初始角度不能大于限位塞角度！";
                            dialog.ShowDialog();
                            return;
                        }
                    }

                }

                this.playBtn.imgStop.IsEnabled = true;
                this.playBtn.imgNext.IsEnabled = false;
                this.playBtn.imgPrev.IsEnabled = false;

                ControlIsEnabled(false);

                if (Mode == EvaluateModeEnum.Strength)
                {
                    sinWpf1.Uint = "牛";
                }
                else
                {
                    sinWpf1.Uint = "度";
                }

                sinWpf1.ReSet();
                eilelc21.Rest();

                if (Move == null)
                {
                    Move = new LumbarRobotController.MoveHandler(RobotController_Move);
                    LumbarRobotController.RobotController.Move += Move;
                }

                if (StopAction == null)
                {
                    StopAction = new EventHandler(RobotController_StopAction);
                    LumbarRobotController.RobotController.StopAction += StopAction;
                }

                if (SetForce == null)
                {
                    SetForce = new SetForceHandler(RobotController_SetForce);
                    LumbarRobotController.RobotController.SetForce += SetForce;
                }

                if (StartAction == null)
                {
                    StartAction = new EventHandler(RobotController_StartAction);
                    LumbarRobotController.RobotController.StartAction += StartAction;
                }


                if (Mode == EvaluateModeEnum.Range)
                {
                    SetTargetLine(Time, InitialPosition, Angle);
                }
                else
                {
                    SetTargetLine(Time, 0, Power);
                }

                LumbarRobotController.RobotController.DoEvaluateAction(Action, Mode);

                txtPrompt.Text = "系统正在复位请放松";

                //禁用fit按钮
                btnActionFree.IsEnabled = false;
            }
        }
        #endregion

        #endregion

        #region 控制器事件

        void RobotController_StartAction(object sender, EventArgs e)
        {
            this.Dispatcher.Invoke(new Action(() =>
            {
                txtPrompt.Text = "训练正在进行";

                //设置当前次数
                this.txtTimes.Text = "1";
                
            }));
        }

        float maxForce = 0;
        float minForce = 0;

        void RobotController_SetForce(float force)
        {
            if (force > maxForce && force > 0) maxForce = force;
            if (force < minForce && force < 0) minForce = force;
            this.Dispatcher.Invoke(new Action(() =>
            {
                if (!LumbarRobotController.RobotController.IsDrawLine())
                {
                    sinWpf1.DrawRunLine(force);
                }
            }));
        }

        void RobotController_LimitSwitchChanged(object sender, EventArgs e)
        {
            SetLimitSwitch();
        }

        void RobotController_StopAction(object sender, EventArgs e)
        {
            this.Dispatcher.Invoke(new Action(() =>
            {
                playBtn.Init(1, 1);

                //设置按钮
                btnActionFree.IsEnabled = true;

                Stop(IsShowDialog);
                IsShowDialog = true;
            }));
        }

        void RobotController_NextTime(object sender, EventArgs e)
        {
            this.Dispatcher.Invoke(new Action(() =>
            {
                //sinWpf1.DrawPI();
            }));
        }

        void RobotController_Move(LumbarRobotController.PositionArgs args)
        {
                //if (ActionList[CurrentIndex].ActionId == (int)ActionEnum.Rotation)
                if (Action == EvaluateActionEnum.RotationRangeLeft || Action == EvaluateActionEnum.RotationRangeRight
                    || Action == EvaluateActionEnum.RotationStrengthRigth || Action == EvaluateActionEnum.RotationStrengthLeft)
                {
                    if (args.Package.Code == ResponseCodes.RotationData)
                    {
                        this.Dispatcher.Invoke(new Action(() =>
                        {
                            eilelc21.loginRun(args.Package.RotationAngle);
                            if (LumbarRobotController.RobotController.IsReady)
                            {
                                if (LumbarRobotController.RobotController.IsDrawLine())
                                {
                                    sinWpf1.DrawRunLine(args.Package.RotationAngle);
                                }
                            }
                        }
                        ));
                    }
                }
                else
                {
                    if (args.Package.Code == ResponseCodes.BendStretchData)
                    {
                        this.Dispatcher.Invoke(new Action(() =>
                        {
                            eilelc21.loginRun(args.Package.BendStretchAngle - (LumbarRobotController.RobotController.HomingAngle - LumbarRobotController.RobotController.PushRodAngle));
                            if (LumbarRobotController.RobotController.IsReady)
                            {
                                if (LumbarRobotController.RobotController.IsDrawLine())
                                {
                                    sinWpf1.DrawRunLine(args.Package.BendStretchAngle - (LumbarRobotController.RobotController.HomingAngle - LumbarRobotController.RobotController.PushRodAngle));
                                }
                            }
                        }
                        ));
                    }
                }
        }

        void RobotController_PushRodChanged(object sender, EventArgs e)
        {
            SetLimitSwitch();

            Application.Current.Dispatcher.Invoke(new Action(() =>
            {
                userLine1.BarFactHeght = LumbarRobotController.RobotController.PushRodAngle;
            }));
        }

        void RobotController_Alarm(LumbarRobotController.AlarmArgs e)
        {
            
            Application.Current.Dispatcher.Invoke(new Action(() =>
            {
                ShowAlarmDialog();
            }));
        }
        #endregion

        #region 设置

        public void SetParam()
        {
            LumbarRobotController.RobotController.Target =InitialPosition; 
        }
        #endregion

        #region 控制按钮是否可用
        /// <summary>
        /// 控制按钮是否可用
        /// </summary>
        /// <param name="flag"></param>
        public void ControlIsEnabled(bool flag)
        {
           
            this.setControlForce.IsEnabled = flag;
            this.setControlMaxAngle.IsEnabled = flag;
            this.setControlTimes.IsEnabled = flag;
            this.CboMode.IsEnabled = flag;
            this.setControlPosition.IsEnabled = flag;
          

        }
        #endregion

        #region Unloaded事件
        private void UserControl_Unloaded(object sender, RoutedEventArgs e)
        {
            LumbarRobotController.RobotController.IsStop = true;
            LumbarRobotController.RobotController.ControlCommand.PauseCmd();

            if (LimitSwitchChanged != null)
            {
                LumbarRobotController.RobotController.LimitSwitchChanged -= LimitSwitchChanged;
                LimitSwitchChanged = null;
            }

            if (PushRodChanged != null)
            {
                LumbarRobotController.RobotController.PushRodChanged -= PushRodChanged;
                PushRodChanged = null;
            }

            if (AlarmEvent != null)
            {
                LumbarRobotController.RobotController.Alarm -= AlarmEvent;
                AlarmEvent = null;
            }

            if (Move != null)
            {
                LumbarRobotController.RobotController.Move -= Move;
                Move = null;
            }

            if (NextTime != null)
            {
                LumbarRobotController.RobotController.NextTime -= NextTime;
                NextTime = null;
            }

            if (StopAction != null)
            {
                LumbarRobotController.RobotController.StopAction -= StopAction;
                StopAction = null;
            }

            if (SetForce != null)
            {
                LumbarRobotController.RobotController.SetForce -= SetForce;
                SetForce = null;
            }

            if (StartAction != null)
            {
                LumbarRobotController.RobotController.StartAction -= StartAction;
                StartAction = null;
            }

            if (msgBox != null)
            {
                msgBox.Close();
            }
        }
        #endregion

        #region Loaded事件
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            //初始化限位塞
            SetLimitSwitch();

            userLine1.BarFactHeght = LumbarRobotController.RobotController.PushRodAngle;

            if (LimitSwitchChanged == null)
            {
                LimitSwitchChanged = new EventHandler(RobotController_LimitSwitchChanged);
                LumbarRobotController.RobotController.LimitSwitchChanged += LimitSwitchChanged;
            }

            if (PushRodChanged == null)
            {
                PushRodChanged = new EventHandler(RobotController_PushRodChanged);
                LumbarRobotController.RobotController.PushRodChanged += PushRodChanged;
            }

            if (AlarmEvent == null)
            {
                AlarmEvent = new LumbarRobotController.AlarmHandler(RobotController_Alarm);
                LumbarRobotController.RobotController.Alarm += AlarmEvent;
            }

            //上次训练推杆高度
            var record = (from r in MySession.Query<Exerciserecord>()
                          where r.PatientId == ModuleConstant.PatientId
                          select r).OrderByDescending(x => x.StartTime).Take(1).ToList();

            if (record.Count > 0)
            {
                userLine1.BarHeight = record.First().PushRodValue;
            }                         

            //报警信息
            if (LumbarRobotController.RobotController.AlarmCode != 0)
            {
                var args = new LumbarRobotController.AlarmArgs();
                args.AlarmCode = LumbarRobotController.RobotController.AlarmCode;
                RobotController_Alarm(args);
            }
            
            lock (_lock)
            {
                FitResultList = null;
                if (LumbarRobotController.RobotController.IsPause)
                {
                    LumbarRobotController.RobotController.IsPause = false;
                }
                else
                {

                    if (Mode == EvaluateModeEnum.Strength)
                    {
                        sinWpf1.Uint = "牛";
                    }
                    else
                    {
                        sinWpf1.Uint = "度";
                    }

                    sinWpf1.ReSet();
                    eilelc21.Rest();

                    if (Move == null)
                    {
                        Move = new LumbarRobotController.MoveHandler(RobotController_Move);
                        LumbarRobotController.RobotController.Move += Move;
                    }

                    if (NextTime == null)
                    {
                        NextTime = new EventHandler(RobotController_NextTime);
                        LumbarRobotController.RobotController.NextTime += NextTime;
                    }
                    if (StartAction == null)
                    {
                        StartAction = new EventHandler(RobotController_StartAction);
                        LumbarRobotController.RobotController.StartAction += StartAction;
                    }

                    if (Mode == EvaluateModeEnum.Strength)
                    {                            

                        if (SetForce == null)
                        {
                            SetForce = new SetForceHandler(RobotController_SetForce);
                            LumbarRobotController.RobotController.SetForce += SetForce;
                        }
                    }


                    SetParam();

                  
                }

                txtPrompt.Text = "系统正在复位请放松";
              
                //设置当前次数
                this.txtTimes.Text = "0/0";
            }
            
        }


        #endregion

        #region 主动
        private void btnFree_Click(object sender, RoutedEventArgs e)
        {
            LumbarRobotController.RobotController.ControlCommand.QuickStopCmd();
        }
        #endregion

        #region 故障清零
        private void btnClearBreakdown_Click(object sender, RoutedEventArgs e)
        {
            LumbarRobotController.RobotController.ControlCommand.ErrorReset();
        }
        #endregion

        #region 编码器清零
        private void btnClearGeocoder_Click(object sender, RoutedEventArgs e)
        {
            LumbarRobotController.RobotController.ControlCommand.SensorInit();
        }
        #endregion

        #region 设置限位塞
        public void SetLimitSwitch()
        {
           
        }

        public int GetLimitSwitchAngle(int number)
        {
            var angle = (number - 2) * 10 + LumbarRobotController.RobotController.PushRodAngle - 90;
            if (LumbarRobotController.RobotController.CurrentBendStretchAngle < angle)
            {
                angle -= 17;
            }
            else
            {
                angle += 17;
            }
            return (int)angle;
        }
        #endregion

        #region 设置目标线



        private void SetTargetLine(int interval, int position, int maxValue)
        {
            List<double> targetLine = new List<double>();

            for (int i = 0; i < 50; i++)
            {
                targetLine.Add(position);
            }

            int num = 1;
            float k = 1.3f;
            float y = 0;

            while (y < Math.Abs(maxValue))
            {
                y = num * k + position;
                targetLine.Add(y);
                num++;
            }

            num = (int)(interval * 12);

            for (int i = 0; i < num; i++)
            {
                targetLine.Add(y);
            }

            LumbarRobotController.RobotController.TargetLine = targetLine.ToArray();
            sinWpf1.GoldLine(targetLine.ToArray());
        }

        #endregion

        #region 保存训练记录、Fit记录
        private void SaveTrainRecord(EvaluteResult record)
        {
            Application.Current.Dispatcher.Invoke(new Action(() =>
            {
                if (MessageBox.Show("是否保存评测结果？","提示",MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    try
                    {
                        EvaluteDetail ed = new EvaluteDetail();
                        ed.EvaluteDetailDate = DateTime.Now;
                        ed.EvaluteDate = DateTime.Now.Date;
                        if (record.MaxValue.HasValue) ed.MaxV = record.MaxValue.Value;
                        if (record.LastValue.HasValue) ed.LastValue = record.LastValue.Value;
                        if (record.MaxValue.HasValue && record.MaxValue.Value > 0 && record.LastValue.HasValue)
                        {
                            ed.FatigueIndex =(record.MaxValue.Value - record.LastValue.Value) / record.MaxValue.Value;
                        }
                        ed.ModeId = (int)record.EvaluteMode;
                        ed.ActionId = (int)record.EvaluteAction;
                        ed.PatientID = ModuleConstant.PatientId;
                        ed.Interval = Time;
                        if (Mode == EvaluateModeEnum.Range)
                        {
                            ed.TargetValue = Angle;
                        }
                        else
                        {
                            ed.TargetValue = Power;
                        }
                        StringBuilder sb = new StringBuilder();
                        if (record.TargetLine != null)
                        {
                            for (int i = 0; i < record.TargetLine.Length; i++)
                            {
                                if (i == 0)
                                {
                                    sb.Append(record.TargetLine[i].ToString("#0.00"));
                                }
                                else
                                {
                                    sb.Append("|").Append(record.TargetLine[i].ToString("#0.00"));
                                }
                            }
                            ed.Record = sb.ToString();
                        }

                        sb = new StringBuilder();
                       
                        if (record.RealLine != null)
                        {
                            for (int i = 0; i < record.RealLine.Length; i++)
                            {
                                if (i == 0)
                                {
                                    sb.Append(record.RealLine[i].ToString("#0.00"));
                                }
                                else
                                {
                                    sb.Append("|").Append(record.RealLine[i].ToString("#0.00"));
                                }
                            }
                            ed.Record2 = sb.ToString();
                        }
                        

                        MySession.Session.Save(ed);
                        MySession.Session.Flush();

                        if (Mode == EvaluateModeEnum.Range)
                        {
                            FitModeEnum fitMode = FitModeEnum.RotationFit;
                            int? minValue = null;
                            int? maxValue = null;

                            if (Action == EvaluateActionEnum.RotationRangeLeft)
                            {
                                fitMode = FitModeEnum.RotationFit;
                                maxValue = (int)record.MaxValue.Value;
                            }
                            else if (Action == EvaluateActionEnum.RotationRangeRight)
                            {
                                fitMode = FitModeEnum.RotationFit;
                                minValue = (int)record.MaxValue.Value * -1;
                            }
                            else if (Action == EvaluateActionEnum.RangeBend)
                            {
                                fitMode = FitModeEnum.ProtrusiveOrBendFit;
                                minValue = (int)record.MaxValue.Value * -1;
                            }
                            else if (Action == EvaluateActionEnum.RangeProtrusive)
                            {
                                fitMode = FitModeEnum.ProtrusiveOrBendFit;
                                maxValue = (int)record.MaxValue.Value;
                            }

                            var result = (from fr in MySession.Query<FitRecord>()
                                          where fr.PatientID == ModuleConstant.PatientId
                                          && fr.PushRodValue <= LumbarRobotController.RobotController.PushRodAngle + 1
                                          && fr.PushRodValue >= LumbarRobotController.RobotController.PushRodAngle - 1
                                          select fr).OrderByDescending(x => x.CreateTime);
                            FitRecord fitRecord;

                            if (result.Count() > 0)
                            {
                                fitRecord = result.First();
                            }
                            else
                            {
                                fitRecord = new FitRecord();
                                fitRecord.Id = Guid.NewGuid().ToString("N");
                                fitRecord.PatientID = ModuleConstant.PatientId;
                                fitRecord.MinAngle = 0;
                                fitRecord.MinAngle = 0;
                            }

                            fitRecord.ModeID = (int)fitMode;
                            fitRecord.CreateTime = DateTime.Now;
                            fitRecord.PushRodValue = LumbarRobotController.RobotController.PushRodAngle;
                            if (minValue.HasValue)
                            {
                                fitRecord.MinAngle = minValue.Value;
                            }
                            if (maxValue.HasValue)
                            {
                                fitRecord.MaxAngle = maxValue.Value;
                            }

                            MySession.Session.SaveOrUpdate(fitRecord);
                            MySession.Session.Flush();

                            if (fitRecord.ModeID == (int)Common.Enums.FitModeEnum.ProtrusiveOrBendFit)
                            {
                                eilelc21.FltMinusValue = fitRecord.MinAngle;
                                eilelc21.FltValu = fitRecord.MaxAngle;
                            }
                            else if (fitRecord.ModeID == (int)Common.Enums.FitModeEnum.RotationFit)
                            {
                                eilelc21.FltMinusValue = fitRecord.MinAngle;
                                eilelc21.FltValu = fitRecord.MaxAngle;
                            }

                            eilelc21.Rest();
                        }



                    }
                    catch { }
                }
            }));
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
        private bool IsShowDialog = true;
        private void ShowAlarmDialog()
        {
            lock (this)
            {
                if (msgBox != null)
                {
                    return;
                }

                if (LumbarRobotController.RobotController.AlarmCode != 0
                    && LumbarRobotController.RobotController.AlarmCode != 0x400000
                    && LumbarRobotController.RobotController.AlarmCode != 0x800000)
                {
                    LumbarRobot.Services.LumbarRobotController.AlarmArgs args = new LumbarRobotController.AlarmArgs();
                    args.AlarmCode = LumbarRobotController.RobotController.AlarmCode;

                    msgBox = new MyMessageBox();
                    msgBox.lblMsg.Text = "出现" + args.ToString() + "，如无异常，请点击确认，继续训练！";
                    msgBox.lblTitle.Text = "提示信息";
                    msgBox.Topmost = true;
                    msgBox.BtnIsEnable += new EventHandler(msgBox_BtnIsEnable);

                    btnClearGeocoder.IsEnabled = false;
                    btnActionFree.IsEnabled = false;

                    msgBox.Show();
                }
            }
        }
        #endregion

        #region 主动
        private void btnActionFree_Click(object sender, RoutedEventArgs e)
        {
            LumbarRobotController.RobotController.ControlCommand.QuickStopCmd();
        }
        #endregion

        #region 绑定评测结果
        public void BindFitResult(string patientId)
        {
            FitResultCountList = new ObservableCollection<FitResultCount>();
            DataTable tbl = new DataTable();
           
            tbl.Columns.Add("ResultName", typeof(string));   // 模式名称 1 
            tbl.Columns.Add("TotalCount", typeof(string));   //评测次数 3
            gvMode.ItemsSource = FitResultCountList;
        }
        #endregion

        #region 评测改变事件
        private void CboMode_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int defaultPosition = 0;
            switch (CboMode.SelectedIndex)
            {
                case 1:
                    Action = EvaluateActionEnum.RotationRangeLeft;
                    Mode = EvaluateModeEnum.Range;
                    break;
                case 2:
                    Action = EvaluateActionEnum.RotationRangeRight;
                    Mode = EvaluateModeEnum.Range;
                    break;
                case 3:
                    Action = EvaluateActionEnum.RangeProtrusive;
                    Mode = EvaluateModeEnum.Range;
                    break;
                case 4:
                    Action = EvaluateActionEnum.RangeBend;
                    Mode = EvaluateModeEnum.Range;
                    break;
                case 5:
                    Action = EvaluateActionEnum.RotationStrengthLeft;
                    Mode = EvaluateModeEnum.Strength;
                    break;
                case 6:
                    Action = EvaluateActionEnum.RotationStrengthRigth;
                    Mode = EvaluateModeEnum.Strength;
                    break;
                case 7:
                    Action = EvaluateActionEnum.StrengthProtrusive;
                    Mode = EvaluateModeEnum.Strength;
                    break;
                case 8:
                    Action = EvaluateActionEnum.StrengthBend;
                    Mode = EvaluateModeEnum.Strength;
                    defaultPosition = 30;
                    break;
                default:
                    break;
            }
            setControlPosition.Value = defaultPosition;
        }
        #endregion
    }

}
