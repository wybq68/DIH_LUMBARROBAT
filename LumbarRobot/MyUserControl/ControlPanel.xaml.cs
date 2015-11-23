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
using System.Windows.Threading;

namespace LumbarRobot.MyUserControl
{
    public delegate void PreStopHandler(PreStopArgs e);

    public class PreStopArgs : EventArgs
    {
        bool isCancel = false;

        public bool IsCancel
        {
            get { return isCancel; }
            set { isCancel = value; }
        }
    }

    

    /// <summary>
    /// ControlPanel.xaml 的交互逻辑
    /// </summary>
    public partial class ControlPanel : UserControl
    {

        #region 属性
        private object _lock = new object();

        public static readonly DependencyProperty IsSetStopProperty =
          DependencyProperty.Register("IsSetStop", typeof(bool), typeof(ControlPanel), new FrameworkPropertyMetadata(false, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, new PropertyChangedCallback(IsSetStopPropertyChangedCallback)));

        public bool IsSetStop
        {
            get { return (bool)GetValue(IsSetStopProperty); }
            set { SetValue(IsSetStopProperty, value); }
        }


        public static void IsSetStopPropertyChangedCallback(DependencyObject sender, DependencyPropertyChangedEventArgs arg)
        {
            Application.Current.Dispatcher.Invoke(new Action(
                  () =>
                  {
                      bool obj = (bool)arg.NewValue;
                      if (obj == true)
                      {
                          ControlPanel panel = sender as ControlPanel;
                          panel.IsPlaying = false;
                          panel.imgPlay.Source = new BitmapImage(new Uri("pack://application:,,,/images/player_playdown.png"));
                          panel.isPlay = true;
                      }
                  }
                  ), DispatcherPriority.ApplicationIdle);
        }



        public static readonly DependencyProperty IsSetPauseProperty =
         DependencyProperty.Register("IsSetPause", typeof(bool), typeof(ControlPanel), new FrameworkPropertyMetadata(false, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, new PropertyChangedCallback(IsSetPausePropertyChangedCallback)));

        public bool IsSetPause
        {
            get { return (bool)GetValue(IsSetPauseProperty); }
            set { SetValue(IsSetPauseProperty, value); }
        }

        public static void IsSetPausePropertyChangedCallback(DependencyObject sender, DependencyPropertyChangedEventArgs arg)
        {
            Application.Current.Dispatcher.Invoke(new Action(
                  () =>
                  {
                      bool obj = (bool)arg.NewValue;
                      if (obj == true)
                      {
                          ControlPanel panel = sender as ControlPanel;
                          panel.isPlay = true;
                          panel.imgPlay.Source = new BitmapImage(new Uri("pack://application:,,,/images/player_playdown.png"));
                      }
                  }
                  ), DispatcherPriority.ApplicationIdle);
        }



        public static readonly DependencyProperty IsSetPlayProperty =
         DependencyProperty.Register("IsSetPlay", typeof(bool), typeof(ControlPanel), new FrameworkPropertyMetadata(false, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, new PropertyChangedCallback(IsSetPalyPropertyChangedCallback)));

        public bool IsSetPlay
        {
            get { return (bool)GetValue(IsSetPlayProperty); }
            set { SetValue(IsSetPlayProperty, value); }
        }

        public static void IsSetPalyPropertyChangedCallback(DependencyObject sender, DependencyPropertyChangedEventArgs arg)
        {
            Application.Current.Dispatcher.Invoke(new Action(
                  () =>
                  {
                      bool obj = (bool)arg.NewValue;
                      if (obj == true)
                      {
                          ControlPanel panel = sender as ControlPanel;
                          panel.isPlay = false;
                          panel.imgPlay.Source = new BitmapImage(new Uri("pack://application:,,,/images/player_pausedown.png"));
                      }
                  }
                  ), DispatcherPriority.ApplicationIdle);
        }



