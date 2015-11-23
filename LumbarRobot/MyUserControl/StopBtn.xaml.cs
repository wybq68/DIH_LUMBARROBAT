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
using LumbarRobot.Services;

namespace LumbarRobot.MyUserControl
{
    /// <summary>
    /// StopBtn.xaml 的交互逻辑
    /// </summary>
    public partial class StopBtn : UserControl
    {
        public StopBtn()
        {
            InitializeComponent();
        }

        #region 停止
        private void btnStop_Click(object sender, RoutedEventArgs e)
        {
            LumbarRobotController.RobotController.ControlCommand.PauseCmd();
        }
        #endregion
    }
}
