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
using System.Threading;

namespace LumbarRobot.ActionUserControl
{
    /// <summary>
    /// TimeSina.xaml 的交互逻辑
    /// </summary>
    public partial class TimeSina : UserControl
    {
        public TimeSina()
        {
            InitializeComponent();
        }


        #region 变量定义


        double TempHeight = 0;//可用区域高度
        double tempYtep = -15;//不可用区域
        double ArcPixel = 0;//每个角度所占的像素


        private double _LineNumb = 10;
        /// <summary>
        /// 线的数目
        /// </summary>
        public double LineNumb
        {
            get { return _LineNumb; }
            set { _LineNumb = value; }
        }

        /// <summary>
        /// 高度改变时增量
        /// </summary>
        private double HeightStep = 50; 

        private Double _MaxArc = 50;
        /// <summary>
        /// 最大角度
        /// </summary>
        public Double MaxArc
        {
            get { return _MaxArc; }
            set
            {
                _MaxArc = Math.Ceiling(value + (HeightStep - Math.Ceiling(value) % HeightStep));
                ComputerArea();                
                DrawBack();
                AddGoleLine();
                GoleLineMove();
                //AddEliPoint();
            }
        }

        List<double> ld = new List<double>();
        /// <summary>
        /// 目标线数据源
        /// </summary>
        PointCollection mypoint = new PointCollection();
        /// <summary>
        /// 随点移动的线
        /// </summary>
        Line LinkLine;
        /// <summary>
        /// 实时曲线
        /// </summary>
        Polyline polyline1;
        /// <summary>
        /// 第二条实时区线
        /// </summary>
        Polyline RealTimeLine;
        /// <summary>
        /// 第三条实时区线
        /// </summary>
        Polyline Ac_MinArcLine;
        /// <summary>
        /// 目标线
        /// </summary>
        Polyline polyline2;
        Polyline polyline3;
        /// <summary>
        /// 运动的点
        /// </summary>
        Ellipse eli;
        Ellipse eliTwo;
        Ellipse eliTheed;

        /// <summary>
        /// 显示当前时实值
        /// </summary>
        Label OneValueLabel;
        Label TwoValueLabel;
        Label ThreeValueLabel;

        /// <summary>
        /// 运动红点的直径
        /// </summary>
        private double eliWH = 6;
        /// <summary>
        /// 距右边宽度
        /// </summary>
        private double RightW = 45;
        /// <summary>
        /// 显示的单位
        /// </summary>
        private String _Uint = "度";
        /// <summary>
        /// 单位
        /// </summary>
        public String Uint
        {
            get { return _Uint; }
            set
            {
               // this.Uint = null;
                DrawBack();
                _Uint = value;
               
            }
        }


        private int startxy = 40;//图上的画线开始位置
        private int timeX = 40;//X轴的开始位置 
       

        private List<Point> dataY = new List<Point>();
        /// <summary>
        /// 没有经过计算原始的角度值集合
        /// </summary>
        List<double> AC_Arc = new List<double>();

        /// <summary>
        /// 没有经过计算原始的角度值集合
        /// </summary>
        List<double?> AC_ArcTwo = new List<double?>();

        /// <summary>
        /// 最小值时实曲线
        /// </summary>
        List<double?> AC_MinArcList = new List<double?>();

        /// <summary>
        /// 目标线
        /// </summary>
        private List<Point> GoleLine = new List<Point>();
        /// <summary>
        /// 目标原始数据
        /// </summary>
        private List<double> GoleData = new List<double>();
        /// <summary>
        /// 实进数据数据源
        /// </summary>
        private PointCollection myPoints1 = new PointCollection();

        private object lockobject = new object();

        public double tempy = 0;

       

     
        /// <summary>
        /// 背景线颜色
        /// </summary>
        public Brush BacklineColor { get; set; }
        #endregion

        #region 方法
        
       


        /// <summary>
        /// 清除背景控件
        /// </summary>
        public void Cle()
        {
            this.MyCanvas.Children.Clear();
          
            
        }

