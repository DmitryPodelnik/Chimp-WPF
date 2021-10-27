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
using System.Windows.Shapes;

namespace First_App.Views.Windows
{
    /// <summary>
    /// Interaction logic for MessageBox.xaml
    /// </summary>
    public partial class MessageBoxWindow : Window
    {
        private static MessageBoxResult _result = MessageBoxResult.Yes;
        public static MessageBoxResult Result
        {
            get => _result;
            set => _result = value;
        }
        protected MessageBoxWindow()
        {
            InitializeComponent();

            this.Owner = Application.Current.MainWindow;
        }

        // singleton instance of MessageBoxWindow
        // private static MessageBoxWindow _instance;
        public static MessageBoxWindow Create()
        {
            //if (_instance == null)
            //{
            //    _instance = new MessageBoxWindow();
            //}
            return new MessageBoxWindow();
        }

        /// <summary>
        ///     Show custom message box.
        /// </summary>
        /// <param name="message"></param>
        /// <param name="title"></param>
        /// <param name="buttons"></param>
        /// <param name="image"></param>
        /// <returns>MessageBoxResult.Yes if pressed Yes button or MessageBoxResult.No</returns>
        public MessageBoxResult ShowMessageBox(string message, string title, MessageBoxButton buttons = MessageBoxButton.OK,
                                                MessageBoxImage image = MessageBoxImage.Error)
        {
            messageTextBlock.Text = message;
            Title = title;

            switch (buttons)
            {
                case MessageBoxButton.YesNo:

                    yesButton.Visibility = Visibility.Visible;
                    noButton.Visibility = Visibility.Visible;
                    OKButton.Visibility = Visibility.Hidden;

                    break;

                case MessageBoxButton.OK:

                    OKButton.Visibility = Visibility.Visible;
                    yesButton.Visibility = Visibility.Hidden;
                    noButton.Visibility = Visibility.Hidden;

                    break;
            }

            switch (image)
            {
                case MessageBoxImage.Error:

                    errorImage.Visibility = Visibility.Visible;
                    warningImage.Visibility = Visibility.Hidden;
                    infoImage.Visibility = Visibility.Hidden;

                    break;

                case MessageBoxImage.Warning:

                    warningImage.Visibility = Visibility.Visible;
                    errorImage.Visibility = Visibility.Hidden;
                    infoImage.Visibility = Visibility.Hidden;

                    break;

                case MessageBoxImage.Information:

                    infoImage.Visibility = Visibility.Visible;
                    errorImage.Visibility = Visibility.Hidden;
                    warningImage.Visibility = Visibility.Hidden;

                    break;
            }

            this.ShowDialog();

            return _result;
        }

        /// <summary>
        ///     Event occurs after pressing mouse left button down.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonDown(e);
            if (e.ButtonState == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }
    }
}
