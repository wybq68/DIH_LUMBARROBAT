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
using System.Windows.Shapes;

namespace LumbarRobot.MyUserControl
{
    /// <summary>
    /// AlarmDialog.xaml 的交互逻辑
    /// </summary>
    public partial class AlarmDialog : Window
    {
        #region 变量
        public bool IsFlag = false;
        #endregion

        #region 构造
        public AlarmDialog()
        {
            InitializeComponent();
        }
        #endregion

        #region 确定
        private void btnYes_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        #endregion
    }
}