        /// <summary>
        /// 复位 
        /// <param name="IsClearGoleDate">为flase时为只初始化刻度而不清除目标线</param>
        /// </summary>
        public void ReSet(bool IsClearGoleDate=true)
        {
            this.MaxArc = 50;
            timeX = startxy;
            eliTwo.Visibility = Visibility.Hidden;
            eliTheed.Visibility = Visibility.Hidden;
            AC_Arc.Clear();
            AC_ArcTwo.Clear();
            dataY.Clear();          
            myPoints1.Clear();
            mypoint.Clear();
            
            DrawBack();
            if (IsClearGoleDate)
            {
                GoleLine.Clear();
                GoleData.Clear();
            }
            else
            {
                GoldLine(GoleData.ToArray());
            }
        }

        /// <summary>
        /// 画背景线
        /// </summary>
        private void DrawBack()
        {
         
            Cle();
            #region 数动线不动
            double j = MaxArc;
            double numbHeight = TempHeight / LineNumb;//每条线的间距
            for (double i = 0; i <= LineNumb; i++)
            {
                Line Line = new Line();
                Line.Stroke = BacklineColor;
                Line.StrokeThickness = 1;
                Label lb = new Label();
                lb.HorizontalAlignment = HorizontalAlignment.Center;
                lb.Content = j + this.Uint;
                if (i == 0)
                {
                    lb.Margin = new Thickness(-2, i * numbHeight - 8, 0, 0);
                }
                else
                {
                    lb.Margin = new Thickness(-2, i * numbHeight - 15, 0, 0);
                }
                Line.X1 = startxy;
                Line.Y1 = i * numbHeight + tempYtep;
                Line.X2 = this.Width;
                Line.Y2 = i * numbHeight + tempYtep;
                this.MyCanvas.Children.Add(Line);
                this.MyCanvas.Children.Add(lb);
                j -= Math.Ceiling( MaxArc*2/LineNumb);//显示左边的数字
            }
            #endregion
           DarwPolyline();
            
        }
       
        /// <summary>
        /// 加载页面元素
        /// </summary>
        private void DarwPolyline()
        {
            this.AddGoleLine();
            this.AddRealTimeLine();
            this.AddEliPoint();
            AddLabelPoint();
            LinkLine = new Line();
            LinkLine.X1 = startxy;
            LinkLine.X2 = startxy;
            LinkLine.Y1 = 0;
            LinkLine.Y2 = this.Height - this.tempYtep;
            LinkLine.Stroke = new SolidColorBrush(Color.FromRgb(128, 128, 105));
            LinkLine.StrokeThickness = 2;
            this.MyCanvas.Children.Add(LinkLine);

           
        }
        /// <summary>
        /// 实时曲线
        /// </summary>jf
        /// <param name="arc">第一条线值</param>
        /// <param name="arcTwo">第二条线值</param>
        /// <param name="Minarc">第三条线值</param>
        /// <param name="MaxValue">最大值</param>
        /// <param name="MinValue">最小值</param>
        public void DrawRunLine(double arc,double? arcTwo=null,double? Minarc=null,string MaxValue=null,string MinValue=null)
        {

            if (arcTwo != null && Minarc != null && MaxValue != null && MinValue != null)
            {
                eliTwo.Visibility = Visibility.Visible;
                eliTheed.Visibility = Visibility.Visible;
                double marx = Math.Max(Math.Max(Math.Abs(arc), Math.Abs((double)arcTwo)), Math.Abs((double)Minarc));
                marx = Math.Abs(marx);
                if (marx >= this.MaxArc)
                {
                    this.MaxArc = marx;
                }
                if (arcTwo >= Minarc)
                {
                    DrawSina(arc, arcTwo, Minarc, MaxValue, MinValue);
                }
                else
                {
                    DrawSina(arc, Minarc, arcTwo);
                }


            }
            else
            {
                eliTwo.Visibility = Visibility.Hidden;
                eliTheed.Visibility = Visibility.Hidden;
                if (Math.Abs(arc) > this._MaxArc)
                {

                    this.MaxArc = Math.Abs(arc);// GetMaxArcByInpuArc(arc);

                    DrawBack();
                    DrawSina(arc);
                    //AddGoleLine();
                    GoleLineMove();
                }
                else
                {
                    DrawSina(arc);
                }
            }
        }
     
