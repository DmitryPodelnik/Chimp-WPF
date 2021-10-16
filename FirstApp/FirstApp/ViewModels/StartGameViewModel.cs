using First_App.Models.Commands;
using First_App.Models.DataBase;
using First_App.Models.Game;
using First_App.Models.RegistryData;
using First_App.Navigation;
using First_App.Views;
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
    ///     Class of game field view model.
    /// </summary>
    class StartGameViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }

        private Navigator _nav = Navigator.Create();
        // field to work with database
        private ChimpDataBase _database = new();

        /// <summary>
        ///     Command after clicking start game button.
        /// </summary>
        private RelayCommand _startGameCommand;
        public RelayCommand StartGameCommand
        {
            get
            {
                return _startGameCommand ??=
                new RelayCommand(obj =>
                {
                    _database.IncreaseCurrentUserGameScore();
                    // change to play field interface
                    _nav.CurrentViewModel = new PlayFieldViewModel();
                    // assign to Game.IsGameStarted - true
                    Game.IsGameStarted = true;
                });
            }
        }
    }
}
