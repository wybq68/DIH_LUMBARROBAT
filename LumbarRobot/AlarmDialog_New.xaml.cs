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
using LumbarRobot.Interactions;
using LumbarRobot.IViewModel;
using LumbarRobot.Common;
using LumbarRobot.IView;

namespace LumbarRobot
{
    /// <summary>
    /// AlarmDialog.xaml 的交互逻辑
    /// </summary>
    public partial class AlarmDialog_New : AlarmInteractionDialog, IGenericInteractionView<AlarmInfo>, IGenericAdapter<AlarmInfo>
    {
        private readonly IGenericAdapter<AlarmInfo> adapter;

        public AlarmDialog_New()
        {
            this.adapter = new GenericAdapter<AlarmInfo>();
            this.DataContext = this.ViewModel;
            InitializeComponent();
        }

        public void SetEntity(AlarmInfo entity)
        {
            this.ViewModel.SetEntity(entity);
        }

        public AlarmInfo GetEntity()
        {
            return this.ViewModel.GetEntity();
        }

        public IGenericViewModel<AlarmInfo> ViewModel
        {
            get { return this.adapter.ViewModel; }
        }
    }
}

