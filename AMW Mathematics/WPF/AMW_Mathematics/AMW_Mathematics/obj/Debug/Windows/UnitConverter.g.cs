﻿#pragma checksum "..\..\..\Windows\UnitConverter.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "03CD7AF79B670764F83F5442BCC469F5"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using AMW_Mathematics;
using MahApps.Metro.Controls;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms.Integration;
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


namespace AMW_Mathematics.Windows {
    
    
    /// <summary>
    /// UnitConverter
    /// </summary>
    public partial class UnitConverter : MahApps.Metro.Controls.MetroWindow, System.Windows.Markup.IComponentConnector {
        
        
        #line 13 "..\..\..\Windows\UnitConverter.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.GroupBox groupBox;
        
        #line default
        #line hidden
        
        
        #line 15 "..\..\..\Windows\UnitConverter.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox selectUnit;
        
        #line default
        #line hidden
        
        
        #line 23 "..\..\..\Windows\UnitConverter.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label label;
        
        #line default
        #line hidden
        
        
        #line 24 "..\..\..\Windows\UnitConverter.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label label_Copy;
        
        #line default
        #line hidden
        
        
        #line 25 "..\..\..\Windows\UnitConverter.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label label_Copy1;
        
        #line default
        #line hidden
        
        
        #line 26 "..\..\..\Windows\UnitConverter.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label label_Copy2;
        
        #line default
        #line hidden
        
        
        #line 27 "..\..\..\Windows\UnitConverter.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox FromCombo;
        
        #line default
        #line hidden
        
        
        #line 28 "..\..\..\Windows\UnitConverter.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox ToCombo;
        
        #line default
        #line hidden
        
        
        #line 29 "..\..\..\Windows\UnitConverter.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox InputBox;
        
        #line default
        #line hidden
        
        
        #line 30 "..\..\..\Windows\UnitConverter.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox OutputBox;
        
        #line default
        #line hidden
        
        
        #line 31 "..\..\..\Windows\UnitConverter.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label equal;
        
        #line default
        #line hidden
        
        
        #line 32 "..\..\..\Windows\UnitConverter.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button Convert;
        
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
            System.Uri resourceLocater = new System.Uri("/AMW_Mathematics;component/windows/unitconverter.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\Windows\UnitConverter.xaml"
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
            this.groupBox = ((System.Windows.Controls.GroupBox)(target));
            return;
            case 2:
            this.selectUnit = ((System.Windows.Controls.ComboBox)(target));
            
            #line 15 "..\..\..\Windows\UnitConverter.xaml"
            this.selectUnit.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.selectUnit_SelectionChanged);
            
            #line default
            #line hidden
            return;
            case 3:
            this.label = ((System.Windows.Controls.Label)(target));
            return;
            case 4:
            this.label_Copy = ((System.Windows.Controls.Label)(target));
            return;
            case 5:
            this.label_Copy1 = ((System.Windows.Controls.Label)(target));
            return;
            case 6:
            this.label_Copy2 = ((System.Windows.Controls.Label)(target));
            return;
            case 7:
            this.FromCombo = ((System.Windows.Controls.ComboBox)(target));
            return;
            case 8:
            this.ToCombo = ((System.Windows.Controls.ComboBox)(target));
            
            #line 28 "..\..\..\Windows\UnitConverter.xaml"
            this.ToCombo.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.ToCombo_SelectionChanged);
            
            #line default
            #line hidden
            return;
            case 9:
            this.InputBox = ((System.Windows.Controls.TextBox)(target));
            return;
            case 10:
            this.OutputBox = ((System.Windows.Controls.TextBox)(target));
            return;
            case 11:
            this.equal = ((System.Windows.Controls.Label)(target));
            return;
            case 12:
            this.Convert = ((System.Windows.Controls.Button)(target));
            
            #line 32 "..\..\..\Windows\UnitConverter.xaml"
            this.Convert.Click += new System.Windows.RoutedEventHandler(this.Convert_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

