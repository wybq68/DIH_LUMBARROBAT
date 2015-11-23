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
using System.Windows.Shapes;
using System.Windows.Threading;
using System.Threading;

namespace LumbarRobot
{
    /// <summary>
    /// Window1.xaml 的交互逻辑
    /// </summary>
    public partial class Window1 : Window
    {
        public Window1()
        {
            InitializeComponent();
        }

        private void cirCle1_Loaded(object sender, RoutedEventArgs e)
        {
           
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            //wpfclire1.drawclre(80, Brushes.Orange, true, 360);
            //wpfclire1.draback(Brushes.Gray, Brushes.Green, Brushes.Blue);
            //int arc = Convert.ToInt32(textBox1.Text.Trim());
            //wpfclire1.drawclre(arc, Brushes.Orange,true,0);
            //this.eilelc21.drawclre(20, Brushes.Aqua);
        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            int arc=Convert.ToInt32(textBox2.Text.Trim());
            //wpfclire1.draback(Brushes.Gray, Brushes.Green, Brushes.Blue);

            int arc1 = Convert.ToInt32(textBox1.Text.Trim());
           // wpfclire1.drawclre(arc1, Brushes.Orange,true);

           // wpfclire1.drawclre(arc, Brushes.Red);
        }

        private void button3_Click(object sender, RoutedEventArgs e)
        {
            //int arc1 = Convert.ToInt32(textBox3.Text.Trim());
            //this.eilelc21.FltValu = arc1;
            //wpfclire1.draback(Brushes.Gray, Brushes.Green, Brushes.Blue);
            //wpfclire1.drafarc(arc1, Brushes.Orange, true,0);
            //wpfclire1.DraEliee(Brushes.Blue);
        }

        private void button5_Click(object sender, RoutedEventArgs e)
        {
           // wpfclire1.draback(Brushes.Gray, Brushes.Green, Brushes.Blue);
            //int arc = Convert.ToInt32(textBox5.Text.Trim());
            //int arc1 = Convert.ToInt32(textBox4.Text.Trim());
            //////if (arc1 > 15)
            //////{
            //////   // wpfclire1.drawclre(15, Brushes.Orange, true, arc);
            //////  //  wpfclire1.drawclre(arc1 - 15, Brushes.Red, false, arc + 15);
            //////}
            //////else
            //////{
            //////   // wpfclire1.drawclre(arc1, Brushes.Orange, true, arc);
            //////}
        }

        DispatcherTimer dts = new DispatcherTimer();

        private void button4_Click(object sender, RoutedEventArgs e)
        {
            dts.Interval = new TimeSpan(50000);
            dts.Tick += new EventHandler(dts_Tick);
            dts.Start();
        }
        int text = 0;
        void dts_Tick(object sender, EventArgs e)
        {
            Random ran = new Random();
            int a = ran.Next(40, 55);
            //text++;
            //textBox1.Text = text.ToString();
            //if (text > 20)
            //{
            //    text = 0;
            //}
            //this.eilelc21.loginRun(a);
           // this.eilelc21.loginRun(a);
            //sinWpf1.DrawRunLine(a);
           // timeSina1.DrawRunLine(a);
            Thread.Sleep(100);
        }

        private void button6_Click(object sender, RoutedEventArgs e)
        {
            Random ran = new Random();
            int a= ran.Next(-60, 60);
            //this.sinWpf1.DrawRunLine(a);
        }

        private void button7_Click(object sender, RoutedEventArgs e)
        {
            //this.sinWpf1.RunPI = Convert.ToInt32(textBox6.Text.Trim());
        }

        int aa = 0;
        int pinum = 1;
        private void button8_Click(object sender, RoutedEventArgs e)
        {
            Random ran = new Random();
            int a = ran.Next(-40, 40);
           // this.sinWpf1.DrawRunLine(a);
            if (aa % 5 == 0)
            {
              //  this.sinWpf1.RunPI = Convert.ToInt32(pinum);
                pinum++;
            }
            aa++;
            
        }

        private void button9_Click(object sender, RoutedEventArgs e)
        {
           // this.wpfclire1.loginRun(0);
        }

        private void button10_Click(object sender, RoutedEventArgs e)
        {
            //this.sinWpf1.INITC();
        }

        private void button11_Click(object sender, RoutedEventArgs e)
        {
            //this.eilelc21.loginRun(Convert.ToDouble(this.runlong.Text.Trim()));
        }

