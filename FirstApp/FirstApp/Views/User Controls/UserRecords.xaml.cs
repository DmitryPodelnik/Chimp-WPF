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

        }

        private void dataGrid_Loaded(object sender, RoutedEventArgs e)
        {
            DataGridRow row = dataGrid.ItemContainerGenerator.ContainerFromIndex(0) as DataGridRow;
            //dataGrid.ItemContainerGenerator.ContainerFromIndex(0) = row;
            row.Background = new SolidColorBrush(Colors.Red);

            // get resource dictionary with styles
            ResourceDictionary resourceDictionary = Application.Current.Resources.MergedDictionaries[3];

            dataGrid.Columns[0].CellStyle = (Style)resourceDictionary["mainDataGridCellStyle2"];
            dataGrid.Columns[1].CellStyle = (Style)resourceDictionary["mainDataGridCellStyle2"];
            dataGrid.Columns[2].CellStyle = (Style)resourceDictionary["mainDataGridCellStyle2"];
        }
    }
}
