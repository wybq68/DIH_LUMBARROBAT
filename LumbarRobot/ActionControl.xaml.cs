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
using LumbarRobot.Event;

namespace LumbarRobot
{
    /// <summary>
    /// ActionControl.xaml 的交互逻辑
    /// </summary>
    public partial class ActionControl : UserControl
    {
        #region 变量
        /// <summary>
        /// 通知消息
        /// </summary>
        public GenericInteractionRequest<AlarmInfo> NotificationToAlarm { get; private set; }

        public event EventHandler Close;

        private int CurrentIndex = 0;

        private ObservableCollection<ItemDemo> _actionList;

        public ObservableCollection<ItemDemo> ActionList
        {
            get { return _actionList; }
            set { _actionList = value; }
        }

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

        //private bool isShowPanel = true;

        //public bool IsShowPanel
        //{
        //    get
        //    {
        //        return isShowPanel;
        //    }

        //    set
        //    {
        //        isShowPanel = value;
        //        if (!value)
        //        {
        //            gridPanel.Visibility = System.Windows.Visibility.Collapsed;
        //            this.SetVidicon.Visibility = System.Windows.Visibility.Visible;
        //            gridParam.Visibility = System.Windows.Visibility.Hidden;
        //        }
        //        else
        //        {
        //            gridPanel.Visibility = System.Windows.Visibility.Visible;
        //            this.SetVidicon.Visibility = System.Windows.Visibility.Hidden;
        //            gridParam.Visibility = System.Windows.Visibility.Visible;
        //        }
        //    }
        //}

        private FitType fitType = FitType.None;

        public FitType FitType
        {
            get { return fitType; }
            set { fitType = value; }
        }


        MyMessageBox msgBox = null;

        object _lock = new object();
        #endregion

        #region 构造
        public ActionControl(ObservableCollection<ItemDemo> ActionList)
        {
            InitializeComponent();
            NotificationToAlarm = new GenericInteractionRequest<AlarmInfo>();
            ControlIsEnabled(true);
            if (ActionList != null)
            {
                this.ActionList = ActionList;
                MyActionListBox.ItemsSource = ActionList;
                MyActionListBox.SelectedIndex = 0;

                if (ActionList.Count >= 2)
                {
                    this.txtNextAction.Text = ActionList[1].ActionName;
                }
            }
            this.playBtn.imgStop.IsEnabled = false; //停止禁止使用
            playBtn.PlayClick += new EventHandler(playBtn_PlayClick);
            playBtn.PauseClick += new EventHandler(playBtn_PauseClick);
            playBtn.StopClick += new EventHandler(playBtn_StopClick);
            playBtn.NextClick += new EventHandler(playBtn_NextClick);
            playBtn.PrevClick += new EventHandler(playBtn_PrevClick);
            setControlTimes.SelectChange += new EventHandler(SelectChange_Click);
            
            txtPrompt.Text = "提示信息";
            

        }

        #endregion

        #region 返回
        
        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
             this.Close(sender, e);
             IsRefreshEvent.Instance.Publish(true);
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
            this.btnRotationFit.IsEnabled = true;
            this.btnPBFit.IsEnabled = true;
            this.btnActionFree.IsEnabled = true;
            LumbarRobotController.RobotController.Continue();

            System.Threading.Thread.Sleep(100);
            //if (LumbarRobotController.RobotController.AlarmCode != 0)
            //{
            ShowAlarmDialog();
            //}
        }
        #endregion

        #region 选中动作改变事件
        ItemDemo tempItem = null;
        private void MyActionListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SetIsEnabled();

