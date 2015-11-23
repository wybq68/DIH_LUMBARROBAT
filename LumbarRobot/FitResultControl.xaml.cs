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
using LumbarRobot.ViewModels;
using Remotion.Linq.Collections;
using LumbarRobot.DAL;
using System.Data;
using LumbarRobot.Common;
using LumbarRobot.Data;
using LumbarRobot.Enums;
using LumbarRobot.Common.Enums;
using LumbarRobot.Commands;
using Visifire.Charts;
using LumbarRobot.MyUserControl;
using Microsoft.Win32;
using System.IO;
using System.Data.Objects.SqlClient;
using System.Data.Objects;  //在 System.Data.Entity.dll 中


namespace LumbarRobot
{
    /// <summary>
    /// FitResultDialog.xaml 的交互逻辑
    /// </summary>
    public partial class FitResultControl : UserControl
    {

        #region 变量
        /// <summary>
        /// 导出报告集合
        /// </summary>
        public ObservableCollection<ReportItem> ReportItemList = null;

        public event EventHandler Close;
        /// <summary>
        /// 测试报告
        /// </summary>
        private ReportPrintDialog ReportPrintDialog;
        /// <summary>
        /// 日期集合
        /// </summary>
        public ObservableCollection<FitDemo> FitDemoList = null;
        public List<FitDemo> TestList = new List<FitDemo>();
        public ObservableCollection<FitResultDemo> FitResultList = null;
        /// <summary>
        /// 保存疗程
        /// </summary>
        private PrescriptionDialog prescriptionDialog;

        #endregion

        #region 构造
        public FitResultControl()
        {
            InitializeComponent();
            BindDayReport(ModuleConstant.PatientId,true);
        }
        #endregion

        #region 绑定日期训练报告
        public void BindDayReport(string patientId,bool IsCheck)
        {
            if (gvInfo != null && gvFit != null)
            {

                gvInfo.SelectedIndex = -1;
                gvFit.SelectedIndex = -1;
            }

            FitDemoList = new ObservableCollection<FitDemo>();
            DataTable tbl = new DataTable();
            tbl.Columns.Add("DayIndex", typeof(string));    //行号 0
            tbl.Columns.Add("DayTime", typeof(string)); //训练时间  1

            int a = 0;
            var list = from od in MySession.Query<EvaluteDetail>()
                       where od.PatientID == patientId
                       select od;
            var obj = (from p in list
                       group p by new
                       {
                           p.PatientID,
                           p.EvaluteDate

                       } into g
                       select new
                       {
                           patientId = g.Key.PatientID,
                           exerciseDate = g.Key.EvaluteDate


                       }).ToList().OrderByDescending(x => x.exerciseDate);
            foreach (var item in obj)
            {
                a++;
                //格式化时间
                tbl.Rows.Add(a.ToString(), Convert.ToDateTime(item.exerciseDate).ToString("yyyy-MM-dd"));
            }
            if (tbl.Rows.Count > 0)
            {
                FitDemoList.Clear();
                if (tbl.Rows.Count > 0)
                {
                    for (int i = 0; i < tbl.Rows.Count; i++)
                    {
                        DataRow dr = tbl.Rows[i];
                        FitDemo item = new FitDemo();
                        item.IsChecked = IsCheck;
                        item.DayIndex = dr[0].ToString();
                        item.DayTime = dr[1].ToString();
                        FitDemoList.Add(item);
                    }
                }
            }
            gvFit.ItemsSource = FitDemoList;
            TestList.Clear();
            if (IsCheck)
            {
                TestList = FitDemoList.ToList();
            }
            else
            {
                TestList = new List<FitDemo>();
            }
        }
        #endregion

        #region 返回
        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close(sender, e);
        }
        #endregion

        #region 改变事件
        private void gvFit_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataBind();

