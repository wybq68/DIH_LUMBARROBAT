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
using Visifire.Charts;
using System.Data;
using LumbarRobot.DAL;
using LumbarRobot.Data;
using Microsoft.Research.DynamicDataDisplay.Charts;
using Microsoft.Research.DynamicDataDisplay.DataSources;

namespace LumbarRobot.ChartNodes
{
    /// <summary>
    /// ChartNodesControl.xaml 的交互逻辑
    /// </summary>
    public partial class ChartNodesControl : UserControl
    {
        #region 事件
        public event EventHandler Close;
        #endregion

        #region 比对的总集合
        /// <summary>
        /// 被动运动时间集合
        /// </summary>
        List<ModeResultDemo> DateResult1 = new List<ModeResultDemo>();
        /// <summary>
        /// 主动运动时间集合
        /// </summary>
        List<ModeResultDemo> DateResult2 = new List<ModeResultDemo>();
        /// <summary>
        /// 助动运动时间集合
        /// </summary>
        List<ModeResultDemo> DateResult3 = new List<ModeResultDemo>();
        /// <summary>
        /// 主动配重块时间集合
        /// </summary>
        List<ModeResultDemo> DateResult4 = new List<ModeResultDemo>();
        /// <summary>
        /// 主动恒阻力时间集合
        /// </summary>
        List<ModeResultDemo> DateResult5 = new List<ModeResultDemo>();
        /// <summary>
        /// 等长A时间集合
        /// </summary>
        List<ModeResultDemo> DateResult6 = new List<ModeResultDemo>();
        /// <summary>
        /// 等长B时间集合
        /// </summary>
        List<ModeResultDemo> DateResult7 = new List<ModeResultDemo>();
        /// <summary>
        /// 推箱子时间集合
        /// </summary>
        List<ModeResultDemo> DateResult8 = new List<ModeResultDemo>();
        #endregion

        #region 被动运动集合
        DataTable CkGuided_Range = new DataTable();
        DataTable CkGuided_ParticipationStrength = new DataTable();

        /// <summary>
        /// 被动运动：获取参与力量
        /// </summary>
        List<ModeResultDemo> CkGuided_ParticipationStrengthList = new List<ModeResultDemo>();
        /// <summary>
        /// 活动范围
        /// </summary>
        List<ModeResultDemo> CkGuided_RangeList = new List<ModeResultDemo>();

        #endregion

        #region 主动运动集合
        DataTable CkFree_Range = new DataTable();
        DataTable CkFree_ParticipationStrength = new DataTable();

        /// <summary>
        /// 被动运动：获取参与力量
        /// </summary>
        List<ModeResultDemo> CkFree_ParticipationStrengthList = new List<ModeResultDemo>();
        /// <summary>
        /// 活动范围
        /// </summary>
        List<ModeResultDemo> CkFree_RangeList = new List<ModeResultDemo>();
        #endregion

        #region 助动运动集合
        DataTable CkInitiated_Range = new DataTable();
        DataTable CkInitiated_ParticipationStrength = new DataTable();

        /// <summary>
        /// 被动运动：获取参与力量
        /// </summary>
        List<ModeResultDemo> CkInitiated_ParticipationStrengthList = new List<ModeResultDemo>();
        /// <summary>
        /// 活动范围
        /// </summary>
        List<ModeResultDemo> CkInitiated_RangeList = new List<ModeResultDemo>();
        #endregion

        #region 主动配重块集合
        DataTable CkFreeCounterWeight_Range = new DataTable();
        DataTable CkFreeCounterWeight_ParticipationStrength = new DataTable();

        /// <summary>
        /// 被动运动：获取参与力量
        /// </summary>
        List<ModeResultDemo> CkFreeCounterWeight_ParticipationStrengthList = new List<ModeResultDemo>();
        /// <summary>
        /// 活动范围
        /// </summary>
        List<ModeResultDemo> CkFreeCounterWeight_RangeList = new List<ModeResultDemo>();
        #endregion

        #region 主动恒阻力集合
        DataTable CkFreeConstantResistance_Range = new DataTable();
        DataTable CkFreeConstantResistance_ParticipationStrength = new DataTable();

        /// <summary>
        /// 被动运动：获取参与力量
        /// </summary>
        List<ModeResultDemo> CkFreeConstantResistance_ParticipationStrengthList = new List<ModeResultDemo>();
        /// <summary>
        /// 活动范围
        /// </summary>
        List<ModeResultDemo> CkFreeConstantResistance_RangeList = new List<ModeResultDemo>();
        #endregion

        #region 等长A集合
        DataTable CkIsotonicA_Range = new DataTable();
        DataTable CkIsotonicA_ParticipationStrength = new DataTable();

        /// <summary>
        /// 被动运动：获取参与力量
        /// </summary>
        List<ModeResultDemo> CkIsotonicA_ParticipationStrengthList = new List<ModeResultDemo>();
        /// <summary>
        /// 活动范围
        /// </summary>
        List<ModeResultDemo> CkIsotonicA_RangeList = new List<ModeResultDemo>();
        #endregion

        #region 等长B集合
        DataTable CkIsotonicB_Range = new DataTable();
        DataTable CkIsotonicB_ParticipationStrength = new DataTable();

        /// <summary>
        /// 被动运动：获取参与力量
        /// </summary>
        List<ModeResultDemo> CkIsotonicB_ParticipationStrengthList = new List<ModeResultDemo>();
        /// <summary>
        /// 活动范围
        /// </summary>
        List<ModeResultDemo> CkIsotonicB_RangeList = new List<ModeResultDemo>();
        #endregion

        #region 推箱子集合
        DataTable CkSokoban_Range = new DataTable();
        DataTable CkSokoban_ParticipationStrength = new DataTable();

        /// <summary>
        /// 被动运动：获取参与力量
        /// </summary>
        List<ModeResultDemo> CkSokoban_ParticipationStrengthList = new List<ModeResultDemo>();
        /// <summary>
        /// 活动范围
        /// </summary>
        List<ModeResultDemo> CkSokoban_RangeList = new List<ModeResultDemo>();
        #endregion

        #region 图表的总集合
        DataTable DtAllResult = new DataTable();
        #endregion

