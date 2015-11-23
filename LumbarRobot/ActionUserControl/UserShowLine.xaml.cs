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
    /// UserShowLine.xaml 的交互逻辑
    /// </summary>
    public partial class UserShowLine : UserControl
    {
        public UserShowLine()
        {
            InitializeComponent();
        }
        /// <summary>
        /// 背景线颜色
        /// </summary>
        public Brush BacklineColor { get; set; }

        private Double _MaxArc = 40;

        private int startxy = 40;
        /// <summary>
        /// 最大角度
        /// </summary>
        public Double MaxArc
        {
            get { return _MaxArc; }
            set
            {
                _MaxArc = value;
                DrawBack();
            }
        }


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

        /// <summary>
        /// 清除背景控件
        /// </summary>
        public void Cle()
        {
            this.MyCanvas.Children.Clear();

            //for (int i = 0; i < this.MyCanvas.Children.Count;i++ )
            //{
            //    if (this.MyCanvas.Children[i] is Label || this.MyCanvas.Children[i] is Line)
            //    {
            //        this.MyCanvas.Children.Remove(this.MyCanvas.Children[i]);
            //    }

            //}


        }

        private double eliWH = 6;

        private void DrawBack()
        {
            //this.MyCanvas.Children.Clear();
            Cle();
            double temheig = this.Height / MaxArc;
            temheig = temheig * 10;
            double tempy = MaxArc / 2;

            


            for (double i = 0; i <= this.Height / 2; i += temheig)
            {

                Line liz = new Line();
                Line lif = new Line();
                Label lz = new Label();
                Label lf = new Label();
                liz.Stroke = BacklineColor;
                lif.Stroke = BacklineColor;
                lif.StrokeThickness = 1;
                liz.StrokeThickness = 1;
                liz.X1 = startxy;
                liz.Y1 = i;
                liz.X2 = this.Width;
                liz.Y2 = i;
                lif.X1 = startxy;
                lif.Y1 = this.Height - i;
                lif.X2 = this.Width;
                lif.Y2 = this.Height - i;
                lz.Content = tempy + " " + this.Uint;
                lz.Margin = new Thickness(liz.X1 - startxy, liz.Y1 - 15, 0, 0);
                lf.Margin = new Thickness(lif.X1 - startxy, lif.Y1 - 15, 0, 0);
                lf.Content = tempy * -1 + " " + this.Uint;
                this.MyCanvas.Children.Add(lf);
                this.MyCanvas.Children.Add(lz);
                this.MyCanvas.Children.Add(liz);
                this.MyCanvas.Children.Add(lif);
                tempy -= 10;
                // double tempd=0;

            }
            DarwPolyline();

        }

        Line LinkLine;
        Polyline polyline1;
        Polyline polyline2;
        Polyline polyline3;
        Ellipse eli;

        private void DarwPolyline()
        {


            polyline2 = new Polyline();
            polyline2.StrokeThickness = 15;
            polyline2.Opacity = 0.5;
            polyline2.FillRule = FillRule.EvenOdd;
            polyline2.Stroke = new SolidColorBrush(Color.FromRgb(34, 139, 34));
            this.MyCanvas.Children.Add(polyline2);

            polyline3 = new Polyline();
            polyline3.StrokeThickness = 3;
            polyline3.FillRule = FillRule.EvenOdd;
            polyline3.Opacity = 0.5;
            polyline3.Stroke = new SolidColorBrush(Color.FromRgb(255, 255, 255));
            this.MyCanvas.Children.Add(polyline3);

            polyline1 = new Polyline();
            polyline1.StrokeThickness = 3;
            polyline1.FillRule = FillRule.EvenOdd;
            polyline1.Stroke = new SolidColorBrush(Color.FromRgb(203, 133, 82));
            this.MyCanvas.Children.Add(polyline1);

            LinkLine = new Line();
            LinkLine.X1 = startxy;
            LinkLine.X2 = startxy;
            LinkLine.Y1 = 0;
            LinkLine.Y2 = this.Height;
            LinkLine.Stroke = new SolidColorBrush(Color.FromRgb(128, 128, 105));
            LinkLine.StrokeThickness = 2;
            this.MyCanvas.Children.Add(LinkLine);

            eli = new Ellipse();
            eli.Margin = new Thickness(startxy - eliWH / 2, this.Height / 2 - eliWH / 2, 0, 0);
            eli.HorizontalAlignment = HorizontalAlignment.Left;
            eli.VerticalAlignment = VerticalAlignment.Top;
            eli.Fill = System.Windows.Media.Brushes.Red;

            eli.StrokeThickness = 4;

            eli.Stroke = System.Windows.Media.Brushes.Red; //边，金黄色

            eli.Width = eliWH;

            eli.Height = eliWH;
            this.MyCanvas.Children.Add(eli);
        }

        /// <summary>
        /// 目标线
        /// </summary>
        private List<Point> GoleLine = new List<Point>();
        PointCollection mypoint = new PointCollection();
        /// <summary>
        /// 目标线
        /// </summary>
        public void DarwLine(double[] arc,double[] arc2,double[] arc3)
        {
            GoleLine.Clear();
            double temheig = this.Height / MaxArc;
            for (int i = 0; i < arc.Length; i++)
            {
                Point p = new Point();
                p.Y = (-arc[i]) * temheig + MaxArc;
                p.X = i + startxy;
                GoleLine.Add(p);
                if (i + startxy > this.Width)
                {
                    //mypoint.Add(p);
                }
                else
                {
                    mypoint.Add(p);
                }

            }
            polyline1.Points = mypoint;
            polyline2.Points = mypoint;
            polyline3.Points = mypoint;
            //mypoint.Clear();
        }
    }
}
