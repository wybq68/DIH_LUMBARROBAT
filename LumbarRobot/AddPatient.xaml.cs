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
using LumbarRobot.Common;
using LumbarRobot.Data;
using LumbarRobot.DAL;
using LumbarRobot.Event;
using LumbarRobot.MyUserControl;

namespace LumbarRobot
{
    /// <summary>
    /// AddPatient.xaml 的交互逻辑
    /// </summary>
    public partial class AddPatient : UserControl
    {

        public event EventHandler Close;
        private bool IsAdd = false;

        #region 构造
        public AddPatient(bool flag)
        {
            InitializeComponent();
            IsAdd = flag;
            this.birthday.Text = DateTime.Now.ToString("yyyy-MM-dd");
            if (!flag)
            {
                BindData();
            }
        }
        #endregion

        #region 绑定数据
        public void BindData()
        {
            if (ModuleConstant.PatientId != null)
            {
                Syspatient patient = MySession.Session.Get<Syspatient>(ModuleConstant.PatientId);
                this.txtName.Text = patient.UserName;
                this.birthday.Text = patient.BirthDay.ToString("yyyy-MM-dd");
                this.cboArm.SelectedIndex = Convert.ToInt32(patient.AfftectedHand) - 1;
                this.cbodiagnoseTypeId.SelectedIndex = Convert.ToInt32(patient.DiagnoseTypeId) - 1;
                if(patient.BodyHeight.HasValue)
                {
                    this.txtHeight.Text = patient.BodyHeight.ToString();
                }
                if(patient.Weight.HasValue)
                {
                    this.txtWeight.Text = patient.Weight.ToString();
                }
                this.txtPatientCarNo.Text = patient.PatientCarNo;
                this.txtNode.Text = patient.Note;
                this.cboSex.SelectedIndex =Convert.ToInt32(patient.Sex);
            }
        }
        #endregion

        #region 保存

        private void btnSaveDoc_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(this.txtName.Text.Trim()))
            {
                AlarmDialog dialog = new AlarmDialog();
                dialog.lblTitle.Text = "提示信息";
                dialog.lblMsg.Text = "用户名不能为空！";
                dialog.ShowDialog();
                //if (MessageBox.Show("用户名不能为空！", "提示信息", MessageBoxButton.OK, MessageBoxImage.Error) == MessageBoxResult.OK)
                //{
                //}
            }
            else if (!string.IsNullOrEmpty(this.txtWeight.Text) && !Machine.ISNumber(this.txtWeight.Text))
            {
                AlarmDialog dialog = new AlarmDialog();
                dialog.lblTitle.Text = "提示信息";
                dialog.lblMsg.Text = "输入有效体重！";
                dialog.ShowDialog();
                this.txtWeight.Text = "";
                this.txtWeight.Focus();
                
            }
            else if (!string.IsNullOrEmpty(this.txtHeight.Text) && !Machine.ISNumber(this.txtHeight.Text))
            {
                AlarmDialog dialog = new AlarmDialog();
                dialog.lblTitle.Text = "提示信息";
                dialog.lblMsg.Text = "输入有效身高！";
                dialog.ShowDialog();
                this.txtHeight.Text = "";
                this.txtHeight.Focus();
                
            }
            else
            {
                if (IsAdd)
                {
                    Sysuserinfo user = new Sysuserinfo();
                    string userid = Guid.NewGuid().ToString("N");
                    user.Id = userid;//用户ID
                    user.UserType = "2";//用户类型 病人
                    user.UserName = txtName.Text.Trim(); //病人名字
                    user.SysPassWord = "123";  //密码
                    user.UserCode = "";
                    user.LastTime = DateTime.Now; //最后时间

                    MySession.Session.Save(user);

                    Syspatient info = new Syspatient();
                    info.DoctorID = ModuleConstant.UserID;
                    info.UserId = userid;
                    info.PinYin = PinYinConverter.GetFirst(info.UserName);
                    info.DiagnoseTypeId = (Convert.ToInt32(this.cbodiagnoseTypeId.SelectedIndex) + 1).ToString();
                    //info.AfftectedHand = (Convert.ToInt32(this.cboArm.SelectedIndex) + 1).ToString();
                    info.Sex = (Convert.ToInt32(this.cboSex.SelectedIndex) + 1).ToString();
                    info.Id = System.Guid.NewGuid().ToString("N");
                    info.LastTime = DateTime.Now;
                    if (!string.IsNullOrEmpty(this.txtWeight.Text) && Machine.ISNumber(this.txtWeight.Text))
                    {
                        info.Weight = Convert.ToInt32(this.txtWeight.Text);

                    }


                    if (!string.IsNullOrEmpty(this.txtHeight.Text) && Machine.ISNumber(this.txtHeight.Text))
                    {
                        info.BodyHeight = Convert.ToInt32(this.txtHeight.Text);

                    }

                    info.Note = this.txtNode.Text;
                    info.PatientCarNo = this.txtPatientCarNo.Text;
                    info.OpDate = DateTime.Now;
                    info.BirthDay = Convert.ToDateTime(this.birthday.Text);
                    info.UserName = txtName.Text.Trim();
                    MySession.Session.Save(info);
                    MySession.Session.Flush();
                }
                else
                {
                    Syspatient info = MySession.Session.Get<Syspatient>(ModuleConstant.PatientId);
                    Sysuserinfo user = MySession.Session.Get<Sysuserinfo>(info.UserId);

                    user.UserName = txtName.Text.Trim(); //病人名字
                    user.LastTime = DateTime.Now; //最后时间
                    MySession.Session.SaveOrUpdate(user);
                    info.DiagnoseTypeId = (Convert.ToInt32(this.cbodiagnoseTypeId.SelectedIndex) + 1).ToString();
                  //  info.AfftectedHand = (Convert.ToInt32(this.cboArm.SelectedIndex) + 1).ToString();
                    info.Sex = (Convert.ToInt32(this.cboSex.SelectedIndex) + 1).ToString();
                    info.LastTime = DateTime.Now;
                    info.PinYin = PinYinConverter.GetFirst(txtName.Text.Trim());
                    if (!string.IsNullOrEmpty(this.txtWeight.Text) && Machine.ISNumber(this.txtWeight.Text))
                    {
                        info.Weight = Convert.ToInt32(this.txtWeight.Text);
                    }
                    if (!string.IsNullOrEmpty(this.txtHeight.Text) && Machine.ISNumber(this.txtHeight.Text))
                    {
                        info.BodyHeight = Convert.ToInt32(this.txtHeight.Text);
                    }
                    info.Note = this.txtNode.Text;
                    info.PatientCarNo = this.txtPatientCarNo.Text;
                    info.BirthDay = Convert.ToDateTime(this.birthday.Text);
                    info.UserName = txtName.Text.Trim();
                    MySession.Session.SaveOrUpdate(info);
                    MySession.Session.Flush();
                }
                Machine.ClosedTabtip();
                this.Close(sender, e);
                IsRefreshEvent.Instance.Publish(true);

            }
        }
        #endregion

        #region 取消
        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            Machine.ClosedTabtip();
            this.Close(sender, e);
        }
        #endregion

        #region 键盘

        private void txtName_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            Machine.OpenTabtip();
        }
        #endregion
    }
}
