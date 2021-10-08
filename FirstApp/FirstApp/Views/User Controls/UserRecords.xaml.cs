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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace First_App.Views
{
    /// <summary>
    /// Interaction logic for UserRecords.xaml
    /// </summary>
    public partial class UserRecords : UserControl
    {
        public UserRecords()
        {
            InitializeComponent();

            DataContext = new UserRecordsViewModel();
        }
    }
}