            CreateImage();
            
        }

        private void CreateImage()
        {
            if (cbMode.SelectedItem == null || ((ComboBoxItem)cbMode.SelectedItem).Tag.ToString() == "0")
            {
                spImage.Children.Clear();
            }
            else
            {
                int currentActionId = Convert.ToInt32(((ComboBoxItem)cbMode.SelectedItem).Tag);

                //折线图
                Chart chart1 = new Chart();
                chart1.DataPointWidth = 2;
                chart1.Width = 650;
                chart1.Height = 200;
                Axis xaxis1 = new Axis();
                AxisLabels xal1 = new AxisLabels
                {

                    Enabled = true,

                    Angle = -45
                };
                xaxis1.AxisLabels = xal1;
                chart1.AxesX.Add(xaxis1);
                SetTitle(chart1, EnumHelper.GetEnumItemName(currentActionId, typeof(EvaluateActionEnum)));
                chart1.Rendered += new EventHandler(c_Rendered);


                //绑定数据
                var allData = (from od in MySession.Query<EvaluteDetail>()
                               where od.PatientID == ModuleConstant.PatientId && od.ActionId == currentActionId
                               select od).ToList();

                allData = (from od in allData
                           where (from o in TestList select o.DayTime).Contains(GetDate(od.EvaluteDetailDate))
                           select od).ToList();

                //SetDataPoints(chart1, allData, EvaluateActionEnum.RangeProtrusive, "前曲范围");
                //SetDataPoints(chart1, allData, EvaluateActionEnum.RangeBend, "后伸范围");
                //SetDataPoints(chart1, allData, EvaluateActionEnum.RotationRangeLeft, "旋转左范围");
                //SetDataPoints(chart1, allData, EvaluateActionEnum.RotationRangeRight, "旋转右范围");
                SetDataPoints(chart1, allData, (EvaluateActionEnum)currentActionId, EnumHelper.GetEnumItemName(currentActionId, typeof(EvaluateActionEnum)));

                spImage.Children.Clear();
                spImage.Children.Add(chart1);
            }
        }

        private static void SetDataPoints(Chart chart1, List<EvaluteDetail> allData,EvaluateActionEnum action,string name)
        {
            var data = (from ad in allData
                       where ad.ActionId == (int)action
                        select ad).OrderBy(x => x.EvaluteDetailDate).ToList();

            DataSeries ds = new DataSeries();
            ds.RenderAs = RenderAs.Line;
            SolidColorBrush brush = new SolidColorBrush(Colors.Red);
            ds.Color = brush;
            ds.Name = name;
            foreach (var item in data)
            {
                DataPoint dp = new DataPoint();
                dp.YValue = Convert.ToDouble(item.MaxV);
                dp.AxisXLabel = item.EvaluteDetailDate.ToString("yyyy-MM-dd");
                ds.DataPoints.Add(dp);
            }
            chart1.Series.Add(ds);
        }

        private void cbAvg_Checked(object sender, RoutedEventArgs e)
        {
            DataBind();
        }

        private void cbAvg_Unchecked(object sender, RoutedEventArgs e)
        {
            DataBind();
        }

        private List<EvaluteDetail> DataBind()
        {
            if (gvFit.SelectedIndex != -1)
            {
                FitResultList = new ObservableCollection<FitResultDemo>();
                FitDemo FitDemo = this.gvFit.SelectedItem as FitDemo;

                List<EvaluteDetail> list;
                List<EvaluteDetail> listTemp = null ;


                if (cbAvg.IsChecked.HasValue && cbAvg.IsChecked.Value)
                {
                    
                    if (cbMode.SelectedItem == null || ((ComboBoxItem)cbMode.SelectedItem).Tag.ToString() == "0")
                    {
                        listTemp = (from od in MySession.Query<EvaluteDetail>()
                                    where od.PatientID == ModuleConstant.PatientId && od.EvaluteDate == Convert.ToDateTime(FitDemo.DayTime)
                                    select od).OrderByDescending(x => x.EvaluteDetailDate).ToList();
                    }
                    else
                    {
                        listTemp = (from od in MySession.Query<EvaluteDetail>()
                                    where od.PatientID == ModuleConstant.PatientId && od.EvaluteDate == Convert.ToDateTime(FitDemo.DayTime)
                                     && od.ActionId == Convert.ToInt32(((ComboBoxItem)cbMode.SelectedItem).Tag)
                                    select od).OrderByDescending(x => x.EvaluteDetailDate).ToList();
                    }

                    //计算平均值
                    Dictionary<int, float> temp = new Dictionary<int, float>();
                    Dictionary<int, float> templast = new Dictionary<int, float>();
                    Dictionary<int, float> tempFatigueIndex = new Dictionary<int, float>();
                    for (int i = 0; i < listTemp.Count(); i++)
                    {
                        if (temp.ContainsKey(listTemp[i].ActionId))
                        {
                            temp[listTemp[i].ActionId] = (temp[listTemp[i].ActionId] + listTemp[i].MaxV) / 2;
                            templast[listTemp[i].ActionId] = (templast[listTemp[i].ActionId] + listTemp[i].LastValue) / 2;
                            tempFatigueIndex[listTemp[i].ActionId] = (tempFatigueIndex[listTemp[i].ActionId] + listTemp[i].FatigueIndex) / 2;
                        }
                        else
                        {
                            temp[listTemp[i].ActionId] = listTemp[i].MaxV;
                            templast[listTemp[i].ActionId] = listTemp[i].LastValue;
                            tempFatigueIndex[listTemp[i].ActionId] = listTemp[i].FatigueIndex;
                        }
                    }

                    //赋值
                    list = new List<EvaluteDetail>();
                    foreach (var item in temp)
                    {
                        EvaluteDetail ed = new EvaluteDetail();
                        ed.ActionId = item.Key;
                        ed.MaxV = item.Value;
                        ed.LastValue = templast[item.Key];
                        ed.FatigueIndex = tempFatigueIndex[item.Key];
                        ed.EvaluteDetailDate = DateTime.MinValue;
                        list.Add(ed);
                    }
                }
                else
                {
                    if (cbMode.SelectedItem == null || ((ComboBoxItem)cbMode.SelectedItem).Tag.ToString() == "0")
                    {
                        list = (from od in MySession.Query<EvaluteDetail>()
                                where od.PatientID == ModuleConstant.PatientId && od.EvaluteDate == Convert.ToDateTime(FitDemo.DayTime)
                                select od).OrderByDescending(x => x.EvaluteDetailDate).ToList();
                    }
                    else
                    {
                        list = (from od in MySession.Query<EvaluteDetail>()
                                where od.PatientID == ModuleConstant.PatientId && od.EvaluteDate == Convert.ToDateTime(FitDemo.DayTime)
                                 && od.ActionId == Convert.ToInt32(((ComboBoxItem)cbMode.SelectedItem).Tag)
                                select od).OrderByDescending(x => x.EvaluteDetailDate).ToList();
                    }
                }

                DataTable tbl = new DataTable();
                tbl.Columns.Add("EvaluteDetailId", typeof(string));
                tbl.Columns.Add("ActionName", typeof(string));
                tbl.Columns.Add("MaxV", typeof(string));
                tbl.Columns.Add("LastValue", typeof(string));
                tbl.Columns.Add("FatigueIndex", typeof(string));
                tbl.Columns.Add("EvaluteDetailDate", typeof(string));

                foreach (var item in list)
                {
                    tbl.Rows.Add(item.EvaluteDetailId.ToString(), EnumHelper.GetEnumItemName(item.ActionId, typeof(EvaluateActionEnum)), item.MaxV, item.LastValue, item.FatigueIndex, item.EvaluteDetailDate == DateTime.MinValue ? "" : item.EvaluteDetailDate.ToString("yyyy-MM-dd HH:mm:ss"));
                }

                if (tbl.Rows.Count > 0)
                {
                    FitDemoList.Clear();
                    if (tbl.Rows.Count > 0)
                    {
                        for (int i = 0; i < tbl.Rows.Count; i++)
                        {
                            DataRow dr = tbl.Rows[i];
                            FitResultDemo item = new FitResultDemo();
                            item.EvaluteDetailId = dr[0].ToString();
                            item.ActionName = dr[1].ToString();
                            item.MaxV = dr[2].ToString();
                            item.LastValue = dr[3].ToString();
                            item.FatigueIndex = dr[4].ToString();
                            item.EvaluteDetailDate = dr[5].ToString();

                            FitResultList.Add(item);
                        }
                    }
                }

                gvInfo.ItemsSource = FitResultList;

                return listTemp;

            }
            else
            {
                gvInfo.ItemsSource = null;
                return null;
            }
        }
        #endregion

        #region 关闭事件

        public void child_Close(object sender, EventArgs e)
        {
            if (ReportPrintDialog != null)
            {
                ReportPrintDialog.Close();
            }
            if (prescriptionDialog != null)
            {
                prescriptionDialog.Close();
            }
            prescriptionDialog = null;
            ReportPrintDialog = null;
        }
        #endregion

        #region 打印预览

        bool isCanClick = true;

        private void btnPrint_Click(object sender, RoutedEventArgs e)
        {
            FitDemo FitDemo = this.gvFit.SelectedItem as FitDemo;
            //if (FitDemo != null)
            //{
            //    lock (this)
            //    {
            //        if (!isCanClick) return;
            //        this.SetVidicon.Visibility = Visibility.Visible;
            //        //Loading.Visibility = Visibility.Visible;
            //        isCanClick = false;

            //        System.Threading.ThreadPool.QueueUserWorkItem((x) =>
            //        {
            ModuleConstant.FitDate = Convert.ToDateTime(FitDemo.DayTime);
            ReportPrintDialog = new ReportPrintDialog();
            ReportPrintDialog.Parent = this;
            ReportPrintControl child = new ReportPrintControl();
            child.Close += new EventHandler(child_Close);
            ReportPrintDialog.Content = child;
            ReportPrintDialog.Show();
            //            System.Threading.Thread.Sleep(2000);
            //
            //            Application.Current.Dispatcher.BeginInvoke(new Action(() =>
            //            {
            //                Loading.Visibility = Visibility.Collapsed;
            //                this.SetVidicon.Visibility = Visibility.Collapsed;
            //                isCanClick = true;
            //            }));
            //        });

            //    }
            //}
        }
        #endregion

        #region 删除
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            WPFMessageBox msgBox = new WPFMessageBox();
            msgBox.lblMsg.Text = "是否删除！";
            msgBox.lblTitle.Text = "提示信息";
            msgBox.ShowDialog();
            if (msgBox.IsFlag)
            {
                Button btn = sender as Button;
                EvaluteDetail item = MySession.Session.Get<EvaluteDetail>(Convert.ToInt32(btn.Tag));
                MySession.Session.Delete(item);
                MySession.Session.Flush();
                BindDayReport(ModuleConstant.PatientId,true);
            }
        }
        #endregion

        #region 转换
        private DataTable GetInfo(ObservableCollection<FitResultDemo> list)
        {
            DataTable tbl = new DataTable();
            tbl.Columns.Add("序号", typeof(string));  //序号 1
            tbl.Columns.Add("模式", typeof(string));  //模式 2
            tbl.Columns.Add("爆发力", typeof(string));  //爆发力 3
            tbl.Columns.Add("耐力", typeof(string));  //耐力  4
            tbl.Columns.Add("疲劳指数", typeof(string));  //疲劳指数 5
            tbl.Columns.Add("测试时间", typeof(DateTime));      //训练时间 6
             int index = 0;
             foreach (FitResultDemo item in list)
             {
                 index++;
                 tbl.Rows.Add(index.ToString(),  item.MaxV, item.LastValue,  item.FatigueIndex, Convert.ToDateTime(item.EvaluteDetailDate));
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
            tbl.Rows[7]["病人信息"] = "初步诊断：" + EnumHelper.GetEnumItemName(Convert.ToInt32(patient.DiagnoseTypeId), typeof(DiagnoseTypeEnum));
            tbl.Rows[8]["病人信息"] = "备注：" + patient.Note;

            #endregion
            return tbl;
        }
        #endregion

        #region  导出Excel
        private void btnExcel_Click(object sender, RoutedEventArgs e)
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
                //DataTable dt = GetInfo(FitResultList);
                ExportReportByExcel();
                DataTable dt = GetInfoNew(ReportItemList);
                _excelHelper.SaveToExcel(s.FileName, dt);
               // _excelHelper.dataTableToCsv(dt, s.FileName);
                IntPtr hwnd = WindowsAPI.FindWindow("IPTip_Main_Window", null);
                WindowsAPI.SendMessage(hwnd, WindowsAPI.WM_SYSCOMMAND, WindowsAPI.SC_CLOSE, 0);
            }
        }
        #endregion

        #region 保存处方
        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            ObservableCollection<PrescriptionItem> list= SetPrescriptionItems();
            if (list != null && list.Count > 0)
            {
                prescriptionDialog = new PrescriptionDialog();
                prescriptionDialog.Parent = this;

                SavePrescriptionView child = new SavePrescriptionView(list);
                child.Close += new EventHandler(child_Close);
                prescriptionDialog.Content = child;
                prescriptionDialog.Show();
            }
            else
            {
                AlarmDialog msgBox = new AlarmDialog();
                msgBox.lblMsg.Text = "暂无可保存的处方信息！";
                msgBox.lblTitle.Text = "提示信息";
                msgBox.Show();
            }
        }
        #endregion

        #region 保存类
        public ObservableCollection<PrescriptionItem> SetPrescriptionItems()
        {
            ObservableCollection<PrescriptionItem> list = new ObservableCollection<PrescriptionItem>();

            //for (int i = 0; i < 3; i++)
            //{
            //    PrescriptionItem detail = new PrescriptionItem();
            //    detail.Id = Guid.NewGuid().ToString("N");
            //    detail.ActionId = 1;
            //    detail.ActionName = "旋转";
            //    detail.ModeId = 1;
            //    detail.ModeName = "被动运动模式";
            //    detail.PForce = 2;
            //    detail.Speed = 4;
            //    detail.MinAngle = -1;
            //    detail.MaxAngle = 12;
            //    detail.PGroup = 1;
            //    detail.Times =4;
            //    detail.LastLocation = 0;
            //    list.Add(detail);
            //}

            if (FitResultList != null)
            {
            }

            return list;
        }
        #endregion

        #region Title赋值
        /// <summary>
        /// Title赋值
        /// </summary>
        /// <param name="c"></param>
        /// <param name="titleMsg"></param>
        private static void SetTitle(Chart c, string titleMsg)
        {
            // Create a new instance of Title
            Title title = new Title();

            // Set title property
            title.Text = titleMsg;

            // Add title to Titles collection
            c.Titles.Add(title);
        }
        #endregion

        #region  隐藏图表控件广告
        void c_Rendered(object sender, EventArgs e)
        {
            var legend = (sender as Chart).Legends[0];
            var root = legend.Parent as Grid;
            if (root != null)
            {
                root.Children.RemoveAt((sender as Chart).Series.Count + 8);
                root.Children.RemoveAt((sender as Chart).Series.Count + 8);
                (sender as Chart).Legends[0].HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                (sender as Chart).Legends[0].VerticalAlignment = System.Windows.VerticalAlignment.Center;
            }
        }
        #endregion

        #region 报告导出 2015-9-18 艾东
        /// <summary>
        /// 前屈
        /// </summary>
        List<ReportItem> List1 = new List<ReportItem>();
        /// <summary>
        /// 后伸
        /// </summary>
        List<ReportItem> List2 = new List<ReportItem>();
        /// <summary>
        /// 旋转左
        /// </summary>
        List<ReportItem> List3 = new List<ReportItem>();
        /// <summary>
        /// 旋转右
        /// </summary>
        List<ReportItem> List4 = new List<ReportItem>();
        /// <summary>
        /// ROM
        /// </summary>
        List<ReportItem> List5 = new List<ReportItem>();

        
        public void ExportReportByExcel()
        {
            #region 清空数据
            List1.Clear();
            List2.Clear();
            List3.Clear();
            List4.Clear();
            List5.Clear();
            ReportItemList = new ObservableCollection<ReportItem>();
            #endregion

            #region 创建数据
            
            //创建导出的数据表
            ReportItem Item1 = new ReportItem();
            ReportItem Item2 = new ReportItem();
            ReportItem Item3 = new ReportItem();
            ReportItem Item4 = new ReportItem();
            ReportItem Item5 = new ReportItem();
            ReportItem ItemEmpty = new ReportItem();
            Item1.ItemID = "285e677586d64cb9a0411e782112c002";
            Item1.Explosiveforce = "前屈爆发力";
            Item1.Endurance = "前屈耐力";
            Item1.Fatigueindex = "前屈疲劳指数";
            Item1.FitTime = "测试时间";
            Item2.ItemID = "61e5836252974446b8887dfcb30938cc";
            Item2.Explosiveforce = "后伸爆发力";
            Item2.Endurance = "后伸耐力";
            Item2.Fatigueindex = "后伸疲劳指数";
            Item2.FitTime = "测试时间";
            Item3.ItemID = "0362642cd1f5440aa5ff82c782bc25e0";
            Item3.Explosiveforce = "旋转（左）爆发力";
            Item3.Endurance = "旋转（左）耐力";
            Item3.Fatigueindex = "旋转（左）疲劳指数";
            Item3.FitTime = "测试时间";
            Item4.ItemID = "1022d9a5676049aeb9cfffd1af581512";
            Item4.Explosiveforce = "旋转（右）爆发力";
            Item4.Endurance = "旋转（右）耐力";
            Item4.Fatigueindex = "旋转（右）疲劳指数";
            Item4.FitTime = "测试时间";
            Item5.ItemID = "397ff4895dbe420ba16dd45754f33e96";
            Item5.Explosiveforce = "前屈ROM";
            Item5.Endurance = "后伸ROM";
            Item5.Fatigueindex = "旋转（左）ROM";
            Item5.FitTime = "旋转（右）ROM";
            Item5.ROMTime = "测试时间";
            ItemEmpty.ItemID = "";
            ItemEmpty.Explosiveforce = "";
            ItemEmpty.Endurance = "";
            ItemEmpty.Fatigueindex = "";
            ItemEmpty.FitTime = "";
            ItemEmpty.ROMTime = "";
            
            #endregion

            #region 添加到数据
            List1.Add(Item1);
            List2.Add(Item2);
            List3.Add(Item3);
            List4.Add(Item4);
            List5.Add(Item5);
            BindReportItem();
            List1.Add(ItemEmpty);
            List2.Add(ItemEmpty);
            List3.Add(ItemEmpty);
            List4.Add(ItemEmpty);
           
           foreach (ReportItem item in List1)
           {
               ReportItemList.Add(item);
           }
           foreach (ReportItem item in List2)
           {
               ReportItemList.Add(item);
           }
           foreach (ReportItem item in List3)
           {
               ReportItemList.Add(item);
           }
           foreach (ReportItem item in List4)
           {
               ReportItemList.Add(item);
           }

           foreach (ReportItem item in List5)
           {
               ReportItemList.Add(item);
           }
            #endregion
        }
        #endregion

        #region 绑定数据
        private void BindReportItem()
        {

            List<FitDemo> checkList = new List<FitDemo>();

            foreach (FitDemo item in TestList)
            {
                if (item.IsChecked == true)
                {
                    checkList.Add(item);
                }
            }

            List<EvaluteDetail> list;
            List<EvaluteDetail> listTemp = null;

            if (cbAvg.IsChecked.HasValue && cbAvg.IsChecked.Value)
            {
                if (cbMode.SelectedItem == null || ((ComboBoxItem)cbMode.SelectedItem).Tag.ToString() == "0")
                {
                    listTemp = (from od in MySession.Query<EvaluteDetail>()
                                where od.PatientID == ModuleConstant.PatientId
                                select od).OrderByDescending(x => x.EvaluteDetailDate).ToList();
                }
                else
                {
                    listTemp = (from od in MySession.Query<EvaluteDetail>()
                                where od.PatientID == ModuleConstant.PatientId
                                 && od.ActionId == Convert.ToInt32(((ComboBoxItem)cbMode.SelectedItem).Tag)
                                select od).OrderByDescending(x => x.EvaluteDetailDate).ToList();
                }

                #region 计算平均值


                //计算平均值
                Dictionary<int, float> temp = new Dictionary<int, float>();
                Dictionary<int, float> templast = new Dictionary<int, float>();
                Dictionary<int, float> tempFatigueIndex = new Dictionary<int, float>();
                for (int i = 0; i < listTemp.Count(); i++)
                {
                    if (temp.ContainsKey(listTemp[i].ActionId))
                    {
                        temp[listTemp[i].ActionId] = (temp[listTemp[i].ActionId] + listTemp[i].MaxV) / 2;
                        templast[listTemp[i].ActionId] = (templast[listTemp[i].ActionId] + listTemp[i].LastValue) / 2;
                        tempFatigueIndex[listTemp[i].ActionId] = (tempFatigueIndex[listTemp[i].ActionId] + listTemp[i].FatigueIndex) / 2;
                    }
                    else
                    {
                        temp[listTemp[i].ActionId] = listTemp[i].MaxV;
                        templast[listTemp[i].ActionId] = listTemp[i].LastValue;
                        tempFatigueIndex[listTemp[i].ActionId] = listTemp[i].FatigueIndex;
                    }
                }
                #endregion

                #region 赋值


                //赋值
                list = new List<EvaluteDetail>();
                foreach (var item in temp)
                {
                    EvaluteDetail ed = new EvaluteDetail();
                    ed.ActionId = item.Key;
                    ed.MaxV = item.Value;
                    ed.LastValue = templast[item.Key];
                    ed.FatigueIndex = tempFatigueIndex[item.Key];
                    ed.EvaluteDetailDate = DateTime.MinValue;
                    list.Add(ed);
                }
            }
            else
            {
                if (cbMode.SelectedItem == null || ((ComboBoxItem)cbMode.SelectedItem).Tag.ToString() == "0")
                {

                    list = (from od in MySession.Query<EvaluteDetail>()
                            where od.PatientID == ModuleConstant.PatientId
                            select od).OrderByDescending(x => x.EvaluteDetailDate).ToList();
                }
                else
                {
                    list = (from od in MySession.Query<EvaluteDetail>()
                            where od.PatientID == ModuleConstant.PatientId
                             && od.ActionId == Convert.ToInt32(((ComboBoxItem)cbMode.SelectedItem).Tag)
                            select od).OrderByDescending(x => x.EvaluteDetailDate).ToList();


                }
            }
            list = (from od in list
                    join q in checkList on GetDate(od.EvaluteDate) equals q.DayTime
                    select od).ToList();
                #endregion


            #region ROM赋值

            if (checkList.Count > 0)
            {
                foreach (FitDemo item in checkList)
                {
                    ReportItem reportItem = new ReportItem();
                    reportItem.Explosiveforce = GetROM(list, EvaluateActionEnum.RangeProtrusive, item.DayTime);
                    reportItem.Endurance = GetROM(list, EvaluateActionEnum.RangeBend, item.DayTime);
                    reportItem.Fatigueindex = GetROM(list, EvaluateActionEnum.RotationRangeLeft, item.DayTime);
                    reportItem.FitTime = GetROM(list, EvaluateActionEnum.RotationRangeRight, item.DayTime);
                    reportItem.ROMTime = Convert.ToDateTime(item.DayTime).ToString("yyyyMMdd");
                    List5.Add(reportItem);
                }
            }
            #endregion


            foreach (EvaluteDetail item in list)
            {
                ReportItem reportItem = new ReportItem();
                reportItem.ItemID = item.EvaluteDetailId.ToString();
                reportItem.Explosiveforce = item.MaxV.ToString();
                reportItem.Endurance = item.LastValue.ToString();
                reportItem.Fatigueindex = item.FatigueIndex.ToString();
                reportItem.FitTime = Convert.ToDateTime(item.EvaluteDetailDate).ToString("yyyyMMdd");

                if (item.ActionId == (int)EvaluateActionEnum.StrengthProtrusive)
                {
                    List1.Add(reportItem);
                }
                if (item.ActionId == (int)EvaluateActionEnum.StrengthBend)
                {
                    List2.Add(reportItem);
                }
                if (item.ActionId == (int)EvaluateActionEnum.RotationStrengthLeft)
                {
                    List3.Add(reportItem);
                }
                if (item.ActionId == (int)EvaluateActionEnum.RotationStrengthRigth)
                {
                    List4.Add(reportItem);
                }

            }
        }

        private static string GetROM(List<EvaluteDetail> list,EvaluateActionEnum Enum,string dt)
        {
            var obj1 = from od in list
                       where od.ActionId == (int)Enum&&od.EvaluteDate.ToString("yyyy-MM-dd")==dt
                       group od by new { od.ActionId, od.EvaluteDate } into p
                       select new
                       {
                           p.Key.ActionId,
                           maxvalue = p.Max(x => x.MaxV),
                           p.Key.EvaluteDate
                       };
            Demo d = new Demo();
            foreach (var obj in obj1)
            {
                d.ActionID = obj.ActionId.ToString();
                d.Key = obj.EvaluteDate.ToString();
                d.Value = obj.maxvalue.ToString();
            }
            if (d.Value != ""&&d.Value!=null)
            {
                return d.Value;
            }
            else
            {
                return "N/A";
            }
        }
        #endregion

        #region 转换
        private DataTable GetInfoNew(ObservableCollection<ReportItem> list)
        {
            DataTable tbl = new DataTable();
            tbl.Columns.Add("第一项", typeof(string));  //爆发力 1
            tbl.Columns.Add("第二项", typeof(string));  //耐力  2
            tbl.Columns.Add("第三项", typeof(string));  //疲劳指数 3
            tbl.Columns.Add("第四项", typeof(string));      //训练时间 4
            tbl.Columns.Add("第五项", typeof(string));      //训练时间 4
            int index = 0;
            foreach (ReportItem item in list)
            {
                index++;
                tbl.Rows.Add( item.Explosiveforce, item.Endurance, item.Fatigueindex, item.FitTime,item.ROMTime);
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
            tbl.Rows[7]["病人信息"] = "初步诊断：" + EnumHelper.GetEnumItemName(Convert.ToInt32(patient.DiagnoseTypeId), typeof(DiagnoseTypeEnum));
            tbl.Rows[8]["病人信息"] = "备注：" + patient.Note;

            #endregion
            return tbl;
        }
        #endregion

        #region 格式化时间
        public string GetDate(DateTime Add_Date)
        {
            return Add_Date.Year.ToString() + "-" + Add_Date.Month.ToString().PadLeft(2, '0') + "-" + Add_Date.Day.ToString().PadLeft(2, '0');
        }
        #endregion

        #region 复选框
        private void CheckBox_Click(object sender, RoutedEventArgs e)
        {
            CheckBox ck = sender as CheckBox;
            FitDemo FitDemo = this.gvFit.SelectedItem as FitDemo;
            if (ck.IsChecked == true)
                FitDemo.IsChecked = true;
            else
                FitDemo.IsChecked = false;
            
            if (FitDemo.IsChecked == false)
            {
                TestList.Remove(FitDemo);
            }
            else
            {
                TestList.Add(FitDemo);
            }
            CreateImage();
        }
        #endregion

        #region 全选
        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            CheckBox cb = sender as CheckBox;
            BindDayReport(ModuleConstant.PatientId, true);
        }

        private void CheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            CheckBox cb = sender as CheckBox;
            BindDayReport(ModuleConstant.PatientId, false);
        }
        #endregion
    }
}

