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
    /// WPFMessageBox.xaml 的交互逻辑
    /// </summary>
    public partial class WPFMessageBox : Window
    {
        #region 变量
        public bool IsFlag = false;
        #endregion

        #region 构造
        public WPFMessageBox()
        {
            InitializeComponent();
        }
        #endregion

        #region 事件
        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
            IsFlag = true;
            this.Close();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            IsFlag = false;
            this.Close();
        }
        #endregion
    }
}
