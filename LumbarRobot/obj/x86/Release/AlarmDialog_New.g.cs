﻿#pragma checksum "..\..\..\AlarmDialog_New.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "26EE0F0263248DA2C4935A82D6A9B245"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using LumbarRobot.Interactions;
using Microsoft.Expression.Interactivity.Core;
using Microsoft.Expression.Interactivity.Input;
using Microsoft.Expression.Interactivity.Layout;
using Microsoft.Expression.Interactivity.Media;
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
    /// AlarmDialog_New
    /// </summary>
    public partial class AlarmDialog_New : LumbarRobot.Interactions.AlarmInteractionDialog, System.Windows.Markup.IComponentConnector {
        
        
        #line 11 "..\..\..\AlarmDialog_New.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal LumbarRobot.AlarmDialog_New root;
        
        #line default
        #line hidden
        
        
        #line 155 "..\..\..\AlarmDialog_New.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ContentControl BackgroundImageHolder;
        
        #line default
        #line hidden
        
        
        #line 182 "..\..\..\AlarmDialog_New.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock lblTitle;
        
        #line default
        #line hidden
        
        
        #line 187 "..\..\..\AlarmDialog_New.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnClosed;
        
        #line default
        #line hidden
        
        
        #line 203 "..\..\..\AlarmDialog_New.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock lblMsg;
        
        #line default
        #line hidden
        
        
        #line 213 "..\..\..\AlarmDialog_New.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid ButtonsGrid;
        
        #line default
        #line hidden
        
        
        #line 219 "..\..\..\AlarmDialog_New.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnYes;
        
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
            System.Uri resourceLocater = new System.Uri("/LumbarRobot;component/alarmdialog_new.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\AlarmDialog_New.xaml"
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
            this.root = ((LumbarRobot.AlarmDialog_New)(target));
            return;
            case 2:
            this.BackgroundImageHolder = ((System.Windows.Controls.ContentControl)(target));
            return;
            case 3:
            this.lblTitle = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 4:
            this.btnClosed = ((System.Windows.Controls.Button)(target));
            return;
            case 5:
            this.lblMsg = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 6:
            this.ButtonsGrid = ((System.Windows.Controls.Grid)(target));
            return;
            case 7:
            this.btnYes = ((System.Windows.Controls.Button)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

