// Updated by XamlIntelliSenseFileGenerator 01.10.2021 20:40:19
#pragma checksum "..\..\..\..\Views\Chimp.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "9F241FDB1EE2706C21ED1803EDD4E1F23ED757A6"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using First_App;
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


namespace First_App
{


    /// <summary>
    /// Chimp
    /// </summary>
    public partial class Chimp : System.Windows.Window, System.Windows.Markup.IComponentConnector
    {


#line 10 "..\..\..\..\Views\Chimp.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid mainGrid;

#line default
#line hidden


#line 25 "..\..\..\..\Views\Chimp.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid leftAsideGrid;

#line default
#line hidden


#line 36 "..\..\..\..\Views\Chimp.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ColumnDefinition leftAside;

#line default
#line hidden


#line 122 "..\..\..\..\Views\Chimp.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.StackPanel authorizationPanel;

#line default
#line hidden


#line 123 "..\..\..\..\Views\Chimp.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock loginTextBlock;

#line default
#line hidden


#line 124 "..\..\..\..\Views\Chimp.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox loginTextBox;

#line default
#line hidden


#line 127 "..\..\..\..\Views\Chimp.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock passwordTextBlock;

#line default
#line hidden


#line 128 "..\..\..\..\Views\Chimp.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.PasswordBox passwordBox;

#line default
#line hidden


#line 130 "..\..\..\..\Views\Chimp.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button loginButton;

#line default
#line hidden

        private bool _contentLoaded;

        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "5.0.10.0")]
        public void InitializeComponent()
        {
            if (_contentLoaded)
            {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/First App;component/views/chimp.xaml", System.UriKind.Relative);

#line 1 "..\..\..\..\Views\Chimp.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);

#line default
#line hidden
        }

        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "5.0.10.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target)
        {
            switch (connectionId)
            {
                case 1:
                    this.mainGrid = ((System.Windows.Controls.Grid)(target));
                    return;
                case 2:
                    this.leftAsideGrid = ((System.Windows.Controls.Grid)(target));
                    return;
                case 3:
                    this.leftAside = ((System.Windows.Controls.ColumnDefinition)(target));
                    return;
                case 4:
                    this.authorizationPanel = ((System.Windows.Controls.StackPanel)(target));
                    return;
                case 5:
                    this.loginTextBlock = ((System.Windows.Controls.TextBlock)(target));
                    return;
                case 6:
                    this.loginTextBox = ((System.Windows.Controls.TextBox)(target));
                    return;
                case 7:
                    this.passwordTextBlock = ((System.Windows.Controls.TextBlock)(target));
                    return;
                case 8:
                    this.passwordBox = ((System.Windows.Controls.PasswordBox)(target));
                    return;
                case 9:
                    this.loginButton = ((System.Windows.Controls.Button)(target));
                    return;
            }
            this._contentLoaded = true;
        }

        internal System.Windows.Controls.StackPanel accountPanel;
    }
}

