﻿#pragma checksum "..\..\TunnelController.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "FDB2CDB021AB0EDFE067ADCD0C36E5B1"
//------------------------------------------------------------------------------
// <auto-generated>
//     Il codice è stato generato da uno strumento.
//     Versione runtime:4.0.30319.42000
//
//     Le modifiche apportate a questo file possono provocare un comportamento non corretto e andranno perse se
//     il codice viene rigenerato.
// </auto-generated>
//------------------------------------------------------------------------------

using PacketRouter;
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


namespace PacketRouter {
    
    
    /// <summary>
    /// TunnerController
    /// </summary>
    public partial class TunnerController : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector {
        
        
        #line 14 "..\..\TunnelController.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox netIntfACombo;
        
        #line default
        #line hidden
        
        
        #line 15 "..\..\TunnelController.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox netIntfBCombo;
        
        #line default
        #line hidden
        
        
        #line 16 "..\..\TunnelController.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button startChannelButton;
        
        #line default
        #line hidden
        
        
        #line 17 "..\..\TunnelController.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button stopChannelButton;
        
        #line default
        #line hidden
        
        
        #line 18 "..\..\TunnelController.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox channAPort;
        
        #line default
        #line hidden
        
        
        #line 19 "..\..\TunnelController.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox channBPort;
        
        #line default
        #line hidden
        
        
        #line 20 "..\..\TunnelController.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox channelStatusA;
        
        #line default
        #line hidden
        
        
        #line 21 "..\..\TunnelController.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox channelStatusB;
        
        #line default
        #line hidden
        
        
        #line 22 "..\..\TunnelController.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox netAddressesA;
        
        #line default
        #line hidden
        
        
        #line 23 "..\..\TunnelController.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox netAddressesB;
        
        #line default
        #line hidden
        
        
        #line 24 "..\..\TunnelController.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox remoteEndpointAText;
        
        #line default
        #line hidden
        
        
        #line 25 "..\..\TunnelController.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox remoteEndpointBText;
        
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
            System.Uri resourceLocater = new System.Uri("/PacketRouter;component/tunnelcontroller.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\TunnelController.xaml"
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
            this.netIntfACombo = ((System.Windows.Controls.ComboBox)(target));
            return;
            case 2:
            this.netIntfBCombo = ((System.Windows.Controls.ComboBox)(target));
            return;
            case 3:
            this.startChannelButton = ((System.Windows.Controls.Button)(target));
            
            #line 16 "..\..\TunnelController.xaml"
            this.startChannelButton.Click += new System.Windows.RoutedEventHandler(this.startChannelButton_Click);
            
            #line default
            #line hidden
            return;
            case 4:
            this.stopChannelButton = ((System.Windows.Controls.Button)(target));
            
            #line 17 "..\..\TunnelController.xaml"
            this.stopChannelButton.Click += new System.Windows.RoutedEventHandler(this.stopChannelButton_Click);
            
            #line default
            #line hidden
            return;
            case 5:
            this.channAPort = ((System.Windows.Controls.TextBox)(target));
            return;
            case 6:
            this.channBPort = ((System.Windows.Controls.TextBox)(target));
            return;
            case 7:
            this.channelStatusA = ((System.Windows.Controls.TextBox)(target));
            return;
            case 8:
            this.channelStatusB = ((System.Windows.Controls.TextBox)(target));
            return;
            case 9:
            this.netAddressesA = ((System.Windows.Controls.ComboBox)(target));
            return;
            case 10:
            this.netAddressesB = ((System.Windows.Controls.ComboBox)(target));
            return;
            case 11:
            this.remoteEndpointAText = ((System.Windows.Controls.TextBox)(target));
            return;
            case 12:
            this.remoteEndpointBText = ((System.Windows.Controls.TextBox)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

