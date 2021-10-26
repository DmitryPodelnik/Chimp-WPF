using First_App.Views.Windows;
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

namespace First_App.Views.User_Controls
{
    /// <summary>
    /// Interaction logic for EditProfile.xaml
    /// </summary>
    public partial class EditProfile : UserControl
    {
        public EditProfile()
        {
            InitializeComponent();
        }

        /// <summary>
        ///     Bind password property of control to NewPassword property in the viewmodel.
        /// </summary>
        /// <param name="sender">Sender.</param>
        /// <param name="e">Routed event arguments.</param>
        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            try
            {
                if (this.DataContext != null)
                { ((dynamic)this.DataContext).NewPassword = ((PasswordBox)sender).Password; }
            }
            catch (Exception ex)
            {
                MessageBoxWindow.Create().ShowMessageBox(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        /// <summary>
        ///     Bind password property of control to NewPassword property in the viewmodel.
        /// </summary>
        /// <param name="sender">Sender.</param>
        /// <param name="e">Routed event arguments.</param>
        private void PasswordBoxConfirm_PasswordChanged(object sender, RoutedEventArgs e)
        {
            try
            {
                if (this.DataContext != null)
                { ((dynamic)this.DataContext).NewPassword = ((PasswordBox)sender).Password; }
            }
            catch (Exception ex)
            {
                MessageBoxWindow.Create().ShowMessageBox(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
