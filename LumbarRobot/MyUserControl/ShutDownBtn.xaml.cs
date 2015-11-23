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
using LumbarRobot.Common;

namespace LumbarRobot.MyUserControl
{
    /// <summary>
    /// ShutDownBtn.xaml 的交互逻辑
    /// </summary>
    public partial class ShutDownBtn : UserControl
    {
        public ShutDownBtn()
        {
            InitializeComponent();
        }

        #region 关机
        private void btnShutDown_Click(object sender, RoutedEventArgs e)
        {
           
            WPFMessageBox msgBox = new WPFMessageBox();
            msgBox.lblMsg.Text = "是否关机！";
            msgBox.lblTitle.Text = "提示信息";
            msgBox.Topmost = true;
            msgBox.ShowDialog();
            if (msgBox.IsFlag)
            {
                Application.Current.Shutdown();
                System.Threading.Thread.Sleep(1000);
                WindowsAPI.DoExitWin(WindowsAPI.EWX_SHUTDOWN);
            }
            else
            {
            }
        }
        #endregion
    }
}