        public static readonly DependencyProperty IsPlayPrevValidProperty =
        DependencyProperty.Register("IsPlayPrevValid", typeof(bool), typeof(ControlPanel), new FrameworkPropertyMetadata(true, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, new PropertyChangedCallback(IsPlayPrevValidPropertyChangedCallback)));

        public bool IsPlayPrevValid
        {
            get { return (bool)GetValue(IsPlayPrevValidProperty); }
            set { SetValue(IsPlayPrevValidProperty, value); }

        }

        public static void IsPlayPrevValidPropertyChangedCallback(DependencyObject sender, DependencyPropertyChangedEventArgs arg)
        {
            Application.Current.Dispatcher.Invoke(new Action(
                   () =>
                   {
                       bool obj = (bool)arg.NewValue;
                       ControlPanel panel = sender as ControlPanel;

                       if (obj == true)
                       {
                           panel.imgPrev.Source = new BitmapImage(new Uri("pack://application:,,,/images/player_prevdown.png"));
                       }
                       else
                       {
                           panel.imgPrev.Source = new BitmapImage(new Uri("pack://application:,,,/images/player_prev.png"));

                       }
                   }
                   ), DispatcherPriority.ApplicationIdle);

        }

        public static readonly DependencyProperty IsPlayNextValidProperty =
     DependencyProperty.Register("IsPlayNextValid", typeof(bool), typeof(ControlPanel), new FrameworkPropertyMetadata(true, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, new PropertyChangedCallback(IsPlayNextValidPropertyChangedCallback)));

        public bool IsPlayNextValid
        {
            get { return (bool)GetValue(IsPlayNextValidProperty); }
            set { SetValue(IsPlayNextValidProperty, value); }

        }

        public static void IsPlayNextValidPropertyChangedCallback(DependencyObject sender, DependencyPropertyChangedEventArgs arg)
        {
            Application.Current.Dispatcher.Invoke(new Action(
                    () =>
                    {
                        bool obj = (bool)arg.NewValue;
                        ControlPanel panel = sender as ControlPanel;

                        if (obj == true)
                        {
                            panel.imgNext.Source = new BitmapImage(new Uri("pack://application:,,,/images/player_nextdown.png"));
                        }
                        else
                        {
                            panel.imgNext.Source = new BitmapImage(new Uri("pack://application:,,,/images/player_next.png"));

                        }
                    }
                    ), DispatcherPriority.ApplicationIdle);

        }


        //  private bool isPauseValid = true;
        public static readonly DependencyProperty IsPauseValidProperty =
 DependencyProperty.Register("IsPauseValid", typeof(bool), typeof(ControlPanel), new FrameworkPropertyMetadata(true, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, new PropertyChangedCallback(IsPauseValidPropertyChangedCallback)));


        public bool IsPauseValid
        {
            get { return (bool)GetValue(IsPauseValidProperty); }
            set { SetValue(IsPauseValidProperty, value); }

        }

        public static void IsPauseValidPropertyChangedCallback(DependencyObject sender, DependencyPropertyChangedEventArgs arg)
        {
            Application.Current.Dispatcher.Invoke(new Action(
                     () =>
                     {
                         bool obj = (bool)arg.NewValue;
                         ControlPanel panel = sender as ControlPanel;

                         if (obj == true)
                         {
                             panel.imgPrev.Source = new BitmapImage(new Uri("pack://application:,,,/images/player_pausedown.png"));
                         }
                         else
                         {
                             panel.imgPrev.Source = new BitmapImage(new Uri("pack://application:,,,/images/player_pause.png"));

                         }
                     }
                     ), DispatcherPriority.ApplicationIdle);

        }


        public static readonly DependencyProperty IsCanStopProperty =
DependencyProperty.Register("IsCanStop", typeof(bool), typeof(ControlPanel), new FrameworkPropertyMetadata(false, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, new PropertyChangedCallback(IsCanStopPropertyChangedCallback)));