        private void button13_Click(object sender, RoutedEventArgs e)
        {
            double[] pints = new double[30];
            Random rd = new Random();
            pints[0] = 0;
            pints[1] = 0;
            pints[2] = 0;
            pints[3] = 0;
            pints[4] =0;
            pints[5] = 0;
            pints[6] = 0;
            pints[7] = 0;
            pints[8] = 0;
            pints[9] = 0;

            pints[10] = 0;
            pints[11] = 0;
            pints[12] = 0;
            pints[13] = 0;
            pints[14] = 0;
            pints[15] = 0;
            pints[16] = 0;
            pints[17] = 0;
            pints[18] = 0;
            pints[19] = 0;


            pints[20] = 10;
            pints[21] = 10;
            pints[22] =10;
            pints[23] = 10;
            pints[24] = 10;
            pints[25] =10;
            pints[26] = 10;
            pints[27] = 10;
            pints[28] = 10;
            pints[29] = 10;

            //for (int i = 0; i < 10; i++)
            //{               

            //    pints[i]= rd.Next(-60, 60);

               
            //}
           // timeSina1.GoldLine(pints);
        }

        private void slider1_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            //double dd = this.slider1.Value;
            //textBox9.Text = dd.ToString();
//eilelc21.loginRun(dd);
          //  timeSina1.DrawRunLine(dd);
        }

        private void slider2_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
           // double dds = this.slider2.Value;
            //eilelc21.loginRun(dds);
        }

        private void button14_Click(object sender, RoutedEventArgs e)
        {
            //this.userLine1.BarHeight+=5;
            //this.userLine1.BarFactHeght += 4;
        }

        private void button15_Click(object sender, RoutedEventArgs e)
        {
            //this.userLine1.BarHeight -= 5;
            //this.userLine1.BarFactHeght -= 4;
        }

        private void slider4_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            //this.userLine1.BarFactHeght = slider4.Value;
        }

        private void slider3_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            //this.userLine1.BarHeight = slider3.Value;
        }

        private void button16_Click(object sender, RoutedEventArgs e)
        {
            //timeSina1.ReSet();
        }

        private void button17_Click(object sender, RoutedEventArgs e)
        {
            //eilelc21.m
            double arc = Convert.ToDouble(textBox8.Text);
            eilelc21.DarwMovePoint(arc, 10);
        }

        private void button18_Click(object sender, RoutedEventArgs e)
        {
            List<double> arc = new List<double>();
          
            for (int i = 0; i < 350; i++)
            {
                Random rd = new Random();

                arc.Add(rd.Next(-10,30));
        

            }
            //timeSina1.GoldLine(arc.ToArray());
        }

        private void button19_Click(object sender, RoutedEventArgs e)
        {
            //eilelc21.loginRun(Convert.ToDouble(textBox9.Text));
        }

        private void slider1_ValueChanged_1(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            Random rd = new Random();
            this.textBox3.Text = this.slider1.Value.ToString();
            timeSina2.DrawRunLine(this.slider1.Value, this.slider1.Value + 10, this.slider1.Value - 10, (this.slider1.Value + rd.Next(10)).ToString(), (this.slider1.Value - rd.Next(50)).ToString());
           // timeSina2.DrawRunLineTwo(this.slider1.Value + 10);
        }

        private void button2_Click_1(object sender, RoutedEventArgs e)
        {
            timeSina2.ReSet(false);

        }

        private void button3_Click_1(object sender, RoutedEventArgs e)
        {
            timeSina2.MaxArc = Convert.ToDouble(textBox4.Text);
        }

        private void button4_Click_1(object sender, RoutedEventArgs e)
        {
            Random rd = new Random();
            double[] arc = new double[500];
            int s = 5;
            for (int i = 0; i < 500; i++)
            {
                s += 2;
                arc[i] = rd.Next(-60,60);
            }
            timeSina2.GoldLine(arc);
        }

        private void button5_Click_1(object sender, RoutedEventArgs e)
        {
            double ddds = Convert.ToDouble(this.textBox3.Text);
            timeSina2.DrawRunLine(ddds, ddds + 10, ddds - 10, (ddds + 10).ToString(), (ddds - 10).ToString());
        }

        private void slider2_ValueChanged_1(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            timeSina2.DrawRunLine(this.slider2.Value);
        }

        private void button6_Click_1(object sender, RoutedEventArgs e)
        {
            double[] arc = new double[1000];

            arc[0] = 60;
            arc[1] = 0;
            arc[2] = 80;
            arc[3] = 0;
            arc[4] = 50;
            arc[5] = 90;
            arc[6] = 50;
            arc[7] = 90;
            arc[8] = 90;
            timeSina2.ReSet();
        }
    }
}