            ItemDemo item = MyActionListBox.SelectedItem as ItemDemo;
            if (item != null)
            {
                tempItem = item;
                this.txtAction.Text = item.ActionName;
                //this.txtMode.Text = EnumHelper.GetEnumItemName(Convert.ToInt32(item.Mode), typeof(ModeEnum));
                //this.txtSpeed.Text = item.Speed.ToString();
                this.txtTimes.Text = "0/" + (item.Times * item.GroupTimes).ToString();
                //this.txtForce.Text = item.Force.ToString();
                //this.txtMaxAngle.Text = item.MaxAngle.ToString();
                //this.txtMinAngle.Text = item.MinAngle.ToString();
                CurrentIndex = MyActionListBox.SelectedIndex;

                this.CboAction.SelectedIndex = tempItem.ActionId;
                this.setControlForce.Value = tempItem.Force;
                this.setControlMaxAngle.Value = tempItem.MaxAngle;
                this.setControlMinAngle.Value = tempItem.MinAngle;
                this.setControlSpeed.Value = tempItem.Speed;
                this.CboMode.SelectedIndex = tempItem.Mode;
                this.setControlTimes.Value = tempItem.Times;
                this.setControlPosition.Value = tempItem.Position;
                this.setControlGroup.Value = tempItem.GroupTimes;
            }
        }
        #endregion

        #region stop
        private void Stop(bool isShowDialog = true)
        {
            var record = LumbarRobotController.RobotController.GetTrainRecord();
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
            txtPrompt.Text = "当前训练结束！";
            if (isShowDialog)
            {
                AlarmDialog dialog = new AlarmDialog();
                dialog.lblTitle.Text = "提示信息";
                dialog.lblMsg.Text = "当前训练结束！";
                dialog.Topmost = true;
                dialog.ShowDialog();
            }
            //MessageBox.Show("当前训练结束！");
        }

        #endregion

        #region 屈伸Fit

        private void btnFit_Click(object sender, RoutedEventArgs e)
        {
            lock (_lock)
            {
                FitResultList = null;
                if (LumbarRobotController.RobotController.IsPause)
                {
                    LumbarRobotController.RobotController.IsPause = false;
                }
                else
                {
                    sinWpf1.Uint = "度";

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
                    //StopAction = new EventHandler(RobotController_StopAction);
                    //LumbarRobotController.RobotController.StopAction += StopAction;

                    if (StartAction == null)
                    {
                        StartAction = new EventHandler(RobotController_StartAction);
                        LumbarRobotController.RobotController.StartAction += StartAction;
                    }
                    SetParam();
                    LumbarRobotController.RobotController.DoAction(ModeEnum.Fit, ActionEnum.ProtrusiveOrBend);
                    //LimitSwitchChanged = new EventHandler(RobotController_LimitSwitchChanged);
                    //LumbarRobotController.RobotController.LimitSwitchChanged += LimitSwitchChanged;
                }

                this.SetVidicon.Visibility = Visibility.Visible;

                txtPrompt.Text = "系统正在复位请放松";
                txtMsg.Text = "正在进行屈伸Fit，完成后点击确定！";

                //设置当前次数
                this.txtTimes.Text = "0/0";
            }
        }

        #endregion

        #region 旋转Fit
        private void btnRotationFit_Click(object sender, RoutedEventArgs e)
        {
            lock (_lock)
            {
                FitResultList = null;
                if (LumbarRobotController.RobotController.IsPause)
                {
                    LumbarRobotController.RobotController.IsPause = false;
                }
                else
                {
                    sinWpf1.Uint = "度";

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

                    //StopAction = new EventHandler(RobotController_StopAction);
                    //LumbarRobotController.RobotController.StopAction += StopAction;

                    if (StartAction == null)
                    {
                        StartAction = new EventHandler(RobotController_StartAction);
                        LumbarRobotController.RobotController.StartAction += StartAction;
                    }
                    SetParam();
                    LumbarRobotController.RobotController.DoAction(ModeEnum.Fit, ActionEnum.Rotation);
                    //LimitSwitchChanged = new EventHandler(RobotController_LimitSwitchChanged);
                    //LumbarRobotController.RobotController.LimitSwitchChanged += LimitSwitchChanged;
                }
                this.SetVidicon.Visibility = Visibility.Visible;

                txtPrompt.Text = "系统正在复位请放松";
                txtMsg.Text = "正在进行旋转Fit，完成后点击确定！";

                //设置当前次数
                this.txtTimes.Text = "0/0";
            }
        }
        #endregion

        #region 事件处理函数
        #region 设置是否可用
        /// <summary>
        /// 设置是否可用
        /// </summary>
        private void SetIsEnabled()
        {
            if (MyActionListBox.SelectedIndex == ActionList.Count - 1 && ActionList.Count > 1)
            {
                this.playBtn.imgNext.IsEnabled = false;
                this.playBtn.IsPlayNextValid = false;
                this.playBtn.imgPrev.IsEnabled = true;
                this.playBtn.IsPlayPrevValid = true;
            }
            else if (MyActionListBox.SelectedIndex == 0 && ActionList.Count > 1)
            {
                this.playBtn.imgNext.IsEnabled = true;
                this.playBtn.IsPlayNextValid = true;
                this.playBtn.imgPrev.IsEnabled = false;
                this.playBtn.IsPlayPrevValid = false;
            }
            else if (ActionList.Count == 1)
            {
                this.playBtn.imgNext.IsEnabled = false;
                this.playBtn.IsPlayNextValid = false;
                this.playBtn.imgPrev.IsEnabled = false;
                this.playBtn.IsPlayPrevValid = false;
            }
            else
            {
                this.playBtn.imgNext.IsEnabled = true;
                this.playBtn.imgPrev.IsEnabled = true;
                this.playBtn.IsPlayNextValid = true;
                this.playBtn.IsPlayPrevValid = true;
            }
        }
        #endregion

        #region 上一个
        /// <summary>
        /// 上一个
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>        
        void playBtn_PrevClick(object sender, EventArgs e)
        {
            if (ActionList != null)
            {
                if (MyActionListBox.SelectedIndex > 0)
                {

                    MyActionListBox.SelectedIndex--;
                    if (ActionList.Count >= 2)
                    {
                        this.txtNextAction.Text = ActionList[MyActionListBox.SelectedIndex + 1].ActionName;
                    }
                    playBtn.Init(MyActionListBox.SelectedIndex, ActionList.Count);
                    this.playBtn.IsCanStop = false;
                    this.playBtn.imgStop.IsEnabled = false;
                    MyActionListBox.IsEnabled = true;

                    SetIsEnabled();

                    LumbarRobotController.RobotController.ControlCommand.PauseCmd();
                    LumbarRobotController.RobotController.IsStop = true;

                    //初始化限位塞
                    SetLimitSwitch();
                }
            }
        }
        #endregion

        #region 下一个
        /// <summary>
        /// 下一个
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void playBtn_NextClick(object sender, EventArgs e)
        {
            if (ActionList != null)
            {
                if (MyActionListBox.SelectedIndex < ActionList.Count)
                {
                    MyActionListBox.SelectedIndex++;
                    if (ActionList.Count >= 2)
                    {
                        if (MyActionListBox.SelectedIndex < ActionList.Count - 1)
                        {
                            this.txtNextAction.Text = ActionList[MyActionListBox.SelectedIndex + 1].ActionName;
                        }
                        else
                        {
                            this.txtNextAction.Text = "";
                        }
                    }
                    playBtn.Init(MyActionListBox.SelectedIndex, ActionList.Count);
                    this.playBtn.IsCanStop = false;
                    this.playBtn.imgStop.IsEnabled = false;
                    MyActionListBox.IsEnabled = true;

                    SetIsEnabled();

                    LumbarRobotController.RobotController.ControlCommand.PauseCmd();
                    LumbarRobotController.RobotController.IsStop = true;

                    //初始化限位塞
                    SetLimitSwitch();
                }
                
            }
        }
        #endregion

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
            MyActionListBox.IsEnabled = true;

            SetIsEnabled();

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
            SetIsEnabled();
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
                ItemDemo item = ActionList[MyActionListBox.SelectedIndex] as ItemDemo;
                item.MaxAngle = this.setControlMaxAngle.Value;
                ComboBoxItem cbi = (ComboBoxItem)this.CboAction.SelectedItem;
                item.ActionName = cbi.Content.ToString();
                ComboBoxItem cbMode = (ComboBoxItem)this.CboMode.SelectedItem;
                item.ModeName = cbMode.Content.ToString();
                item.MinAngle = this.setControlMinAngle.Value;
                item.Mode = this.CboMode.SelectedIndex;
                item.ActionId = this.CboAction.SelectedIndex;
                item.Speed = this.setControlSpeed.Value;
                item.Times = this.setControlTimes.Value;
                item.Force = this.setControlForce.Value;
                item.Position = this.setControlPosition.Value;
                item.PicturePath = "/images/prescribe.png";
                item.GroupTimes = this.setControlGroup.Value;
                MyActionListBox_SelectionChanged(null, null);

                //设置当前次数
                this.txtTimes.Text = (LumbarRobotController.RobotController.CurrentTimes + 1).ToString();
                if (ActionList != null && ActionList.Count > CurrentIndex)
                {
                    this.txtTimes.Text += "/" + ActionList[CurrentIndex].Times * ActionList[CurrentIndex].GroupTimes;
                }

                if ((ActionEnum)ActionList[CurrentIndex].ActionId == ActionEnum.Bend ||
                    (ActionEnum)ActionList[CurrentIndex].ActionId == ActionEnum.Protrusive ||
                    (ActionEnum)ActionList[CurrentIndex].ActionId == ActionEnum.ProtrusiveOrBend)
                {
                    UPLable.Content = "前屈";
                    Downlabel.Content = "后伸";

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
                        playBtn.Init(MyActionListBox.SelectedIndex, ActionList.Count);
                        AlarmDialog dialog = new AlarmDialog();
                        dialog.lblTitle.Text = "提示信息";
                        dialog.lblMsg.Text = "限位塞还未设置或设置错误！";
                        dialog.ShowDialog();
                        return;
                    }

                    //判断设置角度是否大于限位栓角度
                    if (LumbarRobotController.RobotController.IsConnected)
                    {
                        if (ActionList[CurrentIndex].MinAngle < minAngle)
                        {
                            playBtn.Init(MyActionListBox.SelectedIndex, ActionList.Count);
                            AlarmDialog dialog = new AlarmDialog();
                            dialog.lblTitle.Text = "提示信息";
                            dialog.lblMsg.Text = "最小角度不能小于限位塞角度！";
                            dialog.ShowDialog();
                            return;
                        }

                        if (ActionList[CurrentIndex].MaxAngle > maxAngle)
                        {
                            playBtn.Init(MyActionListBox.SelectedIndex, ActionList.Count);
                            AlarmDialog dialog = new AlarmDialog();
                            dialog.lblTitle.Text = "提示信息";
                            dialog.lblMsg.Text = "最大角度不能大于限位塞角度！";
                            dialog.ShowDialog();
                            return;
                        }
                    }

                    //判断初始位置是否大于或小于最大值和最小值
                    if ((ModeEnum)ActionList[CurrentIndex].Mode != ModeEnum.IsotonicA
                        && (ModeEnum)ActionList[CurrentIndex].Mode != ModeEnum.IsotonicB)
                    {
                        if (ActionList[CurrentIndex].Position > ActionList[CurrentIndex].MaxAngle)
                        {
                            playBtn.Init(MyActionListBox.SelectedIndex, ActionList.Count);
                            AlarmDialog dialog = new AlarmDialog();
                            dialog.lblTitle.Text = "提示信息";
                            dialog.lblMsg.Text = "起始位置不能大于最大角度！";
                            dialog.ShowDialog();
                            return;
                        }

                        if (ActionList[CurrentIndex].Position < ActionList[CurrentIndex].MinAngle)
                        {
                            playBtn.Init(MyActionListBox.SelectedIndex, ActionList.Count);
                            AlarmDialog dialog = new AlarmDialog();
                            dialog.lblTitle.Text = "提示信息";
                            dialog.lblMsg.Text = "起始位置不能小于最小角度！";
                            dialog.ShowDialog();
                            return;
                        }
                    }

                    //加载Fit结果
                    var fit = (from f in MySession.Query<FitRecord>()
                               where f.PatientID == ModuleConstant.PatientId && f.ModeID == (int)Common.Enums.FitModeEnum.ProtrusiveOrBendFit
                               select f).OrderByDescending(x => x.CreateTime).Take(1).ToList();

                    if (fit != null && fit.Count > 0)
                    {
                        eilelc21.FltMinusValue = fit[0].MinAngle;
                        eilelc21.FltValu = fit[0].MaxAngle;
                    }
                    else
                    {
                        playBtn.Init(MyActionListBox.SelectedIndex, ActionList.Count);
                        AlarmDialog dialog = new AlarmDialog();
                        dialog.lblTitle.Text = "提示信息";
                        dialog.lblMsg.Text = "还未测试！请先测试后再进行训练！";
                        dialog.ShowDialog();
                        return;
                    }

                    //判断设置角度是否大于fit角度
                    if (ActionList[CurrentIndex].MinAngle < fit[0].MinAngle)
                    {
                        playBtn.Init(MyActionListBox.SelectedIndex, ActionList.Count);
                        AlarmDialog dialog = new AlarmDialog();
                        dialog.lblTitle.Text = "提示信息";
                        dialog.lblMsg.Text = "最小角度不能小于FIT角度！";
                        dialog.ShowDialog();
                        return;
                    }

                    if (ActionList[CurrentIndex].MaxAngle > fit[0].MaxAngle)
                    {
                        playBtn.Init(MyActionListBox.SelectedIndex, ActionList.Count);
                        AlarmDialog dialog = new AlarmDialog();
                        dialog.lblTitle.Text = "提示信息";
                        dialog.lblMsg.Text = "最大角度不能大于FIT角度！";
                        dialog.ShowDialog();
                        return;
                    }

                }
                else
                {
                    UPLable.Content = "左";
                    Downlabel.Content = "右";

                    //加载Fit结果
                    var fit = (from f in MySession.Query<FitRecord>()
                               where f.PatientID == ModuleConstant.PatientId && f.ModeID == (int)Common.Enums.FitModeEnum.RotationFit
                               select f).OrderByDescending(x => x.CreateTime).Take(1).ToList();

                    if (fit != null && fit.Count > 0)
                    {
                        eilelc21.FltMinusValue = fit[0].MinAngle;
                        eilelc21.FltValu = fit[0].MaxAngle;
                    }
                    else
                    {
                        playBtn.Init(MyActionListBox.SelectedIndex, ActionList.Count);
                        AlarmDialog dialog = new AlarmDialog();
                        dialog.lblTitle.Text = "提示信息";
                        dialog.lblMsg.Text = "还未测试！请先测试后再进行训练！";
                        dialog.ShowDialog();
                        return;
                    }

                    //判断设置角度是否大于fit角度
                    if (ActionList[CurrentIndex].MinAngle < fit[0].MinAngle)
                    {
                        playBtn.Init(MyActionListBox.SelectedIndex, ActionList.Count);
                        AlarmDialog dialog = new AlarmDialog();
                        dialog.lblTitle.Text = "提示信息";
                        dialog.lblMsg.Text = "最小角度不能小于FIT角度！";
                        dialog.ShowDialog();
                        return;
                    }

                    if (ActionList[CurrentIndex].MaxAngle > fit[0].MaxAngle)
                    {
                        playBtn.Init(MyActionListBox.SelectedIndex, ActionList.Count);
                        AlarmDialog dialog = new AlarmDialog();
                        dialog.lblTitle.Text = "提示信息";
                        dialog.lblMsg.Text = "最大角度不能大于FIT角度！";
                        dialog.ShowDialog();
                        return;
                    }
                }

                this.playBtn.imgStop.IsEnabled = true;
                this.playBtn.imgNext.IsEnabled = false;
                this.playBtn.imgPrev.IsEnabled = false;
                
                MyActionListBox.IsEnabled = false;
                SetIsEnabled();
                ControlIsEnabled(false);


                if (LumbarRobotController.RobotController.IsPause)
                {
                    LumbarRobotController.RobotController.IsPause = false;
                }
                else
                {
                    if ((ModeEnum)ActionList[CurrentIndex].Mode == ModeEnum.IsotonicA
                    || (ModeEnum)ActionList[CurrentIndex].Mode == ModeEnum.IsotonicB
                    || (ModeEnum)ActionList[CurrentIndex].Mode == ModeEnum.StrengthEvaluation)
                    {
                        sinWpf1.Uint = "牛";
                    }
                    else
                    {
                        sinWpf1.Uint = "度";
                    }
                    if (LumbarRobotController.RobotController.IsPause)
                    {
                        sinWpf1.ReSet(false);
                    }
                    else
                    {
                        sinWpf1.ReSet(); 
                    }
                    eilelc21.Rest();



                    //设置当前次数
                    this.txtTimes.Text = (LumbarRobotController.RobotController.CurrentTimes + 1).ToString();
                    if (ActionList != null && ActionList.Count > CurrentIndex)
                    {
                        this.txtTimes.Text += "/" + ActionList[CurrentIndex].Times * ActionList[CurrentIndex].GroupTimes;
                    }

                    if (ActionList != null && ActionList.Count > CurrentIndex)
                    {
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

                        SetParam();

                        if ((ModeEnum)ActionList[CurrentIndex].Mode == ModeEnum.IsotonicA)
                        {
                            //设置预设目标
                            if ((ActionEnum)ActionList[CurrentIndex].ActionId == ActionEnum.Protrusive)
                            {
                                SetTargetLineByForce(ActionList[CurrentIndex].Speed, 0, ActionList[CurrentIndex].Force, ActionList[CurrentIndex].Times, ActionList[CurrentIndex].GroupTimes, 0);
                            }
                            else if ((ActionEnum)ActionList[CurrentIndex].ActionId == ActionEnum.Bend)
                            {
                                SetTargetLineByForce(ActionList[CurrentIndex].Speed, -1 * ActionList[CurrentIndex].Force,0, ActionList[CurrentIndex].Times, ActionList[CurrentIndex].GroupTimes, 0);
                            }
                            else if ((ActionEnum)ActionList[CurrentIndex].ActionId == ActionEnum.ProtrusiveOrBend
                                || (ActionEnum)ActionList[CurrentIndex].ActionId == ActionEnum.Rotation)
                            {
                                SetTargetLineByForce(ActionList[CurrentIndex].Speed, -1 * ActionList[CurrentIndex].Force, ActionList[CurrentIndex].Force, ActionList[CurrentIndex].Times, ActionList[CurrentIndex].GroupTimes, 0);
                            }
                        }
                        else if ((ModeEnum)ActionList[CurrentIndex].Mode == ModeEnum.IsotonicB)
                        {
                            SetTargetLineByForce2((ActionEnum)ActionList[CurrentIndex].ActionId, ActionList[CurrentIndex].Speed, ActionList[CurrentIndex].Force, ActionList[CurrentIndex].Times, ActionList[CurrentIndex].GroupTimes, 0);
                        }
                        else
                        {
                            if ((ModeEnum)ActionList[CurrentIndex].Mode != ModeEnum.Guided
                                && (ModeEnum)ActionList[CurrentIndex].Mode != ModeEnum.Fit)
                                
                            {
                                //设置预设目标
                                SetTargetLine(ActionList[CurrentIndex].Speed, ActionList[CurrentIndex].MaxAngle, ActionList[CurrentIndex].MinAngle, ActionList[CurrentIndex].Times, ActionList[CurrentIndex].GroupTimes, ActionList[CurrentIndex].Position);
                            }
                        }

                        LumbarRobotController.RobotController.DoAction((ModeEnum)ActionList[CurrentIndex].Mode, (ActionEnum)ActionList[CurrentIndex].ActionId);

                    }
                }
                txtPrompt.Text = "系统正在复位请放松";

                //禁用fit按钮
                btnRotationFit.IsEnabled = false;
                btnPBFit.IsEnabled = false;
                btnActionFree.IsEnabled = false;
            }
        }

        #endregion

        #region 改变
        void SelectChange_Click(object sender, EventArgs e)
        {
            this.txtTimes.Text = "0/" + (this.setControlTimes.Value * this.setControlGroup.Value).ToString();
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
                if (ActionList != null && ActionList.Count > CurrentIndex)
                {
                    this.txtTimes.Text += "/" + ActionList[CurrentIndex].Times * ActionList[CurrentIndex].GroupTimes;
                }
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
            this.Dispatcher.BeginInvoke(new Action(() =>
            {
                //设置按钮
                //playBtn.IsPlaying = false;
                //playBtn.IsCanStop = false;
                playBtn.Init(MyActionListBox.SelectedIndex, ActionList.Count);

                btnRotationFit.IsEnabled = true;
                btnPBFit.IsEnabled = true;
                btnActionFree.IsEnabled = true;

                Stop(IsShowDialog);
                IsShowDialog = true;
            }));
        }

        void RobotController_NextTime(object sender, EventArgs e)
        {
            this.Dispatcher.Invoke(new Action(() =>
            {
                if (LumbarRobotController.RobotController.CurrentTimes + 1 <= ActionList[CurrentIndex].Times * ActionList[CurrentIndex].GroupTimes)
                {
                    //设置当前次数
                    this.txtTimes.Text = (LumbarRobotController.RobotController.CurrentTimes + 1).ToString();
                    if (ActionList != null && ActionList.Count > CurrentIndex)
                    {
                        this.txtTimes.Text += "/" + ActionList[CurrentIndex].Times * ActionList[CurrentIndex].GroupTimes;
                    }
                }

                //sinWpf1.DrawPI();
            }));
        }

        void RobotController_Move(LumbarRobotController.PositionArgs args)
        {
            if ((ActionList != null && ActionList.Count > CurrentIndex) || fitType!= LumbarRobot.FitType.None)
            {
                //if (ActionList[CurrentIndex].ActionId == (int)ActionEnum.Rotation)
                if(LumbarRobotController.RobotController.currentControl.Action == ActionEnum.Rotation)
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
            if (ActionList != null && ActionList.Count > CurrentIndex)
            {
                if ((ActionEnum)ActionList[CurrentIndex].ActionId == ActionEnum.Rotation)
                {
                    if (FitResult != null && FitResult.RotateMinAngle.HasValue && FitResult.RotateMinAngle.Value > ActionList[CurrentIndex].MinAngle)
                    {
                        LumbarRobotController.RobotController.MinAngle = (int)FitResult.RotateMinAngle.Value;
                    }
                    else
                    {
                        LumbarRobotController.RobotController.MinAngle = ActionList[CurrentIndex].MinAngle;
                    }

                    if (FitResult != null && FitResult.RotateMaxAngle.HasValue && FitResult.RotateMaxAngle.Value < ActionList[CurrentIndex].MaxAngle)
                    {
                        LumbarRobotController.RobotController.MaxAngle = (int)FitResult.RotateMaxAngle.Value;
                    }
                    else
                    {
                        LumbarRobotController.RobotController.MaxAngle = ActionList[CurrentIndex].MaxAngle;
                    }
                }
                else
                {
                    if (FitResult != null && FitResult.BendMinAngle.HasValue && FitResult.BendMinAngle.Value > ActionList[CurrentIndex].MinAngle)
                    {
                        LumbarRobotController.RobotController.MinAngle = (int)FitResult.BendMinAngle.Value;
                    }
                    else
                    {
                        LumbarRobotController.RobotController.MinAngle = ActionList[CurrentIndex].MinAngle;
                    }

                    if (FitResult != null && FitResult.BendMaxAngle.HasValue && FitResult.BendMaxAngle.Value < ActionList[CurrentIndex].MaxAngle)
                    {
                        LumbarRobotController.RobotController.MaxAngle = (int)FitResult.BendMaxAngle.Value;
                    }
                    else
                    {
                        LumbarRobotController.RobotController.MaxAngle = ActionList[CurrentIndex].MaxAngle;
                    }
                }
                LumbarRobotController.RobotController.Speed = ActionList[CurrentIndex].Speed * 10;
                LumbarRobotController.RobotController.Force = ActionList[CurrentIndex].Force * 10;
                LumbarRobotController.RobotController.Times = ActionList[CurrentIndex].Times;
                LumbarRobotController.RobotController.Target = ActionList[CurrentIndex].Position;
                LumbarRobotController.RobotController.GroupNum = ActionList[CurrentIndex].GroupTimes;
            }
        }
        #endregion

        #region 控制按钮是否可用
        /// <summary>
        /// 控制按钮是否可用
        /// </summary>
        /// <param name="flag"></param>
        public void ControlIsEnabled(bool flag)
        {
            this.setControlSpeed.IsEnabled = flag;
            this.setControlGroup.IsEnabled = flag;
            this.setControlForce.IsEnabled = flag;
            this.setControlMinAngle.IsEnabled = flag;
            this.setControlMaxAngle.IsEnabled = flag;
            this.setControlTimes.IsEnabled = flag;
            this.CboMode.IsEnabled = flag;
            this.setControlPosition.IsEnabled = flag;
            this.CboAction.IsEnabled = false;

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
            if (ActionList.Count == 1)
            {
                this.playBtn.IsPlayNextValid = false;
                this.playBtn.IsPlayPrevValid = false;
            }
            else
            {
                this.playBtn.IsPlayPrevValid = false;
                this.playBtn.IsPlayNextValid = true;
            }

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

            if (fitType != LumbarRobot.FitType.None)
            {
                lock (_lock)
                {
                    FitResultList = null;
                    if (LumbarRobotController.RobotController.IsPause)
                    {
                        LumbarRobotController.RobotController.IsPause = false;
                    }
                    else
                    {

                        if (fitType == LumbarRobot.FitType.BendStretchStrength
                            || fitType == LumbarRobot.FitType.RotateStrength)
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
                        //StopAction = new EventHandler(RobotController_StopAction);
                        //LumbarRobotController.RobotController.StopAction += StopAction;

                        if (StartAction == null)
                        {
                            StartAction = new EventHandler(RobotController_StartAction);
                            LumbarRobotController.RobotController.StartAction += StartAction;
                        }

                        if (fitType == LumbarRobot.FitType.BendStretchStrength
                            || fitType == LumbarRobot.FitType.RotateStrength)
                        {                            

                            if (SetForce == null)
                            {
                                SetForce = new SetForceHandler(RobotController_SetForce);
                                LumbarRobotController.RobotController.SetForce += SetForce;
                            }
                        }


                        SetParam();

                        if (fitType == LumbarRobot.FitType.BendStretchRange)
                        {
                            LumbarRobotController.RobotController.DoAction(ModeEnum.Fit, ActionEnum.ProtrusiveOrBend);
                        }
                        else if (fitType == LumbarRobot.FitType.BendStretchStrength)
                        {
                            LumbarRobotController.RobotController.DoAction(ModeEnum.StrengthEvaluation, ActionEnum.ProtrusiveOrBend);
                        }
                        else if (fitType == LumbarRobot.FitType.RotateRange)
                        {
                            LumbarRobotController.RobotController.DoAction(ModeEnum.Fit, ActionEnum.Rotation);
                        }
                        else
                        {
                            LumbarRobotController.RobotController.DoAction(ModeEnum.StrengthEvaluation, ActionEnum.Rotation);
                        }

                        //LimitSwitchChanged = new EventHandler(RobotController_LimitSwitchChanged);
                        //LumbarRobotController.RobotController.LimitSwitchChanged += LimitSwitchChanged;
                    }

                    txtPrompt.Text = "系统正在复位请放松";
                    if (fitType == LumbarRobot.FitType.BendStretchStrength)
                    {
                        txtMsg.Text = "正在进行屈伸力量测试，完成后点击确定！";
                    }
                    else if (fitType == LumbarRobot.FitType.RotateStrength)
                    {
                        txtMsg.Text = "正在进行旋转力量测试，完成后点击确定！";
                    }
                    else if (fitType == LumbarRobot.FitType.BendStretchRange)
                    {
                        txtMsg.Text = "正在进行屈伸Fit，完成后点击确定！";
                    }
                    else
                    {
                        txtMsg.Text = "正在进行旋转Fit，完成后点击确定！";
                    }

                    //设置当前次数
                    this.txtTimes.Text = "0/0";
                }
            }
        }


        #endregion

        #region Fit确认
        private void btnAffirm_Click(object sender, RoutedEventArgs e)
        {
            lock (_lock)
            {
                if (FitResultList == null || FitResultList.Count <= 0)
                {
                    //获取FIT结果
                    FitResult = LumbarRobotController.RobotController.GetFitResult();
                }
                else
                {
                    FitResult = new Services.FitResult();
                    if (FitResultList[0].FitMode == Common.Enums.FitModeEnum.ProtrusiveOrBendFit)
                    {
                        FitResult.BendMaxAngle = 0;
                        FitResult.BendMinAngle = 0;
                    }
                    else if (FitResultList[0].FitMode == Common.Enums.FitModeEnum.RotationFit)
                    {
                        FitResult.RotateMaxAngle = 0;
                        FitResult.RotateMinAngle = 0;
                    }
                    else if (FitResultList[0].FitMode == Common.Enums.FitModeEnum.ProtrusiveOrBendStrengthEvaluation)
                    {
                        FitResult.BendMax = 0;
                        FitResult.BendMin = 0;
                    }
                    else
                    {
                        FitResult.RotateMax = 0;
                        FitResult.RotateMin = 0;
                    }
                    var result = LumbarRobotController.RobotController.GetFitResult();
                    FitResultList.Add(result);
                    //取多次Fit的平均值
                    int count = FitResultList.Count;
                    Common.Enums.FitModeEnum fitMode = Common.Enums.FitModeEnum.ProtrusiveOrBendFit;
                    for (int i = 0; i < count; i++)
                    {
                        if (FitResultList[i].FitMode == Common.Enums.FitModeEnum.ProtrusiveOrBendFit)
                        {
                            FitResult.BendMaxAngle += FitResultList[i].BendMaxAngle.Value;
                            FitResult.BendMinAngle += FitResultList[i].BendMinAngle.Value;
                        }
                        else if (FitResultList[i].FitMode == Common.Enums.FitModeEnum.RotationFit)
                        {
                            FitResult.RotateMaxAngle += FitResultList[i].RotateMaxAngle.Value;
                            FitResult.RotateMinAngle += FitResultList[i].RotateMinAngle.Value;
                        }
                        else if (FitResultList[i].FitMode == Common.Enums.FitModeEnum.ProtrusiveOrBendStrengthEvaluation)
                        {
                            FitResult.BendMax += FitResultList[i].BendMax.Value;
                            FitResult.BendMin += FitResultList[i].BendMin.Value;
                        }
                        else
                        {
                            FitResult.RotateMax += FitResultList[i].RotateMax.Value;
                            FitResult.RotateMin += FitResultList[i].RotateMin.Value;
                        }

                        fitMode = FitResultList[i].FitMode;
                    }

                    if (FitResult.BendMaxAngle.HasValue) FitResult.BendMaxAngle = FitResult.BendMaxAngle.Value / count;
                    if (FitResult.BendMinAngle.HasValue) FitResult.BendMinAngle = FitResult.BendMinAngle.Value / count;
                    if (FitResult.RotateMaxAngle.HasValue) FitResult.RotateMaxAngle = FitResult.RotateMaxAngle.Value / count;
                    if (FitResult.RotateMinAngle.HasValue) FitResult.RotateMinAngle = FitResult.RotateMinAngle.Value / count;
                    if (FitResult.RotateMaxAngle.HasValue) FitResult.BendMax = FitResult.BendMax.Value / count;
                    if (FitResult.RotateMinAngle.HasValue) FitResult.BendMin = FitResult.BendMin.Value / count;
                    if (FitResult.RotateMaxAngle.HasValue) FitResult.RotateMax = FitResult.RotateMax.Value / count;
                    if (FitResult.RotateMinAngle.HasValue) FitResult.RotateMin = FitResult.RotateMin.Value / count;
                    FitResult.FitMode = fitMode;
                }

                //显示FIT结果
                if (FitResult != null)
                {
                    if (FitResult.FitMode == Common.Enums.FitModeEnum.ProtrusiveOrBendFit)
                    {
                        eilelc21.FltMinusValue = FitResult.BendMinAngle.Value;
                        eilelc21.FltValu = FitResult.BendMaxAngle.Value;
                    }
                    else if (FitResult.FitMode == Common.Enums.FitModeEnum.RotationFit)
                    {
                        eilelc21.FltMinusValue = FitResult.RotateMinAngle.Value;
                        eilelc21.FltValu = FitResult.RotateMaxAngle.Value;
                    }

                    SaveFitRecord(FitResult);
                }

                LumbarRobotController.RobotController.IsStop = true;
                Stop();

                if (fitType == LumbarRobot.FitType.None)
                {
                    this.SetVidicon.Visibility = Visibility.Hidden;
                }
                else
                {
                    this.Close(sender, e);
                }
            }
        }
        #endregion

        #region 再测一次
        private void btnNext_Click(object sender, RoutedEventArgs e)
        {
            lock (_lock)
            {
                #region
                if (FitResultList == null) FitResultList = new List<FitResult>();

                //获取FIT结果
                FitResult = LumbarRobotController.RobotController.GetFitResult();

                //显示FIT结果
                if (FitResult != null)
                {
                    if (FitResult.FitMode == Common.Enums.FitModeEnum.ProtrusiveOrBendFit)
                    {
                        eilelc21.FltMinusValue = FitResult.BendMinAngle.Value;
                        eilelc21.FltValu = FitResult.BendMaxAngle.Value;
                    }
                    else if (FitResult.FitMode == Common.Enums.FitModeEnum.RotationFit)
                    {
                        eilelc21.FltMinusValue = FitResult.RotateMinAngle.Value;
                        eilelc21.FltValu = FitResult.RotateMaxAngle.Value;
                    }
                    else if (FitResult.FitMode == Common.Enums.FitModeEnum.ProtrusiveOrBendStrengthEvaluation)
                    {
                        eilelc21.FltMinusValue = FitResult.BendMin.Value;
                        eilelc21.FltValu = FitResult.BendMax.Value;
                    }
                    else
                    {
                        eilelc21.FltMinusValue = FitResult.RotateMin.Value;
                        eilelc21.FltValu = FitResult.RotateMax.Value;
                    }

                    FitResultList.Add(FitResult);
                }

                Stop();

                //开始新Fit
                if (FitResult != null)
                {

                    if (LumbarRobotController.RobotController.IsPause)
                    {
                        LumbarRobotController.RobotController.IsPause = false;
                    }
                    else
                    {
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
                        //StopAction = new EventHandler(RobotController_StopAction);
                        //LumbarRobotController.RobotController.StopAction += StopAction;

                        if (StartAction == null)
                        {
                            StartAction = new EventHandler(RobotController_StartAction);
                            LumbarRobotController.RobotController.StartAction += StartAction;
                        }
                        SetParam();
                        txtPrompt.Text = "系统正在复位请放松";
                        if (FitResult.FitMode == Common.Enums.FitModeEnum.ProtrusiveOrBendFit)
                        {
                            txtMsg.Text = "正在进行曲伸Fit，完成后点击确定！";
                            LumbarRobotController.RobotController.DoAction(ModeEnum.Fit, ActionEnum.ProtrusiveOrBend);
                        }
                        else if(FitResult.FitMode == Common.Enums.FitModeEnum.RotationFit)
                        {
                            txtMsg.Text = "正在进行旋转Fit，完成后点击确定！";
                            LumbarRobotController.RobotController.DoAction(ModeEnum.Fit, ActionEnum.Rotation);
                        }
                        else if (FitResult.FitMode == Common.Enums.FitModeEnum.ProtrusiveOrBendStrengthEvaluation)
                        {
                            txtMsg.Text = "正在进行屈伸力量评测，完成后点击确定！";
                            LumbarRobotController.RobotController.DoAction(ModeEnum.StrengthEvaluation, ActionEnum.ProtrusiveOrBend);
                        }
                        else
                        {
                            txtMsg.Text = "正在进行旋转力量评测，完成后点击确定！";
                            LumbarRobotController.RobotController.DoAction(ModeEnum.StrengthEvaluation, ActionEnum.Rotation);
                        }


                        //LimitSwitchChanged = new EventHandler(RobotController_LimitSwitchChanged);
                        //LumbarRobotController.RobotController.LimitSwitchChanged += LimitSwitchChanged;
                    }

                }
                #endregion
            }

        }
        #endregion

        #region Fit取消
        private void btnCancel2_Click(object sender, RoutedEventArgs e)
        {
            lock (_lock)
            {
                LumbarRobotController.RobotController.IsStop = true;

                Stop();

                if (fitType == LumbarRobot.FitType.None)
                {
                    this.SetVidicon.Visibility = Visibility.Hidden;
                }
                else
                {
                    this.Close(sender, e);
                }
            }
        }
        #endregion

        #region 停止
        private void btnStop_Click(object sender, RoutedEventArgs e)
        {
            LumbarRobotController.RobotController.ControlCommand.PauseCmd();
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
            if (ActionList.Count <= 0) return;

            if ((ActionEnum)ActionList[CurrentIndex].ActionId == ActionEnum.Bend ||
                (ActionEnum)ActionList[CurrentIndex].ActionId == ActionEnum.Protrusive ||
                (ActionEnum)ActionList[CurrentIndex].ActionId == ActionEnum.ProtrusiveOrBend)
            {
                this.Dispatcher.Invoke(new Action(() =>
                {


                    var limitSwitchs = LumbarRobotController.RobotController.LimitSwitch;

                    eilelc21.DrawSaiByDoubles(new double[]{
                        limitSwitchs.Length<1?0:GetLimitSwitchAngle(limitSwitchs[0]),
                        limitSwitchs.Length<2?0:GetLimitSwitchAngle(limitSwitchs[1]),
                        limitSwitchs.Length<3?0:GetLimitSwitchAngle(limitSwitchs[2]),
                        limitSwitchs.Length<4?0:GetLimitSwitchAngle(limitSwitchs[3])
                    });
                }));
            }
            else
            {
                this.Dispatcher.Invoke(new Action(() =>
                {
                    eilelc21.DrawSaiByDoubles(null);
                }));
            }
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

        private void SetTargetLineByForce(int speed, int minForce,int maxForce, int times , int groupNum , int position)
        {
            List<double> targetLine = new List<double>();

            //增加等待
            for (int k = 0; k < 50; k++)
            {
                targetLine.Add(position);
            }

            #region 正弦波

            //for (int n = 0; n < groupNum; n++)
            //{
            //    for (int i = 0; i < times; i++)
            //    {
            //        var num = 625 / (speed * 2);
            //        for (int j = 0; j < num; j++)
            //        {
            //            targetLine.Add(force * 6 * Math.Sin((j / (double)num) * 2 * 3.14159) + position);
            //        }
            //    }

            //    if (n < groupNum - 1)
            //    {
            //        //增加10秒等待
            //        for (int k = 0; k < 100; k++)
            //        {
            //            targetLine.Add(position);
            //        }
            //    }
            //}
            #endregion

            #region 梯形波
            for (int n = 0; n < groupNum; n++)
            {
                for (int i = 0; i < times; i++)
                {
                    var num = 625 / (speed * 2);
                    if (maxForce == 0)
                    {                        
                        for (int j = 0; j < num; j++)
                        {
                            targetLine.Add(position + minForce * 6);
                        }

                        if (i < times - 1)
                        {
                            for (int j = 0; j < num; j++)
                            {
                                targetLine.Add(maxForce * 6 + position);
                            }
                        }
                    }
                    else
                    {
                        for (int j = 0; j < num; j++)
                        {
                            targetLine.Add(maxForce * 6 + position);
                        }

                        if (i < times - 1)
                        {
                            for (int j = 0; j < num; j++)
                            {
                                targetLine.Add(position + minForce * 6);
                            }
                        }
                    }
                }

                //增加10秒等待
                for (int k = 0; k < 100; k++)
                {
                    targetLine.Add(position);
                }
            }

            #endregion

            LumbarRobotController.RobotController.TargetLine = targetLine.ToArray();
            sinWpf1.ReSet();
            sinWpf1.GoldLine(targetLine.ToArray());
        }

        private void SetTargetLineByForce2(ActionEnum action, int speed, int force, int times, int groupNum, int position)
        {
            List<double> targetLine = new List<double>();

            //增加等待
            for (int k = 0; k < 50; k++) 
            {
                targetLine.Add(position);
            }

            for (int n = 0; n < groupNum; n++)
            {
                for (int i = 0; i < times; i++)
                {
                    var num = 625 / (speed * 2);
                    for (int j = 0; j < num; j++)
                    {
                        if (action == ActionEnum.ProtrusiveOrBend || action == ActionEnum.Rotation)
                        {
                            double f = force * 6 * Math.Sin((j / (double)num) * 2 * 3.14159) + position;
                            if (j == num / 4 || j == num * 3 / 4)
                            {
                                //增加10秒等待
                                for (int k = 0; k < num; k++)
                                {
                                    targetLine.Add(f);
                                }
                            }
                            else
                            {
                                targetLine.Add(f);
                            }
                        }
                        else if (action == ActionEnum.Protrusive)
                        {
                            double f = force * 6 * Math.Sin((j / (double)num) * 2 * 3.14159) + position;
                            if (j == num / 4)
                            {
                                for (int k = 0; k < num; k++)
                                {
                                    targetLine.Add(f);
                                }
                            }
                            else
                            {
                                if (f > 0)
                                {
                                    targetLine.Add(f);
                                }
                                else
                                {
                                    targetLine.Add(position);
                                }
                            }
                        }
                        else if (action == ActionEnum.Bend)
                        {
                            double f = force * 6 * Math.Sin((j / (double)num) * 2 * 3.14159) + position;
                            if (j == num * 3 / 4)
                            {
                                //增加10秒等待
                                for (int k = 0; k < 100; k++)
                                {
                                    targetLine.Add(f);
                                }
                            }
                            else
                            {
                                if (f < 0)
                                {
                                    targetLine.Add(f);
                                }
                                else
                                {
                                    targetLine.Add(position);
                                }
                            }
                        }
                    }
                }

                //增加10秒等待
                for (int k = 0; k < 100; k++)
                {
                    targetLine.Add(position);
                }

            }

            LumbarRobotController.RobotController.TargetLine = targetLine.ToArray();
            sinWpf1.GoldLine(targetLine.ToArray());
        }

        private void SetTargetLine(int speed, int maxAngle, int minAngle, int times,int groupNum,int position)
        {
            int center = (maxAngle + minAngle) / 2;
            int range = (maxAngle - minAngle) / 2;

            List<double> targetLine = new List<double>();

            //增加等待
            for (int k = 0; k < 50; k++)
            {
                targetLine.Add(position);
            }

            //找到第一个零点位置
            int startPoint = 0;
            var num = 625 / (speed * 2);
            double lastValue = double.MaxValue;
            bool? flag = null;//Math.Abs(lastValue - position) >= Math.Abs(sin - position)时==true
            for (int j = 0; j < num; j++)
            {
                var sin = Math.Sin((j / (double)num) * 2 * 3.14159) * range + center;
                if (flag.HasValue && Math.Abs(lastValue - position) < Math.Abs(sin - position) && flag.Value)
                {
                    startPoint = j;
                    break;
                }

                if (lastValue != double.MaxValue)
                {
                    if (Math.Abs(lastValue - position) >= Math.Abs(sin - position)) flag = true;
                    else flag = false;
                }

                lastValue = sin;
            }


            for (int n = 0; n < groupNum; n++)
            {
                for (int i = 0; i < times; i++)
                {
                    num = 625 / (speed * 2);
                    for (int j = 0; j < num; j++)
                    {
                        var sin = Math.Sin(((j + startPoint) / (double)num) * 2 * 3.14159);
                        //targetLine.Add(range * sin + center + position);
                        targetLine.Add(range * sin + center);
                    }
                }

                if (n < groupNum - 1)
                {
                    //增加等待
                    for (int k = 0; k < 100; k++)
                    {
                        targetLine.Add(position);
                    }
                }
                else
                {
                    targetLine.Add(position);
                }
            }
            LumbarRobotController.RobotController.TargetLine = targetLine.ToArray();
            sinWpf1.GoldLine(targetLine.ToArray());
        }

        #endregion

        #region 保存训练记录、Fit记录
        private void SaveTrainRecord(TrainRecord record)
        {
            Application.Current.Dispatcher.Invoke(new Action(() =>
            {
                try
                {
                    if (record.ModeId == (int)ModeEnum.Fit) return;
                    if (record != null)
                    {
                        Exerciserecord er = new Exerciserecord();
                        er.Id = Guid.NewGuid().ToString("N");
                        er.ActionId = record.ActionId;
                        er.EndTime = record.EndTime;
                        er.ExerciseDate = record.ExerciseDate;
                        er.ExMinutes = record.ExMinutes;
                        er.Times = record.Times;
                        er.FactTimes = record.FactTimes;
                        er.RobotForce = record.Force;
                        er.IsFit = record.IsFit;
                        er.MaxAngle = record.MaxAngle;
                        er.MinAngle = record.MinAngle;
                        er.ModeId = record.ModeId;
                        er.PatientId = ModuleConstant.PatientId;
                        er.Speed = record.Speed;
                        er.StartTime = record.StartTime;
                        er.Maxforce = record.MaxForce;
                        er.AvgForce = record.AvgForce;
                        er.RealMinAngle = record.RealMinAngle;
                        er.RealMaxAngle = record.RealMaxAngle;
                        er.GroupNum = record.GroupNum;
                        er.PushRodValue = record.PushRodValue;

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
                            er.Record1 = sb.ToString();
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
                            er.Record2 = sb.ToString();
                        }

                        sb = new StringBuilder();
                        if (record.ForceLine != null)
                        {
                            for (int i = 0; i < record.ForceLine.Length; i++)
                            {
                                if (i == 0)
                                {
                                    sb.Append(record.ForceLine[i].ToString("#0.00"));
                                }
                                else
                                {
                                    sb.Append("|").Append(record.ForceLine[i].ToString("#0.00"));
                                }
                            }
                            er.Record3 = sb.ToString();
                        }

                        sb = new StringBuilder();
                        if (record.GroupRecords != null)
                        {
                            for (int i = 0; i < record.GroupRecords.Length; i++)
                            {
                                if (i > 0)
                                {
                                    sb.Append("|");
                                }
                                sb.Append(record.GroupRecords[i].GroupNum).Append(",")
                                    .Append(record.GroupRecords[i].Min.ToString("#0.00")).Append(",")
                                    .Append(record.GroupRecords[i].Max.ToString("#0.00"));
                            }
                            er.GroupRecord = sb.ToString();
                        }

                        MySession.Session.Save(er);
                        MySession.Session.Flush();
                    }
                }
                catch {}
            }));
        }

        private void SaveFitRecord(FitResult fitResult)
        {
            Application.Current.Dispatcher.Invoke(new Action(() =>
            {
                try
                {
                    FitRecord record = new FitRecord();
                    record.Id = Guid.NewGuid().ToString("N");
                    record.PatientID = ModuleConstant.PatientId;
                    record.ModeID = (int)fitResult.FitMode;
                    record.CreateTime = DateTime.Now;
                    record.PushRodValue = LumbarRobotController.RobotController.PushRodAngle;
                    if (fitResult.FitMode == Common.Enums.FitModeEnum.ProtrusiveOrBendFit)
                    {
                        if (fitResult.BendMinAngle.HasValue) record.MinAngle = (int)(fitResult.BendMinAngle.Value);
                        if (fitResult.BendMaxAngle.HasValue) record.MaxAngle = (int)(fitResult.BendMaxAngle.Value);
                    }
                    else if (fitResult.FitMode == Common.Enums.FitModeEnum.RotationFit)
                    {
                        if (fitResult.RotateMinAngle.HasValue) record.MinAngle = (int)fitResult.RotateMinAngle.Value;
                        if (fitResult.RotateMaxAngle.HasValue) record.MaxAngle = (int)fitResult.RotateMaxAngle.Value;
                    }
                    else  if (fitResult.FitMode == Common.Enums.FitModeEnum.ProtrusiveOrBendStrengthEvaluation)
                    {
                        if (fitResult.BendMin.HasValue) record.MinAngle = (int)(fitResult.BendMin.Value);
                        if (fitResult.BendMax.HasValue) record.MaxAngle = (int)(fitResult.BendMax.Value);
                    }
                    else
                    {
                        if (fitResult.RotateMin.HasValue) record.MinAngle = (int)fitResult.RotateMin.Value;
                        if (fitResult.RotateMax.HasValue) record.MaxAngle = (int)fitResult.RotateMax.Value;
                    }

                    MySession.Session.Save(record);
                    MySession.Session.Flush();
                }
                catch { }
            }));
        }

        //private void SaveStrengthEvaluationRecord(FitResult fitResult)
        //{
        //    Application.Current.Dispatcher.Invoke(new Action(() =>
        //    {
        //        try
        //        {
        //            FitRecord record = new FitRecord();
        //            record.Id = Guid.NewGuid().ToString("N");
        //            record.PatientID = ModuleConstant.PatientId;
        //            record.ModeID = (int)fitResult.FitMode;
        //            record.CreateTime = DateTime.Now;
        //            record.PushRodValue = LumbarRobotController.RobotController.PushRodAngle;
        //            if (fitResult.FitMode == Common.Enums.FitModeEnum.ProtrusiveOrBendStrengthEvaluation)
        //            {
        //                if (fitResult.BendMinAngle.HasValue) record.MinAngle = (int)(fitResult.BendMin.Value);
        //                if (fitResult.BendMaxAngle.HasValue) record.MaxAngle = (int)(fitResult.BendMax.Value);
        //            }
        //            else
        //            {
        //                if (fitResult.RotateMinAngle.HasValue) record.MinAngle = (int)fitResult.RotateMin.Value;
        //                if (fitResult.RotateMaxAngle.HasValue) record.MaxAngle = (int)fitResult.RotateMax.Value;
        //            }

        //            MySession.Session.Save(record);
        //            MySession.Session.Flush();
        //        }
        //        catch { }
        //    }));
        //}
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
                    btnRotationFit.IsEnabled = false;
                    btnPBFit.IsEnabled = false;
                    btnActionFree.IsEnabled = false;

                    msgBox.Show();

                    if ((LumbarRobotController.RobotController.AlarmCode & 0x1000000) == 0x1000000
                        || (LumbarRobotController.RobotController.AlarmCode & 0x2000000) == 0x2000000)//25
                    {
                        this.playBtn.IsCanStop = false;
                        this.playBtn.imgStop.IsEnabled = false;
                        MyActionListBox.IsEnabled = true;

                        SetIsEnabled();

                        LumbarRobotController.RobotController.ControlCommand.PauseCmd();
                        IsShowDialog = false;
                        LumbarRobotController.RobotController.IsStop = true;
                    }
                    else
                    {
                        LumbarRobotController.RobotController.IsPause = true;
                        //SetIsEnabled();
                    }


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

        #region 动作改变
        private void CboAction_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (CboAction.SelectedIndex == (int)ActionEnum.Rotation)
            {

                this.setControlMaxAngle.MinValue = 0;
                this.setControlMaxAngle.MaxValue = 35;
                this.setControlMinAngle.MinValue = -35;
                this.setControlMinAngle.MaxValue = 0;
            }
            if (CboAction.SelectedIndex == (int)ActionEnum.Protrusive)
            {
                this.setControlMaxAngle.MinValue = 0;
                this.setControlMaxAngle.MaxValue = 80;
                this.setControlMinAngle.MinValue = -80;
                this.setControlMinAngle.MaxValue = 0;
            }
            if (CboAction.SelectedIndex == (int)ActionEnum.Bend)
            {
                this.setControlMaxAngle.MinValue = 0;
                this.setControlMaxAngle.MaxValue = 80;
                this.setControlMinAngle.MaxValue = 0;
                this.setControlMinAngle.MinValue = -60;
            }
            if (CboAction.SelectedIndex == (int)ActionEnum.ProtrusiveOrBend)
            {
                this.setControlMinAngle.MinValue = -60;
                this.setControlMinAngle.MaxValue = 0;
                this.setControlMaxAngle.MaxValue = 80;
                this.setControlMaxAngle.MinValue = 0;  
            }
        }
        #endregion
    }

    public enum FitType
    {
        None = 0,

        /// <summary>
        /// 屈伸力量
        /// </summary>
        BendStretchStrength = 1,

        /// <summary>
        /// 屈伸范围
        /// </summary>
        BendStretchRange = 2,

        /// <summary>
        /// 旋转力量
        /// </summary>
        RotateStrength = 3,

        /// <summary>
        /// 旋转范围
        /// </summary>
        RotateRange = 4

    }
}
