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
    /// Pager.xaml 的交互逻辑
    /// </summary>
    public partial class Pager : UserControl
    {
        public Pager()
        {
            InitializeComponent();
        }

        public static readonly DependencyProperty PageSizeProperty =
          DependencyProperty.Register("PageSize", typeof(int), typeof(Pager), new FrameworkPropertyMetadata(20, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

        /// <summary>
        /// 每页显示记录数
        /// </summary>
        public int PageSize
        {
            get
            {
                return (int)GetValue(PageSizeProperty);
            }
            set
            {
                SetValue(PageSizeProperty, value);
            }
        }

        private static void PageSizePropertyChangedCallback(DependencyObject sender, DependencyPropertyChangedEventArgs arg)
        {
            Pager p = sender as Pager;

            int pz = (int)arg.NewValue;
            if (p.NMax > 0)
            {
                p.PageCount = Convert.ToInt32(Math.Ceiling(Convert.ToDouble(p.NMax) / Convert.ToDouble(pz)));
            }
            else
            {
                p.PageCount = 0;
            }
        }

        public static readonly DependencyProperty NMaxProperty =
         DependencyProperty.Register("NMax", typeof(int), typeof(Pager), new FrameworkPropertyMetadata(0, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, new PropertyChangedCallback(NMaxPropertyChangedCallback)));

        /// <summary>
        /// 总记录数
        /// </summary>
        public int NMax
        {
            get
            {
                return (int)GetValue(NMaxProperty);
            }

            set
            {
                SetValue(NMaxProperty, value);

            }
        }

        private static void NMaxPropertyChangedCallback(DependencyObject sender, DependencyPropertyChangedEventArgs arg)
        {
            Pager p = sender as Pager;

            int pz = p.PageSize;
            int max = (int)arg.NewValue;
            if (p.NMax > 0)
            {
                p.PageCount = Convert.ToInt32(Math.Ceiling(Convert.ToDouble(max) / Convert.ToDouble(pz)));
            }
            else
            {
                p.PageCount = 0;
            }

        }

        public static readonly DependencyProperty PageCountProperty =
       DependencyProperty.Register("PageCount", typeof(int), typeof(Pager), new FrameworkPropertyMetadata(0));
        private int _pageCount;
        /// <summary>
        /// 页数=总记录数/每页显示记录数
        /// </summary>
        public int PageCount
        {
            get { return (int)GetValue(PageCountProperty); }
            set { SetValue(PageCountProperty, value); }
        }



        public static readonly DependencyProperty PageCurrentProperty =
     DependencyProperty.Register("PageCurrent", typeof(int), typeof(Pager), new FrameworkPropertyMetadata(0));

        /// <summary>
        /// 当前页号
        /// </summary>
        public int PageCurrent
        {
            get { return (int)GetValue(PageCurrentProperty); }
            set { SetValue(PageCurrentProperty, value); }
        }



        #region 事件

        public static readonly RoutedEvent PrevBtndEvent =
          EventManager.RegisterRoutedEvent("PrevClick",
           RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(Pager));

        public event RoutedEventHandler PrevClick
        {
            add
            {
                this.AddHandler(PrevBtndEvent, value);
            }
            remove
            {
                this.RemoveHandler(PrevBtndEvent, value);
            }
        }


        /// <summary>
        /// 第一个按钮事件
        /// </summary>
        public static readonly RoutedEvent FristBtndEvent =
          EventManager.RegisterRoutedEvent("FristClick",
           RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(Pager));

        public event RoutedEventHandler FristClick
        {
            add
            {
                this.AddHandler(FristBtndEvent, value);
            }
            remove
            {
                this.RemoveHandler(FristBtndEvent, value);
            }
        }
        /// <summary>
        /// 下一个按钮事件
        /// </summary>
        public static readonly RoutedEvent NextBtndEvent =
          EventManager.RegisterRoutedEvent("NextClick",
           RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(Pager));

        public event RoutedEventHandler NextClick
        {
            add
            {
                this.AddHandler(NextBtndEvent, value);

            }
            remove
            {
                this.RemoveHandler(NextBtndEvent, value);
            }
        }

        /// <summary>
        /// 下一个按钮事件
        /// </summary>
        public static readonly RoutedEvent EndBtndEvent =
          EventManager.RegisterRoutedEvent("EndClick",
           RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(Pager));

        public event RoutedEventHandler EndClick
        {
            add
            {
                this.AddHandler(EndBtndEvent, value);

            }
            remove
            {
                this.RemoveHandler(EndBtndEvent, value);
            }
        }

        #endregion

        private void btnFirst_Click(object sender, RoutedEventArgs e)
        {
            RoutedEventArgs arg = new RoutedEventArgs(FristBtndEvent, this);
            base.RaiseEvent(arg);
        }

        private void btnPrev_Click(object sender, RoutedEventArgs e)
        {
            RoutedEventArgs arg = new RoutedEventArgs(PrevBtndEvent, this);
            base.RaiseEvent(arg);
        }

        private void btnNext_Click(object sender, RoutedEventArgs e)
        {
            RoutedEventArgs arg = new RoutedEventArgs(NextBtndEvent, this);
            base.RaiseEvent(arg);
        }

        private void btnLast_Click(object sender, RoutedEventArgs e)
        {
            RoutedEventArgs arg = new RoutedEventArgs(EndBtndEvent, this);
            base.RaiseEvent(arg);
        }
    }
}
