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
using LumbarRobot.Common;
using System.Data;
using Remotion.Linq.Collections;
using LumbarRobot.Data;
using LumbarRobot.DAL;
using LumbarRobot.Enums;
using LumbarRobot.ViewModels;
using LumbarRobot.MyUserControl;
using Microsoft.Win32;
using System.IO;
using LumbarRobot.Common.Enums;

namespace LumbarRobot
{
    /// <summary>
    /// ShowReportDetailView.xaml 的交互逻辑
    /// </summary>
    public partial class ShowReportDetailView : UserControl
    {
        #region 事件
        /// <summary>
        /// 关闭事件
        /// </summary>
        public event EventHandler Close;
        #endregion
         
        #region 变量
        /// <summary>
        /// 查询的日期
        /// </summary>
        public string DayTime;
        /// <summary>
        /// 模式Id
        /// </summary>
        public string ModeId;

        ObservableCollection<ReportDetail> dayList = new ObservableCollection<ReportDetail>();
        /// <summary>
        /// 训练结果
        /// </summary>
        private TrainDialog trainRecordDialog;     
        #endregion

        #region 构造
        public ShowReportDetailView()
        {
            InitializeComponent();
        }
        #endregion

        #region 关闭事件
        private void btnClosed_Click(object sender, RoutedEventArgs e)
        {
            if (Close != null)
                Close(sender,e);
        }
        #endregion

        #region ReportDetail 赋值
        private static ReportDetail SetReportDetail(DataRow dr)
        {
            ReportDetail item = new ReportDetail();
            
            item.Time = dr[0].ToString();
            item.TotalTime = dr[1].ToString();
            item.SeeionId = dr[2].ToString();
            item.PatientId = dr[3].ToString();
            item.ActionId = dr[4].ToString();
            item.ModeId = dr[5].ToString();
            item.PushRodValue = dr[6].ToString();
            item.IsFit = dr[7].ToString();
            item.ExerciseDate = dr[8].ToString();
            item.StartTime = dr[9].ToString();
            item.EndTime = dr[10].ToString();
            item.Speed = dr[11].ToString();
            item.RobotForce = dr[12].ToString();
            item.MinAngle = dr[13].ToString();
            item.MaxAngle = dr[14].ToString();
            item.Times = dr[15].ToString();
            item.FactTimes = dr[16].ToString();
            item.Maxforce = dr[17].ToString();
            item.ModeName = dr[18].ToString();
            item.ActionName = dr[19].ToString();
            item.ExItemId = dr[20].ToString();
            return item;
        }
        #endregion

        #region 加载事件
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {

            DataTable tbl = new DataTable();
            if (DayTime != null)
            {
                tbl = ExerciseRecordGroupByDateDetail(DayTime);
            }
            else
            {
                tbl = ExerciseRecordGroupByModeDetail(ModeId);
            }
            if (tbl.Rows.Count > 0)
            {
                dayList.Clear();
                if (tbl.Rows.Count > 0)
                {
                    for (int i = 0; i < tbl.Rows.Count; i++)
                    {
                        DataRow dr = tbl.Rows[i];
                        ReportDetail item=SetReportDetail(dr);
                        dayList.Add(item);
                    }
                }

                gvDetail.ItemsSource = dayList;
            }
        }
        #endregion

        #region 绑定数据
        #region 按照日期
        public DataTable ExerciseRecordGroupByDateDetail(string dateTime)
        {
            DataTable tbl = SetDataTable();

            var list = from od in MySession.Query<Exerciserecord>()
                       where od.PatientId == ModuleConstant.PatientId && od.ExerciseDate ==Convert.ToDateTime(dateTime)
                       select od;
            SetDataSouse(tbl, list);

            return tbl;
        }

        #endregion

        #region 按照模式
        public DataTable ExerciseRecordGroupByModeDetail(string ModeId)
        {
            DataTable tbl = SetDataTable();

            var list = from od in MySession.Query<Exerciserecord>()
                       where od.PatientId == ModuleConstant.PatientId && od.ModeId == Convert.ToInt32(ModeId)
                       select od;
            SetDataSouse(tbl, list);
            return tbl;
        }
        #endregion

