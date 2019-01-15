﻿#pragma checksum "..\..\..\Views\ManageDogsView.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "2FB981FBEB9D4C780C556926C318FDA2F10055C6"
//------------------------------------------------------------------------------
// <auto-generated>
//     Dieser Code wurde von einem Tool generiert.
//     Laufzeitversion:4.0.30319.42000
//
//     Änderungen an dieser Datei können falsches Verhalten verursachen und gehen verloren, wenn
//     der Code erneut generiert wird.
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
using de.rietrob.dogginator_product.dogginator.Views;


namespace de.rietrob.dogginator_product.dogginator.Views {
    
    
    /// <summary>
    /// ManageDogsView
    /// </summary>
    public partial class ManageDogsView : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector {
        
        
        #line 26 "..\..\..\Views\ManageDogsView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.StackPanel DogOverviewIsVisible;
        
        #line default
        #line hidden
        
        
        #line 29 "..\..\..\Views\ManageDogsView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox DogSearchText;
        
        #line default
        #line hidden
        
        
        #line 30 "..\..\..\Views\ManageDogsView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button DogSearch;
        
        #line default
        #line hidden
        
        
        #line 32 "..\..\..\Views\ManageDogsView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListView AvailableDogs;
        
        #line default
        #line hidden
        
        
        #line 37 "..\..\..\Views\ManageDogsView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button EditDog;
        
        #line default
        #line hidden
        
        
        #line 40 "..\..\..\Views\ManageDogsView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button DeleteDog;
        
        #line default
        #line hidden
        
        
        #line 45 "..\..\..\Views\ManageDogsView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.StackPanel DogDetailsIsVisible;
        
        #line default
        #line hidden
        
        
        #line 46 "..\..\..\Views\ManageDogsView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ContentControl ActiveDogsDetailsView;
        
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
            System.Uri resourceLocater = new System.Uri("/Dogginator;component/views/managedogsview.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\Views\ManageDogsView.xaml"
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
            this.DogOverviewIsVisible = ((System.Windows.Controls.StackPanel)(target));
            return;
            case 2:
            this.DogSearchText = ((System.Windows.Controls.TextBox)(target));
            return;
            case 3:
            this.DogSearch = ((System.Windows.Controls.Button)(target));
            return;
            case 4:
            this.AvailableDogs = ((System.Windows.Controls.ListView)(target));
            return;
            case 5:
            this.EditDog = ((System.Windows.Controls.Button)(target));
            return;
            case 6:
            this.DeleteDog = ((System.Windows.Controls.Button)(target));
            return;
            case 7:
            this.DogDetailsIsVisible = ((System.Windows.Controls.StackPanel)(target));
            return;
            case 8:
            this.ActiveDogsDetailsView = ((System.Windows.Controls.ContentControl)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

