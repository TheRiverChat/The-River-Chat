﻿#pragma checksum "..\..\hosting.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "5F7AF399A7F8DA4FAC82D61FA90B884DE63D5AF8082671186E62B38F2C9680C8"
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
using The_River_Chat;


namespace The_River_Chat {
    
    
    /// <summary>
    /// hosting
    /// </summary>
    public partial class hosting : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 14 "..\..\hosting.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label label_name;
        
        #line default
        #line hidden
        
        
        #line 18 "..\..\hosting.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label label_ip;
        
        #line default
        #line hidden
        
        
        #line 25 "..\..\hosting.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label label_port;
        
        #line default
        #line hidden
        
        
        #line 32 "..\..\hosting.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button startserver_btn;
        
        #line default
        #line hidden
        
        
        #line 43 "..\..\hosting.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button UPNP_btn;
        
        #line default
        #line hidden
        
        
        #line 55 "..\..\hosting.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.StackPanel users;
        
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
            System.Uri resourceLocater = new System.Uri("/The River Chat;component/hosting.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\hosting.xaml"
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
            this.label_name = ((System.Windows.Controls.Label)(target));
            return;
            case 2:
            this.label_ip = ((System.Windows.Controls.Label)(target));
            return;
            case 3:
            this.label_port = ((System.Windows.Controls.Label)(target));
            return;
            case 4:
            this.startserver_btn = ((System.Windows.Controls.Button)(target));
            
            #line 37 "..\..\hosting.xaml"
            this.startserver_btn.Click += new System.Windows.RoutedEventHandler(this.startserver_btn_Click);
            
            #line default
            #line hidden
            return;
            case 5:
            this.UPNP_btn = ((System.Windows.Controls.Button)(target));
            
            #line 48 "..\..\hosting.xaml"
            this.UPNP_btn.Click += new System.Windows.RoutedEventHandler(this.UPNP_btn_Click);
            
            #line default
            #line hidden
            return;
            case 6:
            this.users = ((System.Windows.Controls.StackPanel)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

