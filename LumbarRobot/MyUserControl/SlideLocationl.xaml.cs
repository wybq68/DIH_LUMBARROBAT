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

namespace LumbarRobot.MyUserControl
{
    /// <summary>
    /// SlideLocationl.xaml 的交互逻辑
    /// </summary>
    public partial class SlideLocationl : UserControl
    {
        public SlideLocationl()
        {
            InitializeComponent();
            txt2D.Visibility = Visibility.Visible;
            txt3D.Visibility = Visibility.Hidden;
            this.TwoDVisibilityValue = Visibility.Visible;
            this.ThreeDVisibility = Visibility.Hidden;
        }

        #region 属性

        #region 控件是否2D选中值
        public static readonly DependencyProperty TwoDValueCheckProperty =
           DependencyProperty.Register("TwoDValueCheck", typeof(bool), typeof(SlideLocationl), new FrameworkPropertyMetadata(false, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, new PropertyChangedCallback(TwoDValueCheckPropertyChangedCallback)));

        /// <summary>
        /// 控件是否2D选中值
        /// </summary>
        public bool TwoDValueCheck
        {
            get { return (bool)GetValue(TwoDValueCheckProperty); }
            set
            {
                SetValue(TwoDValueCheckProperty, value);
            }
        }

        private static void TwoDValueCheckPropertyChangedCallback(DependencyObject sender, DependencyPropertyChangedEventArgs arg)
        {

        }
        #endregion

        #region 控件是否3D是否显示
        public static readonly DependencyProperty ThreeDVisibilityValueCheckProperty =
           DependencyProperty.Register("ThreeDVisibility", typeof(Visibility), typeof(SlideLocationl), new FrameworkPropertyMetadata(Visibility.Hidden, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, new PropertyChangedCallback(ThreeDVisibilityPropertyChangedCallback)));

        /// <summary>
        /// 控件是否3D是否显示
        /// </summary>
        public Visibility ThreeDVisibility
        {
            get { return (Visibility)GetValue(ThreeDVisibilityValueCheckProperty); }
            set
            {
                SetValue(ThreeDVisibilityValueCheckProperty, value);
            }
        }

        private static void ThreeDVisibilityPropertyChangedCallback(DependencyObject sender, DependencyPropertyChangedEventArgs arg)
        {
            SlideLocationl c = sender as SlideLocationl;
            if (c.ThreeDVisibility == Visibility.Hidden)
            {
                c.TwoDValueCheck = true;
                c.txt2D.Visibility = Visibility.Visible;
                c.txt3D.Visibility = Visibility.Hidden;
                c.DaySwitcher.Style = c.FindResource("normal") as Style;
                c.MonthSwitcher.Style = c.FindResource("selected") as Style;
            }
            else
            {
                c.TwoDValueCheck = false;
                c.txt2D.Visibility = Visibility.Hidden;
                c.txt3D.Visibility = Visibility.Visible;
                c.DaySwitcher.Style = c.FindResource("selected") as Style;
                c.MonthSwitcher.Style = c.FindResource("normal") as Style;

            }
        }
        #endregion

        #region 控件是否2D是否显示
        public static readonly DependencyProperty TwoDVisibilityValueCheckProperty =
           DependencyProperty.Register("TwoDVisibilityValue", typeof(Visibility), typeof(SlideLocationl), new FrameworkPropertyMetadata(Visibility.Hidden, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, new PropertyChangedCallback(TwoDVisibilityPropertyChangedCallback)));

        /// <summary>
        /// 控件是否2D是否显示
        /// </summary>
        public Visibility TwoDVisibilityValue
        {
            get { return (Visibility)GetValue(TwoDVisibilityValueCheckProperty); }
            set { SetValue(TwoDVisibilityValueCheckProperty, value); }
        }

        private static void TwoDVisibilityPropertyChangedCallback(DependencyObject sender, DependencyPropertyChangedEventArgs arg)
        {

            SlideLocationl c = sender as SlideLocationl;
            if (c.TwoDVisibilityValue == Visibility.Hidden)
            {
                c.TwoDValueCheck = false;
                c.txt2D.Visibility = Visibility.Hidden;
                c.txt3D.Visibility = Visibility.Visible;
                c.DaySwitcher.Style = c.FindResource("selected") as Style;
                c.MonthSwitcher.Style = c.FindResource("normal") as Style;

            }
            else
            {
                c.TwoDValueCheck = true;
                c.txt2D.Visibility = Visibility.Visible;
                c.txt3D.Visibility = Visibility.Hidden;
                c.DaySwitcher.Style = c.FindResource("normal") as Style;
                c.MonthSwitcher.Style = c.FindResource("selected") as Style;

            }
        }
        #endregion

        #endregion

        #region 事件
        private void DaySwitcher_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Border b = sender as Border;
            b.Style = this.FindResource("selected") as Style;
            MonthSwitcher.Style = this.FindResource("normal") as Style;
            txt2D.Visibility = Visibility.Hidden;
            txt3D.Visibility = Visibility.Visible;
            this.TwoDVisibilityValue = Visibility.Hidden;
            this.ThreeDVisibility = Visibility.Visible;
        }

        private void MonthSwitcher_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Border b = sender as Border;
            b.Style = this.FindResource("selected") as Style;
            DaySwitcher.Style = this.FindResource("normal") as Style;
            txt2D.Visibility = Visibility.Visible;
            txt3D.Visibility = Visibility.Hidden;
            this.TwoDVisibilityValue = Visibility.Visible;
            this.ThreeDVisibility = Visibility.Hidden;
        }
        #endregion
    }
}