        #region 设置数据源
        private static void SetDataSouse(DataTable tbl, IQueryable<Exerciserecord> list)
        {
            var temp = (from a in list
                        group a by new
                        {
                            a.SeeionId,
                            a.PatientId,
                            a.ActionId,
                            a.ModeId,
                            a.PushRodValue,
                            a.IsFit,
                            a.ExerciseDate,
                            a.StartTime,
                            a.EndTime,
                            a.Speed,
                            a.RobotForce,
                            a.MinAngle,
                            a.MaxAngle,
                            a.Times,
                            a.FactTimes,
                            a.ExMinutes,
                            a.Maxforce,
                            a.Id

                        } into p
                        select new
                        {
                            time = p.Key.ExerciseDate,
                            totalTime = p.Sum(f => f.ExMinutes),
                            p.Key.SeeionId,
                            p.Key.PatientId,
                            p.Key.ActionId,
                            p.Key.ModeId,
                            p.Key.PushRodValue,
                            p.Key.IsFit,
                            p.Key.StartTime,
                            p.Key.EndTime,
                            p.Key.Speed,
                            p.Key.RobotForce,
                            p.Key.MinAngle,
                            p.Key.MaxAngle,
                            p.Key.Times,
                            p.Key.FactTimes,
                            p.Key.ExMinutes,
                            p.Key.Maxforce,
                            p.Key.ExerciseDate,
                            p.Key.Id


                        }).ToList().OrderByDescending(x => x.StartTime);

            foreach (var item in temp)
            {
                string actionName = GetActionName(item.ActionId.ToString());
                string result = GetModeTypeName(item.ModeId.ToString());
                string totalTime = SetTime(item.totalTime.ToString());
                tbl.Rows.Add(Convert.ToDateTime(item.time).ToString("yyyy/MM/dd"), totalTime, item.SeeionId, item.PatientId, item.ActionId.ToString(), item.ModeId.ToString(), item.PushRodValue.ToString(), item.IsFit.ToString() == "1" ? "是" : "否", item.ExerciseDate, Convert.ToDateTime(item.StartTime), Convert.ToDateTime(item.EndTime), item.Speed.ToString(), item.RobotForce.ToString(), item.MinAngle.ToString(), item.MaxAngle.ToString(), item.Times.ToString(), item.FactTimes.ToString(), item.Maxforce.HasValue ? item.Maxforce.ToString() : "0", result, actionName,item.Id);
            }
        }
        #endregion

        #region 设置数据源
        /// <summary>
        /// 设置数据源
        /// </summary>
        /// <returns></returns>
        private static DataTable SetDataTable()
        {
            DataTable tbl = new DataTable();

            tbl.Columns.Add("Time", typeof(DateTime));     //训练时间  0
            tbl.Columns.Add("TotalTime", typeof(string));    //训练时长 1
            tbl.Columns.Add("SeeionId", typeof(string));   // 分组 2
            tbl.Columns.Add("PatientId", typeof(string));    // 病人ＩＤ3
            tbl.Columns.Add("ActionId", typeof(string));     // 动作ID  4
            tbl.Columns.Add("ModeId", typeof(string));       // 模式ID   5
            tbl.Columns.Add("PushRodValue", typeof(string));  //推杆高度  6
            tbl.Columns.Add("IsFit", typeof(string));        //项目名称  7
            tbl.Columns.Add("ExerciseDate", typeof(string));  //模式     8
            tbl.Columns.Add("StartTime", typeof(string));    //模式     9
            tbl.Columns.Add("EndTime", typeof(string));      //是否Fit 10
            tbl.Columns.Add("Speed", typeof(string));        //应到点数 11
            tbl.Columns.Add("RobotForce", typeof(string));    //力量   12
            tbl.Columns.Add("MinAngle", typeof(string));      //最小角度  13
            tbl.Columns.Add("MaxAngle", typeof(string));      //最大角度   14
            tbl.Columns.Add("Times", typeof(string));         //应训练次数 15
            tbl.Columns.Add("FactTimes", typeof(string));      //实训次数  16
            tbl.Columns.Add("Maxforce", typeof(string));       // 最大力量  17
            tbl.Columns.Add("ModeName", typeof(string));       //模式名称  18
            tbl.Columns.Add("ActionName", typeof(string));      //动作名称  19
            tbl.Columns.Add("ExItemId", typeof(string));      //动作记录ID 20
            return tbl;
        }
        #endregion
        #endregion

        #region 时间转换
        private static string SetTime(string date)
        {
            double h = 0, m = 0, s = 0;
            if (date == null || date == "")
                date = "0";
            double time = Convert.ToDouble(date);
            h = (int)Math.Floor(time / 3600);
            if (h > 0)
            {
                m = (int)Math.Floor((time - h * 3600) / 60);
                if (m > 0)
                {
                    s = (int)Math.Floor(time - h * 3600 - m * 60);
                }
                else
                {
                    s = (int)Math.Floor(time - h * 3600);
                }
            }
            else
            {
                m = (int)Math.Floor(time / 60);
                if (m > 0)
                {
                    s = (int)Math.Floor(time - 60 * m);
                }
                else
                {
                    s = (int)Math.Floor(time);
                }
            }
            return h + "时" + m + "分" + s + "秒"; ;
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
        private static string GetActionName(string type)
        {
            return EnumHelper.GetEnumItemName(Convert.ToInt32(type), typeof(ActionEnum));
        }
        #endregion

        #region 选中事件
        private void gvDetail_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            ReportDetail record = gvDetail.SelectedItem as ReportDetail;
            Exerciserecord item = new Exerciserecord();
            item.Id = record.ExItemId;
            trainRecordDialog = new TrainDialog();
            trainRecordDialog.Parent = this;
            TrainRecordDialog child = new TrainRecordDialog();
            child.Close += new EventHandler(child_Close);
            child.Record = item;
            trainRecordDialog.Content = child;
            trainRecordDialog.Show();
        }