        #region 变量集合
        List<ClusteredBarData> data = new List<ClusteredBarData>();
        #endregion

        #region 参数

        #region 中间对象
       public class ModeResultDemo
        {
            private string key;

            public string Key
            {
                get { return key; }
                set { key = value; }
            }
            private string value;

            public string Value
            {
                get { return this.value; }
                set { this.value = value; }
            }

            private string value2;

            public string Value2
            {
                get { return value2; }
                set { value2 = value; }
            }
        }

        public class ChartDemo
        {
            private string exItemId;
            /// <summary>
            /// 动作编号
            /// </summary>
            public string ExItemId
            {
                get { return exItemId; }
                set { exItemId = value; }
            }
            private string exerciseItemName;
            /// <summary>
            /// 动作名称
            /// </summary>
            public string ExerciseItemName
            {
                get { return exerciseItemName; }
                set { exerciseItemName = value; }
            }
            private string startTime;
            /// <summary>
            /// 开始时间
            /// </summary>
            public string StartTime
            {
                get { return startTime; }
                set { startTime = value; }
            }
            private string endTime;
            /// <summary>
            /// 结束时间
            /// </summary>
            public string EndTime
            {
                get { return endTime; }
                set { endTime = value; }
            }
        }


        #endregion

        #region 模式

        #region 被动运动集合
        private void CkGuided_Checked(object sender, RoutedEventArgs e)
        {
            if (CkGuided.IsChecked.Value==true)
            {
                this.ColumnScroll.ScrollToHome();
                Column(Mid, ColumnScroll);
            }
        }

        #endregion

        #region 主动运动集合
        //private void CkFree_Checked(object sender, RoutedEventArgs e)
        //{
        //    if (CkFree.IsChecked.Value == true)
        //    {
        //        this.ColumnScroll.ScrollToHome();
        //        Column(Mid, ColumnScroll);
        //    }
        //}
        #endregion

        #region 助动运动集合
        //private void CkInitiated_Checked(object sender, RoutedEventArgs e)
        //{
        //    if (CkInitiated.IsChecked.Value == true)
        //    {
        //        this.ColumnScroll.ScrollToHome();
        //        Column(Mid, ColumnScroll);
        //    }
        //}
        #endregion

        #region 主动配重块集合
        private void CkFreeCounterWeight_Checked(object sender, RoutedEventArgs e)
        {
            if (CkFreeCounterWeight.IsChecked.Value == true)
            {
                this.ColumnScroll.ScrollToHome();
                Column(Mid, ColumnScroll);
            }
        }
        #endregion

        #region 主动恒阻力集合
        private void CkFreeConstantResistance_Checked(object sender, RoutedEventArgs e)
        {
            if (CkFreeConstantResistance.IsChecked.Value == true)
            {
                this.ColumnScroll.ScrollToHome();
                Column(Mid, ColumnScroll);
            }
        }
        #endregion

        #region 等长A集合
        private void CkIsotonicA_Checked(object sender, RoutedEventArgs e)
        {
            if (CkIsotonicA.IsChecked.Value == true)
            {
                this.ColumnScroll.ScrollToHome();
                Column(Mid, ColumnScroll);
            }
        }
        #endregion

        #region 等长B集合
        private void CkIsotonicB_Checked(object sender, RoutedEventArgs e)
        {
            if (CkIsotonicB.IsChecked.Value == true)
            {
                this.ColumnScroll.ScrollToHome();
                Column(Mid, ColumnScroll);
            }
        }
        #endregion

        //#region 推箱子集合
        //private void CkSokoban_Checked(object sender, RoutedEventArgs e)
        //{
        //    if (CkSokoban.IsChecked.Value == true)
        //    {
        //        this.ColumnScroll.ScrollToHome();
        //        Column(Mid, ColumnScroll);
        //    }
        //}
        //#endregion
        #endregion

        #region 动作名称
        /// <summary>
        /// 显示动作的名称
        /// </summary>
        public ChartDemo myDemo;
        #endregion

        #endregion

        #region 构造
        public ChartNodesControl(ChartDemo item)
        {
            InitializeComponent();

            if (item != null)
            {
                myDemo = item;
                this.txExitemName.Text = myDemo.ExerciseItemName;
                GetChartDemo(myDemo);
            }
            this.ColumnScroll.ScrollToHome();
            Column(Mid, ColumnScroll);
        }
        #endregion

        #region 关闭
        private void btnClosed_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.Close(sender, e);
            myDemo = null;
        }
        #endregion

        #region 方法

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

