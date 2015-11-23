using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;
using Microsoft.Practices.ServiceLocation;
using Microsoft.Practices.Prism.Regions;
using System.Windows;
using Microsoft.Practices.Prism.Events;
using LumbarRobot.ViewModels;

namespace LumbarRobot.Commands
{
    public class PwdLostFoucus : ICommand
    {
        #region Fields

        // Member variables
        private LoginViewModel m_ViewModel;
        #endregion

        #region Constructor

        /// <summary>
        /// Default constructor.
        /// </summary>
        public PwdLostFoucus(LoginViewModel viewModel)
        {
            m_ViewModel = viewModel;
        }

        #endregion

        #region 实现ICommand 接口方法

        /// <summary>
        /// Whether the ShowModuleAViewCommand is enabled. ICommond 接口方法
        /// </summary>
        public bool CanExecute(object parameter)
        {
            return true;
        }

        /// <summary>
        /// Actions to take when CanExecute() changes.
        /// </summary>
        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }


        /// <summary>
        /// Executes the ShowModuleAViewCommand
        /// </summary>
        public void Execute(object parameter)
        {
            if (m_ViewModel.SysPassWord != "")
            {
                m_ViewModel.TagMsg = null;
            }
            else
            {
                m_ViewModel.TagMsg = "密码";
            }
        }
        #endregion
    }
}
