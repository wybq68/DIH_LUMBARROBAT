﻿#pragma checksum "..\..\..\ShowReportDetailView.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "260341E5205BF6B9A7936CA9ABF6004F"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

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
    /// ShowReportDetailView
    /// </summary>
    public partial class ShowReportDetailView : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector {
        
        
        #line 666 "..\..\..\ShowReportDetailView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock xxxxxxx;
        
        #line default
        #line hidden
        
        
        #line 671 "..\..\..\ShowReportDetailView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnExportExcel;
        
        #line default
        #line hidden
        
        
        #line 677 "..\..\..\ShowReportDetailView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnPrint;
        
        #line default
        #line hidden
        
        
        #line 691 "..\..\..\ShowReportDetailView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnClosed;
        
        #line default
        #line hidden
        
        
        #line 709 "..\..\..\ShowReportDetailView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGrid gvDetail;
        
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
            System.Uri resourceLocater = new System.Uri("/LumbarRobot;component/showreportdetailview.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\ShowReportDetailView.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
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
            
            #line 8 "..\..\..\ShowReportDetailView.xaml"
            ((LumbarRobot.ShowReportDetailView)(target)).Loaded += new System.Windows.RoutedEventHandler(this.UserControl_Loaded);
            
            #line default
            #line hidden
            return;
            case 2:
            this.xxxxxxx = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 3:
            this.btnExportExcel = ((System.Windows.Controls.Button)(target));
            
            #line 671 "..\..\..\ShowReportDetailView.xaml"
            this.btnExportExcel.Click += new System.Windows.RoutedEventHandler(this.btnExportExcel_Click);
            
            #line default
            #line hidden
            return;
            case 4:
            this.btnPrint = ((System.Windows.Controls.Button)(target));
            return;
            case 5:
            this.btnClosed = ((System.Windows.Controls.Button)(target));
            
            #line 691 "..\..\..\ShowReportDetailView.xaml"
            this.btnClosed.Click += new System.Windows.RoutedEventHandler(this.btnClosed_Click);
            
            #line default
            #line hidden
            return;
            case 6:
            this.gvDetail = ((System.Windows.Controls.DataGrid)(target));
            
            #line 709 "..\..\..\ShowReportDetailView.xaml"
            this.gvDetail.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.gvDetail_SelectionChanged);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

