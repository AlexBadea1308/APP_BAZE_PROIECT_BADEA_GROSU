﻿#pragma checksum "..\..\..\..\..\Windows\Reservations\AddReservations.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "4C299930EE18F9D140659EF0EB31EC5910752627"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using HotelReservations.Windows;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Controls.Ribbon;
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


namespace HotelReservations.Windows {
    
    
    /// <summary>
    /// AddReservations
    /// </summary>
    public partial class AddReservations : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 23 "..\..\..\..\..\Windows\Reservations\AddReservations.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGrid RoomsDataGrid;
        
        #line default
        #line hidden
        
        
        #line 29 "..\..\..\..\..\Windows\Reservations\AddReservations.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button AddGuestButton;
        
        #line default
        #line hidden
        
        
        #line 30 "..\..\..\..\..\Windows\Reservations\AddReservations.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button EditGuestButton;
        
        #line default
        #line hidden
        
        
        #line 31 "..\..\..\..\..\Windows\Reservations\AddReservations.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button DeleteGuestButton;
        
        #line default
        #line hidden
        
        
        #line 34 "..\..\..\..\..\Windows\Reservations\AddReservations.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGrid GuestsDataGrid;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "7.0.12.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/HotelReservations;V1.0.0.0;component/windows/reservations/addreservations.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\..\Windows\Reservations\AddReservations.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "7.0.12.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.RoomsDataGrid = ((System.Windows.Controls.DataGrid)(target));
            
            #line 23 "..\..\..\..\..\Windows\Reservations\AddReservations.xaml"
            this.RoomsDataGrid.AutoGeneratingColumn += new System.EventHandler<System.Windows.Controls.DataGridAutoGeneratingColumnEventArgs>(this.RoomsDataGrid_AutoGeneratingColumn);
            
            #line default
            #line hidden
            return;
            case 2:
            this.AddGuestButton = ((System.Windows.Controls.Button)(target));
            
            #line 29 "..\..\..\..\..\Windows\Reservations\AddReservations.xaml"
            this.AddGuestButton.Click += new System.Windows.RoutedEventHandler(this.AddGuestButton_Click);
            
            #line default
            #line hidden
            return;
            case 3:
            this.EditGuestButton = ((System.Windows.Controls.Button)(target));
            
            #line 30 "..\..\..\..\..\Windows\Reservations\AddReservations.xaml"
            this.EditGuestButton.Click += new System.Windows.RoutedEventHandler(this.EditGuestButton_Click);
            
            #line default
            #line hidden
            return;
            case 4:
            this.DeleteGuestButton = ((System.Windows.Controls.Button)(target));
            
            #line 31 "..\..\..\..\..\Windows\Reservations\AddReservations.xaml"
            this.DeleteGuestButton.Click += new System.Windows.RoutedEventHandler(this.DeleteGuestButton_Click);
            
            #line default
            #line hidden
            return;
            case 5:
            this.GuestsDataGrid = ((System.Windows.Controls.DataGrid)(target));
            
            #line 34 "..\..\..\..\..\Windows\Reservations\AddReservations.xaml"
            this.GuestsDataGrid.AutoGeneratingColumn += new System.EventHandler<System.Windows.Controls.DataGridAutoGeneratingColumnEventArgs>(this.GuestsDataGrid_AutoGeneratingColumn);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