        #region 绑定数据
        void GetChartDemo(ChartDemo demo)
        {
            #region 清空集合

            #region 对比集合
            DateResult1.Clear();
            DateResult2.Clear();
            DateResult3.Clear();
            DateResult4.Clear();
            DateResult5.Clear();
            DateResult6.Clear();
            DateResult7.Clear();
            DateResult8.Clear();
            #endregion

            #region 获取参与力量
            CkGuided_ParticipationStrengthList.Clear();
            CkFree_ParticipationStrengthList.Clear();
            CkInitiated_ParticipationStrengthList.Clear();
            CkFreeConstantResistance_ParticipationStrengthList.Clear();
            CkFreeCounterWeight_ParticipationStrengthList.Clear();
            CkIsotonicA_ParticipationStrengthList.Clear();
            CkIsotonicB_ParticipationStrengthList.Clear();
            CkSokoban_ParticipationStrengthList.Clear();

            #endregion

            #region 活动范围
            CkGuided_RangeList.Clear();
            CkFree_RangeList.Clear();
            CkInitiated_RangeList.Clear();
            CkFreeConstantResistance_RangeList.Clear();
            CkFreeCounterWeight_RangeList.Clear();
            CkIsotonicA_RangeList.Clear();
            CkIsotonicB_RangeList.Clear();
            CkSokoban_RangeList.Clear();
            #endregion

            #endregion

            #region 数据绑定
            DtAllResult = GetAllTrainResult(ModuleConstant.PatientId, demo.ExItemId, Convert.ToDateTime(demo.StartTime), Convert.ToDateTime(demo.EndTime));

            #region 获得总集合
            var obj = (from p in DtAllResult.AsEnumerable()
                       group p by new { t1 = p.Field<string>("ExerciseDate") } into g
                       select new
                       {
                           g.Key.t1
                       });

            foreach (var item in obj)
            {
                ModeResultDemo d1 = new ModeResultDemo();
                d1.Key = item.t1.ToString();
                d1.Value = "0";
                d1.Value2 = "0";
                ModeResultDemo d2 = new ModeResultDemo();
                d2.Key = item.t1.ToString();
                d2.Value = "0";
                d2.Value2 = "0";
                ModeResultDemo d3 = new ModeResultDemo();
                d3.Key = item.t1.ToString();
                d3.Value = "0";
                d3.Value2 = "0";
                ModeResultDemo d4 = new ModeResultDemo();
                d4.Key = item.t1.ToString();
                d4.Value = "0";
                d4.Value2 = "0";
                ModeResultDemo d5 = new ModeResultDemo();
                d5.Key = item.t1.ToString();
                d5.Value = "0";
                d5.Value2 = "0";
                ModeResultDemo d6 = new ModeResultDemo();
                d6.Key = item.t1.ToString();
                d6.Value = "0";
                d6.Value2 = "0";
                ModeResultDemo d7 = new ModeResultDemo();
                d7.Key = item.t1.ToString();
                d7.Value = "0";
                d7.Value2 = "0";
                ModeResultDemo d8 = new ModeResultDemo();
                d8.Key = item.t1.ToString();
                d8.Value = "0";
                d8.Value2 = "0";
                DateResult1.Add(d1);
                DateResult2.Add(d2);
                DateResult3.Add(d3);
                DateResult4.Add(d4);
                DateResult5.Add(d5);
                DateResult6.Add(d6);
                DateResult7.Add(d7);
                DateResult8.Add(d8);
            }
            #endregion

            #region 被动运动集合
            //获取参与力量
            SetParticipationStrength(DateResult1, CkGuided_ParticipationStrengthList, "1");
            //活动范围
            //SetRange(DateResult1, CkGuided_RangeList, "1");
            Set_Range(DateResult1, CkGuided_RangeList, "1");

            #endregion

            #region 主动运动集合
            //获取参与力量
            SetParticipationStrength(DateResult2, CkFree_ParticipationStrengthList, "2");
            //活动范围
            SetRange(DateResult2, CkFree_RangeList, "2");
            #endregion

            #region 助动运动集合
            //获取参与力量
            SetParticipationStrength(DateResult3, CkInitiated_ParticipationStrengthList, "3");
            //活动范围
            SetRange(DateResult3, CkInitiated_RangeList, "3");

            #endregion

            #region 主动配重块集合
            //获取参与力量
            SetParticipationStrength(DateResult4, CkFreeConstantResistance_ParticipationStrengthList, "4");
            //活动范围
            SetRange(DateResult4, CkFreeConstantResistance_RangeList, "4");

            #endregion

            #region 主动恒阻力集合
            //获取参与力量
            SetParticipationStrength(DateResult5, CkFreeCounterWeight_ParticipationStrengthList, "5");
            //活动范围
            SetRange(DateResult5, CkFreeCounterWeight_RangeList, "5");

            #endregion

            #region 等长A集合
            //获取参与力量
            SetParticipationStrength(DateResult6, CkIsotonicA_ParticipationStrengthList, "6");
            //活动范围
            SetRange(DateResult6, CkIsotonicA_RangeList, "6");

            #endregion

            #region 等长B集合
            //获取参与力量
            SetParticipationStrength(DateResult7, CkIsotonicB_ParticipationStrengthList, "7");
            //活动范围
            SetRange(DateResult7, CkIsotonicB_RangeList, "7");

            #endregion

            #region 推箱子集合
            //获取参与力量
            SetParticipationStrength(DateResult8, CkSokoban_ParticipationStrengthList, "8");
            //活动范围
            SetRange(DateResult8, CkSokoban_RangeList, "8");
            #endregion

            #endregion
        }


