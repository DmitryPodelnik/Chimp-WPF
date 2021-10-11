using First_App.Models.Commands;
using First_App.Models.Game;
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
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }

        private Navigator _nav = Navigator.Create();
        private StartGame _gameFieldUserControl = new();

        /// <summary>
        ///     Command after clicking start game button.
        /// </summary>
        private RelayCommand _startGameCommand;
        public RelayCommand StartGameCommand
        {
            get
            {
                return _startGameCommand =
                (_startGameCommand = new RelayCommand(obj =>
                {
                    _nav.CurrentViewModel = new PlayFieldViewModel();
                    Game.IsGameStarted = true;
                    OnPropertyChanged("Game.IsGameStarted");
                }));
            }
        }
    }
}
