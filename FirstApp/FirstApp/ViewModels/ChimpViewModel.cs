using First_App.Models;
using First_App.Models.Commands;
using First_App.Models.DataBase;
using First_App.Models.Game;
using First_App.Models.RegistryData;
using First_App.Navigation;
using First_App.Services.Authentication;
using First_App.Views;
using FirstApp.Models.DataBase;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Automation.Peers;
using System.Windows.Automation.Provider;
using System.Windows.Controls;

namespace First_App.ViewModels
{
    /// <summary>
    ///     Class of chimp view model.
    /// </summary>
    public class ChimpViewModel : INotifyPropertyChanged
    {
        // field of main window
        private Chimp _chimpWindow = (Chimp)Application.Current.MainWindow;

        private LeftSideMenu _menu = new();

        public Navigator Navigator { get; }
        public Authenticator Authenticator { get; }

        public ChimpViewModel()
        {
            Navigator = Navigator.Create();
            Authenticator = Authenticator.Create();

            SavingRegistryData registry = new();
            if (registry.IsExistsKey("ChimpAuthData"))
            {
                // change to user profile tab
                Navigator.CurrentViewModel = new UserProfileViewModel();
            }
            else
            {
                // change to authorization tab
                Navigator.CurrentViewModel = new AuthorizationViewModel();
            }

        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged ([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
