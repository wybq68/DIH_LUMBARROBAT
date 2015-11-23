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
using LumbarRobot.DAL;
using LumbarRobot.Common;
using System.Windows.Threading;

namespace LumbarRobot.MyUserControl
{
    /// <summary>
    /// SetControl.xaml 的交互逻辑
    /// </summary>
    public partial class SetControl : UserControl
    {
        public static readonly DependencyProperty IsEnableProperty =
            DependencyProperty.Register("IsEnable", typeof(bool), typeof(SetControl), new FrameworkPropertyMetadata(true, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, new PropertyChangedCallback(IsEnablePropertyChangedCallback)));

        public bool IsEnable
        {
            get { return (bool)GetValue(IsEnableProperty); }
            set { SetValue(IsEnableProperty, value); }
        }

        private static void IsEnablePropertyChangedCallback(DependencyObject sender, DependencyPropertyChangedEventArgs arg)
        {
            SetControl c = sender as SetControl;
            bool isenabled = (bool)arg.NewValue;
            c.L_speed.IsEnabled = isenabled;
            c.R_speed.IsEnabled = isenabled;

        }


        public static readonly DependencyProperty MinValueProperty =
           DependencyProperty.Register("MinValue", typeof(int), typeof(SetControl), new FrameworkPropertyMetadata(0));

        /// <summary>
        /// 最小值
        /// </summary>
        public int MinValue
        {
            get { return (int)GetValue(MinValueProperty); }
            set { SetValue(MinValueProperty, value); }
        }


        public static readonly DependencyProperty MaxValueProperty =
           DependencyProperty.Register("MaxValue", typeof(int), typeof(SetControl), new FrameworkPropertyMetadata(200));

        /// <summary>
        /// 最大值
        /// </summary>
        public int MaxValue
        {
            get { return (int)GetValue(MaxValueProperty); }
            set { SetValue(MaxValueProperty, value); }
        }

        public static readonly DependencyProperty SetProperty =
            DependencyProperty.Register("Step", typeof(int), typeof(SetControl), new FrameworkPropertyMetadata(1));
        /// <summary>
        /// 步进值
        /// </summary>
        public int Step
        {
            get { return (int)GetValue(SetProperty); }
            set { SetValue(SetProperty, value); }
        }


        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ValueProperty =
            DependencyProperty.Register("Value", typeof(int), typeof(SetControl), new FrameworkPropertyMetadata(100, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, new PropertyChangedCallback(ValuePropertyChangedCallback), new CoerceValueCallback(CoerceValue)));
        public int Value
        {
            get { return (int)GetValue(ValueProperty); }
            set { SetValue(ValueProperty, value); }
        }


        private static void ValuePropertyChangedCallback(DependencyObject sender, DependencyPropertyChangedEventArgs arg)
        {
            //SetControl obj = sender as SetControl;
            //obj.txtValue.Text = arg.NewValue.ToString();
        }


        /// <summary>
        /// 强制设定value值
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="obj"></param>
        /// <returns></returns>
        private static object CoerceValue(DependencyObject sender, object obj)
        {
            SetControl c = sender as SetControl;
            int newValue = (int)obj;
            int maxValue = c.MaxValue;
            int minValue = c.MinValue;
            if (newValue < minValue)
            {
                return minValue;
            }
            if (newValue > maxValue)
            {
                return maxValue;
            }
            return newValue;
        }


        /// <summary>
        /// 文本属性
        /// </summary>
        public static readonly DependencyProperty ContextTextProperty =
           DependencyProperty.Register("ContextText", typeof(string), typeof(SetControl), new FrameworkPropertyMetadata(null));
        /// <summary>
        /// 文本属性
        /// </summary>
        public string ContextText
        {
            get { return (string)GetValue(ContextTextProperty); }
            set { SetValue(ContextTextProperty, value); }
        }

        bool isLeftButtonEnabled = true;

        public bool IsLeftButtonEnabled
        {
            get { return isLeftButtonEnabled; }
            set
            {
                if (isLeftButtonEnabled != value)
                {
                    isLeftButtonEnabled = value;

                    if (value)
                    {
                        Uri ur = new Uri("/images/addIcon.png", UriKind.Relative);
                        BitmapImage bp = new BitmapImage(ur);
                        L_speed.Source = bp;
                    }
                    else
                    {
                        Uri ur = new Uri("/images/addIconclick.png", UriKind.Relative);
                        BitmapImage bp = new BitmapImage(ur);
                        L_speed.Source = bp;
                    }
                }
            }
        }

        bool isRightButtonEnabled = true;

