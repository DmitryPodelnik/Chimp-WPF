using First_App.Models.Commands;
using First_App.Models.Game;
using First_App.Navigation;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace First_App.ViewModels
{
    class FinishGameTableViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }

        public FinishGameTableViewModel()
        {
            _scoreMessage = Counter.Score.ToString();
        }

        private string _scoreMessage;
        public string ScoreMessage
        {
            get => _scoreMessage;
            set
            {
                _scoreMessage = value;
            }
        }

        /// <summary>
        ///     Command after clicking save score button.
        /// </summary>
        private RelayCommand _saveScoreCommand;
        public RelayCommand SaveScoreCommand
        {
            get
            {
                return _saveScoreCommand =
                (_saveScoreCommand = new RelayCommand(obj =>
                {
                    Navigator.Create().CurrentViewModel = new UserRecordsViewModel();
                }));
            }
        }

        /// <summary>
        ///     Command after clicking try again button.
        /// </summary>
        private RelayCommand _tryAgainCommand;
        public RelayCommand TryAgainCommand
        {
            get
            {
                return _tryAgainCommand =
                (_tryAgainCommand = new RelayCommand(obj =>
                {
                    Navigator.Create().CurrentViewModel = new StartGameViewModel();
                }));
            }
        }
    }
}
