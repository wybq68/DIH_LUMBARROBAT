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
    /// LineChar.xaml 的交互逻辑
    /// </summary>
    public partial class LineChar : UserControl
    {
        public LineChar()
        {
            InitializeComponent();
            Init();
        }


        #region 定义变量
        private Brush _BacklineColor = new SolidColorBrush(Colors.Green);
        /// <summary>
        /// 背景线颜色
        /// </summary>
        public Brush BacklineColor { get; set; }
        /// <summary>
        /// 第一条线
        /// </summary>
        Polyline LineOne;
        /// <summary>
        /// 第二条线
        /// </summary>
        Polyline LineTwo;
        /// <summary>
        /// 第三条线
        /// </summary>
        Polyline LineThreed;
        /// <summary>
        /// 第一条线数据源
        /// </summary>
        PointCollection pontListOne=new PointCollection();
        /// <summary>
        /// 第二条线数据源
        /// </summary>
        PointCollection pontListTwo=new PointCollection();
        /// <summary>
        /// 第三条线数据源
        /// </summary>
        PointCollection PontListThreed=new PointCollection();

        private double _Maxarc = 50;
        /// <summary>
        /// 表示的最大数值
        /// </summary>
        public double Maxarc
        {
            get { return _Maxarc; }
            set 
            {
                temph = this.TempHeight / value;
                _Maxarc = value;
            }
        }
        /// <summary>
        /// 单位值所占的像数
        /// </summary>
        private double temph = 0;

        private int _LineNumb = 5;
        /// <summary>
        /// 线的数量
        /// </summary>
        public int LineNumb
        {
            get { return _LineNumb; }
            set { _LineNumb = value; }
        }

        /// <summary>
        /// 左开始的位置
        /// </summary>
        private double startValue=25;

        /// <summary>
        /// 高度
        /// </summary>
        public double Heght = 0;
        /// <summary>
        /// 宽度
        /// </summary>
        public double Width = 0;

        /// <summary>
        /// 实际可用区域
        /// </summary>
        private double TempHeight=0;
        /// <summary>
        /// 可用区域宽度
        /// </summary>
        private double TempWidth = 0;



        #endregion


        private void Init()
        {
            this.Width = 680;
            this.Height = 180;
            this.MaxHeight = this.Height;
            this.MaxWidth = this.Width;
            temph = this.Height / (this.Maxarc*2);
            ComputerArea();
            DrawBack();
            LineOne = new Polyline();
            LineOne.StrokeThickness = 1;
            //polyline3.FillRule = FillRule.EvenOdd;
            LineOne.Opacity = 1;
            LineOne.Stroke = new SolidColorBrush(Color.FromArgb(255, 127, 184, 14));
            this.MyCanvas.Children.Add(LineOne);

            LineTwo = new Polyline();
            LineTwo.StrokeThickness = 2;
            //polyline3.FillRule = FillRule.EvenOdd;
            LineTwo.Opacity = 1;
            LineTwo.Stroke = new SolidColorBrush(Color.FromArgb(255, 253, 185, 51));
            this.MyCanvas.Children.Add(LineTwo);

            LineThreed = new Polyline();
            LineThreed.StrokeThickness = 3;
            //polyline3.FillRule = FillRule.EvenOdd;
            LineThreed.Opacity = 1;
            LineThreed.Stroke = new SolidColorBrush(Color.FromArgb(255, 18, 26, 42));
            this.MyCanvas.Children.Add(LineThreed);
        }

        /// <summary>
        /// 计算背景区域
        /// </summary>
        private void ComputerArea()
        {
            if (this.Height % LineNumb != 0)
            {
                TempHeight = this.Height - this.Height % LineNumb;
            }
            else
            {
                TempHeight = this.Height - this.Height % LineNumb;
               
            }
            this.TempWidth = this.Width;

           
        }


        public void DrawLine(List<double> point,List<double> point2=null,List<double> point3=null)
        {
            double tempstep = point.Count / this.TempWidth;
            double tempy=TempHeight/2;
            temph = this.Height / (this.Maxarc*2);
            if (point == null || point.Count < 0)
                return;

           

            for (int i = 0; i < point.Count; i++)
            {
                Point p1 = new Point();
                p1.X = i+startValue;
                p1.Y = tempy + (point[i] * (-temph));
                pontListOne.Add(p1);

                
               // this.myGrid.Children.Add(LineOne);

                if (point2 != null)
                {
                    Point p2 = new Point();
                    p2.X = i + startValue;
                    p2.Y = tempy + (point[i] * (-temph));
                    pontListTwo.Add(p2);
                   
                  //  this.myGrid.Children.Add(LineTwo);
                }
                if (point3 != null)
                {
                    Point p3 = new Point();
                    p3.X = i + startValue;
                    p3.Y = tempy + (point3[i] * (-temph));
                    PontListThreed.Add(p3);
                }


            }
            this.LineOne.Points = pontListOne;
            this.LineTwo.Points = pontListTwo;
            this.LineThreed.Points = PontListThreed;

 
        }

        /// <summary>
        /// 画背景线
        /// </summary>
        private void DrawBack()
        {
            double tempy = this.MaxHeight / LineNumb;
            //temph = this.Height / (this.Maxarc * 2);
            double tempaddvalue = this.Maxarc / (LineNumb/2);//每条线之间的数差
            for (double i = 0; i < this.LineNumb; i++)
            {
                double Height = tempy * i;
                Label lb = new Label();
                lb.Content = this.Maxarc - (tempaddvalue * i);
                //lb.HorizontalAlignment = HorizontalAlignment.Center;
                if (i == 0)
                {
                    lb.Margin = new Thickness(-2, Height - 8, 0, 0);
                }
                else
                {
                    lb.Margin = new Thickness(-2, Height - 15, 0, 0);
                }
                Line line = new Line();
                line.Stroke = new SolidColorBrush(Colors.Green);
                line.StrokeThickness = 1;
                line.X1 = this.startValue;
                line.Y1 = Height;
                line.X2 = this.TempWidth;
                line.Y2 = Height;
                this.MyCanvas.Children.Add(line);
                this.MyCanvas.Children.Add(lb);
               

            }

        }

        public void UserControl_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            //ComputerArea();
            //DrawBack();
        }

       
    }
}
