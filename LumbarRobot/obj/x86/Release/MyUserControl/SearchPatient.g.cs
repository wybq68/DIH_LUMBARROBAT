﻿#pragma checksum "..\..\..\..\MyUserControl\SearchPatient.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "BBB32648E0AADE8334AEAE4022D69DF5"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using LumbarRobot.MyUserControl;
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


namespace LumbarRobot.MyUserControl {
    
    
    /// <summary>
    /// SearchPatient
    /// </summary>
    public partial class SearchPatient : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector {
        
        
        #line 194 "..\..\..\..\MyUserControl\SearchPatient.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox txtPatientName;
        
        #line default
        #line hidden
        
        
        #line 204 "..\..\..\..\MyUserControl\SearchPatient.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGrid dg;
        
        #line default
        #line hidden
        
        
        #line 295 "..\..\..\..\MyUserControl\SearchPatient.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal LumbarRobot.MyUserControl.Pager pager;
        
        #line default
        #line hidden
        
        
        #line 314 "..\..\..\..\MyUserControl\SearchPatient.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnAddPatient;
        
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
            System.Uri resourceLocater = new System.Uri("/LumbarRobot;component/myusercontrol/searchpatient.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\MyUserControl\SearchPatient.xaml"
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
            this.txtPatientName = ((System.Windows.Controls.TextBox)(target));
            
            #line 194 "..\..\..\..\MyUserControl\SearchPatient.xaml"
            this.txtPatientName.PreviewMouseDown += new System.Windows.Input.MouseButtonEventHandler(this.btnWhereStr_PreviewMouseDown);
            
            #line default
            #line hidden
            
            #line 194 "..\..\..\..\MyUserControl\SearchPatient.xaml"
            this.txtPatientName.LostFocus += new System.Windows.RoutedEventHandler(this.txtQuery_LostFocus);
            
            #line default
            #line hidden
            
            #line 194 "..\..\..\..\MyUserControl\SearchPatient.xaml"
            this.txtPatientName.TextChanged += new System.Windows.Controls.TextChangedEventHandler(this.txtPatientName_TextChanged);
            
            #line default
            #line hidden
            return;
            case 2:
            this.dg = ((System.Windows.Controls.DataGrid)(target));
            
            #line 204 "..\..\..\..\MyUserControl\SearchPatient.xaml"
            this.dg.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.dg_SelectionChanged);
            
            #line default
            #line hidden
            return;
            case 3:
            this.pager = ((LumbarRobot.MyUserControl.Pager)(target));
            return;
            case 4:
            this.btnAddPatient = ((System.Windows.Controls.Button)(target));
            
            #line 314 "..\..\..\..\MyUserControl\SearchPatient.xaml"
            this.btnAddPatient.Click += new System.Windows.RoutedEventHandler(this.btnAddPatient_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}