        public bool IsCanStop
        {
            get { return (bool)GetValue(IsCanStopProperty); }
            set { SetValue(IsCanStopProperty, value); }

        }

        public static void IsCanStopPropertyChangedCallback(DependencyObject sender, DependencyPropertyChangedEventArgs arg)
        {
            Application.Current.Dispatcher.Invoke(new Action(
                      () =>
                      {
                          ControlPanel panel = sender as ControlPanel;
                          bool flag = (bool)arg.NewValue;
                          if (flag == true)
                          {
                              panel.IsPlaying = false;
                              panel.imgPlay.Source = new BitmapImage(new Uri("pack://application:,,,/images/player_playdown.png"));
                              panel.isPlay = true;
                          }
                      }
                      ), DispatcherPriority.ApplicationIdle);
        }




        private bool isPlay = true;

        //private bool isPlaying = false;

        public static readonly DependencyProperty IsPlayingProperty =
DependencyProperty.Register("IsPlaying", typeof(bool), typeof(ControlPanel), new FrameworkPropertyMetadata(false, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, new PropertyChangedCallback(IsPlayingPropertyChangedCallback)));


        public bool IsPlaying
        {
            get { return (bool)GetValue(IsPlayingProperty); }
            set { SetValue(IsPlayingProperty, value); }

        }

        public static void IsPlayingPropertyChangedCallback(DependencyObject sender, DependencyPropertyChangedEventArgs arg)
        {
            Application.Current.Dispatcher.Invoke(new Action(
                      () =>
                      {
                          bool obj = (bool)arg.NewValue;
                          ControlPanel panel = sender as ControlPanel;

                          if (obj == true)
                          {
                              panel.imgStop.Source = new BitmapImage(new Uri("pack://application:,,,/images/player_stopdown.png"));
                          }
                          else
                          {
                              panel.imgStop.Source = new BitmapImage(new Uri("pack://application:,,,/images/player_stop.png"));

                          }
                      }
                      ), DispatcherPriority.ApplicationIdle);

        }


        public static readonly DependencyProperty IsInitProperty =
DependencyProperty.Register("IsInit", typeof(bool), typeof(ControlPanel), new FrameworkPropertyMetadata(false, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, new PropertyChangedCallback(IsInitPropertChangedCallback)));

        public bool IsInit
        {
            get { return (bool)GetValue(IsInitProperty); }
            set
            {
                SetValue(IsInitProperty, value);
            }

        }

        public static void IsInitPropertChangedCallback(DependencyObject sender, DependencyPropertyChangedEventArgs arg)
        {
            bool obj = (bool)arg.NewValue;
            ControlPanel panel = sender as ControlPanel;

            if (obj == true)
            {
                Application.Current.Dispatcher.Invoke(new Action(
                       () =>
                       {
                           panel.imgPlay.Source = new BitmapImage(new Uri("pack://application:,,,/images/player_playdown.png"));
                           panel.imgPrev.Source = new BitmapImage(new Uri("pack://application:,,,/images/player_prevdown.png"));
                           panel.imgStop.Source = new BitmapImage(new Uri("pack://application:,,,/images/player_stop.png"));
                           panel.imgNext.Source = new BitmapImage(new Uri("pack://application:,,,/images/player_nextdown.png"));
                           panel.IsPlaying = false;
                           panel.IsCanStop = false;
                           panel.IsPauseValid = true;
                           panel.IsPlayNextValid = true;
                           panel.IsPlayPrevValid = true;
                           panel.IsSetPlay = false;
                           panel.IsSetPause = false;
                           panel.IsSetStop = false;



                       }
                       ), DispatcherPriority.ApplicationIdle);
            }
        }

        public static readonly DependencyProperty IsRegRemoteProperty =
DependencyProperty.Register("IsRegRemot", typeof(bool), typeof(ControlPanel), new FrameworkPropertyMetadata(false, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, new PropertyChangedCallback(IsRegRemotePropertyChangedCallback)));


