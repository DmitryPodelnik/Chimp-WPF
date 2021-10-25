using First_App.Models.DataBase;
using First_App.Models.Game;
using First_App.Models.RegistryData;
using First_App.Navigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

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

        // field that stores instance of animation class
        private MainAnimation _mainAnimation = new();
        private Navigator _nav = Navigator.Create();

        private ObservableCollection<Button> _playGrid { get; set; } = new();
        public ObservableCollection<Button> PlayGrid
        {
            get => _playGrid;
            set => _playGrid = value;
        }

        /// <summary>
        ///     MainTabViewModel constructor().
        ///     Get current user from the registry.
        /// </summary>
        public MainTabViewModel()
        {

        }
    }
}
