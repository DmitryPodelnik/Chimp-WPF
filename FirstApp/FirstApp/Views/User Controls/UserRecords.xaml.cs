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

        /// <summary>
        ///     Event occurs when DataGrid size was changed.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UserControl_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            dataGrid.Height = ((Chimp)Application.Current.MainWindow).contentControl.ActualHeight;
            dataGrid.Width = ((Chimp)Application.Current.MainWindow).contentControl.ActualWidth;
        }

        /// <summary>
        ///     Event occurs when DataGrid is loaded.
        ///     Hightlights first 3 rows in DataGrid.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGrid_Loaded(object sender, RoutedEventArgs e)
        {
            HightLightsDataGridTopRows();
        }

        /// <summary>
        ///     Event occurs after mouse moving above DataGrid.
        ///     Hightlights first 3 rows in DataGrid.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGrid_PreviewMouseMove(object sender, MouseEventArgs e)
        {
            HightLightsDataGridTopRows();
        }

        /// <summary>
        ///     Hightlights first 3 rows in DataGrid.
        /// </summary>
        private void HightLightsDataGridTopRows()
        {
            try
            {
                if (dataGrid.Items.Count > 0)
                {
                    DataGridRow row1 = dataGrid.ItemContainerGenerator.ContainerFromIndex(0) as DataGridRow;
                    row1.Background = new SolidColorBrush(Colors.Gold);
                }

                if (dataGrid.Items.Count > 1)
                {
                    DataGridRow row2 = dataGrid.ItemContainerGenerator.ContainerFromIndex(1) as DataGridRow;
                    row2.Background = new SolidColorBrush(Colors.Silver);
                }

                if (dataGrid.Items.Count > 2)
                {
                    DataGridRow row3 = dataGrid.ItemContainerGenerator.ContainerFromIndex(2) as DataGridRow;
                    row3.Background = new SolidColorBrush(Colors.Coral);
                }
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

        /// <summary>
        ///     Event occurs after click username button.
        ///     Show modal window of selected user profile.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            SelectedUserProfile userProfileWindow = new SelectedUserProfile(button.Content.ToString());
            userProfileWindow.Owner = Application.Current.MainWindow;
            userProfileWindow.ShowDialog();
        }
    }
}
