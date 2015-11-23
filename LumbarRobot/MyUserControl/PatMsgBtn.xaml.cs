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
using LumbarRobot.ViewModels;

namespace LumbarRobot.MyUserControl
{
    /// <summary>
    /// PatMsgBtn.xaml 的交互逻辑
    /// </summary>
    public partial class PatMsgBtn : UserControl
    {
        #region 变量
        private AddPatientDialog patient;
        #endregion

        #region 构造
        public PatMsgBtn()
        {
            InitializeComponent();
            SelectPatientEvent.Instance.Subscribe(GetPatient);
        }
        #endregion

        #region 获取患者信息
        public void GetPatient(Syspatient obj)
        {
            if (obj != null)
            {
                this.lblPatient.Text = obj.UserName;
                ModuleConstant.PatientId = obj.Id;
                ModuleConstant.PatientName = obj.UserName;

            }
            else
            {
                this.lblPatient.Text = null;
                ModuleConstant.PatientId = null;
                ModuleConstant.PatientName = null;

            }
        }
        #endregion

        #region 单击事件
        private void lblPatient_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

            patient = new AddPatientDialog();
            patient.Parent = this;
            AddPatient child = new AddPatient(false);
            child.Close += new EventHandler(child_Close);
            patient.Content = child;
            patient.Show();
        }

        public void child_Close(object sender, EventArgs e)
        {
            patient.Close();
        }
        #endregion
    }
}