        /// <summary>
        /// 添加目标线
        /// </summary>
        private void AddGoleLine()
        {
            polyline2 = new Polyline();
            polyline2.StrokeThickness = 15;
            polyline2.Opacity = 0.5;
            // polyline2.FillRule = FillRule.EvenOdd;
            polyline2.Stroke = new SolidColorBrush(Color.FromRgb(34, 139, 34));
            this.MyCanvas.Children.Add(polyline2);

            polyline3 = new Polyline();
            polyline3.StrokeThickness = 3;
            //polyline3.FillRule = FillRule.EvenOdd;
            polyline3.Opacity = 0.5;
            polyline3.Stroke = new SolidColorBrush(Color.FromRgb(255, 255, 255));
            this.MyCanvas.Children.Add(polyline3);
        }
        /// <summary>
        /// 添加实时曲线
        /// </summary>
        private void AddRealTimeLine()
        {
            polyline1 = new Polyline();
            polyline1.StrokeThickness = 3;
            // polyline1.FillRule = FillRule.EvenOdd;
            polyline1.Stroke = new SolidColorBrush(Color.FromRgb(203, 133, 82));

            RealTimeLine = new Polyline();
            RealTimeLine.StrokeThickness = 3;
            // polyline1.FillRule = FillRule.EvenOdd;
            RealTimeLine.Stroke = new SolidColorBrush(Color.FromRgb(20, 13, 82));


            Ac_MinArcLine = new Polyline();
            Ac_MinArcLine.StrokeThickness = 3;
            // polyline1.FillRule = FillRule.EvenOdd;
            Ac_MinArcLine.Stroke = new SolidColorBrush(Color.FromRgb(20, 13, 82));

            this.MyCanvas.Children.Add(polyline1);
            this.MyCanvas.Children.Add(RealTimeLine);
            this.MyCanvas.Children.Add(Ac_MinArcLine);
        }

        /// <summary>
        /// 添加运动小球
        /// </summary>
        private void AddEliPoint()
        {
            eli = new Ellipse();
            eli.Margin = new Thickness(startxy - eliWH / 2, TempHeight / 2 - eliWH / 2, 0, 0);
            eli.HorizontalAlignment = HorizontalAlignment.Left;
            eli.VerticalAlignment = VerticalAlignment.Top;
            eli.Fill = System.Windows.Media.Brushes.Red;
            eli.StrokeThickness = 4;
            eli.Stroke = System.Windows.Media.Brushes.Red; //边，金黄色
            eli.Width = eliWH;
            eli.Height = eliWH;
            
            this.MyCanvas.Children.Add(eli);

            eliTwo = new Ellipse();
            eliTwo.Margin = new Thickness(startxy - eliWH / 2, TempHeight / 2 - eliWH / 2, 0, 0);
            eliTwo.HorizontalAlignment = HorizontalAlignment.Left;
            eliTwo.VerticalAlignment = VerticalAlignment.Top;
            eliTwo.Fill = System.Windows.Media.Brushes.Blue;
            eliTwo.StrokeThickness = 4;
            eliTwo.Stroke = System.Windows.Media.Brushes.Blue; //边，金黄色
            eliTwo.Width = eliWH;
            eliTwo.Height = eliWH;
            eliTwo.Visibility= Visibility.Hidden;
            this.MyCanvas.Children.Add(eliTwo);

            //第三条线的点
            eliTheed = new Ellipse();
            eliTheed.Margin = new Thickness(startxy - eliWH / 2, TempHeight / 2 - eliWH / 2, 0, 0);
            eliTheed.HorizontalAlignment = HorizontalAlignment.Left;
            eliTheed.VerticalAlignment = VerticalAlignment.Top;
            eliTheed.Fill = System.Windows.Media.Brushes.Blue;
            eliTheed.StrokeThickness = 4;
            eliTheed.Stroke = System.Windows.Media.Brushes.Blue; //边，金黄色
            eliTheed.Width = eliWH;
            eliTheed.Height = eliWH;
            eliTheed.Visibility = Visibility.Hidden;
            this.MyCanvas.Children.Add(eliTheed);
        }

        /// <summary>
        /// 添加运动数字
        /// </summary>
        private void AddLabelPoint()
        {
            OneValueLabel = new Label();
            this.MyCanvas.Children.Add(OneValueLabel);

            TwoValueLabel = new Label();
            this.MyCanvas.Children.Add(TwoValueLabel);

            ThreeValueLabel = new Label();
            this.MyCanvas.Children.Add(ThreeValueLabel);
        }
      

