using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;
using LumbarRobot.ViewModels;

namespace LumbarRobot.Commands
{
    public class WhereStrCommand : ICommand
    {
        #region Fields
        // Member variables
        private SearchPatientViewModel m_ViewModel;
        #endregion

        public WhereStrCommand(SearchPatientViewModel viewModel)
        {
            m_ViewModel = viewModel;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public void Execute(object parameter)
        {
            //刷新列表
            m_ViewModel.PageCurrent = 1;

            if (String.IsNullOrEmpty(parameter.ToString()))
            {
                m_ViewModel.WhereStr = "";
            }
            else
            {
                m_ViewModel.WhereStr = parameter.ToString();
            }
           
        }
    }
}