        public bool IsRightButtonEnabled
        {
            get { return isRightButtonEnabled; }
            set
            {
                if (isRightButtonEnabled != value)
                {
                    isRightButtonEnabled = value;
                    if (value)
                    {
                        Uri ur = new Uri("/images/reductionIconClick.png", UriKind.Relative);
                        BitmapImage bp = new BitmapImage(ur);
                        R_speed.Source = bp;

                    }
                    else
                    {
                        Uri ur = new Uri("/images/reductionIcon.png", UriKind.Relative);
                        BitmapImage bp = new BitmapImage(ur);
                        R_speed.Source = bp;
                    }
                }
            }
        }

        public SetControl()
        {
            InitializeComponent();
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += timer1_Tick;
        }

        #region 事件

        /// <summary>
        /// 开始
        /// </summary>
        public event EventHandler SelectChange;

       
        
        /// <summary>
        /// 左键
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnLeft_Click(object sender, MouseButtonEventArgs e)
        {
            GlobalVar.DispatcherLock.DoAction(() =>
            {
                if (isLeftButtonEnabled && Value > MinValue)
                {
                   
                    Value = Value - Step >= MinValue ? Value - Step : MinValue;
                    if (SelectChange != null)
                        SelectChange(sender, e);

                }
            });
        }

        /// <summary>
        /// 右键
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRight_Click(object sender, MouseButtonEventArgs e)
        {
            GlobalVar.DispatcherLock.DoAction(() =>
            {
                if (isRightButtonEnabled && Value < MaxValue)
                {
                   
                    Value = Value + Step <= MaxValue ? Value + Step : MaxValue;
                    if (SelectChange != null)
                        SelectChange(sender, e);
                }
            });
        }

        /// <summary>
        /// 鼠标进入
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Image_MouseEnter(object sender, MouseEventArgs e)
        {
            Image ig = sender as Image;
            if (ig.Tag.ToString() == "left")
            {
                Uri ur = new Uri("../images/reductionIconClick.png", UriKind.Relative);
                BitmapImage bp = new BitmapImage(ur);
                ig.Source = bp;
            }
            else if (ig.Tag.ToString() == "right")
            {
                Uri ur = new Uri("../images/addIconclick.png", UriKind.Relative);
                BitmapImage bp = new BitmapImage(ur);
                ig.Source = bp;
            }
        }
        /// <summary>
        /// 鼠标离开
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Image_MouseLeave(object sender, MouseEventArgs e)
        {
            Image ig = sender as Image;
            if (ig.Tag.ToString() == "left")
            {
                Uri ur = new Uri("../images/reductionIcon.png", UriKind.Relative);
                BitmapImage bp = new BitmapImage(ur);
                ig.Source = bp;
            }
            else if (ig.Tag.ToString() == "right")
            {
                Uri ur = new Uri("../images/addIcon.png", UriKind.Relative);
                BitmapImage bp = new BitmapImage(ur);
                ig.Source = bp;
            }
        }
        #endregion

        #region 按钮长按事件

        #region 变量
        
        
        private DispatcherTimer timer;

        private bool GoalLevaladd = false;

        private bool GoalLevealDiff = false;

        private int PushTime = 0;//第一次按下时间

        private int PushSoptTime = 2;

        #endregion

        #region 加
        private void R_speed_MouseDown(object sender, MouseButtonEventArgs e)
        {
            PushTime = 0;
            GoalLevaladd = true;
            timer.Start();
        }

        private void R_speed_MouseUp(object sender, MouseButtonEventArgs e)
        {
            GoalLevaladd = false;
            timer.Stop();
        }
        #endregion

        #region 减

        private void L_speed_MouseDown(object sender, MouseButtonEventArgs e)
        {
            PushTime = 0;
            GoalLevealDiff = true;
            timer.Start();
        }

        private void L_speed_MouseUp(object sender, MouseButtonEventArgs e)
        {
            GoalLevealDiff = false;
            timer.Stop();
        }
        #endregion
        private void timer1_Tick(object sender, EventArgs e)
        {
            //（你的定时处理）
            if (GoalLevaladd)
            {
                if (this.Value < MaxValue)
                {
                    if (PushTime < PushSoptTime)
                    {
                        PushTime++;
                        // return;
                    }
                    else
                    {
                        if (this.Value + this.Step > MaxValue)
                        {
                            this.Value = MaxValue;
                            
                        }
                        else
                        {
                            Value = Value + Step;
                        }
                        if (SelectChange != null)
                            SelectChange(sender, e);
                    }

                }
            }
            else if (GoalLevealDiff && this.Value > 0)
            {
                if (this.Value > 0)
                {

                    if (PushTime < PushSoptTime)
                    {
                        PushTime++;
                        //return;
                    }
                    else
                    {
                        if (this.Value - this.Step < MinValue)
                        {
                            Value = MinValue;
                        }
                        else
                        {
                            Value = Value - Step;
                        }
                        if (SelectChange != null)
                            SelectChange(sender, e);
                    }
                }

            }
        }
        #endregion


        
    }
}
