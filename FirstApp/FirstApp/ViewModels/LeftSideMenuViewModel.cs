using First_App.Models;
using First_App.Models.Commands;
using First_App.Models.DataBase;
using First_App.Models.DataBase.Models;
using First_App.Models.Game;
using First_App.Models.RegistryData;
using First_App.Navigation;
using First_App.Services.Authentication;
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
    ///     Class of left side menu view model.
    /// </summary>
    class LeftSideMenuViewModel : INotifyPropertyChanged
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
        ///     Command after clicking main tab(Chimp) button.
        /// </summary>
        private RelayCommand _returnToMainTabCommand;
        public RelayCommand ReturnToMainTabCommand
        {
            get
            {
                return _returnToMainTabCommand ??=
                new RelayCommand(obj =>
                {
                    // if game is started then
                    if (Game.IsGameStarted)
                    {
                        // are you sure to finish current game?
                        if (IsSureToFinishGame())
                        {
                            // save current game record to database
                            SaveNewRecord();
                            // show start main tab
                            ShowMainTab();
                            // assign to Game.IsGameStarted - false
                            Game.IsGameStarted = false;
                        }
                        return;
                    }
                    // show start main tab
                    ShowMainTab();
                });
            }
        }

        /// <summary>
        ///     Hide panels excepting main tab.
        /// </summary>
        private void ShowMainTab()
        {
            // change to main tab
            _nav.CurrentViewModel = new MainTabViewModel();
        }

        /// <summary>
        ///     Command after clicking play button.
        /// </summary>
        private RelayCommand _playCommand;
        public RelayCommand PlayCommand
        {
            get
            {
                return _playCommand ??=
                new RelayCommand(obj =>
                {
                    // if game is started then
                    if (Game.IsGameStarted)
                    {
                        // are you sure to finish current game?
                        if (IsSureToFinishGame())
                        {
                            // save current game record to database
                            SaveNewRecord();
                            // show start game tab
                            StartPlay();
                            // assign to Game.IsGameStarted - false
                            Game.IsGameStarted = false;
                        }
                        return;
                    }
                    // show start game tab
                    StartPlay();
                });
            }
        }

        /// <summary>
        ///     Change user contol to Start Game tab.
        /// </summary>
        private void StartPlay()
        {
            // change to start game tab
            _nav.CurrentViewModel = new StartGameViewModel();
        }

        /// <summary>
        ///     Command after clicking profile button.
        /// </summary>
        private RelayCommand _showProfileCommand;
        public RelayCommand ShowProfileCommand
        {
            get
            {
                return _showProfileCommand ??=
                new RelayCommand(obj =>
                {
                    // if game is started then
                    if (Game.IsGameStarted)
                    {
                        // are you sure to finish current game?
                        if (IsSureToFinishGame())
                        {
                            // save current game record to database
                            SaveNewRecord();
                            // show user profile tab
                            ShowProfile();
                            // assign to Game.IsGameStarted - false
                            Game.IsGameStarted = false;
                        }
                        return;
                    }
                    // show user profile tab
                    ShowProfile();
                });
            }
        }

        /// <summary>
        ///     Change user contol to Profile tab.
        /// </summary>
        private void ShowProfile()
        {
            _nav.CurrentViewModel = new UserProfileViewModel();
        }

        /// <summary>
        ///     Command after clicking records button.
        /// </summary>
        private RelayCommand _showRecordsCommand;
        public RelayCommand ShowRecordsCommand
        {
            get
            {
                return _showRecordsCommand ??=
                new RelayCommand(obj =>
                {
                    // if game is started then
                    if (Game.IsGameStarted)
                    {
                        // are you sure to finish current game?
                        if (IsSureToFinishGame())
                        {
                            // save current game record to database
                            SaveNewRecord();
                            // show user records tab
                            ShowRecords();
                            // assign to Game.IsGameStarted - false
                            Game.IsGameStarted = false;
                        }
                        return;
                    }
                    // show user records tab
                    ShowRecords();
                });
            }
        }

        /// <summary>
        ///     Change user contol to Records tab.
        /// </summary>
        private void ShowRecords()
        {
            if (_nav.CurrentViewModel is not UserRecordsViewModel)
            {
                _nav.CurrentViewModel = new UserRecordsViewModel();
            }
        }

        /// <summary>
        ///     Command after clicking exit button or
        ///     any left aside button while game is being.
        /// </summary>
        private RelayCommand _exitCommand;
        public RelayCommand ExitCommand
        {
            get
            {
                return _exitCommand ??=
                new RelayCommand(obj =>
                {
                    // if game is started then
                    if (Game.IsGameStarted)
                    {
                        // are you sure to finish current game?
                        if (IsSureToFinishGame())
                        {
                            // if yes then is not current user null
                            if (SavingRegistryData.GetCurrentUser() is not null)
                            {
                                // save current game record to database
                                SaveNewRecord();
                                ExitFromAccount();
                                // assign to Game.IsGameStarted - false
                                Game.IsGameStarted = false;

                                return;
                            }
                            ExitGame();
                        }
                        return;
                    }
                    if (SavingRegistryData.GetCurrentUser() is not null)
                    {
                        ExitFromAccount();
                        // assign to Game.IsGameStarted - false
                        Game.IsGameStarted = false;

                        return;
                    }
                    ExitGame();
                });
            }
        }

        /// <summary>
        ///     Saves a new record into database.
        /// </summary>
        private void SaveNewRecord()
        {
            Record newRecord = new();
            newRecord.Date = DateTime.Now.ToString();
            newRecord.UserId = _database.GetUser(SavingRegistryData.GetCurrentUser()).Id;
            newRecord.Score = Counter.Score;
            Counter.Score = 4;

            _database.AddRecord(newRecord);
            // update current user data in the database: game count, max score and average score
            _database.UpdateCurrentUserData();
        }

        /// <summary>
        ///     Closes game if you are sure.
        /// </summary>
        private void ExitGame()
        {
            var res = MessageBox.Show("Are you sure to exit the game?", "Exit", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (res == MessageBoxResult.Yes)
            {
                // close chimp window
                ((Chimp)Application.Current.MainWindow).Close();
            }
        }

        /// <summary>
        ///     Hides panels excepting authorization tab.
        ///     Exits from account and forward to authorization tab.
        /// </summary>
        private void ExitFromAccount()
        {
            var res = MessageBox.Show("Are you sure to exit from the account?", "Exit", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (res == MessageBoxResult.Yes)
            {
                // update current user data in the database: game count, max score and average score
                _database.UpdateLastSeenTime();
                SavingRegistryData registry = new();
                // remove user data from registry
                registry.RemoveUserData();
                // reset current user in authenticator
                Authenticator.Create().CurrentUser = null;
                // show authorization panel
                _nav.CurrentViewModel = new AuthorizationViewModel();
            }
        }

        /// <summary>
        ///     If user wants to finish current game.
        /// </summary>
        /// <returns>True if yes or false.</returns>
        private bool IsSureToFinishGame()
        {
            MessageBoxResult res =
                MessageBox.Show("Are you sure to finish the game and save current result?",
                                "Warning",
                                MessageBoxButton.YesNo,
                                MessageBoxImage.Warning
                );
            if (res == MessageBoxResult.Yes)
            {
                return true;
            }
            return false;
        }
    }
}