        #region 柱状图
        /// <summary>
        /// 柱状图
        /// </summary>
        /// <param name="gvColumn"></param>
        /// <param name="ColumnScroll"></param>
        /// <param name="LineScroll"></param>
        public void Column(Grid gvColumn, ScrollViewer ColumnScroll)
        {
            #region 初始化
            ColumnScroll.Visibility = Visibility.Visible;

            Grid Column_Range = gvColumn.Children[0] as Grid;
            Grid Column_ParticipationStrength = gvColumn.Children[1] as Grid;

            Column_Range.Children.Clear();
            Column_ParticipationStrength.Children.Clear();
           
            Color[] colors = new Color[] { Colors.Red, Colors.LawnGreen, Colors.Blue, Colors.Yellow, Colors.Magenta, Colors.Black, Colors.Brown, Colors.DimGray };

            data = new List<ClusteredBarData>();
            #endregion

            #region 实例图表
           
            //最大力量
            Chart chart1 = new Chart();
            chart1.DataPointWidth = 2;
            chart1.Width = 870;
            Axis xaxis1 = new Axis();
            AxisLabels xal1 = new AxisLabels
            {

                Enabled = true,

                Angle = -45
            };
            xaxis1.AxisLabels = xal1;
            chart1.AxesX.Add(xaxis1);
            SetTitle(chart1, "主动参与力量");
            chart1.Rendered += new EventHandler(c_Rendered);
           
            Chart chart3 = new Chart();
            //主动/被动关节活动度
            SetTitle(chart3, "活动范围");
            chart3.DataPointWidth = 2;
            chart3.Width = 870;
            Axis xaxis3 = new Axis();
            AxisLabels xal3 = new AxisLabels
            {

                Enabled = true,

                Angle = -45
            };
            xaxis3.AxisLabels = xal3;
            chart3.AxesX.Add(xaxis3);
            chart3.Rendered += new EventHandler(c_Rendered);
           
            #endregion

            #region 被动运动集合
            if (CkGuided.IsChecked.Value==true)
            {
                #region 活动范围
                //活动范围
                if (CkGuided_RangeList.Count > 0)
                {
                    DataSeries ds = new DataSeries();
                    ds.RenderAs = RenderAs.Column;
                    SolidColorBrush brush = new SolidColorBrush(Colors.Red);
                    ds.Color = brush;
                    ds.Name = "被动运动模式";
                    foreach (var item in CkGuided_RangeList)
                    {
                        DataPoint dp = new DataPoint();
                        dp.YValue = Convert.ToDouble(item.Value);
                        dp.AxisXLabel = item.Key.ToString();
                        ds.DataPoints.Add(dp);
                    }
                    chart3.Series.Add(ds);
                }
                #endregion

                #region 活动范围 对比 
                //活动范围
                if (CkGuided_RangeList.Count > 0)
                {
                    var groupRecords = new List<RecordData>();
                    //int count=0;
                    foreach (var item in CkGuided_RangeList)
                    {
                        bool flag = true;
                        string date = Convert.ToDateTime(item.Key).ToString("yyyy/MM/dd");
                        var x = dateAxis.ConvertToDouble(Convert.ToDateTime(date));

                        for (int i = 0; i < data.Count; i++)
                        {
                            if (data[i].X == x)
                            {
                                BarData bd = new BarData { YMin = Convert.ToDouble(item.Value2), YMax = Convert.ToDouble(item.Value) };
                                bd.BrushColor = colors[0];
                                data[i].BarDatas.Add(bd);
                                flag = false;
                                break;
                            }
                        }

                        if (flag)
                        {
                            ClusteredBarData cbd = new ClusteredBarData();
                            List<BarData> barDatas = new List<BarData>();

                            cbd.X = x;
                            BarData bd = new BarData { YMin = Convert.ToDouble(item.Value2), YMax = Convert.ToDouble(item.Value) };
                            bd.BrushColor = colors[0];
                            barDatas.Add(bd);
                            cbd.BarDatas = barDatas;
                            data.Add(cbd);
                        }

                    }
                }
                #endregion

                #region 参与力量
                //参与力量
                if (CkGuided_ParticipationStrengthList.Count > 0)
                {
                    DataSeries ds = new DataSeries();
                    ds.RenderAs = RenderAs.Column;
                    SolidColorBrush brush = new SolidColorBrush(Colors.Red);
                    ds.Color = brush;
                    ds.Name = "被动运动模式";
                    foreach (var item in CkGuided_ParticipationStrengthList)
                    {
                        DataPoint dp = new DataPoint();
                        dp.YValue = Convert.ToDouble(item.Value);
                        dp.AxisXLabel = item.Key.ToString();
                        ds.DataPoints.Add(dp);
                    }
                    chart1.Series.Add(ds);
                }
                #endregion
            }
            #endregion

            #region 主动运动集合
            //if (CkFree.IsChecked.Value == true)
            //{
            //    #region 活动范围
            //    //活动范围
            //    if (CkFree_RangeList.Count > 0)
            //    {
            //        DataSeries ds = new DataSeries();
            //        ds.RenderAs = RenderAs.Column;
            //        SolidColorBrush brush = new SolidColorBrush(Colors.LawnGreen);
            //        ds.Color = brush;
            //        ds.Name = "主动运动";
            //        foreach (var item in CkFree_RangeList)
            //        {
            //            DataPoint dp = new DataPoint();
            //            dp.YValue = Convert.ToDouble(item.Value);
            //            dp.AxisXLabel = item.Key.ToString();
            //            ds.DataPoints.Add(dp);
            //        }
            //        chart3.Series.Add(ds);
            //    }
            //    #endregion

            //    #region 活动范围 对比
            //    //活动范围
            //    if (CkFree_RangeList.Count > 0)
            //    {
            //        var groupRecords = new List<RecordData>();
            //        //int count=0;
            //        foreach (var item in CkFree_RangeList)
            //        {
            //            bool flag = true;
            //            string date = Convert.ToDateTime(item.Key).ToString("yyyy/MM/dd");
            //            var x = dateAxis.ConvertToDouble(Convert.ToDateTime(date));

            //            for (int i = 0; i < data.Count; i++)
            //            {
            //                if (data[i].X == x)
            //                {
            //                    BarData bd = new BarData { YMin = Convert.ToDouble(item.Value2), YMax = Convert.ToDouble(item.Value) };
            //                    bd.BrushColor = colors[1];
            //                    data[i].BarDatas.Add(bd);
            //                    flag = false;
            //                    break;
            //                }
            //            }

            //            if (flag)
            //            {
            //                ClusteredBarData cbd = new ClusteredBarData();
            //                List<BarData> barDatas = new List<BarData>();
            //                cbd.X = x;
            //                BarData bd = new BarData { YMin = Convert.ToDouble(item.Value2), YMax = Convert.ToDouble(item.Value) };
            //                bd.BrushColor = colors[1];
            //                barDatas.Add(bd);
            //                cbd.BarDatas = barDatas;
            //                data.Add(cbd);
            //            }

            //        }

            //    }
            //    #endregion

            //    #region 参与力量
            //    //参与力量
            //    if (CkFree_ParticipationStrengthList.Count > 0)
            //    {
            //        DataSeries ds = new DataSeries();
            //        ds.RenderAs = RenderAs.Column;
            //        SolidColorBrush brush = new SolidColorBrush(Colors.LawnGreen);
            //        ds.Color = brush;
            //        ds.Name = "主动运动";
            //        foreach (var item in CkFree_ParticipationStrengthList)
            //        {
            //            DataPoint dp = new DataPoint();
            //            dp.YValue = Convert.ToDouble(item.Value);
            //            dp.AxisXLabel = item.Key.ToString();
            //            ds.DataPoints.Add(dp);
            //        }
            //        chart1.Series.Add(ds);
            //    }
            //    #endregion
            //}
            #endregion

            #region 助动运动集合
            //if (CkInitiated.IsChecked.Value == true)
            //{
            //    #region 活动范围
            //    //活动范围
            //    if (CkInitiated_RangeList.Count > 0)
            //    {
            //        DataSeries ds = new DataSeries();
            //        ds.RenderAs = RenderAs.Column;
            //        SolidColorBrush brush = new SolidColorBrush(Colors.Blue);
            //        ds.Color = brush;
            //        ds.Name = "助动运动";
            //        foreach (var item in CkInitiated_RangeList)
            //        {
            //            DataPoint dp = new DataPoint();
            //            dp.YValue = Convert.ToDouble(item.Value);
            //            dp.AxisXLabel = item.Key.ToString();
            //            ds.DataPoints.Add(dp);
            //        }
            //        chart3.Series.Add(ds);
            //    }
            //    #endregion

            //    #region 活动范围 对比
            //    //活动范围
            //    if (CkInitiated_RangeList.Count > 0)
            //    {
            //        var groupRecords = new List<RecordData>();
            //        //int count=0;
            //        foreach (var item in CkInitiated_RangeList)
            //        {
            //            bool flag = true;
            //            string date = Convert.ToDateTime(item.Key).ToString("yyyy/MM/dd");
            //            var x = dateAxis.ConvertToDouble(Convert.ToDateTime(date));

            //            for (int i = 0; i < data.Count; i++)
            //            {
            //                if (data[i].X == x)
            //                {
            //                    BarData bd = new BarData { YMin = Convert.ToDouble(item.Value2), YMax = Convert.ToDouble(item.Value) };
            //                    bd.BrushColor = colors[2];
            //                    data[i].BarDatas.Add(bd);
            //                    flag = false;
            //                    break;
            //                }
            //            }

            //            if (flag)
            //            {
            //                ClusteredBarData cbd = new ClusteredBarData();
            //                List<BarData> barDatas = new List<BarData>();
            //                cbd.X = x;
            //                BarData bd = new BarData { YMin = Convert.ToDouble(item.Value2), YMax = Convert.ToDouble(item.Value) };
            //                bd.BrushColor = colors[2];
            //                barDatas.Add(bd);
            //                cbd.BarDatas = barDatas;
            //                data.Add(cbd);
            //            }

            //        }

            //    }
            //    #endregion

            //    #region 参与力量
            //    //参与力量
            //    if (CkInitiated_ParticipationStrengthList.Count > 0)
            //    {
            //        DataSeries ds = new DataSeries();
            //        ds.RenderAs = RenderAs.Column;
            //        SolidColorBrush brush = new SolidColorBrush(Colors.Blue);
            //        ds.Color = brush;
            //        ds.Name = "助动运动";
            //        foreach (var item in CkInitiated_ParticipationStrengthList)
            //        {
            //            DataPoint dp = new DataPoint();
            //            dp.YValue = Convert.ToDouble(item.Value);
            //            dp.AxisXLabel = item.Key.ToString();
            //            ds.DataPoints.Add(dp);
            //        }
            //        chart1.Series.Add(ds);
            //    }
            //    #endregion
            //}
            #endregion

            #region 主动配重块集合
            if (CkFreeCounterWeight.IsChecked.Value == true)
            {
                #region 活动范围
                //活动范围
                if (CkFreeCounterWeight_RangeList.Count > 0)
                {
                    DataSeries ds = new DataSeries();
                    ds.RenderAs = RenderAs.Column;
                    SolidColorBrush brush = new SolidColorBrush(Colors.Yellow);
                    ds.Color = brush;
                    ds.Name = "向心、离心运动模式";
                    foreach (var item in CkFreeCounterWeight_RangeList)
                    {
                        DataPoint dp = new DataPoint();
                        dp.YValue = Convert.ToDouble(item.Value);
                        dp.AxisXLabel = item.Key.ToString();
                        ds.DataPoints.Add(dp);
                    }
                    chart3.Series.Add(ds);
                }
                #endregion

                #region 活动范围 对比
                //活动范围
                if (CkFreeCounterWeight_RangeList.Count > 0)
                {
                    var groupRecords = new List<RecordData>();
                    //int count=0;
                    foreach (var item in CkFreeCounterWeight_RangeList)
                    {

                        bool flag = true;
                        string date = Convert.ToDateTime(item.Key).ToString("yyyy/MM/dd");
                        var x = dateAxis.ConvertToDouble(Convert.ToDateTime(date));

                        for (int i = 0; i < data.Count; i++)
                        {
                            if (data[i].X == x)
                            {
                                BarData bd = new BarData { YMin = Convert.ToDouble(item.Value2), YMax = Convert.ToDouble(item.Value) };
                                bd.BrushColor = colors[3];
                                data[i].BarDatas.Add(bd);
                                flag = false;
                                break;
                            }
                        }

                        if (flag)
                        {
                            ClusteredBarData cbd = new ClusteredBarData();
                            List<BarData> barDatas = new List<BarData>();
                            cbd.X = x;
                            BarData bd = new BarData { YMin = Convert.ToDouble(item.Value2), YMax = Convert.ToDouble(item.Value) };
                            bd.BrushColor = colors[3];
                            barDatas.Add(bd);
                            cbd.BarDatas = barDatas;
                            data.Add(cbd);
                        }

                    }

                }
                #endregion

                #region 参与力量
                //参与力量
                if (CkFreeCounterWeight_ParticipationStrengthList.Count > 0)
                {
                    DataSeries ds = new DataSeries();
                    ds.RenderAs = RenderAs.Column;
                    SolidColorBrush brush = new SolidColorBrush(Colors.Yellow);
                    ds.Color = brush;
                    ds.Name = "向心、离心运动模式";
                    foreach (var item in CkFreeCounterWeight_ParticipationStrengthList)
                    {
                        DataPoint dp = new DataPoint();
                        dp.YValue = Convert.ToDouble(item.Value);
                        dp.AxisXLabel = item.Key.ToString();
                        ds.DataPoints.Add(dp);
                    }
                    chart1.Series.Add(ds);
                }
                #endregion
            }
            #endregion

            #region 主动恒阻力集合
            if (CkFreeConstantResistance.IsChecked.Value == true)
            {
                #region 活动范围
                //活动范围
                if (CkFreeConstantResistance_RangeList.Count > 0)
                {
                    DataSeries ds = new DataSeries();
                    ds.RenderAs = RenderAs.Column;
                    SolidColorBrush brush = new SolidColorBrush(Colors.Magenta);
                    ds.Color = brush;
                    ds.Name = "等张运动模式";
                    foreach (var item in CkFreeConstantResistance_RangeList)
                    {
                        DataPoint dp = new DataPoint();
                        dp.YValue = Convert.ToDouble(item.Value);
                        dp.AxisXLabel = item.Key.ToString();
                        ds.DataPoints.Add(dp);
                    }
                    chart3.Series.Add(ds);
                }
                #endregion

                #region 活动范围 对比
                //活动范围
                if (CkFreeConstantResistance_RangeList.Count > 0)
                {
                    var groupRecords = new List<RecordData>();
                    //int count=0;
                    foreach (var item in CkFreeConstantResistance_RangeList)
                    {
                        bool flag = true;
                        string date = Convert.ToDateTime(item.Key).ToString("yyyy/MM/dd");
                        var x = dateAxis.ConvertToDouble(Convert.ToDateTime(date));

                        for (int i = 0; i < data.Count; i++)
                        {
                            if (data[i].X == x)
                            {
                                BarData bd = new BarData { YMin = Convert.ToDouble(item.Value2), YMax = Convert.ToDouble(item.Value) };
                                bd.BrushColor = colors[4];
                                data[i].BarDatas.Add(bd);
                                flag = false;
                                break;
                            }
                        }

                        if (flag)
                        {
                            ClusteredBarData cbd = new ClusteredBarData();
                            List<BarData> barDatas = new List<BarData>();
                            cbd.X = x;
                            BarData bd = new BarData { YMin = Convert.ToDouble(item.Value2), YMax = Convert.ToDouble(item.Value) };
                            bd.BrushColor = colors[4];
                            barDatas.Add(bd);
                            cbd.BarDatas = barDatas;
                            data.Add(cbd);
                        }
                    }

                }
                #endregion

                #region 参与力量
                //参与力量
                if (CkFreeConstantResistance_ParticipationStrengthList.Count > 0)
                {
                    DataSeries ds = new DataSeries();
                    ds.RenderAs = RenderAs.Column;
                    SolidColorBrush brush = new SolidColorBrush(Colors.Magenta);
                    ds.Color = brush;
                    ds.Name = "等张运动模式";
                    foreach (var item in CkFreeConstantResistance_ParticipationStrengthList)
                    {
                        DataPoint dp = new DataPoint();
                        dp.YValue = Convert.ToDouble(item.Value);
                        dp.AxisXLabel = item.Key.ToString();
                        ds.DataPoints.Add(dp);
                    }
                    chart1.Series.Add(ds);
                }
                #endregion
            }
            #endregion

            #region 等长A集合
            if (CkIsotonicA.IsChecked.Value == true)
            {
                #region 活动范围
                //活动范围
                if (CkIsotonicA_RangeList.Count > 0)
                {
                    DataSeries ds = new DataSeries();
                    ds.RenderAs = RenderAs.Column;
                    SolidColorBrush brush = new SolidColorBrush(Colors.Black);
                    ds.Color = brush;
                    ds.Name = "等长运动模式";
                    foreach (var item in CkIsotonicA_RangeList)
                    {
                        DataPoint dp = new DataPoint();
                        dp.YValue = Convert.ToDouble(item.Value);
                        dp.AxisXLabel = item.Key.ToString();
                        ds.DataPoints.Add(dp);
                    }
                    chart3.Series.Add(ds);
                }
                #endregion

                #region 活动范围 对比
                //活动范围
                if (CkIsotonicA_RangeList.Count > 0)
                {
                    var groupRecords = new List<RecordData>();
                    //int count=0;
                    foreach (var item in CkIsotonicA_RangeList)
                    {
                        bool flag = true;
                        string date = Convert.ToDateTime(item.Key).ToString("yyyy/MM/dd");
                        var x = dateAxis.ConvertToDouble(Convert.ToDateTime(date));

                        for (int i = 0; i < data.Count; i++)
                        {
                            if (data[i].X == x)
                            {
                                BarData bd = new BarData { YMin = Convert.ToDouble(item.Value2), YMax = Convert.ToDouble(item.Value) };
                                bd.BrushColor = colors[5];
                                data[i].BarDatas.Add(bd);
                                flag = false;
                                break;
                            }
                        }

                        if (flag)
                        {
                            ClusteredBarData cbd = new ClusteredBarData();
                            List<BarData> barDatas = new List<BarData>();
                            cbd.X = x;
                            BarData bd = new BarData { YMin = Convert.ToDouble(item.Value2), YMax = Convert.ToDouble(item.Value) };
                            bd.BrushColor = colors[5];
                            barDatas.Add(bd);
                            cbd.BarDatas = barDatas;
                            data.Add(cbd);
                        }

                    }

                }
                #endregion

                #region 参与力量
                //参与力量
                if (CkIsotonicA_ParticipationStrengthList.Count > 0)
                {
                    DataSeries ds = new DataSeries();
                    ds.RenderAs = RenderAs.Column;
                    SolidColorBrush brush = new SolidColorBrush(Colors.Black);
                    ds.Color = brush;
                    ds.Name = "等长运动模式";
                    foreach (var item in CkIsotonicA_ParticipationStrengthList)
                    {
                        DataPoint dp = new DataPoint();
                        dp.YValue = Convert.ToDouble(item.Value);
                        dp.AxisXLabel = item.Key.ToString();
                        ds.DataPoints.Add(dp);
                    }
                    chart1.Series.Add(ds);
                }
                #endregion
            }
            #endregion

            #region 等长B集合
            if (CkIsotonicB.IsChecked.Value == true)
            {
                #region 活动范围
                //活动范围
                if (CkIsotonicB_RangeList.Count > 0)
                {
                    DataSeries ds = new DataSeries();
                    ds.RenderAs = RenderAs.Column;
                    SolidColorBrush brush = new SolidColorBrush(Colors.Brown);
                    ds.Color = brush;
                    ds.Name = "协调性训练模式";
                    foreach (var item in CkIsotonicB_RangeList)
                    {
                        DataPoint dp = new DataPoint();
                        dp.YValue = Convert.ToDouble(item.Value);
                        dp.AxisXLabel = item.Key.ToString();
                        ds.DataPoints.Add(dp);
                    }
                    chart3.Series.Add(ds);
                }
                #endregion

                #region 活动范围 对比
                //活动范围
                if (CkIsotonicB_RangeList.Count > 0)
                {
                    var groupRecords = new List<RecordData>();
                    //int count=0;
                    foreach (var item in CkIsotonicB_RangeList)
                    {
                        bool flag = true;
                        string date = Convert.ToDateTime(item.Key).ToString("yyyy/MM/dd");
                        var x = dateAxis.ConvertToDouble(Convert.ToDateTime(date));

                        for (int i = 0; i < data.Count; i++)
                        {
                            if (data[i].X == x)
                            {
                                BarData bd = new BarData { YMin = Convert.ToDouble(item.Value2), YMax = Convert.ToDouble(item.Value) };
                                bd.BrushColor = colors[6];
                                data[i].BarDatas.Add(bd);
                                flag = false;
                                break;
                            }
                        }

                        if (flag)
                        {
                            ClusteredBarData cbd = new ClusteredBarData();
                            List<BarData> barDatas = new List<BarData>();
                            cbd.X = x;
                            BarData bd = new BarData { YMin = Convert.ToDouble(item.Value2), YMax = Convert.ToDouble(item.Value) };
                            bd.BrushColor = colors[6];
                            barDatas.Add(bd);
                            cbd.BarDatas = barDatas;
                            data.Add(cbd);
                        }

                    }

                }
                #endregion

                #region 参与力量
                //参与力量
                if (CkIsotonicB_ParticipationStrengthList.Count > 0)
                {
                    DataSeries ds = new DataSeries();
                    ds.RenderAs = RenderAs.Column;
                    SolidColorBrush brush = new SolidColorBrush(Colors.Brown);
                    ds.Color = brush;
                    ds.Name = "协调性训练模式";
                    foreach (var item in CkIsotonicB_ParticipationStrengthList)
                    {
                        DataPoint dp = new DataPoint();
                        dp.YValue = Convert.ToDouble(item.Value);
                        dp.AxisXLabel = item.Key.ToString();
                        ds.DataPoints.Add(dp);
                    }
                    chart1.Series.Add(ds);
                }
                #endregion
            }
            #endregion

            #region 推箱子集合
            //if (CkSokoban.IsChecked.Value == true)
            //{
            //    #region 活动范围
            //    //活动范围
            //    if (CkSokoban_RangeList.Count > 0)
            //    {
            //        DataSeries ds = new DataSeries();
            //        ds.RenderAs = RenderAs.Column;
            //        SolidColorBrush brush = new SolidColorBrush(Colors.DimGray);
            //        ds.Color = brush;
            //        ds.Name = "推箱子";
            //        foreach (var item in CkSokoban_RangeList)
            //        {
            //            DataPoint dp = new DataPoint();
            //            dp.YValue = Convert.ToDouble(item.Value);
            //            dp.AxisXLabel = item.Key.ToString();
            //            ds.DataPoints.Add(dp);
            //        }
            //        chart3.Series.Add(ds);
            //    }
                #endregion

            #region 活动范围 对比
            //    //活动范围
            //    if (CkSokoban_RangeList.Count > 0)
            //    {
            //        var groupRecords = new List<RecordData>();
            //        //int count=0;
            //        foreach (var item in CkSokoban_RangeList)
            //        {
            //            bool flag = true;
            //            string date = Convert.ToDateTime(item.Key).ToString("yyyy/MM/dd");
            //            var x = dateAxis.ConvertToDouble(Convert.ToDateTime(date));

            //            for (int i = 0; i < data.Count; i++)
            //            {
            //                if (data[i].X == x)
            //                {
            //                    BarData bd = new BarData { YMin = Convert.ToDouble(item.Value2), YMax = Convert.ToDouble(item.Value) };
            //                    bd.BrushColor = colors[7];
            //                    data[i].BarDatas.Add(bd);
            //                    flag = false;
            //                    break;
            //                }
            //            }

            //            if (flag)
            //            {
            //                ClusteredBarData cbd = new ClusteredBarData();
            //                List<BarData> barDatas = new List<BarData>();
            //                cbd.X = x;
            //                BarData bd = new BarData { YMin = Convert.ToDouble(item.Value2), YMax = Convert.ToDouble(item.Value) };
            //                bd.BrushColor = colors[7];
            //                barDatas.Add(bd);
            //                cbd.BarDatas = barDatas;
            //                data.Add(cbd);
            //            }
            //        }

            //    }
            #endregion

            //    #region 参与力量
            //    //参与力量
            //    if (CkSokoban_ParticipationStrengthList.Count > 0)
            //    {
            //        DataSeries ds = new DataSeries();
            //        ds.RenderAs = RenderAs.Column;
            //        SolidColorBrush brush = new SolidColorBrush(Colors.DimGray);
            //        ds.Color = brush;
            //        ds.Name = "推箱子";
            //        foreach (var item in CkSokoban_ParticipationStrengthList)
            //        {
            //            DataPoint dp = new DataPoint();
            //            dp.YValue = Convert.ToDouble(item.Value);
            //            dp.AxisXLabel = item.Key.ToString();
            //            ds.DataPoints.Add(dp);
            //        }
            //        chart1.Series.Add(ds);
            //    }
            //    #endregion
            //}
            //#endregion

            #region 添加图表到容器
            Column_Range.Children.Add(chart3);
            Column_ParticipationStrength.Children.Add(chart1);

            #endregion

            #region 添加数据
            EnumerableDataSource<ClusteredBarData> value = new EnumerableDataSource<ClusteredBarData>(data);
            value.SetXMapping(k => k.X);
            value.SetYMapping(k => k.BarDatas[0].YMax);

            DataContext = value;
            #endregion
        }
        #endregion

