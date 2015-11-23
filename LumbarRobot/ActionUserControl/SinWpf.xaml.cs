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

namespace LumbarRobot.ActionUserControl
{
    /// <summary>
    /// SinWpf.xaml 的交互逻辑
    /// </summary>
    public partial class SinWpf : UserControl
    {
        public SinWpf()
        {
            InitializeComponent();
        }

        private double _MaxArc=120;
        /// <summary>
        /// 最大角度
        /// </summary>
        public double MaxArc
        {
            get { return _MaxArc; }
            set { _MaxArc = value; }
        }

        /// <summary>
        /// x轴起点从标
        /// </summary>
        private double StartXValue = 25;
        /// <summary>
        /// Y轴起点坐标
        /// </summary>
        private double StartYValue = 5;

        /// <summary>
        /// 背景颜色
        /// </summary>
        public Brush BackColor { get; set; }

        /// <summary>
        /// 折线颜色
        /// </summary>
        public Brush LineColor { get; set; }
        /// <summary>
        /// 周期线颜色
        /// </summary>
        public Brush PILineColor { get; set; }

        /// <summary>
        /// 字体颜色
        /// </summary>
        public Brush FontColor { get; set; }

        /// <summary>
        /// 背景线颜色
        /// </summary>
        public Brush BacklineColor { get; set; }

        /// <summary>
        /// 存点的集合
        /// </summary>
        PointCollection ponints = new PointCollection();

        /// <summary>
        /// 多少个点开始清理前一点
        /// </summary>
        public int parmer = 3500;


        private int _RunPI = 1;
        /// <summary>
        /// 一共同个周其
        /// </summary>
        public int RunPI
        {
            get { return _RunPI; }
            set
            { 
                _RunPI = value;
                DrawPI();
            }
        }

        /// <summary>
        /// 记录周其数
        /// </summary>
        private List<Point> ListPoint = new List<Point>();

        /// <summary>
        /// 画背景
        /// </summary>
        private void DrawBack()
        {
            this.back.Children.Clear();
            this.back.Background = BackColor;
            int tempvalue = 0;
            if (MaxArc % 20 > 0)
            {
                tempvalue = (int)(MaxArc + (20 - (MaxArc % 20)))/2;
            }
            else
            {
                tempvalue =(int) MaxArc/2;
            }

            int temp =(int) this.Height / (tempvalue*2);          

            for (double i = 0; i <= this.Height; i += temp*15)
            {
                Line li = new Line();
                li.Stroke = BacklineColor;
                li.StrokeThickness = 1;
                li.X1 = StartXValue;
                li.Y1 = i + StartYValue;
                li.X2 = this.Width;
                li.Y2 = i + StartYValue;
                Label lb = new Label();
                lb.Content = (int)tempvalue;
                tempvalue -= 10;
                lb.Margin = new Thickness(0, i-7,0,0);
                this.back.Children.Add(lb);
                this.back.Children.Add(li);

            }

        }

        /// <summary>
        /// 得到每个角度占的高度像素
        /// </summary>
        /// <returns></returns>
        private double Getheiht()
        {
          return  this.Height / MaxArc;
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            DrawBack();
        }

        /// <summary>
        /// 根据角度画线
        /// </summary>
        /// <param name="Arc"></param>
        public void DrawRunLine(double Arc,bool ISPI=false)
        {
            Polyline backline = new Polyline();
            backline.Stroke = new SolidColorBrush(Color.FromRgb(251, 154, 6));   
            backline.StrokeThickness = 1;
            if (ponints.Count > 0)
            {
                
                var tempxv = this.Width / ponints.Count;
                PointCollection newpointcon = new PointCollection();
                List<Point> pt = new List<Point>();
                if (ponints.Count > parmer)
                {
                    ponints.RemoveAt(0);
                    ListPoint.RemoveAt(0);
                    GC.Collect();
                    
                }

                double x = StartXValue;
                //遍历点
                foreach (Point ps in ponints)
                {
                    Point p1 = new Point();
                    p1.X = x;
                    x += tempxv;
                    p1.Y = ps.Y;
                    newpointcon.Add(p1);
                   
                    
                }
                foreach (Point u in this.ListPoint)
                {
                    Point p2 = new Point();
                    p2.X =u.X;
                    x += tempxv;
                    p2.Y = u.Y;
                    pt.Add(p2);
                }

                ListPoint.Clear();
                ponints.Clear();
                if (!ISPI)
                {
                    //这点没有周期线
                    newpointcon.Add(new Point(this.Width, Arc * (-1) * Getheiht() + this.Height / 2));

                    ListPoint.Add(new Point(0, 0));
                   
                }
                else
                {
                    //有周期线
                    newpointcon.Add(new Point(this.Width, Arc * (-1) * Getheiht() + this.Height / 2));
                    ListPoint.Add(new Point(0, this.Width));
                   
                }
               
                //if(newpointcon.Count>
                ponints = newpointcon;
                ListPoint = pt;
            }
            else
            {
                Point p = new Point();
                p.X = StartXValue;
                p.Y = Arc*Getheiht()+this.Height/2;
                ponints.Add(p);
            }

            backline.Points = ponints;
            this.back.Children.Add(backline);
            DrawPI2();
        }

        /// <summary>
        /// 画周期
        /// </summary>
        public void DrawPI()
        {

            double temppi = (this.Width - StartXValue) / RunPI;

            int countnumb = RunPI;
           this.back.Children.Clear();
            DrawBack();
            DrawRunLine(0, true);
            for (double i = countnumb; i >0 ; i --)
            {
                Label lb = new Label();
                lb.Content = i;
                //lb.FontStyle=new SystemColors
                lb.Margin = new Thickness(i * temppi + StartXValue, 0, 0, 0);
                Line li = new Line();
           
                DoubleCollection dc = new DoubleCollection();
                dc.Add(5);
                dc.Add(2);
                li.StrokeDashArray = dc;
                li.Stroke =PILineColor;
                li.StrokeThickness = 1;
                li.X1 = i * temppi + StartXValue;
                li.Y1 = StartYValue;
                li.X2 = i * temppi + StartXValue;
                li.Y2 = this.Height;
                this.back.Children.Add(lb);
                this.back.Children.Add(li);
               
            }
        }

        public void DrawPI2()
        {
            //foreach (UIElement c in this.back.Children)
            //{
            //    if (c is Line)
            //    {
            //        this.back.Children.Remove(c);
            //    }
            //}
            foreach (Point p in ListPoint)
            {
                if (p.X == 1)
                {
                    Line li = new Line();
                    DoubleCollection dc = new DoubleCollection();
                    dc.Add(5);
                    dc.Add(2);
                    li.StrokeDashArray = dc;
                    li.Stroke = PILineColor;
                    li.StrokeThickness = 1;
                    li.X1 = p.Y;
                    li.X2 = p.Y;
                    li.Y1 = 0;
                    li.Y2 = this.Height;
                    this.back.Children.Add(li);
                }
            }
            
        }
        /// <summary>
        /// 复位控件
        /// </summary>
        public void ReSet()
        {
            ponints.Clear();
            DrawBack();
        }
        
    }

   
}