        public void DrawRunLine(Point point)
        {
            myPoints1.Add(point);
        }
        /// <summary>
        /// 目标线
        /// <param name="IsClear">为true清除目标线</param>
        /// </summary>
        public void GoldLine(double[] arc,bool IsClear=true)
        {
            //this.MyCanvas.RegisterName("gloeLine", mypoint);
            mypoint.Clear();
            if (IsClear)
            {
                GoleData.Clear();//清除目标数据
                GoleData.AddRange(arc);//添加新的目标线值
            }
           
            //this.MyCanvas.Children.Remove(polyline2);
            //this.MyCanvas.Children.Remove(polyline3);
            if (arc.Length < 1)
                return;
            double maxarc = (from max in arc select max).Max();
            if (maxarc > this.MaxArc)
            {
                this.MaxArc = maxarc;
            }

            for (int i = 0; i < arc.Length; i++)
            {
                Point p = new Point();
                p.Y = -arc[i] * ArcPixel + this.TempHeight / 2 + this.tempYtep;
                p.X = i + startxy;
               // GoleLine.Add(p);
                if (i + startxy > this.Width)
                {
                    //mypoint.Add(p);
                }
                else
                {
                    mypoint.Add(p);
                }

            }
            polyline2.Points = mypoint;
            polyline3.Points = mypoint;
            
            //this.MyCanvas.Children.Add(polyline2);
            //this.MyCanvas.Children.Add(polyline3);
           // mypoint.Clear();
        }
        /// <summary>
        /// 画时间时间曲线
        /// </summary>
        /// <param name="arc">第一条线</param>
        /// <param name="arcTwo">第二条线</param>
        /// <param name="MinArc">第三条线</param>
        /// <param name="MaxValue">最大值</param>
        /// <param name="MinValue">最小值</param>
        public void DrawSina(double arc,double? arcTwo=null,double? MinArc=null,string MaxValue=null,string MinValue=null)
        {
            AC_Arc.Add(arc);

            if (arcTwo != null&&MinArc!=null)
            {
                if (AC_ArcTwo.Count < AC_Arc.Count)
                {
                    for (int i = 0; i < AC_Arc.Count - AC_ArcTwo.Count; i++)
                    {
                        AC_ArcTwo.Insert(0, 0);
                        AC_MinArcList.Insert(0, 0);
                    }
                }
                AC_ArcTwo.Add(arcTwo);
                AC_MinArcList.Add(MinArc);
            }

            if (timeX <= this.Width - RightW)
            {
                //没有超出所画区域范围
               
                timeX++; 

            }
            else
            {
                AC_Arc.RemoveAt(0);
                if (arcTwo != null&&AC_MinArcList!=null)
                {
                    AC_ArcTwo.RemoveAt(0);
                    AC_MinArcList.RemoveAt(0);
                }
                if (GoleData.Count > 0)
                {
                    GoleData.RemoveAt(0);
                }
                GoleLineMove();
                //超出所画区域范围
            }
            this.LinkLine.X1 = timeX;
            this.LinkLine.X2 = timeX;
            if (arcTwo != null && MinArc != null && MaxValue != null && MinValue!=null)
            {
                ReaTime(AC_Arc, AC_ArcTwo,this.AC_MinArcList,MaxValue,MinValue);
            }
            else
            {
                ReaTime(AC_Arc);
            }
               
           
            
        }
        /// <summary>
        /// 画时实区线
        /// </summary>
        /// <param name="ac"></param>
        private void ReaTime(List<double> ac, List<double?> acTwo = null, List<double?> acMinArc = null, string MaxValue = null, string MinValue = null)
        {
            if (ac.Count > 0)
            {
                double XX = startxy;
                PointCollection PcPoint = new PointCollection();
                double pointY = 0;//第一条线Y点
                PointCollection PcPointTwo = new PointCollection();
                double pointYTwo = 0;//第二条线Y点
                PointCollection acminarcPoints = new PointCollection();
                double acMinarcY = 0;//第三条线Y点
                for (int arc = 0; arc < ac.Count;arc++ )
                {
                    #region 第一条曲线
                    pointY = -ac[arc] * ArcPixel + this.TempHeight / 2 + this.tempYtep;
                    eli.Margin = new Thickness(timeX - eliWH / 2, pointY - eliWH / 2, 0, 0);
                    //OneValueLabel.Content = Math.Ceiling( ac[arc]).ToString();
                    //OneValueLabel.Margin = new Thickness(timeX - eliWH / 2, pointY - eliWH / 2, 0, 0);
                    Point pt = new Point();
                    pt.X = XX;
                    pt.Y = pointY;
                    PcPoint.Add(pt); 
                    #endregion
                    if (acTwo != null && acTwo.Count >= ac.Count && acMinArc!=null&&MaxValue!=null&&MinValue!=null)
                    {

                        #region 第二条曲线
                        pointYTwo = -(double)acTwo[arc] * ArcPixel + this.TempHeight / 2 + this.tempYtep;
                        eliTwo.Margin = new Thickness(timeX - eliWH / 2, pointYTwo - eliWH / 2, 0, 0);
                        if (MaxValue != null)
                        {
                            TwoValueLabel.Content = MaxValue;
                        }
                        TwoValueLabel.Margin = new Thickness(timeX - eliWH / 2, pointYTwo-20, 0, 0);
                        Point pttwo = new Point();
                        pttwo.X = XX;
                        pttwo.Y = pointYTwo;
                        PcPointTwo.Add(pttwo); 
                        #endregion

                        #region 第三条曲线
                        acMinarcY = -(double)AC_MinArcList[arc] * ArcPixel + this.TempHeight / 2 + this.tempYtep;
                        eliTheed.Margin = new Thickness(timeX - eliWH / 2, acMinarcY - eliWH / 2, 0, 0);
                        if (MinValue != null)
                        {
                            ThreeValueLabel.Content = MinValue;
                        }
                        ThreeValueLabel.Margin = new Thickness(timeX - eliWH / 2, acMinarcY - eliWH, 0, 0);
                        Point AcminArcPoint = new Point();
                        AcminArcPoint.X = XX;
                        AcminArcPoint.Y = acMinarcY;
                        acminarcPoints.Add(AcminArcPoint); 
                        #endregion
                    }
                    XX++;

                }            

                polyline1.Points = PcPoint;
                RealTimeLine.Points = PcPointTwo;
                Ac_MinArcLine.Points = acminarcPoints;
                
            }
        }

