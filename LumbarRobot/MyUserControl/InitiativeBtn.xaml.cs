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
    /// InitiativeBtn.xaml 的交互逻辑
    /// </summary>
    public partial class InitiativeBtn : UserControl
    {
        public InitiativeBtn()
        {
            InitializeComponent();
        }

        #region 主动
        private void BtnInitiative_Click(object sender, RoutedEventArgs e)
        {
            LumbarRobotController.RobotController.ControlCommand.QuickStopCmd();
        }
        #endregion
    }
}
