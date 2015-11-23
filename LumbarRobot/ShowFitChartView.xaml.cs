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
using System.Data;
using LumbarRobot.DAL;
using LumbarRobot.Data;
using LumbarRobot.ChartNodes;
using Microsoft.Research.DynamicDataDisplay.Charts;
using DynamicDataDisplay.Markers.DataSources;
using Microsoft.Research.DynamicDataDisplay.Navigation;
using Microsoft.Research.DynamicDataDisplay.Charts.Navigation;
using System.ComponentModel;
using Microsoft.Research.DynamicDataDisplay;
using Microsoft.Research.DynamicDataDisplay.DataSources;
using Microsoft.Research.DynamicDataDisplay.PointMarkers;
using System.Globalization;

namespace LumbarRobot
{
    /// <summary>
    /// ShowFitChartView.xaml 的交互逻辑
    /// </summary>
    public partial class ShowFitChartView : UserControl
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
        /// FitID
        /// </summary>
        public string ModeId;
        /// <summary>
        /// 是否返回
        /// </summary>
        public bool IsBack;
        #endregion

        #region 构造
        public ShowFitChartView(string dayTime,string modeid)
        {
            InitializeComponent();
            GetChartDemo(Convert.ToDateTime(dayTime), modeid);
            Column();
        }
        #endregion

        #region 比对的总集合
        /// <summary>
        /// Fit集合
        /// </summary>
        List<ModeResultDemo> DateResult1 = new List<ModeResultDemo>();

        #region 被动运动集合
        DataTable CkGuided_Range = new DataTable();
        #endregion

        #region 图表的总集合
        DataTable DtAllResult = new DataTable();

        /// <summary>
        /// 集合
        /// </summary>
        List<ModeResultDemo> RangeList = new List<ModeResultDemo>();
        #endregion

        #region 参数
        #region 中间对象
        class ModeResultDemo
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
        #endregion

        #region 动作名称
        /// <summary>
        /// 显示动作的名称
        /// </summary>
        public string FitName;
        #endregion

        #endregion

        #endregion

        #region 加载事件
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            if (FitName != null)
            {
                this.txExitemName.Text = FitName.ToString();
            }
        }
        #endregion

        #region 关闭
        private void btnClosed_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.Close(sender, e);
        }
        #endregion

        #region 方法

        #region 绑定数据
      
        void GetChartDemo(DateTime creatTime, string ModeId)
        {
          
            #region 数据绑定
            DtAllResult = GetAllTrainResult(ModuleConstant.PatientId, creatTime);
            #endregion

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
                    DateResult1.Add(d1);
                }
            #endregion

            Set_Range(DateResult1, RangeList, ModeId);
        }
        #endregion
        #endregion

        #region 柱状图
        /// <summary>
        /// 柱状图
        /// </summary>
        /// <param name="gvColumn"></param>
        /// <param name="ColumnScroll"></param>
        /// <param name="LineScroll"></param>
        public void Column()
        {
            #region 活动范围
            //活动范围
            if (RangeList.Count > 0)
            {
                
                List<ClusteredBarData> data = new List<ClusteredBarData>();
                Color[] colors = new Color[] { Colors.Red, Colors.Blue, Colors.Yellow };
               
                foreach (var item in RangeList)
                {
                    ClusteredBarData cbd = new ClusteredBarData();
                    List<BarData> barDatas = new List<BarData>();
                    string date = Convert.ToDateTime(item.Key).ToString("yyyy/MM/dd");
                    cbd.X = dateAxis.ConvertToDouble(Convert.ToDateTime(date));
                    BarData bd = new BarData { YMin =Convert.ToDouble(item.Value2), YMax =Convert.ToDouble(item.Value) };
                    bd.BrushColor = colors[0];
                    barDatas.Add(bd);
                    cbd.BarDatas = barDatas;
                    data.Add(cbd);
                }
                EnumerableDataSource<ClusteredBarData> value = new EnumerableDataSource<ClusteredBarData>(data);
                value.SetXMapping(k => k.X);
                value.SetYMapping(k => k.BarDatas[0].YMax);

                DataContext = value;
            }
            #endregion
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
        public DataTable GetAllTrainResult(System.String patientId, System.DateTime CreatTime)
        {
            DataTable tbl = new DataTable();

            tbl.Columns.Add("ExerciseDate", typeof(string));          //操作时间
            tbl.Columns.Add("ModeId", typeof(string));               //模式
            tbl.Columns.Add("RealMinAngle", typeof(string));          //最小角度
            tbl.Columns.Add("RealMaxAngle", typeof(string));          //最大角度


            var list = (from od in MySession.Query<FitRecord>()
                        where od.PatientID == patientId && od.CreateTime == CreatTime
                        select od).ToList();


            var maxTime = (from p in list
                           group p by new
                           {
                               p.PatientID,
                               p.ModeID,
                               p.CreateTime,
                               p.MaxAngle,
                               p.MinAngle
                           } into g
                           select new
                           {
                               g.Key.PatientID,
                               g.Key.CreateTime,
                               g.Key.ModeID,
                               g.Key.MinAngle,
                               g.Key.MaxAngle


                           }).ToList();

            var obj = (from p in list
                       from m in maxTime
                       where (p.CreateTime == m.CreateTime && p.PatientID == m.PatientID && p.ModeID == m.ModeID)
                       select new
                       {

                           p.CreateTime,           //操作时间
                           p.ModeID,                //模式
                           p.MaxAngle,           //最大角度
                           p.MinAngle            //最小角度

                       }).OrderBy(x => x.CreateTime);
            foreach (var item in obj)
            {
                tbl.Rows.Add(item.CreateTime, item.ModeID, item.MinAngle, item.MaxAngle);
            }

            return tbl;
        }
        #endregion

    }
}
