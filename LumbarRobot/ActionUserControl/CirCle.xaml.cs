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

namespace LumbarRobot.ActionUserControl
{
    /// <summary>
    /// CirCle.xaml 的交互逻辑
    /// </summary>
    public partial class CirCle : UserControl
    {
        public CirCle()
        {
            InitializeComponent();
        } 
        private int _LineNub = 2;
        /// <summary>
        /// 刻度条数
        /// </summary>
        public int LineNub
        {
            get { return _LineNub; }
            set { _LineNub = value; }
        }

        /// <summary>
        /// 放大倍数
        /// </summary>
        int linb = 20;

        private int nDiameter = 200;
        private int nRadius;
        private System.Drawing.Point nCenterPoint;
        Graphics g;

        private void p1_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
        {
            if (this.p1.Width < this.p1.Height)
            {
                nDiameter = (int)this.p1.Width - 2;
            }
            else
            {
                nDiameter = (int)this.p1.Height - 2;

            }
            g = this.p1.CreateGraphics();
            bimt = new Bitmap(nDiameter, nDiameter);
            DrawCenter(g);


        }
        Bitmap bimt;

        /// <summary>
        /// 画背景员
        /// </summary>
        /// <param name="g"></param>
        public void DrawCenter(Graphics g)
        {
            
            nCenterPoint = new System.Drawing.Point(nDiameter / 2, nDiameter / 2);
            nRadius = nDiameter / 2;
            System.Drawing.Pen p = new System.Drawing.Pen(System.Drawing.Color.Blue, 3);
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            g.DrawEllipse(p, 0, 0, nDiameter, nDiameter);
            //g.DrawEllipse(p, 0, 0, 280, 280);

        }
        /// <summary>
        /// 画训练值（刻度）
        /// </summary>
        /// <param name="arc">角度</param>
        /// <param name="drawColor">颜色</param>
        public void DrawArc(float arc, System.Drawing.Color drawColor)
        {
            g = this.p1.CreateGraphics();
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            System.Drawing.Pen p = new System.Drawing.Pen(drawColor, 1);
            int nLength = 0;
            for (int i = 0; i < arc * linb; i += 2)
            {

                nLength = nRadius - (nDiameter / 30);

                g.DrawLine(
                    p,
                    new System.Drawing.Point((int)(nLength * Math.Cos(i * Math.PI / 180)) + nCenterPoint.X,
                    (int)(-nLength * Math.Sin(i * Math.PI / 180)) + nCenterPoint.X),
                    new System.Drawing.Point((int)(nDiameter / 2 * Math.Cos(i * Math.PI / 180)) + nCenterPoint.X,
                    (int)(-(nDiameter / 2) * Math.Sin(i * Math.PI / 180)) + nCenterPoint.X));
            }
        }





    }
}