        #endregion

        #region 获取集合方法

        #region 获取参与力量
        /// <summary>
        /// 获取参与力量
        /// </summary>
        /// <param name="list1"></param>
        /// <param name="list"></param>
        /// <param name="ModeId"></param>
        private void SetParticipationStrength(List<ModeResultDemo> list1, List<ModeResultDemo> list, string ModeId)
        {
            var Obj = (from p in DtAllResult.AsEnumerable()
                       where p.Field<string>("ModeId") == ModeId
                       group p by new { Key = p.Field<string>("ExerciseDate"), Value = p.Field<string>("AvgForce"), modeId = p.Field<string>("ModeId") } into g
                       select new
                       {
                           g.Key.Key,
                           g.Key.Value
                       }
                );

            foreach (var item in list1)
            {
                ModeResultDemo d = new ModeResultDemo();
                foreach (var item1 in Obj)
                {
                    if (item.Key == item1.Key)
                    {
                        item.Value = item1.Value;
                    }
                }
                d.Key = Convert.ToDateTime(item.Key.ToString()).ToString("yyyy-MM-dd");
                d.Value = item.Value.ToString();
                list.Add(d);
            }
        }

        #endregion

        #region 活动范围
        /// <summary>
        /// 活动范围
        /// </summary>
        /// <param name="list1"></param>
        /// <param name="list"></param>
        /// <param name="ModeId"></param>
        private void SetRange(List<ModeResultDemo> list1, List<ModeResultDemo> list, string ModeId)
        {
            var Obj = (from p in DtAllResult.AsEnumerable()
                       where p.Field<string>("ModeId") == ModeId
                       group p by new { Key = p.Field<string>("ExerciseDate"), Value = p.Field<string>("RealMaxAngle"), modeId = p.Field<string>("ModeId") } into g
                       select new
                       {
                           g.Key.Key,
                           g.Key.Value
                       }
                );

            foreach (var item in list1)
            {
                ModeResultDemo d = new ModeResultDemo();
                foreach (var item1 in Obj)
                {
                    if (item.Key == item1.Key)
                    {
                        item.Value = item1.Value;
                    }
                }
                d.Key = Convert.ToDateTime(item.Key.ToString()).ToString("yyyy-MM-dd");
                d.Value = item.Value.ToString();
                list.Add(d);
            }
        }
        #endregion

