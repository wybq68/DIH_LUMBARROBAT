﻿#pragma checksum "..\..\..\..\ChartNodes\ChartNodesControl.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "D4CEC90920C6F236494C06AE1E8D6531"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using DynamicDataDisplay.Markers;
using DynamicDataDisplay.Markers.Filters;
using DynamicDataDisplay.Markers.MarkerGenerators;
using Microsoft.Research.DynamicDataDisplay;
using Microsoft.Research.DynamicDataDisplay.Charts;
using Microsoft.Research.DynamicDataDisplay.Charts.Axes;
using Microsoft.Research.DynamicDataDisplay.Charts.Axes.Numeric;
using Microsoft.Research.DynamicDataDisplay.Charts.Isolines;
using Microsoft.Research.DynamicDataDisplay.Charts.Maps;
using Microsoft.Research.DynamicDataDisplay.Charts.Maps.Network;
using Microsoft.Research.DynamicDataDisplay.Charts.Markers;
using Microsoft.Research.DynamicDataDisplay.Charts.Navigation;
using Microsoft.Research.DynamicDataDisplay.Charts.NewLine;
using Microsoft.Research.DynamicDataDisplay.Charts.Selectors;
using Microsoft.Research.DynamicDataDisplay.Charts.Shapes;
using Microsoft.Research.DynamicDataDisplay.Common.Palettes;
using Microsoft.Research.DynamicDataDisplay.Controls;
using Microsoft.Research.DynamicDataDisplay.Converters;
using Microsoft.Research.DynamicDataDisplay.DataSources;
using Microsoft.Research.DynamicDataDisplay.Maps.Charts;
using Microsoft.Research.DynamicDataDisplay.Maps.Charts.TiledRendering;
using Microsoft.Research.DynamicDataDisplay.Maps.DeepZoom;
using Microsoft.Research.DynamicDataDisplay.Maps.Servers;
using Microsoft.Research.DynamicDataDisplay.Maps.Servers.FileServers;
using Microsoft.Research.DynamicDataDisplay.Maps.Servers.Network;
using Microsoft.Research.DynamicDataDisplay.Markers.MarkerGenerators.Rendering;
using Microsoft.Research.DynamicDataDisplay.MarkupExtensions;
using Microsoft.Research.DynamicDataDisplay.Navigation;
using Microsoft.Research.DynamicDataDisplay.PointMarkers;
using Microsoft.Research.DynamicDataDisplay.ViewportRestrictions;
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


namespace LumbarRobot.ChartNodes {
    
    
    /// <summary>
    /// ChartNodesControl
    /// </summary>
    public partial class ChartNodesControl : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector {
        
        
        #line 351 "..\..\..\..\ChartNodes\ChartNodesControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock txExitemName;
        
        #line default
        #line hidden
        
        
        #line 354 "..\..\..\..\ChartNodes\ChartNodesControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnClosed;
        
        #line default
        #line hidden
        
        
        #line 388 "..\..\..\..\ChartNodes\ChartNodesControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Image imgred;
        
        #line default
        #line hidden
        
        
        #line 396 "..\..\..\..\ChartNodes\ChartNodesControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Image imgYellow;
        
        #line default
        #line hidden
        
        
        #line 400 "..\..\..\..\ChartNodes\ChartNodesControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Image imgOrange;
        
        #line default
        #line hidden
        
        
        #line 408 "..\..\..\..\ChartNodes\ChartNodesControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Image imgspringgreen;
        
        #line default
        #line hidden
        
        
        #line 412 "..\..\..\..\ChartNodes\ChartNodesControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Image imgdeepink;
        
        #line default
        #line hidden
        
        
        #line 420 "..\..\..\..\ChartNodes\ChartNodesControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.CheckBox CkGuided;
        
        #line default
        #line hidden
        
        
        #line 426 "..\..\..\..\ChartNodes\ChartNodesControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.CheckBox CkIsotonicA;
        
        #line default
        #line hidden
        
        
        #line 429 "..\..\..\..\ChartNodes\ChartNodesControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.CheckBox CkIsotonicB;
        
        #line default
        #line hidden
        
        
        #line 437 "..\..\..\..\ChartNodes\ChartNodesControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.CheckBox CkFreeCounterWeight;
        
        #line default
        #line hidden
        
        
        #line 441 "..\..\..\..\ChartNodes\ChartNodesControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.CheckBox CkFreeConstantResistance;
        
        #line default
        #line hidden
        
        
        #line 448 "..\..\..\..\ChartNodes\ChartNodesControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ScrollViewer ColumnScroll;
        
        #line default
        #line hidden
        
        
        #line 466 "..\..\..\..\ChartNodes\ChartNodesControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid Mid;
        
        #line default
        #line hidden
        
        
        #line 475 "..\..\..\..\ChartNodes\ChartNodesControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid Grid_FW;
        
