﻿#pragma checksum "..\..\..\..\MyUserControl\Pager.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "69AF67C1D36AF2CCC3C3F468844AE80A"
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


namespace LumbarRobot.MyUserControl {
    
    
    /// <summary>
    /// Pager
    /// </summary>
    public partial class Pager : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector {
        
        
        #line 166 "..\..\..\..\MyUserControl\Pager.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnFirst;
        
        #line default
        #line hidden
        
        
        #line 169 "..\..\..\..\MyUserControl\Pager.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnPrev;
        
        #line default
        #line hidden
        
        
        #line 173 "..\..\..\..\MyUserControl\Pager.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnNext;
        
        #line default
        #line hidden
        
        
        #line 177 "..\..\..\..\MyUserControl\Pager.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnLast;
        
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
            System.Uri resourceLocater = new System.Uri("/LumbarRobot;component/myusercontrol/pager.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\MyUserControl\Pager.xaml"
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
            this.btnFirst = ((System.Windows.Controls.Button)(target));
            
            #line 166 "..\..\..\..\MyUserControl\Pager.xaml"
            this.btnFirst.Click += new System.Windows.RoutedEventHandler(this.btnFirst_Click);
            
            #line default
            #line hidden
            return;
            case 2:
            this.btnPrev = ((System.Windows.Controls.Button)(target));
            
            #line 169 "..\..\..\..\MyUserControl\Pager.xaml"
            this.btnPrev.Click += new System.Windows.RoutedEventHandler(this.btnPrev_Click);
            
            #line default
            #line hidden
            return;
            case 3:
            this.btnNext = ((System.Windows.Controls.Button)(target));
            
            #line 173 "..\..\..\..\MyUserControl\Pager.xaml"
            this.btnNext.Click += new System.Windows.RoutedEventHandler(this.btnNext_Click);
            
            #line default
            #line hidden
            return;
            case 4:
            this.btnLast = ((System.Windows.Controls.Button)(target));
            
            #line 177 "..\..\..\..\MyUserControl\Pager.xaml"
            this.btnLast.Click += new System.Windows.RoutedEventHandler(this.btnLast_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

