using ChimpControlLibrary;
using First_App.ViewModels;
using First_App.Views.Windows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
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
    ///     Interaction logic for UserRecords.xaml
    /// </summary>
    public partial class UserRecords : UserControl
    {
        public UserRecords()
        {
            InitializeComponent();

        }

        private void UserControl_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            dataGrid.Height = ((Chimp)Application.Current.MainWindow).contentControl.ActualHeight;
            dataGrid.Width = ((Chimp)Application.Current.MainWindow).contentControl.ActualWidth;
        }

        private void dataGrid_Loaded(object sender, RoutedEventArgs e)
        {
            HightLightsDataGridTopRows();
        }

        private void dataGrid_PreviewMouseMove(object sender, MouseEventArgs e)
        {
            HightLightsDataGridTopRows();
        }

        private void HightLightsDataGridTopRows()
        {
            try
            {
                DataGridRow row1 = dataGrid.ItemContainerGenerator.ContainerFromIndex(0) as DataGridRow;
                row1.Background = new SolidColorBrush(Colors.Gold);

                DataGridRow row2 = dataGrid.ItemContainerGenerator.ContainerFromIndex(1) as DataGridRow;
                row2.Background = new SolidColorBrush(Colors.Silver);

                DataGridRow row3 = dataGrid.ItemContainerGenerator.ContainerFromIndex(2) as DataGridRow;
                row3.Background = new SolidColorBrush(Colors.Coral);
            }
            catch (NullReferenceException ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            SelectedUserProfile userProfile = new SelectedUserProfile(button.Content.ToString());
            userProfile.ShowDialog();
        }
    }
}
