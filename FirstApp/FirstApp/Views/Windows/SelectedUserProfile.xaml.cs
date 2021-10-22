using First_App.ViewModels;
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
    /// Interaction logic for SelectedUserProfile.xaml
    /// </summary>
    public partial class SelectedUserProfile : Window
    {
        public SelectedUserProfile() {}
        public SelectedUserProfile(string username)
        {
            InitializeComponent();

            DataContext = new UserProfileViewModel(username);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonDown(e);
            if (e.ButtonState == MouseButtonState.Pressed)
                DragMove();
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void crossButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
