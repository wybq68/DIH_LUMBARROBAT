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
    /// MyMessageBox.xaml 的交互逻辑
    /// </summary>
    public partial class MyMessageBox : Window
    {
        #region 变量
        /// <summary>
        /// 是否禁用Fit按钮
        /// </summary>
        public bool IsBtnEnable = false;
        /// <summary>
        /// 退出事件
        /// </summary>
        public event EventHandler BtnIsEnable;
        #endregion

        #region 构造

        #endregion
        public MyMessageBox()
        {
            InitializeComponent();
            IsBtnEnable = false;
           
        }

        #region 事件
        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
            IsBtnEnable = true;
            if (BtnIsEnable != null)
                BtnIsEnable(sender,e);
        }
        #endregion

    }
}
