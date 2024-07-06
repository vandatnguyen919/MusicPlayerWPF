﻿#pragma checksum "..\..\..\MusicPlayer.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "08D7356B0CABA12C4A592F0C738D4D4068777D7A"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using MusicPlayerUI;
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


namespace MusicPlayerUI {
    
    
    /// <summary>
    /// MusicPlayer
    /// </summary>
    public partial class MusicPlayer : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 29 "..\..\..\MusicPlayer.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ContentControl mainContentControl;
        
        #line default
        #line hidden
        
        
        #line 38 "..\..\..\MusicPlayer.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.MediaElement mediaElement;
        
        #line default
        #line hidden
        
        
        #line 48 "..\..\..\MusicPlayer.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock currentTimeTextBlock;
        
        #line default
        #line hidden
        
        
        #line 50 "..\..\..\MusicPlayer.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock totalTimeTextBlock;
        
        #line default
        #line hidden
        
        
        #line 52 "..\..\..\MusicPlayer.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Slider playbackSlider;
        
        #line default
        #line hidden
        
        
        #line 59 "..\..\..\MusicPlayer.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Slider volumeSlider;
        
        #line default
        #line hidden
        
        
        #line 60 "..\..\..\MusicPlayer.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock volumeValueTextBlock;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "8.0.4.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/MusicPlayerUI;component/musicplayer.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\MusicPlayer.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "8.0.4.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            
            #line 22 "..\..\..\MusicPlayer.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.ShowHomeView);
            
            #line default
            #line hidden
            return;
            case 2:
            
            #line 23 "..\..\..\MusicPlayer.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.ShowAlbumsView);
            
            #line default
            #line hidden
            return;
            case 3:
            
            #line 24 "..\..\..\MusicPlayer.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.ShowArtistsView);
            
            #line default
            #line hidden
            return;
            case 4:
            this.mainContentControl = ((System.Windows.Controls.ContentControl)(target));
            return;
            case 5:
            this.mediaElement = ((System.Windows.Controls.MediaElement)(target));
            return;
            case 6:
            this.currentTimeTextBlock = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 7:
            this.totalTimeTextBlock = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 8:
            this.playbackSlider = ((System.Windows.Controls.Slider)(target));
            
            #line 52 "..\..\..\MusicPlayer.xaml"
            this.playbackSlider.ValueChanged += new System.Windows.RoutedPropertyChangedEventHandler<double>(this.PlaybackSlider_ValueChanged);
            
            #line default
            #line hidden
            return;
            case 9:
            
            #line 56 "..\..\..\MusicPlayer.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Play_Click);
            
            #line default
            #line hidden
            return;
            case 10:
            
            #line 57 "..\..\..\MusicPlayer.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Pause_Click);
            
            #line default
            #line hidden
            return;
            case 11:
            this.volumeSlider = ((System.Windows.Controls.Slider)(target));
            
            #line 59 "..\..\..\MusicPlayer.xaml"
            this.volumeSlider.ValueChanged += new System.Windows.RoutedPropertyChangedEventHandler<double>(this.VolumeSlider_ValueChanged);
            
            #line default
            #line hidden
            return;
            case 12:
            this.volumeValueTextBlock = ((System.Windows.Controls.TextBlock)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

