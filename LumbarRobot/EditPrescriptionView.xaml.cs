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
using LumbarRobot.Enums;
using LumbarRobot.ViewModels;

namespace LumbarRobot
{
    /// <summary>
    /// EditPrescriptionView.xaml 的交互逻辑
    /// </summary>
    public partial class EditPrescriptionView : UserControl
    {

        #region 事件
        /// <summary>
        /// 关闭事件
        /// </summary>
        public event EventHandler Close;
        #endregion

        #region 变量
        private PrescriptionItem MyChangeItem = new PrescriptionItem();
        #endregion

        #region 加载事件
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            BindData();
        }
        #endregion

        #region 构造
        public EditPrescriptionView(PrescriptionItem MyItem)
        {
            InitializeComponent();
            MyChangeItem = MyItem;
        }
        #endregion

        #region 关闭事件
        private void btnClosed_Click(object sender, RoutedEventArgs e)
        {
            if (Close != null)
                Close(sender, e);
        }
        #endregion

        #region 保存修改
        private void btnSavePrescription_Click(object sender, RoutedEventArgs e)
        {
            MyChangeItem.MaxAngle = this.setControlMaxAngle.Value;
            ComboBoxItem cbi = (ComboBoxItem)this.CboAction.SelectedItem;
            MyChangeItem.ActionName = cbi.Content.ToString();
            ComboBoxItem cbMode = (ComboBoxItem)this.CboMode.SelectedItem;
            MyChangeItem.ModeName = cbMode.Content.ToString();
            MyChangeItem.MinAngle = this.setControlMinAngle.Value;
            MyChangeItem.ModeId = this.CboMode.SelectedIndex;
            MyChangeItem.ActionId = this.CboAction.SelectedIndex;
            MyChangeItem.Speed = this.setControlSpeed.Value;
            MyChangeItem.Times = this.setControlTimes.Value;
            MyChangeItem.PForce = this.setControlForce.Value;
            MyChangeItem.LastLocation = this.setControlPosition.Value;
            MyChangeItem.PGroup = this.setControlGroup.Value;
            Close(sender, e);
        }
        #endregion

        #region 绑定数据
        private void BindData()
        {
            if (MyChangeItem != null)
            {
                this.CboAction.SelectedIndex = MyChangeItem.ActionId;
                this.CboMode.SelectedIndex = MyChangeItem.ModeId;
                this.setControlMaxAngle.Value = MyChangeItem.MaxAngle;
                this.setControlMinAngle.Value = MyChangeItem.MinAngle;
                this.setControlSpeed.Value = MyChangeItem.Speed;
                this.setControlTimes.Value = MyChangeItem.Times;
                this.setControlForce.Value = MyChangeItem.PForce;
                this.setControlPosition.Value = MyChangeItem.LastLocation;
                if (MyChangeItem.PGroup.HasValue)
                {
                    this.setControlGroup.Value = MyChangeItem.PGroup.Value;
                }
            }
        }
        #endregion

        #region 动作改变
        private void CboAction_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (CboAction.SelectedIndex != 0)
            {
                this.setControlSpeed.IsEnabled = true;
                this.setControlForce.IsEnabled = true;
                this.setControlMinAngle.IsEnabled = true;
                this.setControlMaxAngle.IsEnabled = true;
                this.setControlTimes.IsEnabled = true;
                this.CboMode.IsEnabled = true;
                this.setControlGroup.IsEnabled = true;
                this.setControlPosition.IsEnabled = true;
                if (CboAction.SelectedIndex == (int)ActionEnum.Rotation)
                {
                    this.setControlMaxAngle.MinValue = 0;
                    this.setControlMaxAngle.MaxValue = 35;
                    this.setControlMinAngle.MinValue = -35;
                    this.setControlMinAngle.MaxValue = 0;
                    this.setControlMinAngle.Value = -20;
                    this.setControlMaxAngle.Value = 20;
                }
                if (CboAction.SelectedIndex == (int)ActionEnum.Protrusive)
                {
                    this.setControlMaxAngle.MinValue = 0;
                    this.setControlMaxAngle.MaxValue = 80;
                    this.setControlMinAngle.MinValue = -80;
                    this.setControlMinAngle.MaxValue = 0;
                    this.setControlMinAngle.Value = 0;
                    this.setControlMaxAngle.Value = 20;
                }
                if (CboAction.SelectedIndex == (int)ActionEnum.Bend)
                {
                    this.setControlMaxAngle.MinValue = 0;
                    this.setControlMaxAngle.MaxValue = 80;
                    this.setControlMinAngle.MaxValue = 0;
                    this.setControlMinAngle.MinValue = -60;
                    this.setControlMinAngle.Value = -20;
                    this.setControlMaxAngle.Value = 0;
                }
                if (CboAction.SelectedIndex == (int)ActionEnum.ProtrusiveOrBend)
                {
                    this.setControlMinAngle.MinValue = -60;
                    this.setControlMinAngle.MaxValue = 0;
                    this.setControlMaxAngle.MaxValue = 80;
                    this.setControlMaxAngle.MinValue = 0;
                    this.setControlMinAngle.Value = -20;
                    this.setControlMaxAngle.Value = 20;
                }
            }
            else
            {
                CboMode.SelectedIndex = 0;
                this.setControlSpeed.Value = 2;
                this.setControlForce.Value = 3;
                this.setControlMinAngle.Value = -20;
                this.setControlMaxAngle.Value = 20;
                this.setControlTimes.Value = 5;
                this.setControlGroup.Value = 1;
            }
        }

        #endregion

       
    }
}
