using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LumbarRobot.Common;
using Microsoft.Practices.Prism.Regions;
using LumbarRobot.Data;
using System.Collections.ObjectModel;
using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.ServiceLocation;
using LumbarRobot.DAL;
using LumbarRobot.Event;
using System.Windows.Input;
using LumbarRobot.Commands;

namespace LumbarRobot.ViewModels
{
    public class SearchPatientViewModel : ViewModelBase, INavigationAware
    {
        #region Fields

        private string sortField = "LastTime";
        /// <summary>
        /// 排序字段
        /// </summary>
        public string SortField
        {
            get { return sortField; }
            set { sortField = value; }
        }

        private string ascOrDesc = "desc";
        /// <summary>
        /// 排序
        /// </summary>
        public string AscOrDesc
        {
            get { return ascOrDesc; }
            set { ascOrDesc = value; }
        }

        private string whereStr = "";
        /// <summary>
        /// 查询条件
        /// </summary>
        public string WhereStr
        {
            get { return whereStr; }
            set
            {
                whereStr = value;
                this.RaisePropertyChangedEvent("WhereStr");
            }
        }


        private Syspatient patientInfo;
        /// <summary>
        /// 病人信息
        /// </summary>
        public Syspatient PatientInfo
        {
            get { return patientInfo; }
            set { patientInfo = value; }
        }

        // Property variables
        private ObservableCollection<Syspatient> p_Patient;
        private int p_ItemCount;

        public ObservableCollection<Syspatient> SysPatientInfoList
        {
            get { return p_Patient; }
            set
            {
                p_Patient = value;
                this.RaisePropertyChangedEvent("SysPatientInfoList");
            }
        }
        /// <summary>
        /// The currently-selected grocery item.
        /// </summary>
        private Syspatient selectedItem;

        public Syspatient SelectedItem
        {
            get { return selectedItem; }
            set
            {
                selectedItem = value;
                this.RaisePropertyChangedEvent("SelectedItem");
                if (SelectedItem != null)
                {
                    ModuleConstant.PatientId = SelectedItem.Id;
                    ModuleConstant.PatientName=SelectedItem.UserName;
                    ModuleConstant.Syspatient = SelectedItem;
                }
            }
        }

        public int ItemCount
        {
            get { return p_ItemCount; }

            set
            {
                p_ItemCount = value;
                base.RaisePropertyChangedEvent("ItemCount");
            }
        }
        /// <summary>
        /// 输出值
        /// </summary>
        private string outPut;
        /// <summary>
        /// 输出值
        /// </summary>
        public string Output
        {
            get
            {
                return outPut;
            }
            set
            {
                outPut = value;
                base.RaisePropertyChangedEvent("Output");
            }
        }

        private int pageSize = 9;
        /// <summary>
        /// 分页大小
        /// </summary>
        public int PageSize
        {
            get { return pageSize; }
            set
            {
                pageSize = value;
                this.RaisePropertyChangedEvent("PageSize");
            }
        }

        private int totalCount;
        /// <summary>
        /// 记录总数
        /// </summary>
        public int TotalCount
        {
            get { return totalCount; }
            set
            {
                totalCount = value;
                this.RaisePropertyChangedEvent("TotalCount");
            }
        }

        private int pageCurrent = 1;
        /// <summary>
        /// 当前页数
        /// </summary>
        public int PageCurrent
        {
            get { return pageCurrent; }
            set
            {
                pageCurrent = value;
                this.RaisePropertyChangedEvent("PageCurrent");
            }
        }

        #region 是否刷新病人列表
        private bool isRefresh = false;

        public bool IsRefresh
        {
            get { return isRefresh; }
            set 
            {
                isRefresh = value;
                if (isRefresh)
                    BindSyspatient();
            }
        }
        #endregion

        #endregion

        #region ICommand接口
        /// <summary>
        ///定义的登录页面“登录按钮”事件
        /// </summary>
        
        public ICommand WhereStrBtnCommand { get; private set; }
        #endregion

        #region INavigationAware Members

        /// <summary>
        /// 当前的视图模型是否可以处理请求的导航行为，通常用来指定当前的视图/模型是否可以重用
        /// </summary>
        /// <param name="navigationContext"></param>
        /// <returns></returns>
        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return true;
        }
        /// <summary>
        /// 当前的页面导航到其他页面的时候发生。
        /// </summary>
        /// <param name="navigationContext"></param>
        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
            throw new System.NotImplementedException();
        }
        /// <summary>
        /// 当前的页面被导航到以后发生，这个函数可以用来处理URI的参数
        /// </summary>
        /// <param name="navigationContext"></param>
        public void OnNavigatedTo(NavigationContext navigationContext)
        {
        }

        #endregion

        #region 构造
        public SearchPatientViewModel()
        {
           
            IsRefreshEvent.Instance.Subscribe(GetBool);
            BindSyspatient();
            WhereStrBtnCommand = new WhereStrCommand(this);
        }

        private void BindSyspatient()
        {
            SysPatientInfoList = new ObservableCollection<Syspatient>();
            var list = from od in MySession.Query<Syspatient>()
                       select od;
            list = (from od in list
                    select od).Skip((1 - 1) * 9).Take(9);
            foreach (Syspatient obj in list.ToList())
            {
                this.SysPatientInfoList.Add(obj);
            }
        }
        #endregion

        #region 获取是否返回值
        public void GetBool(bool obj)
        {
            this.IsRefresh = obj;
        }
        #endregion

        #region 刷新病人信息列表
        /// <summary>
        ///刷新病人信息列表
        /// </summary>
        /// <param name="a"></param>
        private void GetSysUserInfo()
        {

        }
        #endregion

        #region 导航完成后，检测Button 状态
        /// <summary>
        /// 导航完成后，检测Button 状态
        /// Sets the IsChecked state of the Task Button when navigation is completed.
        /// </summary>
        /// <param name="publisher">The publisher of the event.</param>
        private void OnNavigationCompleted(string publisher)
        {

        }
        #endregion
    }
}
