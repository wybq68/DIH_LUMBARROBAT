﻿#pragma checksum "..\..\..\..\MyUserControl\ControlPanel.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "BA6B603DE5C830EBFC8E996E328C17F4"
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
    /// ControlPanel
    /// </summary>
    public partial class ControlPanel : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector {
        
        
        #line 43 "..\..\..\..\MyUserControl\ControlPanel.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Image imgPrev;
        
        #line default
        #line hidden
        
        
        #line 44 "..\..\..\..\MyUserControl\ControlPanel.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Image imgPlay;
        
        #line default
        #line hidden
        
        
        #line 45 "..\..\..\..\MyUserControl\ControlPanel.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Image imgStop;
        
        #line default
        #line hidden
        
        
        #line 46 "..\..\..\..\MyUserControl\ControlPanel.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Image imgNext;
        
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
            System.Uri resourceLocater = new System.Uri("/LumbarRobot;component/myusercontrol/controlpanel.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\MyUserControl\ControlPanel.xaml"
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
            this.imgPrev = ((System.Windows.Controls.Image)(target));
            
            #line 43 "..\..\..\..\MyUserControl\ControlPanel.xaml"
            this.imgPrev.MouseDown += new System.Windows.Input.MouseButtonEventHandler(this.btnPrev_Click);
            
            #line default
            #line hidden
            return;
            case 2:
            this.imgPlay = ((System.Windows.Controls.Image)(target));
            
            #line 44 "..\..\..\..\MyUserControl\ControlPanel.xaml"
            this.imgPlay.MouseDown += new System.Windows.Input.MouseButtonEventHandler(this.btnPlay_Click);
            
            #line default
            #line hidden
            return;
            case 3:
            this.imgStop = ((System.Windows.Controls.Image)(target));
            
            #line 45 "..\..\..\..\MyUserControl\ControlPanel.xaml"
            this.imgStop.MouseDown += new System.Windows.Input.MouseButtonEventHandler(this.btnStop_Click);
            
            #line default
            #line hidden
            return;
            case 4:
            this.imgNext = ((System.Windows.Controls.Image)(target));
            
            #line 46 "..\..\..\..\MyUserControl\ControlPanel.xaml"
            this.imgNext.MouseDown += new System.Windows.Input.MouseButtonEventHandler(this.btnNext_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

