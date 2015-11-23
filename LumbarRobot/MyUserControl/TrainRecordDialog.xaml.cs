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
using LumbarRobot.DAL;
using LumbarRobot.Data;
using Microsoft.Research.DynamicDataDisplay;
using Microsoft.Research.DynamicDataDisplay.DataSources;
using Microsoft.Research.DynamicDataDisplay.Charts.Navigation;
using Microsoft.Research.DynamicDataDisplay.ViewportRestrictions;
using Microsoft.Research.DynamicDataDisplay.Charts;
using Microsoft.Research.DynamicDataDisplay.Common.Auxiliary;
using Microsoft.Research.DynamicDataDisplay.Common;

namespace LumbarRobot.MyUserControl
{
    /// <summary>
    /// TrainRecordDialog.xaml 的交互逻辑
    /// </summary>
    public partial class TrainRecordDialog : UserControl
    {

        #region 变量
        
        #endregion

        #region 事件
        public event EventHandler Close;
        #endregion

        #region 参数
        Exerciserecord record;

        public Exerciserecord Record
        {
            get { return record; }
            set { record = value; }
        }
        #endregion

        #region 构造
        public TrainRecordDialog()
        {
            InitializeComponent();
        }
        #endregion

        #region 加载事件
        private void plotter_Loaded(object sender, RoutedEventArgs e)
        {
            innerPlotter.ViewportBindingConverter = new InjectedPlotterVerticalSyncConverter(innerPlotter);

            if (Record != null && !string.IsNullOrEmpty(Record.Id))
            {
                Record = MySession.Session.Get<Exerciserecord>(Record.Id);

                if (Record != null)
                {
                    float[] targetLine = null;
                    float[] actionLine = null;
                    float[] powerLine = null;
                    int xLength = int.MaxValue;

                    if (Record.Record1 != null && !string.IsNullOrEmpty(Record.Record1)) //目标轨迹
                    {
                        var temp = record.Record1.Split('|');
                        targetLine = new float[temp.Length];
                        for (int i = 0; i < temp.Length; i++)
                        {
                            if (!string.IsNullOrEmpty(temp[i]))
                            {
                                targetLine[i] = Convert.ToSingle(temp[i]);
                            }
                        }
                        if (temp.Length < xLength) xLength = temp.Length;
                    }

                    if (Record.Record2 != null && !string.IsNullOrEmpty(Record.Record2)) //实际运动轨迹
                    {
                        var temp = record.Record2.Split('|');
                        actionLine = new float[temp.Length];
                        for (int i = 0; i < temp.Length; i++)
                        {
                            if (!string.IsNullOrEmpty(temp[i]))
                            {
                                actionLine[i] = Convert.ToSingle(temp[i]);
                            }
                        }
                        if (temp.Length < xLength) xLength = temp.Length;
                    }

                    if (Record.Record3 != null && !string.IsNullOrEmpty(Record.Record3)) //力量
                    {
                        var temp = record.Record3.Split('|');
                        powerLine = new float[temp.Length];
                        for (int i = 0; i < temp.Length; i++)
                        {
                            if (!string.IsNullOrEmpty(temp[i]))
                            {
                                powerLine[i] = Convert.ToSingle(temp[i]);
                            }
                        }
                        if (temp.Length < xLength) xLength = temp.Length;
                    }

                    if (xLength < int.MaxValue)
                    {
                        //添加X轴
                        var xs = Enumerable.Range(1, xLength).Select(i => i * 1).ToArray();
                        //var xds = xs.AsXDataSource();

                        plotter.Visible = DataRect.Create(0, -60, xLength, 60);

                        //添加目标轨迹
                        if (targetLine != null)
                        {
                            var target = targetLine.Take(xLength).ToArray();
                            //var tl = target.Join(xds);
                            var tl = new List<Point>();
                            for (int i = 0; i < xLength; i++)
                            {
                                Point p = new Point(xs[i], target[i]);
                                tl.Add(p);
                            }
                            var ds = new EnumerableDataSource<Point>(tl);
                            ds.SetXYMapping(pt => pt);
                            plotter.AddLineGraph(ds, Colors.Red, 2, "目标轨迹");
                        }

                        //添加运动轨迹
                        if (actionLine != null)
                        {
                            var action = actionLine.Take(xLength).ToArray();
                            //var al = action.Join(xds);
                            var al = new List<Point>();
                            for (int i = 0; i < xLength; i++)
                            {
                                Point p = new Point(xs[i], action[i]);
                                al.Add(p);
                            }
                            var ds = new EnumerableDataSource<Point>(al);
                            ds.SetXYMapping(pt => pt);
                            plotter.AddLineGraph(ds, Colors.Blue, 2, "运动轨迹");
                        }

                        //添加力量
                        if (powerLine != null)
                        {
                            var power = powerLine.Take(xLength).ToArray();
                            //var pl = power.Join(xds);
                            var pl = new List<Point>();
                            for (int i = 0; i < xLength; i++)
                            {
                                Point p = new Point(xs[i], power[i]);
                                pl.Add(p);
                            }
                            var ds = new EnumerableDataSource<Point>(pl);
                            ds.SetXYMapping(pt => pt);
                            innerPlotter.AddLineGraph(ds, Colors.Yellow, 2, "力量");
                        }
                    }


                    //添加分组训练信息（图2）
                    if (record.GroupRecord != null)
                    {
                        List<ClusteredBarData> barData = new List<ClusteredBarData>();

                        var group = record.GroupRecord.Split('|');
                        var groupRecords = new List<RecordData>();
                        for (int i = 0; i < group.Length; i++)
                        {
                            ClusteredBarData cbd = new ClusteredBarData();
                            var temp = group[i].Split(',');
                            BarData bd = new BarData { YMin = Convert.ToSingle(temp[1]), YMax = Convert.ToSingle(temp[2]) };
                            bd.BrushColor = Colors.Blue;
                            cbd.X = Convert.ToInt32(temp[0]);
                            cbd.BarDatas = new List<BarData>();
                            cbd.BarDatas.Add(bd);
                            barData.Add(cbd);
                        }

                        EnumerableDataSource<ClusteredBarData> ds = new EnumerableDataSource<ClusteredBarData>(barData);
                        ds.SetXMapping(x => x.X);
                        ds.SetYMapping(y => y.BarDatas[0].YMax);
                        DataContext = ds;
                    }
                }

            }
        }
        #endregion

        #region 关闭
        private void btnClosed_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.Close(sender, e);
        }
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
