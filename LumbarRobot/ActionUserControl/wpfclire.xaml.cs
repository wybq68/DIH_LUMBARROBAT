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
    /// wpfclire.xaml 的交互逻辑
    /// </summary>
    public partial class wpfclire : UserControl
    {
        public wpfclire()
        {
            InitializeComponent();           
          
           
        }
        #region 变量
        
       

        private int _LineNub = 2;
        /// <summary>
        /// 刻度条数
        /// </summary>
        public int LineNub
        {
            get { return _LineNub; }
            set { _LineNub = value; }
        }

       
        double _MaxArc = 20;
        /// <summary>
        /// 最大度数
        /// </summary>
        public double MaxArc
        {
            get { return _MaxArc; }
            set { _MaxArc = value; }
        }

        private double _SetArc;
        /// <summary>
        /// 设定不能超过的角度
        /// </summary>
        public double SetArc
        {
            get { return _SetArc; }
            set { _SetArc = value; }
        }

        private int nDiameter = 195;
        private int nRadius;

        /// <summary>
        /// 圆的颜色
        /// </summary>
        public Brush ElicleColor{get;set;}
        /// <summary>
        /// 塞的颜色
        /// </summary>
        public Brush BackColor { get; set; }

        /// <summary>
        /// flt颜色
        /// </summary>
        public Brush FltColor { get; set; }
        /// <summary>
        /// 运行的颜色
        /// </summary>
        public Brush RunColor { get; set; }
        /// <summary>
        /// 超过flt颜色
        /// </summary>
        public Brush OutFltColor { get; set; }
        /// <summary>
        /// 超过塞的颜色
        /// </summary>
        public Brush OutBackColor { get; set; }

        /// <summary>
        /// 塞的正方向值
        /// </summary>
        public double BackValue { get; set; }
        /// <summary>
        /// 塞的负方向值
        /// </summary>
        public double BackMinusValu { get; set; }
        /// <summary>
        /// Flat正方向值
        /// </summary>
        public double FltValu { get; set; }
        /// <summary>
        /// Flat负方向值 
        /// </summary>
        public Double FltMinusValue { get; set; }

        /// <summary>
        /// 
        /// </summary>
        private double _LinWidth=4;
        /// <summary>
        /// 画图线的宽度
        /// </summary>
        public double LinWidth
        {
            get { return _LinWidth; }
            set { _LinWidth = value; }
        }

        List<Line> lines = new List<Line>();

        Label lab = new Label();
        /// <summary>
        /// 运动的值
        /// </summary>
       // public double RunValu { get; set; }

        Point nCenterPoint;
        #endregion
        #region 方法
        
        

        /// <summary>
        /// 画直线
        /// </summary>
        /// <param name="l"></param>
        /// <param name="nLength"></param>
        /// <param name="i"></param>
        private Line DrawLine(ref Line l, double nLength, double i)
        {
            l.X1 = (nLength * Math.Cos(i * Math.PI / 180)) + nCenterPoint.X;
            l.Y1 = (-nLength * Math.Sin(i * Math.PI / 180)) + nCenterPoint.X;
            l.X2 = (nDiameter / 2 * Math.Cos(i * Math.PI / 180)) + nCenterPoint.X;
            l.Y2 = (-(nDiameter / 2) * Math.Sin(i * Math.PI / 180)) + nCenterPoint.X;
            return l;
        }

        /// <summary>
        /// 画正角度值
        /// </summary>
        /// <param name="arc">要画的角度</param>
        /// <param name="rgb">颜色</param>
        /// <param name="IsFlt">是否是flt</param>
        /// <param name="stararc">开始角度</param>
        public void drawclre(double arc, Brush rgb,bool IsFlt=false,double stararc=0,double Lineintervar=2)
        {
            arc++;
            stararc += 90;
            
            if (this.Width < this.Height)
            {
                nDiameter = (int)this.Width-80 ;
            }
            else
            {
                nDiameter = (int)this.Height-80;

            }
            
            double interval = 360 / (MaxArc * 2);
            arc=arc* interval;
           // stararc =arc+ 90;
            nCenterPoint = new Point(nDiameter / 2, nDiameter / 2);//new System.Drawing.Point(nDiameter / 2, nDiameter / 2);
            nRadius = nDiameter / 2;          
            double nLength = 0;
            if (stararc > 90)
            {
                stararc = Math.Abs((stararc - 90)) * interval + 90;
            }
           
            

            for (double i = stararc; i < arc + stararc; i += Lineintervar)
            {                
                Line l = new Line();
                l.Stroke = rgb;
                l.StrokeThickness = _LinWidth;
               // Console.WriteLine(i);
                if (i + interval >= arc + stararc && IsFlt == true)
                {
                    nLength = nRadius - (nDiameter / 5);

                    DrawLine(ref l, nLength, i);
                    grid.Children.Add(l);
                    break;

                }
                else
                {
                    nLength = nRadius - (nDiameter / 20);
                }
                if (i + interval <= arc + stararc )
                {
                    DrawLine(ref l, nLength, i);

                    grid.Children.Add(l);
                }
               
            }


        }

        /// <summary>
        /// 画负的角度
        /// </summary>
        /// <param name="arc"></param>
        /// <param name="rgb"></param>
        /// <param name="IsFlt"></param>
        /// <param name="stararc"></param>
        public void drafarc(double arc, Brush rgb, bool IsFlt = false, double stararc = 0, double Lineintervar =2,bool ISminus=false)
        {
            if (this.Width < this.Height)
            {
                nDiameter = (int)this.Width - 80;
            }
            else
            {
                nDiameter = (int)this.Height - 80;

            }
            //arc--;
           
            stararc = Math.Abs(90 + stararc);
            double interval = 90-(360 / (MaxArc * 2) * arc);//算出实际角度
            arc = arc * interval;
            nCenterPoint = new Point(nDiameter / 2, nDiameter / 2);
            nRadius = nDiameter / 2;
            double nLength = 0;
            for (double i = stararc; i > interval; i -= Lineintervar)
            {
               
                Line l = new Line();
                if (ISminus)
                {
                    if (i < 0)
                    {
                        l.Stroke = OutFltColor;
                    }
                    else
                    {
                        l.Stroke = rgb;
                    }
                }
                else
                {
                    l.Stroke = rgb;
                }
                l.StrokeThickness = _LinWidth;

                if (i - Lineintervar <= interval && IsFlt == true)
                {
                    nLength = nRadius - (nDiameter / 5);
                  
                  DrawLine(ref l, nLength, i);
                   
                    grid.Children.Add(l);
                    break;

                }
                else
                {
                    nLength = nRadius - (nDiameter / 20);
                }

                 DrawLine(ref l, nLength, i);
                 grid.Children.Add(l);
                 



            }



        }

       /// <summary>
        /// 画外过刻度
       /// </summary>
       /// <param name="rgb">颜色</param>
       /// <param name="buttenLine">每个多少刻度画一条刻度线</param>

        public void drakedu(Brush rgb, int buttenLine, int stararc = 90)
        {

            if (this.Width < this.Height)
            {
                nDiameter = (int)(this.Width-40);
            }
            else
            {
                nDiameter = (int)(this.Height-40);

            }

            double interval = 360 / (MaxArc*2); 

            nCenterPoint = new Point(nDiameter / 2, nDiameter / 2);//new System.Drawing.Point(nDiameter / 2, nDiameter / 2);
            nRadius = nDiameter / 2;
            double nLength = 0;
            Grid g1 = new Grid();
            g1.Height = this.Height - 20;
            g1.Width = this.Width - 20;
            g1.Margin = new Thickness(20, 20, 0, 0);
            int j = 0;
            for (double i = stararc; i < stararc+360; i += interval * buttenLine)
            {
               
                Label tb = new Label();
                    Line l = new Line();
                    l.Stroke = rgb;
                    l.StrokeThickness = 1;
                    nLength = nRadius - (nDiameter / 15);//线的长短
                    DrawLine(ref l, nLength, i);
                 //   tb.Content = (((i - 90) / MaxArc）-1）* buttenLine).ToString();
                
                    tb.Content = j.ToString();
                    if (i - 90 < 180)
                    {
                        tb.Margin = new Thickness(l.X2-10, l.Y2, 0, 0);
                    }
                    else if (i - 90 == 90)
                    {
                        tb.Margin = new Thickness(l.X1-20, l.Y1, 0, 0);
                    }
                    else
                    {
                        tb.Margin = new Thickness(l.X1 + 10, l.Y1, 0, 0);
                    }
                    g1.Children.Add(tb);                    
                    g1.Children.Add(l);
                    if (j < MaxArc)
                    {
                        j += buttenLine;
                    }
                    
                    else
                    {
                        j -= (buttenLine);
                        j = (-j);
                    }
            }
            aa.Children.Add(g1);
        }

        /// <summary>
        /// 画圆
        /// </summary>
        /// <param name="rgb"></param>
        public void DraEliee(Brush rgb)
        {
            Ellipse el = new Ellipse();
            el.Stroke = rgb;
            el.StrokeThickness = 3;
            //aa.Children.Clear();
            grid.Children.Clear();           
            grid.Children.Add(el);
        }

     

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            DraEliee(ElicleColor);
            //drawclre((int)this.MaxArc*2, Brushes.Blue,false,0,0.5);
            //drakedu(Brushes.Red, 10);
            AddLable();
            SetFltSai(FltColor, ElicleColor, BackColor, FltValu, FltMinusValue, BackValue, BackMinusValu);
        }

        private void AddLable()
        {
            //lab.Width = 89;
            //lab.Height = 40;
            lab.HorizontalContentAlignment = HorizontalAlignment.Center;
            
                lab.Margin = new Thickness(this.Width / 2 - 20, this.Height / 2 - 20, 0, 0);
        
            lab.FontFamily = new System.Windows.Media.FontFamily("微软黑");
            lab.FontSize = 35;           
            
            lab.Content = 0;
            aa.Children.Add(lab);
        }

        /// <summary>
        /// 绘制背景
        /// </summary>
        /// <param name="waikedu">外刻度颜色</param>
        /// <param name="maikedu">内刻度颜色</param>
        /// <param name="Ellicle">圆的颜色</param>
        public void draback(Brush waikedu, Brush maikedu, Brush Ellicle,bool ISdrawWai=false)
        {

            DraEliee(Ellicle);
            drawclre((int)this.MaxArc*2 , waikedu);
            if (ISdrawWai)
            {
                drakedu(maikedu, 10);
            }
        }

        /// <summary>
        /// 设置flt值和塞值
        /// </summary>
        /// <param name="Ellicle">圆的颜色</param>
        /// <param name="maikedu">内圆刻度颜色</param>
        /// <param name="waikedu">外边刻度颜色</param>
        /// <param name="fltval">正方向flt值</param>
        /// <param name="fltfvalu">负方向flt值</param>
        /// <param name="sai">正方向塞值</param>
        /// <param name="fsai">负方向塞值</param>
        public void SetFltSai(Brush fltColor, Brush Ellicle,Brush SaiColor, double fltval, double fltfvalu, double sai, double fsai)
        {

            this.DraEliee(Ellicle);
            this.drakedu(Brushes.Red, 10);
           
            //塞值
            this.drafarc(fsai, SaiColor, true);
            this.drawclre(sai, SaiColor, true);
            //flt值
            this.drafarc(fltfvalu, fltColor, true);
            this.drawclre(fltval, fltColor, true);
        }
        /// <summary>
        /// 运动时调用
        /// </summary>
        /// <param name="RunValu">角度，为0时为复位</param>
        public void loginRun(double RunValu)
        {
            this.grid.Children.Clear();
            SetFltSai(FltColor, ElicleColor, BackColor, FltValu, FltMinusValue, BackValue, BackMinusValu);
            lab.Content = RunValu;
            if (RunValu >= 0)
            {
                if (RunValu > FltValu)
                {
                    this.drawclre(FltValu, RunColor, false);
                    this.drawclre(RunValu-FltValu, OutFltColor, false, FltValu);
                    //this.drawclre(RunValu, OutFltColor, false);
                }
                else if (RunValu > BackValue)
                {
                    this.drawclre(BackValue, RunColor, false);
                    this.drawclre(RunValu-BackValue,OutBackColor, false,BackValue);
                }
                else
                {
                    this.drawclre(RunValu, RunColor, false);
                }
            }
            else
            {

                if (Math.Abs(RunValu) > FltMinusValue)
                {
                    //this.drafarc(FltMinusValue, RunColor, false);
                    this.drafarc(Math.Abs(RunValu) , RunColor, false, 0, 0.25, true);
                }
                else if (Math.Abs(RunValu) > BackMinusValu)
                {
                    //this.drafarc(BackMinusValu, RunColor, false);
                    this.drafarc(Math.Abs(RunValu), RunColor, false, 0, 0.25, true);
                }
                else
                {
                    this.drafarc(Math.Abs(RunValu), RunColor, false);
                }
            }

        }
        #endregion
    }
}
