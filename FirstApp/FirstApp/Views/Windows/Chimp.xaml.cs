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

        /// <summary>
        ///     Event occurs when chimp window is loaded.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            _top = this.Top;
            _left = this.Left;
            _width = this.Width;
            _height = this.Height;
            this.MaxWidth = SystemParameters.WorkArea.Width;
            this.MaxHeight = SystemParameters.WorkArea.Height;
        }

        /// <summary>
        ///     Event occurs after clicking minimize button.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void minimizeButton_Click(object sender, RoutedEventArgs e)
        {
            SystemCommands.MinimizeWindow(this);
        }

        /// <summary>
        ///     Event occurs after clicking maximize button.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void maximizeButton_Click(object sender, RoutedEventArgs e)
        {
            ResizeWindow();
        }

        /// <summary>
        ///     Event occurs after clicking cross button.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void crossButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        /// <summary>
        ///     Event occurs after mouse left double click on the chrome button.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                ResizeWindow();
            }
        }

        /// <summary>
        ///     Event occurs after window content rendered.
        /// </summary>
        /// <param name="e"></param>
        protected override void OnContentRendered(EventArgs e)
        {
            base.OnContentRendered(e);
            if (SizeToContent == SizeToContent.WidthAndHeight)
            {
                InvalidateMeasure();
            }
        }

        /// <summary>
        ///     Event occurs when window size was changed.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            //if (this.WindowState == WindowState.Maximized)
            //    this.Left = SystemParameters.WorkArea.Left;
            //    this.Top = SystemParameters.WorkArea.Top;
            //    this.Width = SystemParameters.WorkArea.Width;
            //    this.Height = SystemParameters.WorkArea.Height;
            //    this.ResizeMode = ResizeMode.NoResize;
            //    this.WindowState = WindowState.Normal;
            //    minMaxIcon.Source = new BitmapImage(
            //       new Uri("pack://application:,,,/First App;component/Views/Windows/normal.png")
            //    );
            //}

        }

        /// <summary>
        ///     Resizes chimp window.
        /// </summary>
        private void ResizeWindow()
        {
            try
            {
                if (this.Height == SystemParameters.WorkArea.Height)
                {
                    this.WindowState = WindowState.Normal;
                    this.Left = _left;
                    this.Top = _top;
                    this.Width = _width;
                    this.Height = _height;
                    this.ResizeMode = ResizeMode.CanResize;
                    minMaxIcon.Source = new BitmapImage(
                        new Uri("pack://application:,,,/First App;component/Views/Windows/maximize.png")
                    );
                }
                else if (this.WindowState != WindowState.Maximized)
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

        /// <summary>
        ///     Event occurs after pressing mouse left button down.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (this.WindowState == WindowState.Maximized)
            {
                this.Left = _left;
                this.Width = _width;
                this.Height = _height;
                this.ResizeMode = ResizeMode.CanResize;
                minMaxIcon.Source = new BitmapImage(
                    new Uri("pack://application:,,,/First App;component/Views/Windows/maximize.png")
                );
            }
            base.OnMouseLeftButtonDown(e);
            if (e.ButtonState == MouseButtonState.Pressed)
                DragMove();
        }
    }
}
