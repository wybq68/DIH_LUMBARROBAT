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
using LumbarRobot.ViewModels;
using Remotion.Linq.Collections;
using LumbarRobot.MyUserControl;
using LumbarRobot.DAL;
using System.Threading;

namespace LumbarRobot
{
    /// <summary>
    /// SavePrescription.xaml 的交互逻辑
    /// </summary>
    public partial class SavePrescriptionView : UserControl
    {
        #region 事件
        /// <summary>
        /// 关闭事件
        /// </summary>
        public event EventHandler Close;
        #endregion

        #region 变量
        /// <summary>
        /// 修改疗程
        /// </summary>
        private EditPrescriptionDialog EditPrescriptionDialog;
        /// <summary>
        /// 保存疗程
        /// </summary>
        private SaveCourseDialog SaveCourseDialog;
        /// <summary>
        /// 结果集合
        /// </summary>
        System.Collections.ObjectModel.ObservableCollection<ItemDemo> listResult = null;
        
        private object objLock = new object();
        /// <summary>
        /// 处方对象
        /// </summary>
        public ObservableCollection<PrescriptionItem> PrescriptionList = new ObservableCollection<PrescriptionItem>();
        #endregion

        #region 构造
        public SavePrescriptionView(ObservableCollection<PrescriptionItem> list)
        {
            InitializeComponent();
            if (list.Count > 0)
            {
                PrescriptionList = list;
                gvDetail.ItemsSource = list;
            }
            listResult = new System.Collections.ObjectModel.ObservableCollection<ItemDemo>();
        }
        #endregion

        #region 关闭事件
        private void btnClosed_Click(object sender, RoutedEventArgs e)
        {
            if (Close != null)
                Close(sender, e);
        }
        #endregion

        #region 绑定数据
        public void BindData()
        {
            gvDetail.ItemsSource = null;
            if (PrescriptionList.Count > 0)
            {
                gvDetail.ItemsSource = PrescriptionList;
            }
        }
        #endregion

        #region 修改
        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
            EditPrescriptionDialog = new EditPrescriptionDialog();
            EditPrescriptionDialog.Parent = this;
            PrescriptionItem MyItem = new PrescriptionItem();
            Button btn = sender as Button;
            foreach (var item in PrescriptionList)
            {
                if (item.Id == btn.Tag.ToString())
                {
                    MyItem = item;
                    break;
                }
            }
            EditPrescriptionView child = new EditPrescriptionView(MyItem);
            child.Close += new EventHandler(child_Close);
            for (int i = 0; i < PrescriptionList.Count; i++)
            {
                PrescriptionItem item = PrescriptionList[i];
                if (item.Id == btn.Tag.ToString())
                {
                    PrescriptionList[i] = MyItem;
                    break;
                }
            }
           
            EditPrescriptionDialog.Content = child;
            EditPrescriptionDialog.Show();
           

        }
        #endregion

        #region 删除
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            WPFMessageBox msgBox = new WPFMessageBox();
            msgBox.lblMsg.Text = "是否删除！";
            msgBox.lblTitle.Text = "提示信息";
            msgBox.ShowDialog();
            if (msgBox.IsFlag)
            {
                Button btn = sender as Button;

                foreach (var item in PrescriptionList)
                {
                    if (item.Id == btn.Tag.ToString())
                    {
                        PrescriptionList.Remove(item);
                        break;
                    }
                }
                BindData();
            }
        }
        #endregion

        #region 保存处方
        private void btnSavePrescription_Click(object sender, RoutedEventArgs e)
        {
            SetPrescriptionItems();
            ThreadPool.QueueUserWorkItem(new WaitCallback((obj) =>
            {
                lock (objLock)
                {
                    Application.Current.Dispatcher.Invoke(new Action(() =>
                    {
                        if (listResult.Count > 0)
                        {
                            if (SaveCourseDialog == null)
                            {
                                SaveCourseDialog = new SaveCourseDialog();
                                SaveCourseDialog.Parent = this;
                                SaveCourseView child = new SaveCourseView(listResult);
                                child.Close += new EventHandler(child_Close);
                                SaveCourseDialog.Content = child;
                                SaveCourseDialog.Show();
                            }
                        }

                    }));
                }
            }));
        }

        #region 获得保存的疗程对象
        public void SetPrescriptionItems()
        {
            if (listResult != null)
            {
                listResult.Clear();
            }
            foreach (var myItem in PrescriptionList)
            {
                ItemDemo item = new ItemDemo();
                item.Id = myItem.Id;
                item.MaxAngle = myItem.MaxAngle;
                item.ActionName = myItem.ActionName;
                item.ModeName = myItem.ModeName;
                item.MinAngle = myItem.MinAngle;
                item.Mode = myItem.ModeId;
                item.ActionId = myItem.ActionId;
                item.Speed = myItem.Speed;
                item.Times = myItem.Times;
                item.Force = myItem.PForce;
                item.Position = myItem.LastLocation;
                if (myItem.PGroup.HasValue)
                {
                    item.GroupTimes = myItem.PGroup.Value;
                }
                item.PicturePath = "/images/prescribe.png";
                listResult.Add(item);
            }
        }
        #endregion

        #endregion

        #region 关闭事件

        public void child_Close(object sender, EventArgs e)
        {
            if (SaveCourseDialog != null)
            {
                SaveCourseDialog.Close();
            }
            SaveCourseDialog = null;

            if (EditPrescriptionDialog != null)
            {
                EditPrescriptionDialog.Close();
                BindData();
            }
            EditPrescriptionDialog = null;
        }
        #endregion
    }
}
