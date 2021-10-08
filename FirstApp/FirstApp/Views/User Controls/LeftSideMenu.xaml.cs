using First_App.Models.RegistryData;
using First_App.ViewModels;
using System;
using System.Collections.Generic;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace First_App.Views
{
    /// <summary>
    /// Interaction logic for LeftSideMenu.xaml
    /// </summary>
    public partial class LeftSideMenu : UserControl
    {
        public LeftSideMenu()
        {
            InitializeComponent();

            DataContext = new LeftSideMenuViewModel();
        }

        /// <summary>
        ///     Actions after loading main window.
        ///     Verify whether registry has ChimpAuthData key.
        ///     If has then open profile tab or disable left buttons.
        /// </summary>
        /// <param name="sender">Sender.</param>
        /// <param name="e">Event arguments.</param>
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            SavingRegistryData registry = new();
            if (registry.IsExistsKey("ChimpAuthData"))
            {
                // click to profile buttom and forward to profile tab
                RadioButtonAutomationPeer peer = new(profileRadioButton);
                IInvokeProvider invokeProv = peer.GetPattern(PatternInterface.Invoke) as IInvokeProvider;
                invokeProv.Invoke();

                return;
            }
            else // disable left buttons
            {
                //recordsButton.IsEnabled = false;
                //profileButton.IsEnabled = false;
                //playButton.IsEnabled = false;
                //mainTabButton.IsEnabled = false;
            }
        }
    }
}