        #line default
        #line hidden
        
        
        #line 476 "..\..\..\..\ChartNodes\ChartNodesControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid Grid_Power;
        
        #line default
        #line hidden
        
        
        #line 477 "..\..\..\..\ChartNodes\ChartNodesControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid Grid_C;
        
        #line default
        #line hidden
        
        
        #line 485 "..\..\..\..\ChartNodes\ChartNodesControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal Microsoft.Research.DynamicDataDisplay.ChartPlotter plotter2;
        
        #line default
        #line hidden
        
        
        #line 487 "..\..\..\..\ChartNodes\ChartNodesControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal Microsoft.Research.DynamicDataDisplay.Charts.HorizontalDateTimeAxis dateAxis;
        
        #line default
        #line hidden
        
        
        #line 489 "..\..\..\..\ChartNodes\ChartNodesControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal Microsoft.Research.DynamicDataDisplay.Charts.ClusteredBarChart barChart;
        
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
            System.Uri resourceLocater = new System.Uri("/LumbarRobot;component/chartnodes/chartnodescontrol.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\ChartNodes\ChartNodesControl.xaml"
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
            this.txExitemName = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 2:
            this.btnClosed = ((System.Windows.Controls.Button)(target));
            
            #line 354 "..\..\..\..\ChartNodes\ChartNodesControl.xaml"
            this.btnClosed.PreviewMouseLeftButtonDown += new System.Windows.Input.MouseButtonEventHandler(this.btnClosed_PreviewMouseLeftButtonDown);
            
            #line default
            #line hidden
            return;
            case 3:
            this.imgred = ((System.Windows.Controls.Image)(target));
            return;
            case 4:
            this.imgYellow = ((System.Windows.Controls.Image)(target));
            return;
            case 5:
            this.imgOrange = ((System.Windows.Controls.Image)(target));
            return;
            case 6:
            this.imgspringgreen = ((System.Windows.Controls.Image)(target));
            return;
            case 7:
            this.imgdeepink = ((System.Windows.Controls.Image)(target));
            return;
            case 8:
            this.CkGuided = ((System.Windows.Controls.CheckBox)(target));
            
            #line 420 "..\..\..\..\ChartNodes\ChartNodesControl.xaml"
            this.CkGuided.Checked += new System.Windows.RoutedEventHandler(this.CkGuided_Checked);
            
            #line default
            #line hidden
            return;
            case 9:
            this.CkIsotonicA = ((System.Windows.Controls.CheckBox)(target));
            
            #line 426 "..\..\..\..\ChartNodes\ChartNodesControl.xaml"
            this.CkIsotonicA.Checked += new System.Windows.RoutedEventHandler(this.CkIsotonicA_Checked);
            
            #line default
            #line hidden
            return;
            case 10:
            this.CkIsotonicB = ((System.Windows.Controls.CheckBox)(target));
            
            #line 429 "..\..\..\..\ChartNodes\ChartNodesControl.xaml"
            this.CkIsotonicB.Checked += new System.Windows.RoutedEventHandler(this.CkIsotonicB_Checked);
            
            #line default
            #line hidden
            return;
            case 11:
            this.CkFreeCounterWeight = ((System.Windows.Controls.CheckBox)(target));
            
            #line 437 "..\..\..\..\ChartNodes\ChartNodesControl.xaml"
            this.CkFreeCounterWeight.Checked += new System.Windows.RoutedEventHandler(this.CkFreeCounterWeight_Checked);
            
            #line default
            #line hidden
            return;
            case 12:
            this.CkFreeConstantResistance = ((System.Windows.Controls.CheckBox)(target));
            
            #line 441 "..\..\..\..\ChartNodes\ChartNodesControl.xaml"
            this.CkFreeConstantResistance.Checked += new System.Windows.RoutedEventHandler(this.CkFreeConstantResistance_Checked);
            
            #line default
            #line hidden
            return;
            case 13:
            this.ColumnScroll = ((System.Windows.Controls.ScrollViewer)(target));
            return;
            case 14:
            this.Mid = ((System.Windows.Controls.Grid)(target));
            return;
            case 15:
            this.Grid_FW = ((System.Windows.Controls.Grid)(target));
            return;
            case 16:
            this.Grid_Power = ((System.Windows.Controls.Grid)(target));
            return;
            case 17:
            this.Grid_C = ((System.Windows.Controls.Grid)(target));
            return;
            case 18:
            this.plotter2 = ((Microsoft.Research.DynamicDataDisplay.ChartPlotter)(target));
            return;
            case 19:
            this.dateAxis = ((Microsoft.Research.DynamicDataDisplay.Charts.HorizontalDateTimeAxis)(target));
            return;
            case 20:
            this.barChart = ((Microsoft.Research.DynamicDataDisplay.Charts.ClusteredBarChart)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

