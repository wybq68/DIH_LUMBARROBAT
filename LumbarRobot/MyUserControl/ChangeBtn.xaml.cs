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
using LumbarRobot.Event;
using LumbarRobot.Data;

namespace LumbarRobot.MyUserControl
{
    /// <summary>
    /// ChangeBtn.xaml 的交互逻辑
    /// </summary>
    public partial class ChangeBtn : UserControl
    {
        #region 构造
        public ChangeBtn()
        {
            InitializeComponent();
        }
        #endregion


        #region 切换用户
        private void btnChange_Click(object sender, RoutedEventArgs e)
        {
            WPFMessageBox msgBox = new WPFMessageBox();
            msgBox.lblMsg.Text = "是否切换用户！";
            msgBox.lblTitle.Text = "提示信息";
            msgBox.ShowDialog();
            if (msgBox.IsFlag)
            //if (MessageBox.Show("是否切换用户！", "提示信息", MessageBoxButton.OKCancel, MessageBoxImage.Error) == MessageBoxResult.OK)
            {
                Syspatient patient = null;
                SelectPatientEvent.Instance.Publish(patient);
                Login login = new Login();
                login.IsLogin = true;
                login.Show();
            }
        }
        #endregion
    }
}
