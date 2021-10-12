using First_App.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
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

namespace First_App.Views
{
    /// <summary>
    ///     Interaction logic for Authorization.xaml
    /// </summary>
    public partial class Authorization : UserControl
    {
        public Authorization()
        {
            InitializeComponent();
        }

        /// <summary>
        ///     Bind password property of control to password property in the viewmodel.
        /// </summary>
        /// <param name="sender">Sender.</param>
        /// <param name="e">Router event arguments.</param>
        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (this.DataContext != null)
            { ((dynamic)this.DataContext).Password = ((PasswordBox)sender).Password; }
        }
    }
}