        #region 测试活动范围
        /// <summary>
        /// 活动范围
        /// </summary>
        /// <param name="list1"></param>
        /// <param name="list"></param>
        /// <param name="ModeId"></param>
        private void Set_Range(List<ModeResultDemo> list1, List<ModeResultDemo> list, string ModeId)
        {
            var Obj = (from p in DtAllResult.AsEnumerable()
                       where p.Field<string>("ModeId") == ModeId
                       group p by new { Key = p.Field<string>("ExerciseDate"), Value = p.Field<string>("RealMaxAngle"), Value2 = p.Field<string>("RealMinAngle"), modeId = p.Field<string>("ModeId") } into g
                       select new
                       {
                           g.Key.Key,
                           g.Key.Value,
                           g.Key.Value2
                       }
                );

            foreach (var item in list1)
            {
                ModeResultDemo d = new ModeResultDemo();
                foreach (var item1 in Obj)
                {
                    if (item.Key == item1.Key)
                    {
                        item.Value = item1.Value;
                        item.Value2 = item1.Value2;
                    }
                }
                d.Key = Convert.ToDateTime(item.Key.ToString()).ToString("yyyy-MM-dd");
                d.Value = item.Value.ToString();
                d.Value2 = item.Value2.ToString();
                list.Add(d);
            }
        }
        #endregion

