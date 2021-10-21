using First_App.Models.RegistryData;
using First_App.Navigation;
using First_App.Services.Authentication;
using First_App.ViewModels;
using First_App.Views;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Automation.Peers;
using System.Windows.Automation.Provider;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace First_App
{
    /// <summary>
    ///     Interaction logic for Chimp.xaml
    /// </summary>
    ///

    public partial class Chimp : Window
    {
        private static double _top;
        private static double _left;
        private static double _width;
        private static double _height;
        public Chimp()
        {
            InitializeComponent();

            ChimpViewModel vm = new();
            DataContext = vm;
            Closing += vm.OnWindowClosing;
        }


        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            _top = this.Top;
            _left = this.Left;
            _width = this.Width;
            _height = this.Height;
            this.MaxWidth = SystemParameters.WorkArea.Width;
            this.MaxHeight = SystemParameters.WorkArea.Height;
        }

        private void minimizeButton_Click(object sender, RoutedEventArgs e)
        {
            SystemCommands.MinimizeWindow(this);
        }

        private void maximizeButton_Click(object sender, RoutedEventArgs e)
        {
            ResizeWindow();
        }
        private void crossButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void Button_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            ResizeWindow();
        }
        protected override void OnContentRendered(EventArgs e)
        {
            base.OnContentRendered(e);
            if (SizeToContent == SizeToContent.WidthAndHeight)
            {
                InvalidateMeasure();
            }
        }

        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (this.WindowState == WindowState.Maximized)
            {
                this.Left = SystemParameters.WorkArea.Left;
                this.Top = SystemParameters.WorkArea.Top + 20;
                this.Width = SystemParameters.WorkArea.Width;
                this.Height = SystemParameters.WorkArea.Height;
                this.ResizeMode = ResizeMode.NoResize;
                this.WindowState = WindowState.Normal;
                minMaxIcon.Source = new BitmapImage(
                   new Uri("pack://application:,,,/First App;component/Views/Windows/normal.png")
                );
            }
        }

        private void ResizeWindow()
        {
            try
            {
                if (this.Height != SystemParameters.WorkArea.Height)
                {
                    this.Left = SystemParameters.WorkArea.Left;
                    this.Top = SystemParameters.WorkArea.Top;
                    this.Width = SystemParameters.WorkArea.Width;
                    this.Height = SystemParameters.WorkArea.Height;
                    this.ResizeMode = ResizeMode.NoResize;
                    minMaxIcon.Source = new BitmapImage(
                        new Uri("pack://application:,,,/First App;component/Views/Windows/normal.png")
                    );
                }
                else
                {
                    this.Left = _left;
                    this.Top = _top;
                    this.Width = _width;
                    this.Height = _height;
                    this.ResizeMode = ResizeMode.CanResize;
                    minMaxIcon.Source = new BitmapImage(
                        new Uri("pack://application:,,,/First App;component/Views/Windows/maximize.png")
                    );
                }
            }
            catch (ArgumentNullException ex)
            {
                MessageBox.Show(ex.Message);
            }
            catch (FileNotFoundException ex)
            {
                MessageBox.Show(ex.Message);
            }
            catch (UriFormatException ex)
            {
                MessageBox.Show(ex.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Button_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonDown(e);
            if (e.ButtonState == MouseButtonState.Pressed)
                DragMove();
        }
    }
}
