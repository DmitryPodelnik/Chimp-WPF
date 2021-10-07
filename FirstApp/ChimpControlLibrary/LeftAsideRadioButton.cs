using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ChimpControlLibrary
{
    /// <summary>
    /// Follow steps 1a or 1b and then 2 to use this custom control in a XAML file.
    ///
    /// Step 1a) Using this custom control in a XAML file that exists in the current project.
    /// Add this XmlNamespace attribute to the root element of the markup file where it is
    /// to be used:
    ///
    ///     xmlns:MyNamespace="clr-namespace:ChimpControlLibrary"
    ///
    ///
    /// Step 1b) Using this custom control in a XAML file that exists in a different project.
    /// Add this XmlNamespace attribute to the root element of the markup file where it is
    /// to be used:
    ///
    ///     xmlns:MyNamespace="clr-namespace:ChimpControlLibrary;assembly=ChimpControlLibrary"
    ///
    /// You will also need to add a project reference from the project where the XAML file lives
    /// to this project and Rebuild to avoid compilation errors:
    ///
    ///     Right click on the target project in the Solution Explorer and
    ///     "Add Reference"->"Projects"->[Select this project]
    ///
    ///
    /// Step 2)
    /// Go ahead and use your control in the XAML file.
    ///
    ///     <MyNamespace:CustomControl1/>
    ///
    /// </summary>
    public class LeftAsideRadioButton : RadioButton
    {
        public static DependencyProperty SourceProperty;
        public static DependencyProperty TextProperty;

        static LeftAsideRadioButton()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(LeftAsideRadioButton), new FrameworkPropertyMetadata(typeof(LeftAsideRadioButton)));
            // registration dependency properties
            SourceProperty = DependencyProperty.Register("Source", typeof(BitmapImage), typeof(LeftAsideRadioButton));
            TextProperty = DependencyProperty.Register("Text", typeof(string), typeof(LeftAsideRadioButton));
        }
        public BitmapImage Source
        {
            get { return (BitmapImage)GetValue(SourceProperty); }
            set { SetValue(SourceProperty, value); }
        }
        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }
    }
}
