using First_App.Models.Commands;
using First_App.Models.DataBase;
using First_App.Models.DataBase.Models;
using First_App.Models.Game;
using First_App.Models.RegistryData;
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
        public void OnPropertyChanged(string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }

        // field to work with database
        private ChimpDataBase _database = new();

        public FinishGameTableViewModel()
        {
            _scoreMessage = Counter.Score.ToString();
        }

        // field that shows finish score
        private string _scoreMessage;
        public string ScoreMessage
        {
            get => _scoreMessage;
            set => _scoreMessage = value;
        }

        /// <summary>
        ///     Command after clicking save score button.
        /// </summary>
        private RelayCommand _saveScoreCommand;
        public RelayCommand SaveScoreCommand
        {
            get
            {
                return _saveScoreCommand ??=
                new RelayCommand(obj =>
                {
                    // save current game record to database
                    SaveNewRecord();
                    // assign to Game.IsGameStarted - false
                    Game.IsGameStarted = false;
                    // change to user records tab
                    Navigator.Create().CurrentViewModel = new UserRecordsViewModel();
                });
            }
        }

        /// <summary>
        ///     Saves new record to database.
        /// </summary>
        private void SaveNewRecord()
        {
            Record newRecord = new();
            newRecord.Date = DateTime.Now.ToString();
            newRecord.UserId = _database.GetUser(SavingRegistryData.GetCurrentUser()).Id;
            newRecord.Score = Game.lastScore;

            _database.AddRecord(newRecord);
            _database.UpdateCurrentUserData();
        }

        /// <summary>
        ///     Command after clicking try again button.
        /// </summary>
        private RelayCommand _tryAgainCommand;
        public RelayCommand TryAgainCommand
        {
            get
            {
                return _tryAgainCommand ??=
                new RelayCommand(obj =>
                {
                    // change to start game tab
                    Navigator.Create().CurrentViewModel = new StartGameViewModel();
                    // assign to Game.IsGameStarted - false
                    Game.IsGameStarted = false;
                });
            }
        }
    }
}
