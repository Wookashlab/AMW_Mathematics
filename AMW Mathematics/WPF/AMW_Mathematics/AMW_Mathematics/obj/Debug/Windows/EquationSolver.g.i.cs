﻿#pragma checksum "..\..\..\Windows\EquationSolver.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "6E2CC436D7F99C8D91EE10AF5A1217E7"
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
    /// EquationSolver
    /// </summary>
    public partial class EquationSolver : MahApps.Metro.Controls.MetroWindow, System.Windows.Markup.IComponentConnector, System.Windows.Markup.IStyleConnector {
        
        
        #line 13 "..\..\..\Windows\EquationSolver.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TabControl SelectBorder;
        
        #line default
        #line hidden
        
        
        #line 14 "..\..\..\Windows\EquationSolver.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox CoEquations;
        
        #line default
        #line hidden
        
        
        #line 23 "..\..\..\Windows\EquationSolver.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TabControl MainBorder;
        
        #line default
        #line hidden
        
        
        #line 24 "..\..\..\Windows\EquationSolver.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListView ListViewExp;
        
        #line default
        #line hidden
        
        
        #line 34 "..\..\..\Windows\EquationSolver.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button Solve;
        
        #line default
        #line hidden
        
        
        #line 35 "..\..\..\Windows\EquationSolver.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.GroupBox groupBox;
        
        #line default
        #line hidden
        
        
        #line 37 "..\..\..\Windows\EquationSolver.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid SolverExample;
        
        #line default
        #line hidden
        
        
        #line 38 "..\..\..\Windows\EquationSolver.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock textBlock;
        
        #line default
        #line hidden
        
        
        #line 45 "..\..\..\Windows\EquationSolver.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListView SolverResultList;
        
        #line default
        #line hidden
        
        
        #line 52 "..\..\..\Windows\EquationSolver.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.GridView cos;
        
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
            System.Uri resourceLocater = new System.Uri("/AMW_Mathematics;component/windows/equationsolver.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\Windows\EquationSolver.xaml"
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
            this.SelectBorder = ((System.Windows.Controls.TabControl)(target));
            return;
            case 2:
            this.CoEquations = ((System.Windows.Controls.ComboBox)(target));
            
            #line 14 "..\..\..\Windows\EquationSolver.xaml"
            this.CoEquations.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.CoEquations_SelectionChanged);
            
            #line default
            #line hidden
            return;
            case 3:
            this.MainBorder = ((System.Windows.Controls.TabControl)(target));
            return;
            case 4:
            this.ListViewExp = ((System.Windows.Controls.ListView)(target));
            return;
            case 5:
            this.Solve = ((System.Windows.Controls.Button)(target));
            
            #line 34 "..\..\..\Windows\EquationSolver.xaml"
            this.Solve.Click += new System.Windows.RoutedEventHandler(this.Solve_Click);
            
            #line default
            #line hidden
            return;
            case 6:
            this.groupBox = ((System.Windows.Controls.GroupBox)(target));
            return;
            case 7:
            this.SolverExample = ((System.Windows.Controls.Grid)(target));
            return;
            case 8:
            this.textBlock = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 9:
            this.SolverResultList = ((System.Windows.Controls.ListView)(target));
            return;
            case 10:
            this.cos = ((System.Windows.Controls.GridView)(target));
            return;
            }
            this._contentLoaded = true;
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        void System.Windows.Markup.IStyleConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 11:
            
            #line 77 "..\..\..\Windows\EquationSolver.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.RemoveEquation_Click);
            
            #line default
            #line hidden
            break;
            }
        }
    }
}

