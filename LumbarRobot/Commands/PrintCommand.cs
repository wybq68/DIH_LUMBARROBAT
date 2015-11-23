using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;
using LumbarRobot.ViewModels;
using System.Windows.Controls;
using LumbarRobot.Common;
using System.Windows;

namespace LumbarRobot.Commands
{
    public class PrintCommand : ICommand
    {
        #region Fields

        // Member variables
       // private ReportPrintViewModel m_ViewModel;

        private PrintTest m_ViewModel;
        #endregion

        #region Constructor

        /// <summary>
        /// Default constructor.
        /// </summary>
        //public PrintCommand(ReportPrintViewModel viewModel)
        //{
        //    m_ViewModel = viewModel;
        //}
        public PrintCommand(PrintTest viewModel)
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
            FontsClearup.FixRegistryFonts();
            PrintDialog printdialog1 = new PrintDialog();
            //if (PrinterOffline.GetPrinterStatusInt(Parameter.printname) != 0)
            //{
            //    string str = PrinterOffline.GetPrinterStatus(Parameter.printname);
            //    MessageBox.Show("提示", "打印机处于" + str + "状态!");
            //}
            //else
            //{
                printdialog1.PrintDocument(m_ViewModel.mydocument.DocumentPaginator, DateTime.Now.ToString("yyyyMMdd")+"报告");
            //}
        }
        #endregion
    }
}
