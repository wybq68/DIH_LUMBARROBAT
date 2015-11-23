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
using System.Drawing;
using System.Drawing.Drawing2D;

namespace LumbarRobot.ActionUserControl
{
    /// <summary>
    /// SinControl.xaml 的交互逻辑
    /// </summary>
    public partial class SinControl : UserControl
    {
        public SinControl()
        {
            InitializeComponent();
        }

        private void picback_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
        {
            //Darwsin(1);

        }


        public void DaringSinXY()
        {
            Graphics g = this.picback.CreateGraphics();
            g.SmoothingMode = SmoothingMode.AntiAlias;
            System.Drawing.Pen pn = new System.Drawing.Pen(System.Drawing.Color.Green, 2);
            g.Clear(this.picback.BackColor);
            //画Y轴
            g.DrawLine(pn, 0, 0, 0, this.picback.Height);
            //画X轴
            g.DrawLine(pn, 0, this.picback.Height / 2, this.picback.Width, this.picback.Height / 2);
            pn.Dispose();
            g.Dispose();
        }

        public void Darwsin(int numb)
        {
            DaringSinXY();
            draw(numb);
            Drawline(numb);
        }

        public void draw(int numb)
        {
            Graphics g = this.picback.CreateGraphics();
            g.SmoothingMode = SmoothingMode.AntiAlias;
            //g.Clear(this.picback.BackColor);
            System.Drawing.Pen pn = new System.Drawing.Pen(System.Drawing.Color.Green, 2);
            int maxY = (int)this.picback.Height;//获取窗体的高
            int maxX = (int)this.picback.Width - 1;//获取窗体的宽

            double f = numb * 2.0 * Math.PI / maxX;
            int lmin = 0;
            int rmax = maxX; //this.picback.Width;//把具体的宽度分成200等份
            int count = rmax - lmin;
            PointF[] mypoint = new PointF[count + 1];
            int x = 0;
            for (int i = lmin; i <= rmax; i++)
            {
                //0.5为半个波形占容器高度的50%
                //根据具体的容器高度设置PointF点的Y坐标，（具体情况而定）

                double temp = 0.5 * (maxY - maxY * Math.Sin(f * i));
                mypoint[x] = new PointF((float)i, (float)temp);
                x++;
            }
            g.DrawCurve(pn, mypoint);
            pn.Dispose();
            g.Dispose();
        }

        private void Drawline(int numb)
        {
            Graphics g = this.picback.CreateGraphics();
            g.SmoothingMode = SmoothingMode.AntiAlias;
            System.Drawing.Pen pn = new System.Drawing.Pen(System.Drawing.Color.Red, 1);
            pn.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;
            int tempw = this.picback.Width / numb;
            for (int i = 0; i <= this.picback.Width; i += tempw)
            {
                //g.DrawLine(pn, i, 0, i, this.Height);
                g.DrawLine(pn, (float)i, 0f, (float)i, (float)this.Height);
            }
        }
    }
}