        /// <summary>
        /// 画周期线
        /// </summary>
        public void DrwPI()
        {
            ld.ToArray();
        }

       
        /// <summary>
        /// 画目标线
        /// </summary>
        private void GoleLineMove()
        {
            this.MyCanvas.Children.Remove(polyline2);
            this.MyCanvas.Children.Remove(polyline3);
            if (GoleData.Count > 0)
            {

                //DrawBack();
               
                
                double XX = startxy;
                PointCollection PcPoint = new PointCollection();
                double pointY = 0;
                foreach (double arc in GoleData)
                {
                    if (XX <= this.Width)
                    {
                        pointY = -arc * ArcPixel + this.TempHeight / 2 + this.tempYtep;
                     
                       // eli.Margin = new Thickness(timeX - eliWH / 2, pointY - eliWH / 2, 0, 0);

                        Point pt = new Point();
                        pt.X = XX;
                        pt.Y = pointY;
                        PcPoint.Add(pt);
                        XX++;
                    }

                }
                this.MyCanvas.Children.Add(polyline2);
                this.MyCanvas.Children.Add(polyline3);
                polyline2.Points = PcPoint;
                polyline3.Points = PcPoint;
                
            }
        }
        /// <summary>
        /// 计算背景区域
        /// </summary>
        private void ComputerArea()
        {
            if (this.Height % LineNumb != 0)
            {
                TempHeight = this.Height - this.Height % LineNumb;
                tempYtep = Math.Floor((this.Height - TempHeight) / 4);//画线的起始角度
                
            }
            else
            {
                TempHeight = this.Height - this.Height % LineNumb;
                tempYtep = 0;
            }

            ArcPixel = TempHeight / (this.MaxArc * 2);//每个角度所占的像素
        }

        private void UserControl_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            ComputerArea();
            this.DrawBack();
        }

        #endregion

    }
}