#region 报告对象

/// <summary>
/// 报告对象
/// </summary>
public class ReportItem
{

    //285e677586d64cb9a0411e782112c002 前屈
    //61e5836252974446b8887dfcb30938cc 后伸
    //0362642cd1f5440aa5ff82c782bc25e0 旋转左
    //1022d9a5676049aeb9cfffd1af581512 旋转右
    //397ff4895dbe420ba16dd45754f33e96 ROM

    private string _ItemID;
    /// <summary>
    /// 专属ID
    /// </summary>
    public string ItemID
    {
        get { return _ItemID; }
        set { _ItemID = value; }
    }
    private string _Explosiveforce;
    /// <summary>
    /// 爆发力 OR 前屈ROM
    /// </summary>
    public string Explosiveforce
    {
        get { return _Explosiveforce; }
        set { _Explosiveforce = value; }
    }
    private string _Endurance;
    /// <summary>
    /// 耐力 OR 后伸ROM
    /// </summary>
    public string Endurance
    {
        get { return _Endurance; }
        set { _Endurance = value; }
    }
    private string _Fatigueindex;
    /// <summary>
    /// 疲劳指数 OR 旋转（左）ROM
    /// </summary>
    public string Fatigueindex
    {
        get { return _Fatigueindex; }
        set { _Fatigueindex = value; }
    }
    private string _FitTime;
    /// <summary>
    /// 测试时间 OR 旋转（右）ROM
    /// </summary>
    public string FitTime
    {
        get { return _FitTime; }
        set { _FitTime = value; }
    }

    private string _ROMTime;
    /// <summary>
    /// ROM 测试时间
    /// </summary>
    public string ROMTime
    {
        get { return _ROMTime; }
        set { _ROMTime = value; }
    }
}
#endregion

#region ROM值对象

public class Demo
{
    private string key;
    /// <summary>
    /// Key值
    /// </summary>
    public string Key
    {
        get { return key; }
        set { key = value; }
    }

    private string _ActionID;
    /// <summary>
    /// 模式ID
    /// </summary>
    public string ActionID
    {
        get { return _ActionID; }
        set { _ActionID = value; }
    }
    private string value;
    /// <summary>
    /// 值
    /// </summary>
    public string Value
    {
        get { return this.value; }
        set { this.value = value; }
    }
}
#endregion