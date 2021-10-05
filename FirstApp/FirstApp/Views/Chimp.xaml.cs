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
using System.Windows.Shapes;

namespace First_App
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Chimp : Window
    {
        public Chimp()
        {
            InitializeComponent();

            DataContext = new ChimpViewModel();
        }

        /**
         * Actions after loading main window
         * Verify whether registry has ChimpAuthData key
         * If has then open profile tab or disable left buttons
         * 
         * @param sender - sender
         * @param e - event arguments
         */
        private void mainGrid_Loaded(object sender, RoutedEventArgs e)
        {
            SavingRegistryData registry = new();
            if (registry.IsExistsKey("ChimpAuthData"))
            {
                // click to profile buttom and forward to profile tab
                ButtonAutomationPeer peer = new ButtonAutomationPeer(profileButton);
                IInvokeProvider invokeProv = peer.GetPattern(PatternInterface.Invoke) as IInvokeProvider;
                invokeProv.Invoke();

                return;
            }
            else // disable left buttons
            {
                recordsButton.IsEnabled = false;
                profileButton.IsEnabled = false;
                playButton.IsEnabled = false;
                mainTabButton.IsEnabled = false;
            }
        }
    }
}