        #region 获得总训练图表集合 2013-11-4 ADD
        /// <summary>
        /// 获得总训练图表集合 2013-11-4 ADD
        /// </summary>
        /// <param name="queryable"></param>
        /// <param name="PatientID"></param>
        /// <param name="ExItemID"></param>
        /// <param name="StartTime"></param>
        /// <param name="ModeId"></param>
        /// <returns></returns>
        public DataTable GetAllTrainResult(System.String patientId, System.String ExItemID, System.DateTime StartTime, System.DateTime EndTime)
        {
            DataTable tbl = new DataTable();
            tbl.Columns.Add("StartTime", typeof(string));            //开始时间
            tbl.Columns.Add("ExMinutes", typeof(string));            //用时
            tbl.Columns.Add("AvgForce", typeof(string));             //参与力量
            tbl.Columns.Add("ExerciseDate", typeof(string));          //操作时间
            tbl.Columns.Add("ModeId", typeof(string));               //模式
            tbl.Columns.Add("RealMinAngle", typeof(string));          //最小角度
            tbl.Columns.Add("RealMaxAngle", typeof(string));          //最大角度


            var list = (from od in MySession.Query<Exerciserecord>()
                       where od.PatientId == patientId && od.ActionId == Convert.ToInt32(ExItemID) && od.StartTime > StartTime && od.EndTime < EndTime
                       select od).ToList();


            var maxTime = (from p in list
                           group p by new
                           {
                               p.PatientId,
                               p.ActionId,
                               p.ModeId,
                               p.ExerciseDate,
                               p.RealMaxAngle,
                               p.RealMinAngle
                           } into g
                           select new
                           {
                               time = g.Max(a => a.StartTime),
                               g.Key.PatientId,
                               g.Key.ActionId,
                               g.Key.ModeId,
                               g.Key.RealMaxAngle,
                               g.Key.RealMinAngle

                               
                           }).ToList();

            var obj = (from p in list
                       from m in maxTime
                       where (p.StartTime == m.time && p.PatientId == m.PatientId && p.ActionId == m.ActionId && p.ModeId == m.ModeId)
                       select new
                       {
                           p.StartTime,             //开始时间
                           p.ExMinutes,             //用时
                           p.AvgForce,              //最大力
                           p.ExerciseDate,           //操作时间
                           p.ModeId,                //模式
                           p.RealMaxAngle,           //最大角度
                           p.RealMinAngle            //最小角度

                       }).OrderBy(x => x.StartTime);
            foreach (var item in obj)
            {
                tbl.Rows.Add(item.StartTime.ToString(), item.ExMinutes, item.AvgForce, item.ExerciseDate, item.ModeId, item.RealMinAngle, item.RealMaxAngle);
            }

            return tbl;
        }
        #endregion

        #endregion

        #endregion
    }

    public class RecordData
    {
        private double yMin;        
        public double YMin { get { return yMin; } set { yMin = value; } }

        private double yMax;
        public double YMax { get { return yMax; } set { yMax = value; } }

        private double x;
        public double X { get { return x; } set { x = value; } }
    }
}
