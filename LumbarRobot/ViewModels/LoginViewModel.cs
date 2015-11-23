using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LumbarRobot.Common;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.ServiceLocation;
using Microsoft.Practices.Prism.Events;
using System.Windows.Input;
using LumbarRobot.Commands;

namespace LumbarRobot.ViewModels
{
    public class LoginViewModel : ViewModelBase, INavigationAware
    {

        #region 属性

        #region 密码
        private string _sysPassWord = null;

        public string SysPassWord
        {
            get { return _sysPassWord; }
            set
            {
                _sysPassWord = value;
                this.RaisePropertyChangedEvent("SysPassWord");
            }
        }
        #endregion

        #region  Tag
        private string tagMsg = "密码";

        public string TagMsg
        {
            get { return tagMsg; }
            set
            {
                tagMsg = value;
                this.RaisePropertyChangedEvent("TagMsg");
            }
        }
        #endregion
        #endregion


        #region ICommand接口
    
        public ICommand PwdLostFoucusTag { get; private set; }

        #endregion

        #region 构造
        public LoginViewModel()
        {
            PwdLostFoucusTag = new PwdLostFoucus(this);
        }
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

        }
        /// <summary>
        /// 当前的页面被导航到以后发生，这个函数可以用来处理URI的参数
        /// </summary>
        /// <param name="navigationContext"></param>
        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            if (TagMsg == null || TagMsg == "")
            {
                TagMsg = "密码";
            }
        }
        #endregion
    }
}
