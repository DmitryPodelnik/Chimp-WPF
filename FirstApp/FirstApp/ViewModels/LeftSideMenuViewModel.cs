using First_App.Models;
using First_App.Models.Commands;
using First_App.Models.DataBase;
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
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }

        private Navigator _nav = Navigator.Create();
        // field to work with database
        private ChimpDataBase _database = new();
        // field of main window
        private Chimp _chimpWindow = (Chimp)Application.Current.MainWindow;

        /// <summary>
        ///     Command after clicking main tab(Chimp) button.
        /// </summary>
        private RelayCommand _returnToMainTabCommand;
        public RelayCommand ReturnToMainTabCommand
        {
            get
            {
                return _returnToMainTabCommand =
                (_returnToMainTabCommand = new RelayCommand(obj =>
                {
                    if (Game.IsGameStarted)
                    {
                        if (IsSureToFinishGame())
                        {
                            ShowMainTab();
                            Game.IsGameStarted = false;
                            OnPropertyChanged("Game.IsGameStarted");
                        }
                        return;
                    }
                    ShowMainTab();
                }));
            }
        }

        /// <summary>
        ///     Hide panels excepting main tab.
        /// </summary>
        private void ShowMainTab()
        {
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
                return _playCommand =
                (_playCommand = new RelayCommand(obj =>
                {
                    if (Game.IsGameStarted)
                    {
                        if (IsSureToFinishGame())
                        {
                            StartPlay();
                            Game.IsGameStarted = false;
                            OnPropertyChanged("Game.IsGameStarted");
                        }
                        return;
                    }
                    StartPlay();
                }));
            }
        }

        /// <summary>
        ///     Hide panels excepting play tab.
        /// </summary>
        private void StartPlay()
        {
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
                return _showProfileCommand =
                (_showProfileCommand = new RelayCommand(obj =>
                {
                    if (Game.IsGameStarted)
                    {
                        if (IsSureToFinishGame())
                        {
                            ShowProfile();
                            Game.IsGameStarted = false;
                            OnPropertyChanged("Game.IsGameStarted");
                        }
                        return;
                    }
                    ShowProfile();
                }));
            }
        }

        /// <summary>
        ///     Hide panels excepting profile tab.
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
                return _showRecordsCommand =
                (_showRecordsCommand = new RelayCommand(obj =>
                {
                    if (Game.IsGameStarted)
                    {
                        if (IsSureToFinishGame())
                        {
                            ShowRecords();
                            Game.IsGameStarted = false;
                            OnPropertyChanged("Game.IsGameStarted");
                        }
                        return;
                    }
                    ShowRecords();
                }));
            }
        }

        /// <summary>
        ///     Hide panels excepting play tab.
        /// </summary>
        private void ShowRecords()
        {
            _nav.CurrentViewModel = new UserRecordsViewModel();
        }

        /// <summary>
        ///     Command after clicking exit button.
        /// </summary>
        private RelayCommand _exitCommand;
        public RelayCommand ExitCommand
        {
            get
            {
                return _exitCommand =
                (_exitCommand = new RelayCommand(obj =>
                {
                    if (Game.IsGameStarted)
                    {
                        if (IsSureToFinishGame())
                        {
                            if (SavingRegistryData.GetCurrentUser() is not null)
                            {
                                ExitFromAccount();
                                Game.IsGameStarted = false;
                                OnPropertyChanged("Game.IsGameStarted");
                                return;
                            }
                            ExitGame();
                        }
                        return;
                    }
                    if (SavingRegistryData.GetCurrentUser() is not null)
                    {
                        ExitFromAccount();
                        Game.IsGameStarted = false;
                        OnPropertyChanged("Game.IsGameStarted");
                        return;
                    }
                    ExitGame();
                }));
            }
        }

        /// <summary>
        ///     Close game if you sure.
        /// </summary>
        private void ExitGame()
        {
            var res = MessageBox.Show("Are you sure to exit the game?", "Exit", MessageBoxButton.YesNo);

            if (res == MessageBoxResult.Yes)
            {
                _chimpWindow.Close();
            }
        }

        /// <summary>
        ///     Hide panels excepting authorization tab.
        ///     Exit from account and forward to authorization tab.
        /// </summary>
        private void ExitFromAccount()
        {
            var res = MessageBox.Show("Are you sure to exit from the account?", "Exit", MessageBoxButton.YesNo);

            if (res == MessageBoxResult.Yes)
            {
                SavingRegistryData registry = new();
                registry.RemoveUserData();
                Authenticator.Create().CurrentUser = null;

                _nav.CurrentViewModel = new AuthorizationViewModel();
            }
        }

        private bool IsSureToFinishGame()
        {
            MessageBoxResult res = MessageBox.Show(
                    "Are you sure to finish the game and save current result?",
                    "Warning",
                    MessageBoxButton.YesNo
                );
            if (res == MessageBoxResult.Yes)
            {
                return true;
            }
            return false;
        }
    }
}
