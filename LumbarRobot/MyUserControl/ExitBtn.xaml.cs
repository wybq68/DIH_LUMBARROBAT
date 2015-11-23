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
using LumbarRobot.Event;
using LumbarRobot.Data;

namespace LumbarRobot.MyUserControl
{
    /// <summary>
    /// ExitBtn.xaml 的交互逻辑
    /// </summary>
    public partial class ExitBtn : UserControl
    {
        public ExitBtn()
        {
            InitializeComponent();
        }

        #region 退出
        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            WPFMessageBox msgBox = new WPFMessageBox();
            msgBox.lblMsg.Text = "是否退出！";
            msgBox.lblTitle.Text = "提示信息";
            msgBox.Topmost = true;
            msgBox.ShowDialog();
            if (msgBox.IsFlag)
            //if (MessageBox.Show("是否退出！", "提示信息", MessageBoxButton.OKCancel, MessageBoxImage.Error) == MessageBoxResult.OK)
            {
                Syspatient patient = null;
                SelectPatientEvent.Instance.Publish(patient);
                //ModuleConstant.PatientId = null;
                //ModuleConstant.PatientName = null;
                //ModuleConstant.UserName = null;
                Application.Current.Shutdown();
            }
        }
        #endregion
    }
}
