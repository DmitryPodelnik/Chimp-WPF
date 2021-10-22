using First_App.Models.DataBase;
using First_App.Models.RegistryData;
using First_App.Navigation;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace First_App.ViewModels
{
    /// <summary>
    ///     Class of main tab view model.
    /// </summary>
    class MainTabViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }

        private Navigator _nav = Navigator.Create();

        /// <summary>
        ///     UserProfileViewModel constructor().
        ///     Get current user from the registry.
        /// </summary>
        public MainTabViewModel()
        {

        }
    }
}