        public bool IsRegRemot
        {
            get { return (bool)GetValue(IsRegRemoteProperty); }
            set { SetValue(IsRegRemoteProperty, value); }

        }

        public static void IsRegRemotePropertyChangedCallback(DependencyObject sender, DependencyPropertyChangedEventArgs arg)
        {
            bool obj = (bool)arg.NewValue;
            ControlPanel panel = sender as ControlPanel;
        }

        #endregion

        #region 事件
        /// <summary>
        /// 开始
        /// </summary>
        public event EventHandler PlayClick;
        /// <summary>
        /// 下一个
        /// </summary>
        public event EventHandler NextClick;
        /// <summary>
        /// 上一个
        /// </summary>
        public event EventHandler PrevClick;
        /// <summary>
        /// 停止
        /// </summary>
        public event EventHandler StopClick;
        /// <summary>
        /// 暂停
        /// </summary>
        public event EventHandler PauseClick;
        #endregion

        #region 构造
        public ControlPanel()
        {
            InitializeComponent();
        }
        #endregion

        #region 开始
        private void btnPlay_Click(object sender, RoutedEventArgs e)
        {
            IsPlaying = true;
           
            if (isPlay)
            {
                imgPlay.Source = new BitmapImage(new Uri("pack://application:,,,/images/player_pausedown.png"));
                isPlay = false;

                if (PlayClick != null)
                    PlayClick(sender, e);
               
            }
            else
            {
                imgPlay.Source = new BitmapImage(new Uri("pack://application:,,,/images/player_playdown.png"));
                isPlay = true;

                if (PauseClick != null)
                    PauseClick(sender, e);
            }

        }
        #endregion

        #region 停止
        private void btnStop_Click(object sender, RoutedEventArgs e)
        {
            IsInit = true;
            isPlay = true;
            IsPlaying = false;
            imgPlay.Source = new BitmapImage(new Uri("pack://application:,,,/images/player_playdown.png"));
            if (StopClick != null)
            {
                StopClick(sender, e);
            }
        }
        #endregion

        #region 下一个
        private void btnNext_Click(object sender, RoutedEventArgs e)
        {
            if (NextClick != null)
                NextClick(sender, e);
        }
        #endregion

        #region 上一个
        private void btnPrev_Click(object sender, RoutedEventArgs e)
        {
            if (PrevClick != null)
                PrevClick(sender, e);
        }
        #endregion

        #region 初始化
        /// <summary>
        /// 初始化
        /// </summary>
        public void Init(int selectIndex,int itemsCount)
        {
            if (selectIndex == itemsCount - 1 && itemsCount > 1)
            {
                this.imgNext.IsEnabled = false;
                this.IsPlayNextValid = false;
                this.imgPrev.IsEnabled = true;
                this.IsPlayPrevValid = true;
            }
            else if (selectIndex == 0 && itemsCount > 1)
            {
                this.imgNext.IsEnabled = true;
                this.IsPlayNextValid = true;
                this.imgPrev.IsEnabled = false;
                this.IsPlayPrevValid = false;
            }
            else if (itemsCount == 1)
            {
                this.imgNext.IsEnabled = false;
                this.IsPlayNextValid = false;
                this.imgPrev.IsEnabled = false;
                this.IsPlayPrevValid = false;
            }
            else
            {
                this.imgNext.IsEnabled = true;
                this.imgPrev.IsEnabled = true;
                this.IsPlayNextValid = true;
                this.IsPlayPrevValid = true;
            }

            this.IsPlaying = false;
            this.IsCanStop = false;
            this.IsPauseValid = true;
            this.IsSetPlay = false;
            this.IsSetPause = false;
            this.IsSetStop = false;
            this.isPlay = true;
            this.imgPlay.Source = new BitmapImage(new Uri("pack://application:,,,/images/player_playdown.png"));
        }
        #endregion
    }
}
