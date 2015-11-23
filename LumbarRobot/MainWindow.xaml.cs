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
using LumbarRobot.ViewModels;
using LumbarRobot.Enums;
using System.Threading;
using LumbarRobot.Services;
using LumbarRobot.MyUserControl;
using LumbarRobot.Event;
using LumbarRobot.DAL;
using LumbarRobot.Data;
using System.Data;
using LumbarRobot.ChartNodes;
using LumbarRobot.Common.Enums;
using LumbarRobot.Common;
using LumbarRobot.ModulesCommon;

namespace LumbarRobot
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow
    {

        #region 加载Load事件
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
           // GlobalVar.GameReturn += new ULTROBOT.ModulesCommon.OSC.GameReturnEventHandler(GlobalVar_GameReturn);
            this.CboAction.SelectedIndex = 0;
            this.CboMode.SelectedIndex = 0;
            listResult = new ObservableCollection<ItemDemo>();
            dayList = new ObservableCollection<DayDemo>();
            modeList = new ObservableCollection<ModeDemo>();
            SelectPatientEvent.Instance.Subscribe(GetPatient);
            SaveCourseEvent.Instance.Subscribe(GetBool);
            
            //SelectPatientEvent.Instance.Subscribe(GetPatient);
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
            patientInfos.dg.SelectedIndex = 0;
            SelectPatientEvent.Instance.Subscribe(GetPatient);
            LumbarRobotController.RobotController.Disconnected += new EventHandler(RobotController_Disconnected);
            LumbarRobotController.RobotController.Connected += new EventHandler(RobotController_Connected);
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

        #region 变量
        /// <summary>
        /// 结果集合
        /// </summary>
        public ObservableCollection<ItemDemo> listResult = null;
        /// <summary>
        /// 日期集合
        /// </summary>
        public ObservableCollection<DayDemo> dayList = null;
        /// <summary>
        /// 模式列表
        /// </summary>
        public ObservableCollection<ModeDemo> modeList = null;
        /// <summary>
        /// 疗程
        /// </summary>
        ObservableCollection<Prescription> PrescriptionList = null;
        /// <summary>
        /// 训练动作集合
        /// </summary>
        ObservableCollection<ExItemDemo> ExItemDemoList = null;
        /// <summary>
        /// 旋转的Fit集合
        /// </summary>
        ObservableCollection<FitResultX> FitResultXList = null;
        /// <summary>
        /// 曲伸的Fit集合
        /// </summary>
        ObservableCollection<FitResultQ> FitResultQList = null;
        private ActionControlDialog ControlDialog;
        /// <summary>
        /// 测试报告
        /// </summary>
        private ReportPrintDialog ReportPrintDialog;
        /// <summary>
        /// 测试报告列表
        /// </summary>
        private FitResultDialog FitResultDialog;
        private SaveCourseDialog SaveCourseDialog;
        private object objLock = new object();
        private Syspatient patientInfo = null;
        private ShowReportDetailDialog showReport;
        private ShowFitChartDialog fitDialog;
        private ChartNodesDialog chartNodes;
        private ExItemDemo myItem;
        FitResultQ FitResultQ = null;
        FitResultX FitResultX = null; 
        FitResultQ FitResultQ_Old = null;
        FitResultX FitResultX_Old = null;
       
        #endregion

        #region 构造
        public MainWindow()
        {
            InitializeComponent();
            BindMenu();
        }
        #endregion

        #region 开始
        private void btnStart_Click(object sender, RoutedEventArgs e)
        {
            ThreadPool.QueueUserWorkItem(new WaitCallback((obj) =>
          {
              lock (objLock)
              {
                  Application.Current.Dispatcher.Invoke(new Action(() =>
                  {
                      //SelectPatientEvent.Instance.Subscribe(GetPatient);
                      if (patientInfo == null)
                      {
                          AlarmDialog dialog = new AlarmDialog();
                          dialog.lblTitle.Text = "提示信息";
                          dialog.lblMsg.Text = "请选择病人!";
                          dialog.ShowDialog();
                      }
                      else
                      {
                          //&& MyActionListBox.SelectedIndex>=0 
                          //if (listResult.Count > MyActionListBox.SelectedIndex && MyActionListBox.SelectedIndex != -1 && listResult.Count > 0)
                          //{
                          //    WPFMessageBox msgBox = new WPFMessageBox();
                          //    msgBox.lblMsg.Text = "是否修改！";
                          //    msgBox.lblTitle.Text = "提示信息";
                          //    msgBox.ShowDialog();
                          //    if (msgBox.IsFlag)
                          //    {
                          //        ComboBoxItem cbi = (ComboBoxItem)this.CboAction.SelectedItem;
                          //        ComboBoxItem cbiMode = (ComboBoxItem)this.CboMode.SelectedItem;
                          //        if (MyActionListBox.SelectedIndex != -1)
                          //        {
                          //            if (listResult.Count > 0)
                          //            {
                          //                new Thread(() =>
                          //                {
                          //                    this.Dispatcher.Invoke(new Action(() =>
                          //                    {
                          //                        int testIndex = MyActionListBox.SelectedIndex;
                          //                        ItemDemo item = listResult[MyActionListBox.SelectedIndex] as ItemDemo;
                          //                        item.ActionName = cbi.Content.ToString();
                          //                        item.ActionId = CboAction.SelectedIndex;
                          //                        ItemDemo changeItem = new ItemDemo();
                          //                        SetItem(item, changeItem);
                          //                        listResult.RemoveAt(MyActionListBox.SelectedIndex);
                          //                        listResult.Insert(testIndex, changeItem);
                          //                        MyActionListBox.DataContext = listResult;
                          //                        MyActionListBox.SelectedIndex = testIndex;

                          //                    }));
                          //                }).Start();
                          //            }
                          //        }
                          //    }
                              //ItemDemo item = listResult[MyActionListBox.SelectedIndex];
                              //item.Force = this.setControlForce.Value;
                              //item.MaxAngle = this.setControlMaxAngle.Value;
                              //item.MinAngle = this.setControlMinAngle.Value;
                              //item.Speed = this.setControlSpeed.Value;
                              //item.ActionId = this.CboAction.SelectedIndex;
                              //item.Mode = this.CboMode.SelectedIndex;
                              //item.Times = this.setControlTimes.Value;
                              //item.Position = this.setControlPosition.Value;
                              //item.GroupTimes = this.setControlGroup.Value;
                          //}
                          if (listResult.Count > 0)
                          {
                              if (ControlDialog == null)
                              {
                                  Syspatient syspatient = new Syspatient();
                                  syspatient = MySession.Session.Get<Syspatient>(ModuleConstant.PatientId);
                                  syspatient.LastTime = DateTime.Now;
                                  MySession.Session.SaveOrUpdate(syspatient);
                                  MySession.Session.Flush();
                                  ControlDialog = new ActionControlDialog();
                                  ControlDialog.Parent = this;
                                  ActionControl child = new ActionControl(listResult);
                                  //child.ActionList = listResult;
                                  child.Close += new EventHandler(child_Close);
                                  ControlDialog.Content = child;
                                  ControlDialog.Show();
                              }
                          }
                          else
                          {
                              AlarmDialog dialog = new AlarmDialog();
                              dialog.lblTitle.Text = "提示信息";
                              dialog.lblMsg.Text = "请选择训练动作及模式!";
                              dialog.ShowDialog();
                          }
                          //else
                          //{
                          //    AlarmDialog dialog = new AlarmDialog();
                          //    dialog.lblTitle.Text = "提示信息";
                          //    dialog.lblMsg.Text = "请选择训练动作!";
                          //    dialog.ShowDialog();
                          //    //if (MessageBox.Show("请选择训练动作！", "提示信息", MessageBoxButton.OK, MessageBoxImage.Error) == MessageBoxResult.OK)
                          //    //{
                          //    //}
                          //}
                      }
                  }));
              }
          }));
        }

        #region 获取患者信息
        public void GetPatient(Syspatient obj)
        {
            if (obj != null)
            {
                if (tabChoose.SelectedIndex == -1)
                {
                    tabChoose.SelectedIndex = 0;
                }
                patientInfo = obj;
                ModuleConstant.PatientId = obj.Id;
                ModuleConstant.PatientName = obj.UserName;
                ModuleConstant.Syspatient = obj;
                BindDayReport(ModuleConstant.PatientId);
                BindModeReport(ModuleConstant.PatientId);
                BindPrescription(ModuleConstant.PatientId);
                if (tabChoose.SelectedIndex == 2)
                {
                    this.BeginTime.Text = DateTime.Now.ToString("yyyy-MM-dd");
                    this.EndTime.Text = DateTime.Now.ToString("yyyy-MM-dd");
                    BindExerciserecord(ModuleConstant.PatientId);
                }
            }
            else
            {
                patientInfo = null;
                ModuleConstant.PatientId = null;
                ModuleConstant.PatientName = null;
                ModuleConstant.Syspatient = null;
                BindDayReport("");
                BindModeReport("");
                BindPrescription("");
                
            }
            listResult.Clear();
            CboAction.SelectedIndex = 0;
            CboMode.SelectedIndex = 0;
            this.setControlSpeed.Value = 2;
            this.setControlForce.Value = 3;
            this.setControlMinAngle.Value = -20;
            this.setControlMaxAngle.Value = 20;
            this.setControlTimes.Value = 5;
            this.setControlPosition.Value = 0;
            this.setControlGroup.Value = 1;
            CourseListBox.SelectedIndex = -1;
        }
        #endregion

        #region 获取bool
        public void GetBool(bool obj)
        {
            if (obj)
            {
                tabChoose.SelectedIndex = 0;
                BindPrescription(ModuleConstant.PatientId);
            }
            else
            {
                tabChoose.SelectedIndex = 0;
                BindPrescription("");

            }
        }
        #endregion

        #region 关闭事件

        public void child_Close(object sender, EventArgs e)
        {
            if (ControlDialog != null)
            {
                ControlDialog.Close();
            }
            ControlDialog = null;
            if (ReportPrintDialog != null)
            {
                ReportPrintDialog.Close();
            }
            ReportPrintDialog = null;

            if (FitResultDialog != null)
            {
                FitResultDialog.Close();
            }
            FitResultDialog = null;

            if (SaveCourseDialog != null)
            {
                SaveCourseDialog.Close();
            }
            SaveCourseDialog = null;
        }
        #endregion

        #endregion

        #region 动作改变
        private void CboAction_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (CboAction.SelectedIndex != 0)
            {
              
                this.setControlSpeed.IsEnabled = true;
                this.setControlForce.IsEnabled = true;
                this.setControlMinAngle.IsEnabled = true;
                this.setControlMaxAngle.IsEnabled = true;
                this.setControlTimes.IsEnabled = true;
                this.CboMode.IsEnabled = true;
                this.setControlGroup.IsEnabled = true;
                this.setControlPosition.IsEnabled = true;
                if (CboAction.SelectedIndex == (int)ActionEnum.Rotation)
                {
                    this.setControlMaxAngle.MinValue = 0;
                    this.setControlMaxAngle.MaxValue = 35;
                    this.setControlMinAngle.MinValue = -35;
                    this.setControlMinAngle.MaxValue = 0;
                    this.setControlMinAngle.Value = -20;
                    this.setControlMaxAngle.Value = 20;
                }
                if (CboAction.SelectedIndex == (int)ActionEnum.Protrusive)
                {
                    this.setControlMaxAngle.MinValue = 0;
                    this.setControlMaxAngle.MaxValue = 80;
                    this.setControlMinAngle.MinValue = -80;
                    this.setControlMinAngle.MaxValue = 0;
                    this.setControlMinAngle.Value = 0;
                    this.setControlMaxAngle.Value = 20;
                }
                if (CboAction.SelectedIndex == (int)ActionEnum.Bend)
                {
                    this.setControlMaxAngle.MinValue = 0;
                    this.setControlMaxAngle.MaxValue = 80;
                    this.setControlMinAngle.MaxValue = 0;
                    this.setControlMinAngle.MinValue = -60;
                    this.setControlMinAngle.Value = -20;
                    this.setControlMaxAngle.Value = 0;
                }
                if (CboAction.SelectedIndex == (int)ActionEnum.ProtrusiveOrBend)
                {
                    this.setControlMinAngle.MinValue = -60;
                    this.setControlMinAngle.MaxValue = 0;
                    this.setControlMaxAngle.MaxValue = 80;
                    this.setControlMaxAngle.MinValue = 0;
                    this.setControlMinAngle.Value = -20;
                    this.setControlMaxAngle.Value = 20;
                }
            }
            else
            {
                CboMode.SelectedIndex = 0;
                this.setControlSpeed.Value = 2;
                this.setControlForce.Value = 3;
                this.setControlMinAngle.Value = -20;
                this.setControlMaxAngle.Value = 20;
                this.setControlTimes.Value = 5;
                this.setControlGroup.Value = 1;
            }
        }

        #endregion

        #region 保存动作
        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            tempItem = null;
            MyActionListBox.SelectedIndex = -1;

            if (listResult.Count < 12)
            {
                if (CboAction.SelectedIndex == 0)
                {
                    AlarmDialog dialog = new AlarmDialog();
                    dialog.lblTitle.Text = "提示信息";
                    dialog.lblMsg.Text = "请选择动作！";
                    dialog.ShowDialog();
                    //if (MessageBox.Show("请选择动作！", "提示信息", MessageBoxButton.OK, MessageBoxImage.Error) == MessageBoxResult.OK)
                    //{ }

                }
                else
                {
                    if (CboMode.SelectedIndex == 0)
                    {
                        AlarmDialog dialog = new AlarmDialog();
                        dialog.lblTitle.Text = "提示信息";
                        dialog.lblMsg.Text = "请选择模式！";
                        dialog.ShowDialog();
                        //if (MessageBox.Show("请选择模式！", "提示信息", MessageBoxButton.OK, MessageBoxImage.Error) == MessageBoxResult.OK)
                        //{ }
                    }
                    else
                    {

                        ItemDemo item = new ItemDemo();
                        item.Id = Guid.NewGuid().ToString("N");
                        item.MaxAngle = this.setControlMaxAngle.Value;
                        ComboBoxItem cbi = (ComboBoxItem)this.CboAction.SelectedItem;
                        item.ActionName = cbi.Content.ToString();
                        ComboBoxItem cbMode = (ComboBoxItem)this.CboMode.SelectedItem;
                        item.ModeName = cbMode.Content.ToString();
                        item.MinAngle = this.setControlMinAngle.Value;
                        item.Mode = EnumHelper.GetEnumItemValueByName(((ComboBoxItem)this.CboMode.SelectedItem).Tag.ToString(), typeof(ModeEnum));//this.CboMode.SelectedIndex;
                        item.ActionId = EnumHelper.GetEnumItemValueByName(((ComboBoxItem)this.CboAction.SelectedItem).Tag.ToString(),typeof(ActionEnum)); //this.CboAction.SelectedIndex;
                        item.Speed = this.setControlSpeed.Value;
                        item.Times = this.setControlTimes.Value;
                        item.Force = this.setControlForce.Value;
                        item.Position = this.setControlPosition.Value;
                        item.GroupTimes = this.setControlGroup.Value;
                        item.PicturePath = "/images/prescribe.png";
                        listResult.Add(item);

                        if (listResult.Count > 0)
                        {
                            MyActionListBox.DataContext = listResult;
                            //MyActionListBox.SelectedIndex = listResult.Count - 1;
                        }
                    }
                }
            }
            else
            {
                AlarmDialog dialog = new AlarmDialog();
                dialog.lblTitle.Text = "提示信息";
                dialog.lblMsg.Text = "最多选择12个动作！";
                dialog.ShowDialog();
                //if (MessageBox.Show("最多选择12个动作！", "提示信息", MessageBoxButton.OK, MessageBoxImage.Error) == MessageBoxResult.OK)
                //{ }
            }
        }
        #endregion

        #region 选中动作改变事件
        ItemDemo tempItem = null;
        private void MyActionListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (tempItem != null)
            {
                tempItem.Force = this.setControlForce.Value;
                tempItem.MaxAngle = this.setControlMaxAngle.Value;
                tempItem.MinAngle = this.setControlMinAngle.Value;
                tempItem.Speed = this.setControlSpeed.Value;
                tempItem.ActionId = EnumHelper.GetEnumItemValueByName(((ComboBoxItem)this.CboAction.SelectedItem).Tag.ToString(), typeof(ActionEnum)); //this.CboAction.SelectedIndex;
                tempItem.Mode = EnumHelper.GetEnumItemValueByName(((ComboBoxItem)this.CboMode.SelectedItem).Tag.ToString(), typeof(ModeEnum)); //this.CboMode.SelectedIndex;
                tempItem.Times = this.setControlTimes.Value;
                tempItem.Position = this.setControlPosition.Value;
                tempItem.GroupTimes = this.setControlGroup.Value;
            }
            ItemDemo item = MyActionListBox.SelectedItem as ItemDemo;
            if (item != null)
            {
                tempItem = item;
                this.setControlForce.Value = item.Force;
                this.setControlMaxAngle.Value = item.MaxAngle;
                this.setControlMinAngle.Value = item.MinAngle;
                this.setControlSpeed.Value = item.Speed;
                //this.CboAction.SelectedIndex = item.ActionId;
                //this.CboMode.SelectedIndex = item.Mode;
                string name = EnumHelper.GetEnumValueName((ActionEnum)item.ActionId);
                foreach (ComboBoxItem it in this.CboAction.Items)
                {
                    if (it.Tag.ToString() == name)
                    {
                        it.IsSelected = true;
                        break;
                    }
                }

                name = EnumHelper.GetEnumValueName((ModeEnum)item.Mode);
                foreach (ComboBoxItem it in this.CboMode.Items)
                {
                    if (it.Tag.ToString() == name)
                    {
                        it.IsSelected = true;
                        break;
                    }
                }
                this.setControlTimes.Value = item.Times;
                this.setControlPosition.Value = item.Position;
                this.setControlGroup.Value = item.GroupTimes;
            }
        }
        #endregion

        #region 删除操作
        private void btnMyAction_Click(object sender, RoutedEventArgs e)
        {
            WPFMessageBox msgBox = new WPFMessageBox();
            msgBox.lblMsg.Text = "是否删除！";
            msgBox.lblTitle.Text = "提示信息";
            msgBox.ShowDialog();
            if (msgBox.IsFlag)
            //if (MessageBox.Show("是否删除！", "提示信息", MessageBoxButton.OKCancel, MessageBoxImage.Error) == MessageBoxResult.OK)
            {
                ItemDemo item = MyActionListBox.SelectedItem as ItemDemo;
                listResult.Remove(item);

                if (listResult.Count == 0)
                {
                    CboAction.SelectedIndex = 0;
                    CboMode.SelectedIndex = 0;
                    this.setControlSpeed.Value = 2;
                    this.setControlForce.Value = 3;
                    this.setControlMinAngle.Value = -20;
                    this.setControlMaxAngle.Value = 20;
                    this.setControlTimes.Value = 5;
                    this.setControlPosition.Value = 0;
                    this.setControlGroup.Value = 1;
                }
                else
                {
                    MyActionListBox.SelectedIndex = 0;
                }
            }
        }
        #endregion

        #region 清空动作列表
        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            WPFMessageBox msgBox = new WPFMessageBox();
            msgBox.lblMsg.Text = "是否清空动作列表！";
            msgBox.lblTitle.Text = "提示信息";
            msgBox.ShowDialog();
            if (msgBox.IsFlag)
            //if (MessageBox.Show("是否清空动作列表！", "提示信息", MessageBoxButton.OKCancel, MessageBoxImage.Error) == MessageBoxResult.OK)
            {
                listResult.Clear();
                CboAction.SelectedIndex = 0;
                CboMode.SelectedIndex = 0;
                this.setControlSpeed.Value = 2;
                this.setControlForce.Value = 3;
                this.setControlMinAngle.Value = -20;
                this.setControlMaxAngle.Value = 20;
                this.setControlTimes.Value = 5;
                this.setControlPosition.Value = 0;
                this.setControlGroup.Value = 1;
            }
        }
        #endregion

        #region 获得模式名称
        /// <summary>
        /// 获得模式名称
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        private static string GetModeTypeName(string type)
        {
            return EnumHelper.GetEnumItemName(Convert.ToInt32(type), typeof(ModeEnum));
        }
        #endregion

        #region 获得动作名称
        /// <summary>
        /// 获得动作名称
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        private static string GetActionTypeName(string type)
        {
            return EnumHelper.GetEnumItemName(Convert.ToInt32(type), typeof(ActionEnum));
        }
        #endregion

        #region 绑定日期训练报告
        public void BindDayReport(string patientId)
        {
            //gvLog.Items.Clear();
            dayList = new ObservableCollection<DayDemo>();
            DataTable tbl = new DataTable();
            tbl.Columns.Add("Index", typeof(string));    //行号 0
            tbl.Columns.Add("Time", typeof(string)); //训练时间  1
            tbl.Columns.Add("TotalTime", typeof(string)); //训练时长 2

            int a = 0;
            var list = from od in MySession.Query<Exerciserecord>()
                       where od.PatientId == patientId
                       select od;
            var obj = (from p in list
                       group p by new
                       {
                           p.PatientId,
                           p.ExerciseDate

                       } into g
                       select new
                       {
                           patientId = g.Key.PatientId,
                           exerciseDate = g.Key.ExerciseDate,
                           totalTime = g.Sum(p => p.ExMinutes)

                       }).ToList().OrderByDescending(x => x.exerciseDate);
            foreach (var item in obj)
            {
                a++;
                //格式化时间
                string result = Format.SetTime(item.totalTime.ToString());
                tbl.Rows.Add(a.ToString(), Convert.ToDateTime(item.exerciseDate).ToString("yyyy/MM/dd"), result);
            }
            if (tbl.Rows.Count > 0)
            {
                dayList.Clear();
                if (tbl.Rows.Count > 0)
                {
                    for (int i = 0; i < tbl.Rows.Count; i++)
                    {
                        DataRow dr = tbl.Rows[i];
                        DayDemo item = new DayDemo();
                        item.DayIndex = dr[0].ToString();
                        item.DayTime = dr[1].ToString();
                        item.DayTotalTime = dr[2].ToString();
                        dayList.Add(item);
                    }
                }
            }

            gvLog.ItemsSource = dayList;

        }
        #endregion

        #region 绑定模式报告
        public void BindModeReport(string patientId)
        {
            //gvMode.Items.Clear();
            modeList = new ObservableCollection<ModeDemo>();
            DataTable tbl = new DataTable();
            tbl.Columns.Add("ModeID", typeof(string));      // 模式ID     0 
            tbl.Columns.Add("ModeName", typeof(string));    // 模式名称 1 
            tbl.Columns.Add("TotalTime", typeof(string));   //训练时长 3

            var list = from od in MySession.Query<Exerciserecord>()
                       where od.PatientId==patientId
                       select od;
            var temp = from a in list
                       group a by new
                       {
                           a.PatientId,
                           a.ModeId,
                       } into p
                       select new
                       {
                           pid = p.Key.PatientId,
                           ModeId = p.Key.ModeId,
                           totalTime = p.Sum(f => f.ExMinutes)
                       };

            foreach (var item in temp)
            {
                string result = GetModeTypeName(item.ModeId.ToString());
                string totalTime = Format.SetTime(item.totalTime.ToString());
                tbl.Rows.Add(item.ModeId, result, totalTime);
            }
            //按照日期查询数据库

            if (tbl.Rows.Count > 0)
            {
                modeList.Clear();
                if (tbl.Rows.Count > 0)
                {
                    for (int i = 0; i < tbl.Rows.Count; i++)
                    {
                        DataRow dr = tbl.Rows[i];
                        ModeDemo item = new ModeDemo();
                        item.ModeID = dr[0].ToString();
                        item.ModeName = dr[1].ToString();
                        item.TotalTime = dr[2].ToString();
                        modeList.Add(item);
                    }
                }
            }
            
            gvMode.ItemsSource = modeList;
        }
        #endregion

        #region 绑定内置疗程
        public void BindPrescription(string PatientId)
        {
            PrescriptionList = new ObservableCollection<Prescription>();
            var list = (from od in MySession.Query<Prescription>()
                        where od.PatientId == null || od.PatientId == PatientId
                        select od).OrderBy(s=>s.LastTime).ToList();


            if (list.Count > 0)
            {
                PrescriptionList.Clear();

                foreach (var item in list)
                {
                    PrescriptionList.Add(item);
                }
            }
            CourseListBox.ItemsSource = PrescriptionList;
        }
        #endregion

        #region 绑定训练记录
        /// <summary>
        /// 绑定训练记录
        /// </summary>
        /// <param name="patientId">病人ID</param>
        public void BindExerciserecord(string patientId)
        {
            ExItemDemoList = new ObservableCollection<ExItemDemo>();
            DataTable tbl = new DataTable();
            tbl.Columns.Add("ExItemId", typeof(string));      // 动作ID   0 
            tbl.Columns.Add("ExItemName", typeof(string));    // 动作名称 1 

            var list = from od in MySession.Query<Exerciserecord>()
                       where od.PatientId == patientId
                       select od;
            var temp = from a in list
                       group a by new
                       {
                           a.PatientId,
                           a.ActionId
                       } into p
                       select new
                       {
                           pid = p.Key.PatientId,
                           ActionId = p.Key.ActionId
                       };

            foreach (var item in temp)
            {
                string result = GetActionTypeName(item.ActionId.ToString());
                tbl.Rows.Add(item.ActionId, result);
            }

            if (tbl.Rows.Count > 0)
            {
                ExItemDemoList.Clear();
                if (tbl.Rows.Count > 0)
                {
                    for (int i = 0; i < tbl.Rows.Count; i++)
                    {
                        DataRow dr = tbl.Rows[i];
                        ExItemDemo item = new ExItemDemo();
                        item.ExItemId = dr[0].ToString();
                        item.ExItemName = dr[1].ToString();
                        ExItemDemoList.Add(item);
                    }
                }
            }

            gvItem.ItemsSource = ExItemDemoList;
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

        #region 推杆

        #region 推杆上
        /// <summary>
        /// 推杆上
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnTUp_Click(object sender, RoutedEventArgs e)
        {
           // LumbarRobotController.RobotController.ControlCommand.PushRodUp();
        }
        #endregion

        #region 推杆下
        /// <summary>
        /// 推杆下
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnTDown_Click(object sender, RoutedEventArgs e)
        {
           // LumbarRobotController.RobotController.ControlCommand.PushRodDowm();
        }
        #endregion

        #region 停止
        /// <summary>
        /// 推杆停止
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnTStop_Click(object sender, RoutedEventArgs e)
        {
            //LumbarRobotController.RobotController.ControlCommand.PushRodStop();
        }
        #endregion

        #endregion

        #region 日期报告改变事件
        private void gvLog_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (gvLog.SelectedIndex != -1)
            {
                gvMode.SelectedIndex = -1;
            }
            DayDemo dayModel = this.gvLog.SelectedItem as DayDemo;
            if (dayModel != null)
            {
                showReport = new ShowReportDetailDialog();
                showReport.Parent = this;
                ShowReportDetailView child = new ShowReportDetailView();
                child.DayTime = dayModel.DayTime;
                child.ModeId = null;
                child.Close += new EventHandler(showReportchild_Close);
                showReport.Content = child;
                showReport.Show();
            }
        }
        #endregion

        #region 关闭
        public void showReportchild_Close(object sender, EventArgs e)
        {
            showReport.Close();
            gvLog.SelectedIndex = -1;
            gvLog.SelectedIndex = -1;
        }
        #endregion

        #region 模式报告改变事件
        private void gvMode_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (gvMode.SelectedIndex != -1)
            {
                gvLog.SelectedIndex = -1;
            }

            ModeDemo modeModel = this.gvMode.SelectedItem as ModeDemo;
            if (modeModel != null)
            {
                showReport = new ShowReportDetailDialog();
                showReport.Parent = this;
                ShowReportDetailView child = new ShowReportDetailView();
                child.ModeId = modeModel.ModeID;
                child.DayTime = null;
                child.Close += new EventHandler(showReportchild_Close);
                showReport.Content = child;
                showReport.Show();
            }
        }
        #endregion

        #region 疗程选择
        private void CourseListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
        #endregion

        #region 选择事件
        private void tabChoose_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int selectIndex = tabChoose.SelectedIndex;
            switch (selectIndex)
            {
                case 0:
                    gvProtrusiveOrBendFit.SelectedIndex = -1;
                    gvRotationFit.SelectedIndex = -1;
                    gvItem.SelectedIndex = -1;
                    gvLog.SelectedIndex = -1;
                    gvMode.SelectedIndex = -1;
                    BindPrescription(ModuleConstant.PatientId);
                    break;
                case 1:
                    gvProtrusiveOrBendFit.SelectedIndex = -1;
                    gvRotationFit.SelectedIndex = -1;
                    gvItem.SelectedIndex = -1;
                    CourseListBox.SelectedIndex = -1;
                    if (ModuleConstant.PatientId != null && gvItem.SelectedIndex == -1)
                    {
                        BindDayReport(ModuleConstant.PatientId);
                        BindModeReport(ModuleConstant.PatientId);
                    }
                    break;
                case 2:
                    gvProtrusiveOrBendFit.SelectedIndex = -1;
                    gvRotationFit.SelectedIndex = -1;
                    gvLog.SelectedIndex = -1;
                    gvMode.SelectedIndex = -1;
                    CourseListBox.SelectedIndex = -1;
                    if (ModuleConstant.PatientId != null && gvItem.SelectedIndex == -1)
                    {
                        gvItem.SelectedIndex = -1;
                        this.BeginTime.Text = DateTime.Now.ToString("yyyy-MM-dd");
                        this.EndTime.Text = DateTime.Now.ToString("yyyy-MM-dd");
                        BindExerciserecord(ModuleConstant.PatientId);
                    }
                    break;
                case 3:
                    gvItem.SelectedIndex = -1;
                    gvLog.SelectedIndex = -1;
                    gvMode.SelectedIndex = -1;
                    CourseListBox.SelectedIndex = -1;
                    if (ModuleConstant.PatientId != null && gvProtrusiveOrBendFit.SelectedIndex == -1 && gvRotationFit.SelectedIndex == -1)
                    {
                        BindFitResultQ(ModuleConstant.PatientId);
                        BindFitResultX(ModuleConstant.PatientId);
                    }
                    break;
                default:
                    break;
            }
        }
        #endregion

        #region 选择疗程
        private void btnSelectAction_Click(object sender, RoutedEventArgs e)
        {
            //int index=this

            Prescription Prescription = CourseListBox.SelectedItem as Prescription;
            if (Prescription != null)
            {
                var list = (from od in MySession.Query<Prescriptiondetails>()
                            where od.PrescriptionId == Prescription.Id
                            select od).OrderBy(x => x.LastTime).ToList();

                if (list.Count > 0)
                {
                    foreach (var item in list)
                    {
                        ItemDemo demo = new ItemDemo();
                        demo.Id = item.Id;
                        demo.MaxAngle = Convert.ToInt32(item.MaxAngle);
                        demo.ActionName = GetModeTypeName(item.ActionId.ToString());
                        demo.ModeName = GetModeTypeName(item.ModeId.ToString());
                        demo.MinAngle = Convert.ToInt32(item.MinAngle);
                        demo.Mode = Convert.ToInt32(item.ModeId);
                        demo.ActionId = Convert.ToInt32(item.ActionId);
                        demo.Speed = Convert.ToInt32(item.Speed);
                        demo.Times = Convert.ToInt32(item.Times);
                        demo.Force = Convert.ToInt32(item.PForce);
                        demo.Position = Convert.ToInt32(item.LastLocation);
                        if (item.PGroup.HasValue)
                        {
                            demo.GroupTimes = Convert.ToInt32(item.PGroup);
                        }
                        demo.PicturePath = "/images/prescribe.png";
                        listResult.Add(demo);
                    }
                    if (listResult.Count > 0)
                    {
                        MyActionListBox.DataContext = listResult;
                        MyActionListBox.SelectedIndex = listResult.Count - 1;
                    }
                }
            }
            else
            {
                AlarmDialog msgBox = new AlarmDialog();
                msgBox.lblMsg.Text = "请选择疗程！";
                msgBox.lblTitle.Text = "提示信息";
                msgBox.Show();
            }
        }
        #endregion

        #region 查询图表
        private void btnSelectChart_Click(object sender, RoutedEventArgs e)
        {
            if (myItem == null)
            {
            }
            else
            {
                chartNodes = new ChartNodesDialog();
                chartNodes.Parent = this;
                LumbarRobot.ChartNodes.ChartNodesControl.ChartDemo item = new LumbarRobot.ChartNodes.ChartNodesControl.ChartDemo();
                item.EndTime =Convert.ToDateTime(this.EndTime.Text).AddDays(1).ToString("yyyy-MM-dd");
                item.StartTime = this.BeginTime.Text;
                item.ExItemId = myItem.ExItemId;
                item.ExerciseItemName = myItem.ExItemName;
                ChartNodesControl child = new ChartNodesControl(item);
               
                child.Close += new EventHandler(childChartNode_Close);
                chartNodes.Content = child;
                chartNodes.Show();
            }
        }

        public void childChartNode_Close(object sender, EventArgs e)
        {
            chartNodes.Close();
        }
        #endregion

        #region 动作记录改变事件
        private void gvItem_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int index = gvItem.SelectedIndex;
            if (index != -1)
            {
                myItem = gvItem.SelectedItem as ExItemDemo;
            }
        }
        #endregion

        #region 旋转Fit改变事件
        private void gvProtrusiveOrBendFit_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (gvProtrusiveOrBendFit.SelectedIndex != -1)
            {
                gvRotationFit.SelectedIndex = -1;
            }

            FitResultQ = this.gvProtrusiveOrBendFit.SelectedItem as FitResultQ;

            if (FitResultQ != null)
            {
                fitDialog = new ShowFitChartDialog();
                fitDialog.Parent = this;
                ShowFitChartView child = new ShowFitChartView(FitResultQ.DayTime, FitResultQ.ModeId);
                child.DayTime = FitResultQ.DayTime;
                child.ModeId = FitResultQ.ModeId;
                child.FitName = EnumHelper.GetEnumItemName(Convert.ToInt32(FitResultQ.ModeId), typeof(FitModeEnum)).ToString();
                child.Close += new EventHandler(FitResultQchild_Close);
                fitDialog.Content = child;
                fitDialog.Show();

            }
        }

        public void FitResultQchild_Close(object sender, EventArgs e)
        {
            fitDialog.Close();
            gvRotationFit.SelectedIndex = -1;
            gvProtrusiveOrBendFit.SelectedIndex = -1;
           
        }
        #endregion

        #region 曲伸Fit改变事件
        private void gvRotationFit_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (gvRotationFit.SelectedIndex != -1)
            {
                gvProtrusiveOrBendFit.SelectedIndex = -1;
            }

            FitResultX = this.gvRotationFit.SelectedItem as FitResultX;

            if (FitResultX != null)
            {
                fitDialog = new ShowFitChartDialog();
                fitDialog.Parent = this;
                ShowFitChartView child = new ShowFitChartView(FitResultX.DayTime, FitResultX.ModeId);
                child.DayTime = FitResultX.DayTime;
                child.ModeId = FitResultX.ModeId;
                child.FitName = EnumHelper.GetEnumItemName(Convert.ToInt32(FitResultX.ModeId), typeof(FitModeEnum)).ToString();
                child.Close += new EventHandler(FitResultXchild_Close);
                fitDialog.Content = child;
                fitDialog.Show();
            }
        }

        public void FitResultXchild_Close(object sender, EventArgs e)
        {
            fitDialog.Close();
            gvRotationFit.SelectedIndex = -1;
            gvProtrusiveOrBendFit.SelectedIndex = -1;
        }
        #endregion

        #region 绑定旋转Fit记录
        /// <summary>
        /// 绑定旋转Fit记录
        /// </summary>
        /// <param name="patientId"></param>
        public void BindFitResultX(string patientId)
        {
            FitResultXList = new ObservableCollection<FitResultX>();
            DataTable tbl = new DataTable();
            tbl.Columns.Add("Index", typeof(string));    //行号 0
            tbl.Columns.Add("Time", typeof(string));      //训练时间  1
            tbl.Columns.Add("LeftValue", typeof(string));     // 左 2 
            tbl.Columns.Add("RightValue", typeof(string));    // 右 3 
            tbl.Columns.Add("ModeId", typeof(string));      //模式名称 4
            int count = 0;

           var listX = from od in MySession.Query<FitRecord>()
                       where od.PatientID == patientId && od.ModeID ==Convert.ToInt32(FitModeEnum.RotationFit)
                       select od;
            var temp = from a in listX
                       group a by new
                       {
                           a.PatientID,
                           a.CreateTime,
                           a.MaxAngle,
                           a.MinAngle,
                           a.ModeID
                       } into p
                       select new
                       {
                           pid = p.Key.PatientID,
                           exerciseDate = p.Key.CreateTime,
                           p.Key.MaxAngle,
                           p.Key.MinAngle,
                           p.Key.ModeID
                       };
           
            foreach (var item in temp)
            { 
                count++;
                tbl.Rows.Add(count.ToString(),Convert.ToDateTime(item.exerciseDate).ToString(),item.MaxAngle,item.MinAngle,item.ModeID);
            }
            //按照日期查询数据库
           
            if (tbl.Rows.Count > 0)
            {
                FitResultXList.Clear();
                if (tbl.Rows.Count > 0)
                {
                    for (int i = 0; i < tbl.Rows.Count; i++)
                    {
                        DataRow dr = tbl.Rows[i];
                        FitResultX item = new FitResultX();
                        item.DayIndex = dr[0].ToString();
                        item.DayTime = dr[1].ToString();
                        item.LeftValue = dr[2].ToString();
                        item.RightValue=dr[3].ToString();
                        item.ModeId = dr[4].ToString();
                        FitResultXList.Add(item);
                    }
                }
            }
            
            gvRotationFit.ItemsSource = FitResultXList;
        }
        #endregion

        #region 绑定曲伸Fit记录
        /// <summary>
        /// 绑定曲伸Fit记录
        /// </summary>
        /// <param name="patientId"></param>
        public void BindFitResultQ(string patientId)
        {
            FitResultQList = new ObservableCollection<FitResultQ>();
            DataTable tbl = new DataTable();
            tbl.Columns.Add("Index", typeof(string));        //行号 0
            tbl.Columns.Add("Time", typeof(string));         //训练时间  1
            tbl.Columns.Add("MaxtValue", typeof(string));     //最大值 2 
            tbl.Columns.Add("MinValue", typeof(string));      //最小 3 
            tbl.Columns.Add("ModeId", typeof(string));        //模式名称 4
            int count = 0;
           
            var listX = from od in MySession.Query<FitRecord>()
                        where od.PatientID == patientId && od.ModeID == Convert.ToInt32(FitModeEnum.ProtrusiveOrBendFit)
                        select od;
            var temp = from a in listX
                       group a by new
                       {
                           a.PatientID,
                           a.CreateTime,
                           a.MaxAngle,
                           a.MinAngle,
                           a.ModeID
                       } into p
                       select new
                       {
                           pid = p.Key.PatientID,
                           exerciseDate = p.Key.CreateTime,
                           p.Key.MaxAngle,
                           p.Key.MinAngle,
                           p.Key.ModeID
                       };

            foreach (var item in temp)
            {
                count++;
                tbl.Rows.Add(count.ToString(), Convert.ToDateTime(item.exerciseDate).ToString(), item.MaxAngle, item.MinAngle,item.ModeID);
            }
            //按照日期查询数据库

            if (tbl.Rows.Count > 0)
            {

                FitResultQList.Clear();
                if (tbl.Rows.Count > 0)
                {
                    for (int i = 0; i < tbl.Rows.Count; i++)
                    {
                        DataRow dr = tbl.Rows[i];
                        FitResultQ item = new FitResultQ();
                        item.DayIndex = dr[0].ToString();
                        item.DayTime = dr[1].ToString();
                        item.MaxValue = dr[2].ToString();
                        item.MinValue = dr[3].ToString();
                        item.ModeId = dr[4].ToString();
                        FitResultQList.Add(item);
                    }
                }
            }

            gvProtrusiveOrBendFit.ItemsSource = FitResultQList;
        }
        #endregion

        #region 修改选中项
        private void btnChange_Click(object sender, RoutedEventArgs e)
        {
            WPFMessageBox msgBox = new WPFMessageBox();
            msgBox.lblMsg.Text = "是否修改！";
            msgBox.lblTitle.Text = "提示信息";
            msgBox.Topmost = true;
            msgBox.ShowDialog();
            if (msgBox.IsFlag)
            {
                ComboBoxItem cbi = (ComboBoxItem)this.CboAction.SelectedItem;
                ComboBoxItem cbiMode = (ComboBoxItem)this.CboMode.SelectedItem;
                if (MyActionListBox.SelectedIndex != -1)
                {
                    if (listResult.Count > 0)
                    {
                        new Thread(() =>
                        {
                            this.Dispatcher.Invoke(new Action(() =>
                            {
                                int testIndex = MyActionListBox.SelectedIndex;
                                ItemDemo item = listResult[MyActionListBox.SelectedIndex] as ItemDemo;
                                item.ActionName = cbi.Content.ToString();
                                item.ActionId = CboAction.SelectedIndex;
                                item.Mode = CboMode.SelectedIndex;
                                item.ModeName = cbiMode.Content.ToString();
                                item.Force = setControlForce.Value;
                                item.Speed = setControlSpeed.Value;
                                item.MinAngle = setControlMinAngle.Value;
                                item.MaxAngle = setControlMaxAngle.Value;
                                item.GroupTimes = item.GroupTimes;
                                item.Times = setControlTimes.Value;
                                item.Position = setControlPosition.Value;
                                item.GroupTimes = setControlGroup.Value;
                                ItemDemo changeItem = new ItemDemo();
                                SetItem(item, changeItem);
                                listResult.RemoveAt(MyActionListBox.SelectedIndex);
                                listResult.Insert(testIndex, changeItem);
                                MyActionListBox.DataContext = listResult;
                                MyActionListBox.SelectedIndex = testIndex;

                            }));
                        }).Start();
                    }
                }
            }
        }
        /// <summary>
        /// Item赋值
        /// </summary>
        /// <param name="item">原始</param>
        /// <param name="changeItem">新的</param>
        private void SetItem(ItemDemo item, ItemDemo changeItem)
        {
            changeItem.ActionId = item.ActionId;
            changeItem.ActionName = item.ActionName;
            changeItem.Mode = CboMode.SelectedIndex;
            changeItem.Force = item.Force;
            changeItem.GroupTimes = item.GroupTimes;
            changeItem.Id = item.Id;
            changeItem.MaxAngle = item.MaxAngle;
            changeItem.MinAngle = item.MinAngle;
            changeItem.Mode = item.Mode;
            changeItem.ModeName = item.ModeName;
            changeItem.PicturePath = item.PicturePath;
            changeItem.Position = item.Position;
            changeItem.Speed = item.Speed;
            changeItem.Times = item.Times;
        }
        #endregion

        #region 绑定menu的数据
        public void BindMenu()
        {
            List<NameValueInfo> listValues = new List<NameValueInfo>();
            listValues.Clear();
            List<NameValueInfo> listValues1 = new List<NameValueInfo>();
            listValues1.Clear();
                NameValueInfo valueinfo1 = new NameValueInfo();
                valueinfo1.nValue = 1;
                valueinfo1.sName = "旋转(左)";
                valueinfo1.sValue = "1";
                listValues.Add(valueinfo1);
                NameValueInfo valueinfo2 = new NameValueInfo();
                valueinfo2.nValue = 2;
                valueinfo2.sName = "旋转(右)";
                valueinfo2.sValue = "2";
                listValues.Add(valueinfo2);

                NameValueInfo valueinfo3 = new NameValueInfo();
                valueinfo3.nValue = 3;
                valueinfo3.sName = "前屈";
                valueinfo3.sValue = "3";
                listValues.Add(valueinfo3);
                NameValueInfo valueinfo4 = new NameValueInfo();
                valueinfo4.nValue = 4;
                valueinfo4.sName = "后伸";
                valueinfo4.sValue = "4";
                listValues.Add(valueinfo4);

                NameValueInfo valueinfo5 = new NameValueInfo();
                valueinfo5.nValue = 5;
                valueinfo5.sName = "旋转(左)";
                valueinfo5.sValue = "5";
                listValues1.Add(valueinfo5);
                NameValueInfo valueinfo6 = new NameValueInfo();
                valueinfo6.nValue = 6;
                valueinfo6.sName = "旋转(右)";
                valueinfo6.sValue = "6";
                listValues1.Add(valueinfo6);

                NameValueInfo valueinfo7 = new NameValueInfo();
                valueinfo7.nValue = 7;
                valueinfo7.sName = "前屈";
                valueinfo7.sValue = "7";
                listValues1.Add(valueinfo7);
                NameValueInfo valueinfo8 = new NameValueInfo();
                valueinfo8.nValue = 8;
                valueinfo8.sName = "后伸";
                valueinfo8.sValue = "8";
                listValues1.Add(valueinfo8);
            
            subMenu.Items.Clear();
            foreach (var item in listValues)
            {
                MenuItem mi = new MenuItem();
                if (item.sName.Length > 20)
                    mi.Header = item.sName.Substring(0, 10) + "...";
                else
                    mi.Header = item.sName;
                mi.Tag = item.sValue;
                mi.Click += new System.Windows.RoutedEventHandler(MenuClick);
                mi.SetResourceReference(MenuItem.StyleProperty, "MainMenuItem");
                subMenu.Items.Add(mi);
            }

            subMenu1.Items.Clear();
            foreach (var item in listValues1)
            {
                MenuItem mi = new MenuItem();
                if (item.sName.Length > 20)
                    mi.Header = item.sName.Substring(0, 10) + "...";
                else
                    mi.Header = item.sName;
                mi.Tag = item.sValue;
                mi.Click += new System.Windows.RoutedEventHandler(MenuClick);
                mi.SetResourceReference(MenuItem.StyleProperty, "MainMenuItem");
                subMenu1.Items.Add(mi);
            }
        }

        private void MenuClick(object sender, RoutedEventArgs e)
        {
            //实现代码
            MenuItem item = (MenuItem)sender;//根据sender引用控件。
            switch (item.Tag.ToString())
            {
                case "1": //旋转(左)
                    if (ControlDialog == null)
                    {
                        ControlDialog = new ActionControlDialog();
                        ControlDialog.Parent = this;
                        FitActionControl child = new FitActionControl(listResult);
                        //child.IsShowPanel = false;
                        child.Action = EvaluateActionEnum.RotationRangeLeft;
                        child.Mode = EvaluateModeEnum.Range;
                        child.Close += new EventHandler(child_Close);
                        ControlDialog.Content = child;
                        ControlDialog.Show();
                    }
                    break;
                case "2": //旋转(右)
                    if (ControlDialog == null)
                    {
                        ControlDialog = new ActionControlDialog();
                        ControlDialog.Parent = this;
                        FitActionControl child = new FitActionControl(listResult);
                        //child.IsShowPanel = false;
                        child.Action = EvaluateActionEnum.RotationRangeRight;
                        child.Mode = EvaluateModeEnum.Range;
                        child.Close += new EventHandler(child_Close);
                        ControlDialog.Content = child;
                        ControlDialog.Show();
                    }
                    break;
                case "3": //前屈
                    if (ControlDialog == null)
                    {
                        ControlDialog = new ActionControlDialog();
                        ControlDialog.Parent = this;
                        FitActionControl child = new FitActionControl(listResult);
                        //child.IsShowPanel = false;
                        child.Action = EvaluateActionEnum.RangeProtrusive;
                        child.Mode = EvaluateModeEnum.Range;
                        child.Close += new EventHandler(child_Close);
                        ControlDialog.Content = child;
                        ControlDialog.Show();
                    }
                    break;
                case "4": //后伸
                    if (ControlDialog == null)
                    {
                        ControlDialog = new ActionControlDialog();
                        ControlDialog.Parent = this;
                        FitActionControl child = new FitActionControl(listResult);
                        //child.IsShowPanel = false;
                        child.Action = EvaluateActionEnum.RangeBend;
                        child.Mode = EvaluateModeEnum.Range;
                        child.Close += new EventHandler(child_Close);
                        ControlDialog.Content = child;
                        ControlDialog.Show();
                    }
                    break;
                case "5": //旋转(左)
                    if (ControlDialog == null)
                    {
                        ControlDialog = new ActionControlDialog();
                        ControlDialog.Parent = this;
                        FitActionControl child = new FitActionControl(listResult);
                        //child.IsShowPanel = false;
                        child.Action = EvaluateActionEnum.RotationStrengthLeft;
                        child.Mode = EvaluateModeEnum.Strength;
                        child.Close += new EventHandler(child_Close);
                        ControlDialog.Content = child;
                        ControlDialog.Show();
                    }
                    break;
                case "6": //旋转(右)
                    if (ControlDialog == null)
                    {
                        ControlDialog = new ActionControlDialog();
                        ControlDialog.Parent = this;
                        FitActionControl child = new FitActionControl(listResult);
                        //child.IsShowPanel = false;
                        child.Action = EvaluateActionEnum.RotationStrengthRigth;
                        child.Mode = EvaluateModeEnum.Strength;
                        child.Close += new EventHandler(child_Close);
                        ControlDialog.Content = child;
                        ControlDialog.Show();
                    }
                    break;
                case "7": //前屈
                    if (ControlDialog == null)
                    {
                        ControlDialog = new ActionControlDialog();
                        ControlDialog.Parent = this;
                        FitActionControl child = new FitActionControl(listResult);
                        //child.IsShowPanel = false;
                        child.Action = EvaluateActionEnum.StrengthProtrusive;
                        child.Mode = EvaluateModeEnum.Strength;
                        child.Close += new EventHandler(child_Close);
                        ControlDialog.Content = child;
                        ControlDialog.Show();
                    }
                    break;
                case "8": //后伸
                    if (ControlDialog == null)
                    {
                        ControlDialog = new ActionControlDialog();
                        ControlDialog.Parent = this;
                        FitActionControl child = new FitActionControl(listResult);
                        //child.IsShowPanel = false;
                        child.Action = EvaluateActionEnum.StrengthBend;
                        child.Mode = EvaluateModeEnum.Strength;
                        child.Close += new EventHandler(child_Close);
                        ControlDialog.Content = child;
                        ControlDialog.Show();
                    }
                    break;
               
            }
        }
        #endregion

        #region 打印
        /// <summary>
        /// 打印
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnReport_Click(object sender, RoutedEventArgs e)
        {
            ThreadPool.QueueUserWorkItem(new WaitCallback((obj) =>
         {
             lock (objLock)
             {
                 Application.Current.Dispatcher.Invoke(new Action(() =>
                 {
                     if (patientInfo == null)
                     {
                         AlarmDialog dialog = new AlarmDialog();
                         dialog.lblTitle.Text = "提示信息";
                         dialog.lblMsg.Text = "请选择病人!";
                         dialog.ShowDialog();
                     }
                     else
                     {

                         //if (ReportPrintDialog == null)
                         //{
                         //    ReportPrintDialog = new ReportPrintDialog();
                         //    ReportPrintDialog.Parent = this;
                         //    ReportPrintControl child = new ReportPrintControl();
                         //    child.Close += new EventHandler(child_Close);
                         //    ReportPrintDialog.Content = child;
                         //    ReportPrintDialog.Show();
                         //}

                         if (FitResultDialog == null)
                         {
                             FitResultDialog = new FitResultDialog();
                             FitResultDialog.Parent = this;
                             FitResultControl child = new FitResultControl();
                             child.Close += new EventHandler(child_Close);
                             FitResultDialog.Content = child;
                             FitResultDialog.Show();
                         }
                     }
                 }));
             }
         }));
        }
        #endregion

        #region 保存疗程
        private void btnSaveCourse_Click(object sender, RoutedEventArgs e)
        {
               ThreadPool.QueueUserWorkItem(new WaitCallback((obj) =>
         {
             lock (objLock)
             {
                 Application.Current.Dispatcher.Invoke(new Action(() =>
                 {
                     if (patientInfo == null)
                     {
                         AlarmDialog dialog = new AlarmDialog();
                         dialog.lblTitle.Text = "提示信息";
                         dialog.lblMsg.Text = "请选择病人!";
                         dialog.ShowDialog();
                     }
                     else
                     {
                         if (listResult.Count > 0)
                         {

                             if (SaveCourseDialog == null)
                             {
                                 SaveCourseDialog = new SaveCourseDialog();
                                 SaveCourseDialog.Parent = this;
                                 
                                 SaveCourseView child = new SaveCourseView(listResult);
                                 child.Close += new EventHandler(child_Close);
                                 SaveCourseDialog.Content = child;
                                 SaveCourseDialog.Show();
                             }
                         }
                         else
                         {
                             AlarmDialog dialog = new AlarmDialog();
                             dialog.lblTitle.Text = "提示信息";
                             dialog.lblMsg.Text = "请选择训练动作及模式!";
                             dialog.ShowDialog();
                         }
                     }
                 }));
             }
         }));
        }
        #endregion

        #region 删除处方
        private void btnDelAction_Click(object sender, RoutedEventArgs e)
        {
            Prescription Prescription = CourseListBox.SelectedItem as Prescription;
            if (Prescription != null)
            {
                WPFMessageBox IfMsgBox = new WPFMessageBox();
                IfMsgBox.lblMsg.Text = "是否删除！";
                IfMsgBox.lblTitle.Text = "提示信息";
                IfMsgBox.Topmost = true;
                IfMsgBox.ShowDialog();                                                                                                                             
                if (IfMsgBox.IsFlag)
                {
                    if (Prescription.PatientId != "" && Prescription.PatientId != null)
                    {

                        var list = (from od in MySession.Query<Prescriptiondetails>()
                                    where od.PrescriptionId == Prescription.Id
                                    select od).OrderBy(x => x.LastTime).ToList();

                        if (list.Count > 0)
                        {
                            foreach (var item in list)
                            {
                                MySession.Session.Delete(item);
                                MySession.Session.Flush();
                            }
                        }

                        MySession.Session.Delete(Prescription);
                        MySession.Session.Flush();

                        tabChoose.SelectedIndex = 0;
                        BindPrescription(ModuleConstant.PatientId);
                    }
                    else
                    {
                        AlarmDialog msgBox = new AlarmDialog();
                        msgBox.lblMsg.Text = "内置疗程，不能删除！";
                        msgBox.lblTitle.Text = "提示信息";
                        msgBox.Show();
                    }
                }
                else
                {
                    AlarmDialog msgBox = new AlarmDialog();
                    msgBox.lblMsg.Text = "请选择疗程！";
                    msgBox.lblTitle.Text = "提示信息";
                    msgBox.Show();
                }
            }
               

        }
        #endregion

    }
}



