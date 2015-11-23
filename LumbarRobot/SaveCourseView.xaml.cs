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
using LumbarRobot.Common;
using LumbarRobot.MyUserControl;
using LumbarRobot.DAL;
using LumbarRobot.Data;

using NHibernate;
using System.Collections.ObjectModel;
using LumbarRobot.Event;
using LumbarRobot.ViewModels;

namespace LumbarRobot
{
    /// <summary>
    /// SaveCourseView.xaml 的交互逻辑
    /// </summary>
    public partial class SaveCourseView : UserControl
    {

        #region 变量
        public event EventHandler Close;

        private ObservableCollection<ItemDemo> _actionList;

        public ObservableCollection<ItemDemo> ActionList
        {
            get { return _actionList; }
            set { _actionList = value; }
        }
        /// <summary>
        /// 成功返回
        /// </summary>
        public bool IsSucceed = false;

        #endregion

        public SaveCourseView(ObservableCollection<ItemDemo> ActionList)
        {
            InitializeComponent();
            IsSucceed = false;
            if (ActionList != null)
            {
                this.ActionList = ActionList;
            }
        }

        #region 返回
        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            IntPtr hwnd = WindowsAPI.FindWindow("IPTip_Main_Window", null);
            WindowsAPI.SendMessage(hwnd, WindowsAPI.WM_SYSCOMMAND, WindowsAPI.SC_CLOSE, 0);
            this.Close(sender, e);
        }
        #endregion

        #region 保存
        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(this.txtName.Text.Trim()))
            {
                AlarmDialog dialog = new AlarmDialog();
                dialog.lblTitle.Text = "提示信息";
                dialog.lblMsg.Text = "处方名称能为空！";
                dialog.ShowDialog();
            }
            using (ITransaction tx = MySession.Session.BeginTransaction())
            {
                try
                {
                    Prescription Prescription = new Prescription();
                    string Id= Guid.NewGuid().ToString("N");
                    Prescription.Id = Id;
                    Prescription.PatientId = ModuleConstant.PatientId;
                    Prescription.PrescriptionName = this.txtName.Text.Trim();
                    Prescription.LastTime = DateTime.Now;
                    Prescription.OpTime = DateTime.Now;
                    MySession.Session.Save(Prescription);
                    MySession.Session.Flush();
                    foreach (ItemDemo item in ActionList)
                    {
                        Prescriptiondetails detail = new Prescriptiondetails();
                        detail.Id = Guid.NewGuid().ToString("N");
                        detail.ActionId = item.ActionId;
                        detail.ModeId = item.Mode;
                        detail.PrescriptionId = Id;
                        detail.PForce = item.Force;
                        detail.Speed = item.Speed;
                        detail.MinAngle = item.MinAngle;
                        detail.MaxAngle = item.MaxAngle;
                        detail.PGroup = null;
                        detail.Times = item.Times;
                        detail.LastLocation = item.Position;
                        detail.LastTime = DateTime.Now;
                        MySession.Session.Save(detail);
                        MySession.Session.Flush();
                    }
                    tx.Commit();
                    SaveCourseEvent.Instance.Publish(true);
                    IntPtr hwnd = WindowsAPI.FindWindow("IPTip_Main_Window", null);
                    WindowsAPI.SendMessage(hwnd, WindowsAPI.WM_SYSCOMMAND, WindowsAPI.SC_CLOSE, 0);
                    this.Close(sender, e);

                }
                catch (Exception)
                {
                    tx.Rollback();
                }

            }            
        }
        #endregion


        #region 键盘
        private void txtWeight_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            Machine.OpenTabtip();
        }
        #endregion
    }
}
