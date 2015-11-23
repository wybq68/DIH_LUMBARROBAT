﻿#pragma checksum "..\..\..\ActionControl.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "175CF90672C8995CCC23E61D00E88920"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using LumbarRobot;
using LumbarRobot.ActionUserControl;
using LumbarRobot.Common;
using LumbarRobot.Interactions;
using LumbarRobot.MyUserControl;
using Microsoft.Practices.Prism.Interactivity.InteractionRequest;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Interactivity;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Shell;


namespace LumbarRobot {
    
    
    /// <summary>
    /// ActionControl
    /// </summary>
    public partial class ActionControl : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector {
        
        
        #line 683 "..\..\..\ActionControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid gridPanel;
        
        #line default
        #line hidden
        
        
        #line 702 "..\..\..\ActionControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock tbAction;
        
        #line default
        #line hidden
        
        
        #line 704 "..\..\..\ActionControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox CboAction;
        
        #line default
        #line hidden
        
        
        #line 721 "..\..\..\ActionControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock tbTitle;
        
        #line default
        #line hidden
        
        
        #line 723 "..\..\..\ActionControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox CboMode;
        
        #line default
        #line hidden
        
        
        #line 737 "..\..\..\ActionControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal LumbarRobot.MyUserControl.SetControl setControlForce;
        
        #line default
        #line hidden
        
        
        #line 740 "..\..\..\ActionControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal LumbarRobot.MyUserControl.SetControl setControlMinAngle;
        
        #line default
        #line hidden
        
        
        #line 743 "..\..\..\ActionControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal LumbarRobot.MyUserControl.SetControl setControlMaxAngle;
        
        #line default
        #line hidden
        
        
        #line 746 "..\..\..\ActionControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal LumbarRobot.MyUserControl.SetControl setControlSpeed;
        
        #line default
        #line hidden
        
        
        #line 749 "..\..\..\ActionControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal LumbarRobot.MyUserControl.SetControl setControlGroup;
        
        #line default
        #line hidden
        
        
        #line 752 "..\..\..\ActionControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal LumbarRobot.MyUserControl.SetControl setControlTimes;
        
        #line default
        #line hidden
        
        
        #line 756 "..\..\..\ActionControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal LumbarRobot.MyUserControl.SetControl setControlPosition;
        
        #line default
        #line hidden
        
        
        #line 760 "..\..\..\ActionControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListBox MyActionListBox;
        
        #line default
        #line hidden
        
        
        #line 860 "..\..\..\ActionControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal LumbarRobot.MyUserControl.ControlPanel playBtn;
        
        #line default
        #line hidden
        
        
        #line 874 "..\..\..\ActionControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnClearBreakdown;
        
        #line default
        #line hidden
        
        
        #line 875 "..\..\..\ActionControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnClearGeocoder;
        
        #line default
        #line hidden
        
        
        #line 876 "..\..\..\ActionControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnActionFree;
        
        #line default
        #line hidden
        
        
        #line 877 "..\..\..\ActionControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnPBFit;
        
        #line default
        #line hidden
        
        
        #line 878 "..\..\..\ActionControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnRotationFit;
        
        #line default
        #line hidden
        
        
        #line 879 "..\..\..\ActionControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnCancel;
        
        #line default
        #line hidden
        
        
        #line 889 "..\..\..\ActionControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid gridParam;
        
        #line default
        #line hidden
        
        
        #line 914 "..\..\..\ActionControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock txtAction;
        
        #line default
        #line hidden
        
        
        #line 915 "..\..\..\ActionControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock txtNextAction;
        
        #line default
        #line hidden
        
        
        #line 916 "..\..\..\ActionControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock txtTimes;
        
        #line default
        #line hidden
        
        
        #line 917 "..\..\..\ActionControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label UPLable;
        
        #line default
        #line hidden
        
        
        #line 926 "..\..\..\ActionControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal LumbarRobot.ActionUserControl.Eilelc2 eilelc21;
        
        #line default
        #line hidden
        
        
        #line 929 "..\..\..\ActionControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal LumbarRobot.ActionUserControl.UserLine userLine1;
        
        #line default
        #line hidden
        
        
        #line 935 "..\..\..\ActionControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid SetVidicon;
        
        #line default
        #line hidden
        
        
        #line 940 "..\..\..\ActionControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock txtMsg;
        
        #line default
        #line hidden
        
        
        #line 941 "..\..\..\ActionControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnNext;
        
        #line default
        #line hidden
        
        
        #line 942 "..\..\..\ActionControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnAffirm;
        
        #line default
        #line hidden
        
        
        #line 943 "..\..\..\ActionControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnCancel2;
        
        #line default
        #line hidden
        
        
        #line 944 "..\..\..\ActionControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label Downlabel;
        
        #line default
        #line hidden
        
        
        #line 946 "..\..\..\ActionControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal LumbarRobot.ActionUserControl.TimeSina sinWpf1;
        
        #line default
        #line hidden
        
        
        #line 963 "..\..\..\ActionControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock txtPrompt;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/LumbarRobot;component/actioncontrol.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\ActionControl.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal System.Delegate _CreateDelegate(System.Type delegateType, string handler) {
            return System.Delegate.CreateDelegate(delegateType, this, handler);
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            
            #line 14 "..\..\..\ActionControl.xaml"
            ((LumbarRobot.ActionControl)(target)).Unloaded += new System.Windows.RoutedEventHandler(this.UserControl_Unloaded);
            
            #line default
            #line hidden
            
