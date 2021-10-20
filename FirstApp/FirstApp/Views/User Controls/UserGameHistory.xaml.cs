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
    /// Interaction logic for UserGameHistory.xaml
    /// </summary>
    public partial class UserGameHistory : UserControl
    {
        public UserGameHistory()
        {
            InitializeComponent();
        }

        private void UserControl_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            dataGrid.Height = ((Chimp)Application.Current.MainWindow).contentControl.ActualHeight - 60; // элемент-источник
            dataGrid.Width = ((Chimp)Application.Current.MainWindow).contentControl.ActualWidth;
        }
    }
}
