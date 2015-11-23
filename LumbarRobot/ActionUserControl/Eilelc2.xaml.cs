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
    /// Eilelc2.xaml 的交互逻辑
    /// </summary>
    public partial class Eilelc2 : UserControl
    {
        public Eilelc2()
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


        double _MaxArc = 100;
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
        public Brush ElicleColor { get; set; }
        /// <summary>
        /// 塞的颜色
        /// </summary>
        public Brush BackColor { get; set; }

        /// <summary>
        /// flt颜色
        /// </summary>
        public Brush FltColor { get; set; }

        /// <summary>
        /// flt颜色
        /// </summary>
        public Brush FltColor1 { get; set; }
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
        private double _BackValue;
        /// <summary>
        /// 正塞值
        /// </summary>
        public double BackValue
        {
            get { return _BackValue; }
            set 
            { 
                _BackValue = value;
                //this.drasai(_BackValue);
            }
        }


        /// <summary>
        /// 塞的正方向值1
        /// </summary>
        private double _BackValue1;
        /// <summary>
        /// 正塞值1
        /// </summary>
        public double BackValue1
        {
            get { return _BackValue1; }
            set
            {
                _BackValue1 = value;
                //this.drasai1(_BackValue1);
            }
        }


        /// <summary>
        /// 塞的负方向值
        /// </summary>
        private double _BackMinusValu;
        /// <summary>
        /// 负塞值
        /// </summary>
        public double BackMinusValu
        {
            get { return _BackMinusValu; }
            set 
            { 
                _BackMinusValu = value;
                //this.drasai(_BackMinusValu);
            }
        }


        /// <summary>
        /// 塞的负方向值1
        /// </summary>
        private double _BackMinusValu1;
        /// <summary>
        /// 负塞值1
        /// </summary>
        public double BackMinusValu1
        {
            get { return _BackMinusValu1; }
            set
            {
                _BackMinusValu1 = value;
                
                //this.drasai1(_BackMinusValu1);
            }
        }


        /// <summary>
        /// Flat正方向值
        /// </summary>
        private double _FltValu = 0;
        /// <summary>
        /// flt值
        /// </summary>
        public double FltValu
        {
            get { return _FltValu; }
            set
            {
                _FltValu = value;
                this.draflt(_FltValu);
            }
        }
        private double _FltMinusValue;
        /// <summary>
        /// 负flt值
        /// </summary>
        public double FltMinusValue
        {
            get { return _FltMinusValue; }
            set 
            { 
              
                _FltMinusValue = value;
                this.draflt(_FltMinusValue);
            }
        }
       

        /// <summary>
        /// 
        /// </summary>
        private double _LinWidth = 4;
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

        private double lastvalue = 0; 

        #endregion
        #region 方法



        /// <summary>
        /// 画直线
        /// </summary>
        /// <param name="l"></param>
        /// <param name="nLength">长度</param>
        /// <param name="i">度数</param>
        private Line DrawLine(ref Line l, double nLength, double i)
        {
            l.X1 = (nLength * Math.Cos(i * Math.PI / 180)) + nCenterPoint.X;
            l.Y1 = (-nLength * Math.Sin(i * Math.PI / 180)) + nCenterPoint.X;
            l.X2 = (nDiameter / 2 * Math.Cos(i * Math.PI / 180)) + nCenterPoint.X;
            l.Y2 = (-(nDiameter / 2) * Math.Sin(i * Math.PI / 180)) + nCenterPoint.X;
            return l;
        }

     

        /// <summary>
        /// 画直线
        /// </summary>
        /// <param name="l"></param>
        /// <param name="nLength"></param>
        /// <param name="i"></param>
        private Double[] Lablemark(double nLength, double i)
        {
            double [] db=new Double[4];
           db[0] = (nLength * Math.Cos(i * Math.PI / 180)) + nCenterPoint.X;
           db[1] = (-nLength * Math.Sin(i * Math.PI / 180)) + nCenterPoint.X;
           db[2] = (nDiameter / 2 * Math.Cos(i * Math.PI / 180)) + nCenterPoint.X;
           db[3] = (-(nDiameter / 2) * Math.Sin(i * Math.PI / 180)) + nCenterPoint.X;
            return db;
        }

        

        /// <summary>
        /// 画正角度值
        /// </summary>
        /// <param name="arc">要画的角度</param>
        /// <param name="rgb">颜色</param>
        /// <param name="IsFlt">是否超过设定值（flt）</param>
        /// <param name="stararc">开始角度</param>
        public void drawclre(double arc, Brush rgb, bool IsFlt = false, double stararc = 0, double Lineintervar = 2)
        {
            //arc++;
            
            double interval = 360 / (MaxArc * 2);//每个个度所占分数
            arc = arc * interval;
            if (stararc == 0)
            {
                stararc += 90;
            }
            else
            {
                stararc = stararc * interval + 90;
            }
             //ChangEColor(0, 360, ElicleColor);
            if (arc > 0)
            {
                ChangEColor(stararc, arc, rgb);
            }
            else if (arc < 0)
            {
                ChangEColor(stararc, arc, rgb,true);
            }


        }
        /// <summary>
        /// 画flt值
        /// </summary>
        /// <param name="arc"></param>
        public void draflt(double arc)
        {
            Line l;
            if (this.Width < this.Height)
            {
                nDiameter = (int)this.Width - 80;
            }
            else
            {
                nDiameter = (int)this.Height - 80;

            }
            double interval = 360 / (MaxArc * 2);//每个个度所占分数
            if (arc > 0)
            {
                this.fltz.Visibility = Visibility.Visible;
                l = this.fltz;
                arc = arc * interval + 90;
            }
            else if (arc < 0)
            {
                this.fltf.Visibility = Visibility.Visible;
                l = this.fltf;
                arc = arc * interval + 90;
            }
            else
            {
                this.fltz.Visibility = Visibility.Hidden;
                this.fltf.Visibility = Visibility.Hidden;
                return;
            }
           
            nCenterPoint = new Point(nDiameter / 2, nDiameter / 2);//new System.Drawing.Point(nDiameter / 2, nDiameter / 2);
            nRadius = nDiameter / 2;
            double nLength = 0;
            
            l.Stroke = FltColor;
            l.StrokeThickness = _LinWidth;
            nLength = nRadius - (nDiameter / 5);
            DrawLine(ref l, nLength, arc);
            //grid.Children.Add(l);
        }

        /// <summary>
        /// 画塞值
        /// </summary>
        /// <param name="arc"></param>
        public void drasai(double arc)
        {
            Line l;
            if (this.Width < this.Height)
            {
                nDiameter = (int)this.Width - 80;
            }
            else
            {
                nDiameter = (int)this.Height - 80;

            }
            double interval = 360 / (MaxArc * 2);//每个个度所占分数

            this.saiz.Visibility = Visibility.Visible;
            l = this.saiz;
            arc = arc * interval + 90;
            //if (arc > 0)
            //{
            //    this.saiz.Visibility = Visibility.Visible;
            //    l = this.saiz;
            //    arc = arc * interval + 90;
            //}
            //else if (arc < 0)
            //{
            //    this.saif.Visibility = Visibility.Visible;
            //    l = this.saif;
            //    arc = arc * interval + 90;
            //}
            //else
            //{
            //    this.saiz.Visibility = Visibility.Hidden;
            //    this.saif.Visibility = Visibility.Hidden;
            //    return;
            //}

            nCenterPoint = new Point(nDiameter / 2, nDiameter / 2);//new System.Drawing.Point(nDiameter / 2, nDiameter / 2);
            nRadius = nDiameter / 2;
            double nLength = 0;

            l.Stroke = FltColor;
            l.StrokeThickness = _LinWidth;
            nLength = nRadius - (nDiameter / 5);
            DrawLine(ref l, nLength, arc);
            //grid.Children.Add(l);
        }


        /// <summary>
        /// 四个同时画塞,为空或数组长度为0为隐藏线位赛
        /// </summary>
        /// <param name="arc"></param>
        public void DrawSaiByDoubles(Double[] arcs)
        {
            if (arcs == null||arcs.Length<1)
            {
                this.saiz.Visibility = Visibility.Hidden;
                saif.Visibility = Visibility.Hidden;
                saiz1.Visibility = Visibility.Hidden;
                saif1.Visibility = Visibility.Hidden;
            }
            else
            {
                for (int i = 0; i < arcs.Length; i++)
                {
                    Line l = new Line();
                    if (this.Width < this.Height)
                    {
                        nDiameter = (int)this.Width - 80;
                    }
                    else
                    {
                        nDiameter = (int)this.Height - 80;

                    }
                    double interval = 360 / (MaxArc * 2);//每个个度所占分数

                    if (i == 0)
                    {
                        if (arcs[i] != 0)
                        {
                            this.saiz.Visibility = Visibility.Visible;
                            l = this.saiz;
                            if (Math.Abs(arcs[i]) > this._MaxArc)
                            {
                                arcs[i] = this._MaxArc;
                            }
                            else
                            {
                                arcs[i] = arcs[i] * interval + 90;
                            }
                        }
                        else
                        {
                            this.saiz.Visibility = Visibility.Hidden;
                        }
                    }
                    else if (i == 1)
                    {
                        if (arcs[i] != 0)
                        {
                            saif.Visibility = Visibility.Visible;
                            l = this.saif;
                            if (Math.Abs(arcs[i]) > this._MaxArc)
                            {
                                arcs[i] = this._MaxArc;
                            }
                            else
                            {
                                arcs[i] = arcs[i] * interval + 90;
                            }
                        }
                        else
                        {
                            saif.Visibility = Visibility.Hidden;
                        }
                    }
                    else if (i == 2)
                    {
                        if (arcs[i] != 0)
                        {
                            saiz1.Visibility = Visibility.Visible;
                            l = this.saiz1;
                            if (Math.Abs(arcs[i]) > this._MaxArc)
                            {
                                arcs[i] = this._MaxArc * interval + 90;
                            }
                            else
                            {
                                arcs[i] = arcs[i] * interval + 90;
                            }
                        }
                        else
                        {
                            saiz1.Visibility = Visibility.Hidden;
                        }
                    }
                    else if (i == 3)
                    {
                        if (arcs[i] != 0)
                        {
                            saif1.Visibility = Visibility.Visible;
                            l = this.saif1;
                            if (Math.Abs(arcs[i]) > this._MaxArc)
                            {
                                arcs[i] = this._MaxArc * interval + 90;
                            }
                            else
                            {
                                arcs[i] = arcs[i] * interval + 90;
                            }
                        }
                        else
                        {
                            saif1.Visibility = Visibility.Hidden;
                        }
                    }


                    nCenterPoint = new Point(nDiameter / 2, nDiameter / 2);//new System.Drawing.Point(nDiameter / 2, nDiameter / 2);
                    nRadius = nDiameter / 2;
                    double nLength = 0;

                    l.Stroke = FltColor1;
                    l.StrokeThickness = _LinWidth;
                    nLength = nRadius - (nDiameter / 5);
                    DrawLine(ref l, nLength, arcs[i]);
                }
            }
            //grid.Children.Add(l);
        }


        /// <summary>
        /// 画塞值
        /// </summary>
        /// <param name="arc"></param>
        public void drasai1(double arc)
        {
            Line l;
            if (this.Width < this.Height)
            {
                nDiameter = (int)this.Width - 80;
            }
            else
            {
                nDiameter = (int)this.Height - 80;

            }
            double interval = 360 / (MaxArc * 2);//每个个度所占分数
            if (arc > 0)
            {
                this.saiz1.Visibility = Visibility.Visible;
                l = this.saiz1;
                arc = arc * interval + 90;
            }
            else if (arc < 0)
            {
                this.saif1.Visibility = Visibility.Visible;
                l = this.saif1;
                arc = arc * interval + 90;
            }
            else
            {
                this.saiz1.Visibility = Visibility.Hidden;
                this.saif1.Visibility = Visibility.Hidden;
                return;
            }

            nCenterPoint = new Point(nDiameter / 2, nDiameter / 2);//new System.Drawing.Point(nDiameter / 2, nDiameter / 2);
            nRadius = nDiameter / 2;
            double nLength = 0;

            l.Stroke = FltColor;
            l.StrokeThickness = _LinWidth;
            nLength = nRadius - (nDiameter / 5);
            DrawLine(ref l, nLength, arc);
            //grid.Children.Add(l);
        }

        /// <summary>
        /// 画负的角度
        /// </summary>
        /// <param name="arc"></param>
        /// <param name="rgb"></param>
        /// <param name="IsFlt"></param>
        /// <param name="stararc"></param>
        public void drafarc(double arc, Brush rgb, bool IsFlt = false, double stararc = 0, double Lineintervar = 2, bool ISminus = false)
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
            double interval = 90 - (360 / (MaxArc * 2) * arc);//算出实际角度
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
                nDiameter = (int)(this.Width - 40);
            }
            else
            {
                nDiameter = (int)(this.Height - 40);

            }

            double interval = 360 / (MaxArc * 2);

            nCenterPoint = new Point(nDiameter / 2, nDiameter / 2);//new System.Drawing.Point(nDiameter / 2, nDiameter / 2);
            nRadius = nDiameter / 2;
            double nLength = 0;
            Grid g1 = new Grid();
            g1.Height = this.Height - 20;
            g1.Width = this.Width - 20;
            g1.Margin = new Thickness(20, 20, 0, 0);
            int j = 0;
            for (double i = stararc; i < stararc + 360; i += interval * buttenLine)
            {

                Label tb = new Label();
                Line l = new Line();
                l.Stroke = rgb;
                l.StrokeThickness = 1;
                nLength = nRadius - (nDiameter / 16);//线的长短
                DrawLine(ref l, nLength, i);
                //   tb.Content = (((i - 90) / MaxArc）-1）* buttenLine).ToString();

                tb.Content = j.ToString();

                //double[] dbs = Lablemark(nLength+50, i);
                //tb.Margin = new Thickness(dbs[0], dbs[1], dbs[2], dbs[3]);
                if (i - 90 < 90)
                {
                    tb.Margin = new Thickness(l.X2 - 10, l.Y2 - 20, 0, 0);
                }
                else if (i - 90 < 180)
                {
                    tb.Margin = new Thickness(l.X2 - 10, l.Y2 - 10, 0, 0);
                }
                else if (i - 90 == 90)
                {
                    tb.Margin = new Thickness(l.X1 - 20, l.Y1, 0, 0);
                }
                else if (i - 90 < 270)
                {
                    tb.Margin = new Thickness(l.X1, l.Y1 + 10, 0, 0);
                }
                else if (i - 90 == 270)
                {
                    tb.Margin = new Thickness(l.X1+6, l.Y1-18, 0, 0);
                }
                else
                {
                    tb.Margin = new Thickness(l.X1 + 10, l.Y1 - 30, 0, 0);
                }
                //RotateTransform rt = new RotateTransform(i - 90);
                //tb.RenderTransform = rt;
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
            backGrid.Children.Add(g1);
        }

        /// <summary>
        /// 画圆
        /// </summary>
        /// <param name="rgb"></param>
        public void DraEliee(Brush rgb)
        {
            if (this.Width < this.Height)
            {
                nDiameter = (int)this.Width - 80;
            }
            else
            {
                nDiameter = (int)this.Height - 80;

            }
            nCenterPoint = new Point(nDiameter / 2, nDiameter / 2);//new System.Drawing.Point(nDiameter / 2, nDiameter / 2);
            nRadius = nDiameter / 2;
            double nLength = 0;
            int templinnumb = 0;
            for (double i = 0; i < 360; i ++)
            {
                templinnumb++;
                Line l = new Line();
                l.Stroke = rgb;
                l.StrokeThickness = _LinWidth;               
                nLength = nRadius - (nDiameter / 20);
                lines.Add(l);                
                DrawLine(ref l, nLength, i);
                grid.Children.Add(l);

            }
            int aa = templinnumb;
        }



  

        private void AddLable()
        {
            this.showarc.Margin = new Thickness((this.Width / 10) - (showarc.Width / 2), (this.Height / 32) - (this.showarc.Height / 2),0,0);
        }

        /// <summary>
        /// 绘制背景
        /// </summary>
        /// <param name="waikedu">外刻度颜色</param>
        /// <param name="maikedu">内刻度颜色</param>
        /// <param name="Ellicle">圆的颜色</param>
        public void draback(Brush waikedu, Brush maikedu, Brush Ellicle, bool ISdrawWai = false)
        {

            DraEliee(Ellicle);
            //drawclre((int)this.MaxArc * 2, waikedu);
            //if (ISdrawWai)
            //{
                drakedu(maikedu, 10);
            //}
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
        public void SetFltSai(Brush fltColor, Brush Ellicle, Brush SaiColor, double fltval, double fltfvalu, double sai, double fsai)
        {

            this.DraEliee(Ellicle);
            this.drakedu( new SolidColorBrush(Color.FromRgb(251, 154, 6)), 10);

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
            showarc.Content =(int)RunValu+"度";

            if (RunValu > 0)
            {
                if (lastvalue >= RunValu)
                {
                    ChangEColor(0, 360, ElicleColor);
                }
                lastvalue = RunValu;
               if (RunValu > BackValue)
                {
                    drawclre(RunValu, RunColor);
                    drawclre(RunValu - FltValu, FltColor, false, FltValu);
                    drawclre(RunValu - BackValue, BackColor, false, BackValue);
                    //ChangEColor(0, FltValu, RunColor);
                    //ChangEColor(FltValu, BackValue, FltColor);
                    //ChangEColor(FltValu, RunValu-BackValue, OutFltColor);
                }
                else if (RunValu > FltValu)
                {
                    drawclre(RunValu, RunColor);
                    drawclre(RunValu - FltValu, FltColor,false,FltValu);
                }
               
                else
                {
                    drawclre(RunValu, RunColor);
                }
            }
            else if (RunValu == 0)
            {
                ChangEColor(0, 360, ElicleColor);
            }
            else
            {
                if (lastvalue <= RunValu)
                {
                    ChangEColor(0, 360, ElicleColor);
                }
                lastvalue = RunValu;
                if (RunValu < BackMinusValu)
                {
                    drawclre(RunValu, RunColor);
                    drawclre(-(FltMinusValue - RunValu), FltColor, false, FltMinusValue);
                    drawclre(-(BackMinusValu - RunValu), BackColor, false, BackMinusValu);
                }
                else if (RunValu < FltMinusValue)
                {
                    drawclre(RunValu, RunColor);
                    drawclre(-(FltMinusValue - RunValu), FltColor, false, FltMinusValue);

                }

                else
                {
                    drawclre(RunValu, RunColor);
                }
            }
            //以前逻辑
        }

        /// <summary>
        /// 复位
        /// </summary>
        public void Rest()
        {
            loginRun(0);
        }
        #endregion

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            DraEliee(ElicleColor);
            this.fltz.Stroke = FltColor1;
            this.fltf.Stroke = FltColor1;
            this.saif.Stroke = FltColor1;
            this.saiz.Stroke = FltColor1;
            this.saif1.Stroke = FltColor1;
            this.saiz1.Stroke = FltColor1;
           //drawclre((int)this.MaxArc * 2, Brushes.Red);
            drakedu(Brushes.Red, 10);
            AddLable();
        }

        /// <summary>
        /// 运动改变颜色
        /// </summary>
        /// <param name="start">开始角度</param>
        /// <param name="arc">要旋转的角度</param>
        /// <param name="color">旋转颜色</param>
        /// <param name="isZF">角度正负</param>
        private void ChangEColor(double start,double arc,Brush color,bool isZF=false)
        {
            //Console.WriteLine("输入："+(arc+start));
            try
            {

                if (arc + start > lines.Count)
                {
                    //Console.WriteLine("超出值：" + (arc + start));
                    return;
                }
                //if (isZF == false)
                //{
                
                if (isZF == false)
                {

                    for (double i = start; i < start + arc; i++)
                    {
                        if (lines.Count > i)
                        {
                            int temparc = Convert.ToInt32(i);//定义零时变量计算要旋转的角度
                            if (temparc >= 0 && temparc < 360 && lines.Count <= 360 && lines.Count > 0)
                            {
                                lines.ToArray()[temparc].Stroke = color;
                                //lines[i].Stroke = color;
                            }
                        }
                    }
                }
                else
                {

                    for (double i = start; i > start + arc; i--)
                    {
                        if (i < 0)
                        {
                            if (lines.Count > i + 360)
                            {
                                int temparc = Convert.ToInt32(i + 360);//定义零时变量计算要旋转的角度
                                if (temparc >= 0 && temparc < 360 && lines.Count <= 360 && lines.Count > 0)
                                {
                                    lines.ToArray()[temparc].Stroke = color;
                                }
                            }
                        }
                        else
                        {
                            if (lines.Count > i)
                            {
                                int temparc = Convert.ToInt32(i);
                                if (temparc >= 0 && temparc < 360 && lines.Count <= 360 && lines.Count > 0)
                                {
                                    lines.ToArray()[Convert.ToInt32(i)].Stroke = color;
                                }
                            }
                        }
                    }


                }
            }
            catch (Exception ex)
            {

                //MessageBox.Show((arc + start).ToString());
                throw ex;

            }
            
        }

        private Point AchePrint(double Acr)
        {
            Point p = new Point();

            return p;
            
        }

        /// <summary>
        /// 画痛动点
        /// </summary>
        /// <param name="arc">角度</param>
        /// <param name="fact">等级</param>
        public void DarwMovePoint(double arc, double fact)
        {
            Ellipse ellipse = new Ellipse();//定义椭圆对象

            //属性设置，填充颜色、边粗细、边颜色、宽、高等

            ellipse.Fill = System.Windows.Media.Brushes.Red;

            ellipse.StrokeThickness = 4;

            ellipse.Stroke = System.Windows.Media.Brushes.Red; //边，金黄色

            ellipse.Width = fact;

            ellipse.Height = fact;

            double interval = 360 / (MaxArc * 2);
            nCenterPoint = new Point(nDiameter / 2, nDiameter / 2);//new System.Drawing.Point(nDiameter / 2, nDiameter / 2);
            nRadius = nDiameter / 2;
            double nLength = 0;            
            nLength = nRadius - (nDiameter / 5);
            DrawEllipse(ref ellipse, arc,fact);
            this.grid.Children.Add(ellipse);

        }

        private Ellipse DrawEllipse(ref Ellipse Ellipse, double arc ,double Helt)
        {
            double interval = 360 / (MaxArc * 2);            
            double x = 0;
            double y = 0;
            if (arc >= 0&&arc<=this._MaxArc)
            {
                int aa = (int)(interval * arc+90);
                x = lines[aa].X1;
                y = lines[aa].Y1;
 
            }
            else if (arc >= (-this._MaxArc / 2) && arc < 0)
            {
                int bb =(int) (90 - interval * Math.Abs(arc));
                x = lines[bb].X1;
                y = lines[bb].Y1;
            }
            else
            {
                int cc = (int)(360 - interval * Math.Abs(arc));
                x = lines[cc].X1;
                y = lines[cc].Y1;
            }

            Ellipse.Margin = new Thickness(x - Helt / 2, y - Helt / 2, 0, 0);
            Ellipse.HorizontalAlignment = HorizontalAlignment.Left;
            Ellipse.VerticalAlignment = VerticalAlignment.Top;
            return Ellipse;
        }
    }
}
