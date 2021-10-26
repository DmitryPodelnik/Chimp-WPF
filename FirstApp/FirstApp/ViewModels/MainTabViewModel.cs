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
        private MainAnimation _mainAnimation = MainAnimation.Create();
        private Navigator _nav = Navigator.Create();

        private ObservableCollection<Button> _animationGrid { get; set; } = new();
        public ObservableCollection<Button> AnimationGrid
        {
            get => _animationGrid;
            set => _animationGrid = value;
        }

        /// <summary>
        ///     MainTabViewModel constructor().
        /// </summary>
        public MainTabViewModel()
        {
            // call MainAnimation constructor() and initialize animation cubes
            InitializeAnimationField();
            // Start animation after creating animation field.
            // Animation field initialization of cube buttons.
            _mainAnimation.StartAnimation();
        }

        /// <summary>
        ///     Creates or gets singleton instance of MainAnimation class.
        ///     Calls MainAnimation constructor() pass the animationGrid to constructor
        ///     and initializes animation cubes.
        /// </summary>
        private void InitializeAnimationField()
        {
            // pass play grid link to Game
            _mainAnimation = MainAnimation.Create(_animationGrid);
        }
    }
}
