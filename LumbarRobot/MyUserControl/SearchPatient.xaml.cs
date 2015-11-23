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
using LumbarRobot.Common;
using LumbarRobot.ViewModels;
using LumbarRobot.DAL;
using LumbarRobot.Data;
using System.Data;
using LumbarRobot.Event;

namespace LumbarRobot.MyUserControl
{
    /// <summary>
    /// SearchPatient.xaml 的交互逻辑
    /// </summary>
    public partial class SearchPatient : UserControl
    {

        #region 变量
        private AddPatientDialog patient;

        private int CurrentPageIndex = 1;

        private bool IsReturn = false;

        private int MaxPage = 0;
        #endregion

        #region 构造
        public SearchPatient()
        {
            this.DataContext = new SearchPatientViewModel();
            InitializeComponent();
            IsRefreshEvent.Instance.Subscribe(GetBool);
            BindData(CurrentPageIndex); 
            //if (dg.Items.Count>0)
            //{
            //    dg.SelectedIndex = 0;
            //}
        }
        #endregion

        #region 键盘
        private void txtQuery_LostFocus(object sender, RoutedEventArgs e)
        {
            IntPtr hwnd = WindowsAPI.FindWindow("IPTip_Main_Window", null);
            WindowsAPI.SendMessage(hwnd, WindowsAPI.WM_SYSCOMMAND, WindowsAPI.SC_CLOSE, 0);
        }

        private void btnWhereStr_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            Machine.OpenTabtip();
        }
        #endregion

        #region 增加病人
        private void btnAddPatient_Click(object sender, RoutedEventArgs e)
        {
            patient = new AddPatientDialog();
            patient.Parent = this;
            AddPatient child = new AddPatient(true);
            child.Close += new EventHandler(child_Close);
            patient.Content = child;
            patient.Show();
        }

        public void child_Close(object sender, EventArgs e)
        {
            patient.Close();
            BindData(1);
        }
        #endregion

        #region 绑定数据
        public void BindData(int CurrentPageIndex)
        {
            var list = from od in MySession.Query<Syspatient>()
                       select od;

            if (!string.IsNullOrEmpty(this.txtPatientName.Text))
            {
                list = (from od in list
                        where od.UserName.Contains(this.txtPatientName.Text.Trim())||od.PinYin.Contains(this.txtPatientName.Text.Trim())
                        select od).OrderByDescending(x => x.LastTime);
            }
            else
            {
                list = (from od in list
                        select od).OrderByDescending(x => x.LastTime);
            }
            if (list != null && list.Count() > 0)
            {
                if (list.Count() % 9 == 0)
                {
                    MaxPage = list.Count();
                }
                else
                {
                    MaxPage = list.Count() / 9 + 1;
                }
            }

            list = (from od in list
                    select od).Skip((CurrentPageIndex - 1) * 9).Take(9);
            this.dg.ItemsSource = list.ToList();

            foreach (Syspatient item in list)
            {
                if (item.Id == ModuleConstant.PatientId)
                {
                    dg.SelectedItem = item;
                }
            }

            //if (list.Count() > 0)
            //{
            //    SelectPatientEvent.Instance.Publish(list.ToList()[0]);
            //}
        }
        #endregion

        #region 首页
        private void pager_FristClick(object sender, RoutedEventArgs e)
        {
            CurrentPageIndex = 1;
            BindData(CurrentPageIndex);
        }
        #endregion

        #region 下一页
        private void pager_NextClick(object sender, RoutedEventArgs e)
        {
            if (MaxPage > 1 && CurrentPageIndex < MaxPage)
            {
                CurrentPageIndex++;
            }
            BindData(CurrentPageIndex);
        }
        #endregion

        #region 上一页
        private void pager_PrevClick(object sender, RoutedEventArgs e)
        {
            if (CurrentPageIndex > 1)
            {
                CurrentPageIndex--;
            }
            BindData(CurrentPageIndex);
        }
        #endregion

        #region 末页
        private void pager_EndClick(object sender, RoutedEventArgs e)
        {
            CurrentPageIndex = MaxPage;
            BindData(CurrentPageIndex);
        }
        #endregion

        #region 选中改变事件
        private void dg_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Syspatient patient = this.dg.SelectedItem as Syspatient;
            if (patient != null)
            {
                SelectPatientEvent.Instance.Publish(patient);
            }
        }
        #endregion

        #region 获取返回值
        /// <summary>
        /// 获取返回值
        /// </summary>
        /// <param name="obj"></param>
        public void GetBool(bool obj)
        {
            this.IsReturn = obj;
            if (IsReturn)
            {
                BindData(1);
                Syspatient patient = MySession.Session.Get<Syspatient>(ModuleConstant.PatientId);
                SelectPatientEvent.Instance.Publish(patient);
            }
        }
        #endregion

        #region 查询条件改变事件
        private void txtPatientName_TextChanged(object sender, TextChangedEventArgs e)
        {
            BindData(CurrentPageIndex);
        }
        #endregion
    }
}

