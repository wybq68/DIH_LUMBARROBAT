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
    /// UserLine.xaml 的交互逻辑
    /// </summary>
    public partial class UserLine : UserControl
    {
        public UserLine()
        {
            InitializeComponent();
            DraRang();
        }

        private int addvalues = 100;

        private int maxarc = 100;

        private double _BarHeight;

        /// <summary>
        /// 目标高度
        /// </summary>
        public double BarHeight
        {
            get { return _BarHeight; }
            set 
            {
                double Barh = (this.Height / maxarc) * value + addvalues;
                //if (value > 0)
                //{
                    if (Barh > this.Height)
                    {
                        _BarHeight = this.Height;
                        this.Bar.Y2 = this.Height;
                        SetBarMark(Height,value);
                    }
                    else
                    {
                        _BarHeight = Barh;
                        this.Bar.Y2 = Barh;
                        SetBarMark(Barh,value);
                    }
                //}
            }
        }

        private double _BarFactHeght;

        /// <summary>
        /// 实际高度
        /// </summary>
        public double BarFactHeght
        {
            get { return _BarFactHeght; }
            set 
            {
                double Barh = (this.Height / maxarc) * value + addvalues;
                //if (value > 0)
                //{
                    if (Barh > this.Height)
                    {
                        _BarFactHeght = this.Height;
                        this.Barfact.Y2 = this.Height;
                        SetBarFcatMark(Height, value);
                    }
                    else
                    {
                        _BarFactHeght = Barh;
                        this.Barfact.Y2 = Barh;
                        SetBarFcatMark(Barh,value);
                    }
                //}
            }
        }

        public double ZeroValue = 35;//最小度数

        public double factheight = 275;//实际控件总高度


        /// <summary>
        /// 画一个矩形
        /// </summary>
        private void DraRang()
        {
            Rectangle rect = new Rectangle();
            rect.Stroke = Brushes.Chocolate;
            rect.StrokeThickness =1;
            rect.Width = Bar.StrokeThickness;
            rect.Margin = new Thickness(Bar.Margin.Left - 67, 0, 0, 0);
            rect.Height = this.Height;           
            this.ba.Children.Add(rect);
            double Barh = ((factheight / maxarc) * ZeroValue + addvalues)+(1*(factheight / maxarc));
            Line zerol = new Line();
            zerol.Stroke = Brushes.Black;
            zerol.StrokeThickness = 1;
            zerol.X1 = 2;
            zerol.Y1 = Barh;
            zerol.X2 = Bar.Margin.Left-25;
            zerol.Y2 = Barh;
            this.ba.Children.Add(zerol);
            Label lab = new Label();
            lab.Content = "0";
            lab.FontSize = 15;
            lab.Margin = new Thickness(0, Barh-30, 100, 60);
            this.ba.Children.Add(lab);
        }

        private void SetBarMark(double top, double factvalue)
        {
            this.BarValue.Margin = new Thickness(this.BarValue.Margin.Left, this.Height - top-20, this.BarValue.Margin.Right, this.BarValue.Margin.Bottom);
            this.BarValue.Content = factvalue.ToString("f0");
        }

        private void SetBarFcatMark(double top, double factvalue)
        {
            double heighttop = this.Height - top - 20;
            if (heighttop > this.Height)
                heighttop = this.Height;
            this.BarfactValue.Margin = new Thickness(this.BarfactValue.Margin.Left,heighttop , this.BarfactValue.Margin.Right, this.BarfactValue.Margin.Bottom);
            this.BarfactValue.Content = factvalue.ToString("f0");
        }
    }
}
