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
using LumbarRobot.Requests;
using LumbarRobot.Interactions;
using Remotion.Linq.Collections;
using LumbarRobot.Common;
using LumbarRobot.ViewModels;

namespace LumbarRobot
{
    /// <summary>
    /// ReportPrintControl.xaml 的交互逻辑
    /// </summary>
    public partial class ReportPrintControl : UserControl
    {

        #region 变量
        public event EventHandler Close;

        /// <summary>
        /// 通知消息
        /// </summary>
        public GenericInteractionRequest<AlarmInfo> NotificationToAlarm { get; private set; }

        #endregion

        public ReportPrintControl()
        {
            InitializeComponent();
            NotificationToAlarm = new GenericInteractionRequest<AlarmInfo>();
           // DataContext = new ReportPrintViewModel(this);
            DataContext = new PrintTest(this,gvTest);
        }

        #region 返回
        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close(sender, e);
        }
        #endregion

    }
}