            #line 14 "..\..\..\ActionControl.xaml"
            ((LumbarRobot.ActionControl)(target)).Loaded += new System.Windows.RoutedEventHandler(this.UserControl_Loaded);
            
            #line default
            #line hidden
            return;
            case 2:
            this.gridPanel = ((System.Windows.Controls.Grid)(target));
            return;
            case 3:
            this.tbAction = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 4:
            this.CboAction = ((System.Windows.Controls.ComboBox)(target));
            
            #line 704 "..\..\..\ActionControl.xaml"
            this.CboAction.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.CboAction_SelectionChanged);
            
            #line default
            #line hidden
            return;
            case 5:
            this.tbTitle = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 6:
            this.CboMode = ((System.Windows.Controls.ComboBox)(target));
            return;
            case 7:
            this.setControlForce = ((LumbarRobot.MyUserControl.SetControl)(target));
            return;
            case 8:
            this.setControlMinAngle = ((LumbarRobot.MyUserControl.SetControl)(target));
            return;
            case 9:
            this.setControlMaxAngle = ((LumbarRobot.MyUserControl.SetControl)(target));
            return;
            case 10:
            this.setControlSpeed = ((LumbarRobot.MyUserControl.SetControl)(target));
            return;
            case 11:
            this.setControlGroup = ((LumbarRobot.MyUserControl.SetControl)(target));
            return;
            case 12:
            this.setControlTimes = ((LumbarRobot.MyUserControl.SetControl)(target));
            return;
            case 13:
            this.setControlPosition = ((LumbarRobot.MyUserControl.SetControl)(target));
            return;
            case 14:
            this.MyActionListBox = ((System.Windows.Controls.ListBox)(target));
            
            #line 760 "..\..\..\ActionControl.xaml"
            this.MyActionListBox.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.MyActionListBox_SelectionChanged);
            
            #line default
            #line hidden
            return;
            case 15:
            this.playBtn = ((LumbarRobot.MyUserControl.ControlPanel)(target));
            return;
            case 16:
            this.btnClearBreakdown = ((System.Windows.Controls.Button)(target));
            
            #line 874 "..\..\..\ActionControl.xaml"
            this.btnClearBreakdown.Click += new System.Windows.RoutedEventHandler(this.btnClearBreakdown_Click);
            
            #line default
            #line hidden
            return;
            case 17:
            this.btnClearGeocoder = ((System.Windows.Controls.Button)(target));
            
            #line 875 "..\..\..\ActionControl.xaml"
            this.btnClearGeocoder.Click += new System.Windows.RoutedEventHandler(this.btnClearGeocoder_Click);
            
            #line default
            #line hidden
            return;
            case 18:
            this.btnActionFree = ((System.Windows.Controls.Button)(target));
            
            #line 876 "..\..\..\ActionControl.xaml"
            this.btnActionFree.Click += new System.Windows.RoutedEventHandler(this.btnActionFree_Click);
            
            #line default
            #line hidden
            return;
            case 19:
            this.btnPBFit = ((System.Windows.Controls.Button)(target));
            
            #line 877 "..\..\..\ActionControl.xaml"
            this.btnPBFit.Click += new System.Windows.RoutedEventHandler(this.btnFit_Click);
            
            #line default
            #line hidden
            return;
            case 20:
            this.btnRotationFit = ((System.Windows.Controls.Button)(target));
            
            #line 878 "..\..\..\ActionControl.xaml"
            this.btnRotationFit.Click += new System.Windows.RoutedEventHandler(this.btnRotationFit_Click);
            
            #line default
            #line hidden
            return;
            case 21:
            this.btnCancel = ((System.Windows.Controls.Button)(target));
            
            #line 879 "..\..\..\ActionControl.xaml"
            this.btnCancel.Click += new System.Windows.RoutedEventHandler(this.btnCancel_Click);
            
            #line default
            #line hidden
            return;
            case 22:
            this.gridParam = ((System.Windows.Controls.Grid)(target));
            return;
            case 23:
            this.txtAction = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 24:
            this.txtNextAction = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 25:
            this.txtTimes = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 26:
            this.UPLable = ((System.Windows.Controls.Label)(target));
            return;
            case 27:
            this.eilelc21 = ((LumbarRobot.ActionUserControl.Eilelc2)(target));
            return;
            case 28:
            this.userLine1 = ((LumbarRobot.ActionUserControl.UserLine)(target));
            return;
            case 29:
            this.SetVidicon = ((System.Windows.Controls.Grid)(target));
            return;
            case 30:
            this.txtMsg = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 31:
            this.btnNext = ((System.Windows.Controls.Button)(target));
            
            #line 941 "..\..\..\ActionControl.xaml"
            this.btnNext.Click += new System.Windows.RoutedEventHandler(this.btnNext_Click);
            
            #line default
            #line hidden
            return;
            case 32:
            this.btnAffirm = ((System.Windows.Controls.Button)(target));
            
            #line 942 "..\..\..\ActionControl.xaml"
            this.btnAffirm.Click += new System.Windows.RoutedEventHandler(this.btnAffirm_Click);
            
            #line default
            #line hidden
            return;
            case 33:
            this.btnCancel2 = ((System.Windows.Controls.Button)(target));
            
            #line 943 "..\..\..\ActionControl.xaml"
            this.btnCancel2.Click += new System.Windows.RoutedEventHandler(this.btnCancel2_Click);
            
            #line default
            #line hidden
            return;
            case 34:
            this.Downlabel = ((System.Windows.Controls.Label)(target));
            return;
            case 35:
            this.sinWpf1 = ((LumbarRobot.ActionUserControl.TimeSina)(target));
            return;
            case 36:
            this.txtPrompt = ((System.Windows.Controls.TextBlock)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}