        void child_Close(object sender, EventArgs e)
        {
            trainRecordDialog.Close(); ;
        }
        #endregion

        #region 转换
        private DataTable GetInfo(ObservableCollection<ReportDetail> list)
        {
            DataTable tbl = new DataTable();

            tbl.Columns.Add("序号", typeof(string));  //序号 1
            tbl.Columns.Add("治疗名称", typeof(string));  //项目名称 2
            tbl.Columns.Add("模式", typeof(string));  //开始时间  3
            tbl.Columns.Add("推杆高度", typeof(string));  //训练时间 4
            tbl.Columns.Add("力量", typeof(string));      //模式 5
            tbl.Columns.Add("最大力量", typeof(string));  //训练时长 6
            tbl.Columns.Add("速度", typeof(string));   //是否Fit 7
            tbl.Columns.Add("最大角度", typeof(string));      //速度 8
            tbl.Columns.Add("最小角度", typeof(string));      //力量 9
            tbl.Columns.Add("应训次数", typeof(string));      //速度 10
            tbl.Columns.Add("实训次数", typeof(string));      //力量 11
            tbl.Columns.Add("开始时间", typeof(string));      //速度 12
            tbl.Columns.Add("训练时长", typeof(string));      //力量 13
            int index = 0;
            foreach (ReportDetail item in list)
            {
                index++;
                tbl.Rows.Add(index.ToString(),item.ActionName, item.ModeName, item.PushRodValue, item.RobotForce, item.Speed, item.MaxAngle, item.MinAngle, item.Times,item.Times, item.FactTimes, item.StartTime, item.TotalTime);
            }

            #region 插入病人信息 
          
            //当前患者
            Syspatient patient = MySession.Session.Get<Syspatient>(ModuleConstant.PatientId);
            string PatientName = patient.UserName; //患者
            string PatientSex = patient.Sex == "0" ? "男" : "女";//性别
            string PatientCarNo = patient.PatientCarNo;//病历号
            string Weight = "";
            if (patient.Weight.HasValue)
            {
                Weight = patient.Weight.ToString();
            }
            string BodyHeight = "";
            if (patient.BodyHeight.HasValue)
            {
                BodyHeight = patient.BodyHeight.ToString();
            }
            
            string PatientBirthday = Convert.ToDateTime(patient.BirthDay.ToString()).ToString("yyyy/MM/dd");//生日
            tbl.Columns.Add("病人信息", typeof(string));
            tbl.Columns["病人信息"].SetOrdinal(0);

            if (list.Count < 9)
            {
                for (int i = 0; i < 9 - list.Count; i++)
                {
                    DataRow dr = tbl.NewRow();
                    tbl.Rows.Add(dr);
                }
            }
            tbl.Rows[0]["病人信息"] = "患者：" + PatientName;
            tbl.Rows[1]["病人信息"] = "性别：" + PatientSex;
            tbl.Rows[2]["病人信息"] = "出生日期：" + PatientBirthday;
            tbl.Rows[3]["病人信息"] = "病历号：" + PatientCarNo;
            tbl.Rows[4]["病人信息"] = "身高：" + BodyHeight;
            tbl.Rows[5]["病人信息"] = "体重：" + Weight;
            tbl.Rows[6]["病人信息"] = "病历号：" + PatientCarNo;
            tbl.Rows[7]["病人信息"] = "初步诊断：" + EnumHelper.GetEnumItemName(Convert.ToInt32(patient.DiagnoseTypeId),typeof( DiagnoseTypeEnum));
            tbl.Rows[8]["病人信息"] = "备注：" + patient.Note;

            #endregion
            return tbl;
        }
        #endregion

        #region 导出Excel方法
        /// <summary>
        /// 导出Excel
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnExportExcel_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog s = new SaveFileDialog();
            s.Filter = "Excel|*.xls";
            s.RestoreDirectory = true;
            string str = DateTime.Now.ToString("yyyyMMdd") + DateTime.Now.Second.ToString();
            s.FileName = ModuleConstant.PatientName + "_" + str + "_报告";
            Machine.OpenTabtip();
            s.ShowDialog();

            if (s.FileName != "")
            {
                ExcelHelper _excelHelper = new ExcelHelper();
                DataTable dt = GetInfo(dayList);
                //_excelHelper.SaveToExcel(s.FileName, dt);
                _excelHelper.dataTableToCsv(dt, s.FileName);

                IntPtr hwnd = WindowsAPI.FindWindow("IPTip_Main_Window", null);
                WindowsAPI.SendMessage(hwnd, WindowsAPI.WM_SYSCOMMAND, WindowsAPI.SC_CLOSE, 0);
            }
            else
            {

            }

        }
        #endregion
    }
}
