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

namespace LumbarRobot
{
    /// <summary>
    /// 按照步骤 1a 或 1b 操作，然后执行步骤 2 以在 XAML 文件中使用此自定义控件。
    ///
    /// 步骤 1a) 在当前项目中存在的 XAML 文件中使用该自定义控件。
    /// 将此 XmlNamespace 特性添加到要使用该特性的标记文件的根 
    /// 元素中:
    ///
    ///     xmlns:MyNamespace="clr-namespace:LumbarRobot"
    ///
    ///
    /// 步骤 1b) 在其他项目中存在的 XAML 文件中使用该自定义控件。
    /// 将此 XmlNamespace 特性添加到要使用该特性的标记文件的根 
    /// 元素中:
    ///
    ///     xmlns:MyNamespace="clr-namespace:LumbarRobot;assembly=LumbarRobot"
    ///
    /// 您还需要添加一个从 XAML 文件所在的项目到此项目的项目引用，
    /// 并重新生成以避免编译错误:
    ///
    ///     在解决方案资源管理器中右击目标项目，然后依次单击
    ///     “添加引用”->“项目”->[浏览查找并选择此项目]
    ///
    ///
    /// 步骤 2)
    /// 继续操作并在 XAML 文件中使用控件。
    ///
    ///     <MyNamespace:CustomChars/>
    ///
    /// </summary>
    public class CustomChars : Control
    {
        static CustomChars()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(CustomChars), new FrameworkPropertyMetadata(typeof(CustomChars)));
            
        }
        Canvas cv;
        private List<double> _point2 = new List<double>();
        /// <summary>
        /// 数据源2
        /// </summary>
        public List<double> Point2
        {
            get { return _point2; }
            set 
            {
                _point2 = value;
                if (_point2 != null&&_point2.Count>0)
                GetMaxValue(_point2.Max());
            }
        }

      
        private List<double> _point1 = new List<double>();
        /// <summary>
        /// 数据源1
        /// </summary>
        public List<double> Point1
        {
            get { return _point1; }
            set 
            {
                
                _point1 = value;
                if (_point1 != null&&_point1.Count>0)
                GetMaxValue(_point1.Max());
               // = _point1.Max();
            }
        }

       

        private List<double> _point3 = new List<double>();
        /// <summary>
        /// 数据源3
        /// </summary>
        public List<double> Point3
        {
            get { return _point3; }
            set 
            { 
                _point3 = value;
                if(_point3!=null&&_point3.Count>0)
                GetMaxValue(_point3.Max());
            }
        }

        /// <summary>
        /// 上下间距
        /// </summary>
        public double HeightSpacin = 4;

        /// <summary>
        /// 刻度线颜色
        /// </summary>
        public Brush ScaleColor = new SolidColorBrush(Color.FromArgb(255, 18, 26, 42));

        /// <summary>
        /// 曲线颜色1
        /// </summary>
        public Brush LineColor1 = new SolidColorBrush(Color.FromArgb(155, 18, 26, 242));

        /// <summary>
        /// 曲线颜色2
        /// </summary>
        public Brush LineColor2 = new SolidColorBrush(Color.FromArgb(155, 18, 26, 242));

        /// <summary>
        /// 曲线颜色3
        /// </summary>
        public Brush LineColor3 = new SolidColorBrush(Color.FromArgb(155, 18, 26, 242));

        private double FactHeight;

        /// <summary>
        /// 确定最大值 
        /// </summary>
        /// <param name="value"></param>
        private void GetMaxValue(double value)
        {
            if (value > this.Maxarc)
                this.Maxarc = value;
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            if (Template != null)
            {
                cv = Template.FindName("MyDrawBack", this) as Canvas;
                Init();
                DrawLine();
            }
        }

        /// <summary>
        /// 画背景线
        /// </summary>
        private void DrawBack()
        {
            double tempy = FactHeight / (LineNumb - 1);
            //temph = this.Height / (this.Maxarc * 2);
            double tempaddvalue = this.Maxarc / (LineNumb / 2);//每条线之间的数差
            for (double i = 0; i < this.LineNumb; i++)
            {
                double Height = tempy * i;
                Label lb = new Label();
                lb.Content = this.Maxarc - (tempaddvalue * i);
                //lb.HorizontalAlignment = HorizontalAlignment.Center;
                if (i == 0)
                {
                    lb.Margin = new Thickness(-2, Height - 5, 0, 0);
                }
                else if (i + 1 == this.LineNumb)
                {
                    lb.Margin = new Thickness(-2, Height - 20, 0, 0);
                }
                else
                {
                    lb.Margin = new Thickness(-2, Height - 15, 0, 0);
                }
                Line line = new Line();
                line.Stroke = this.ScaleColor;
                line.StrokeThickness = 1;
                line.X1 = this.startValue;
                line.Y1 = Height + HeightSpacin;
                line.X2 = this.Width;
                line.Y2 = Height + HeightSpacin;
                cv.Children.Add(line);
                cv.Children.Add(lb);
                //this..Children.Add(line);
                //this.MyCanvas.Children.Add(lb);


            }

        }

        private void Init()
        {
            FactHeight = this.Height - this.HeightSpacin * 2;
            DrawBack();
            LineOne = new Polyline();
            LineOne.StrokeThickness = 2;

            //polyline3.FillRule = FillRule.EvenOdd;
            LineOne.Opacity = 1;
            LineOne.Stroke = LineColor1;
            this.cv.Children.Add(LineOne);

            LineTwo = new Polyline();
            LineTwo.StrokeThickness = 2;
            //polyline3.FillRule = FillRule.EvenOdd;
            LineTwo.Opacity = 1;
            LineTwo.Stroke = LineColor2;
            this.cv.Children.Add(LineTwo);

            LineThreed = new Polyline();
            LineThreed.StrokeThickness = 2;
            //polyline3.FillRule = FillRule.EvenOdd;
            LineThreed.Opacity = 1;
            LineThreed.Stroke = LineColor3;
            this.cv.Children.Add(LineThreed);
        }

        public void DrawLine()
        {
            double tempstep = this.Point1.Count / this.Width;
            double tempy = FactHeight / 2;
            double temph = FactHeight / (this.Maxarc * 2);
            if (this.Point1 == null || this.Point1.Count < 0)
                return;

            for (int i = 0; i < this.Point1.Count; i++)
            {
                Point p1 = new Point();
                p1.X = i + startValue;
                p1.Y = tempy + (this.Point1[i] * (-temph)) + HeightSpacin;
                pontListOne.Add(p1);
            }

            if (this.Point2 != null && this.Point2.Count > 0)
            {
                for (int i = 0; i < this.Point2.Count; i++)
                {
                    Point p2 = new Point();
                    p2.X = i + startValue;
                    p2.Y = tempy + (this.Point2[i] * (-temph)) + HeightSpacin;
                    pontListTwo.Add(p2);
                }


            }
            if (this.Point3 != null && this.Point3.Count > 0)
            {
                for (int i = 0; i < this.Point3.Count; i++)
                {
                    Point p3 = new Point();
                    p3.X = i + startValue;
                    p3.Y = tempy + (this.Point3[i] * (-temph)) + HeightSpacin;
                    PontListThreed.Add(p3);
                }
            }
            this.LineOne.Points = pontListOne;
            this.LineTwo.Points = pontListTwo;
            this.LineThreed.Points = PontListThreed;


        }

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
        PointCollection pontListOne = new PointCollection();
        /// <summary>
        /// 第二条线数据源
        /// </summary>
        PointCollection pontListTwo = new PointCollection();
        /// <summary>
        /// 第三条线数据源
        /// </summary>
        PointCollection PontListThreed = new PointCollection();

        private double _Maxarc = 50;
          /// <summary>
        /// 表示的最大数值
        /// </summary>
        public double Maxarc
        {
            get { return _Maxarc; }
            set 
            {
               
                _Maxarc = value;
            }
        }

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
       
    }
}
