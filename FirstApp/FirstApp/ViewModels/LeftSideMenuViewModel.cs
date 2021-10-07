using First_App.Models;
using First_App.Models.Commands;
using First_App.Models.DataBase;
using First_App.Models.RegistryData;
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
                    SelectedAction.CurrentSelectedAction = Actions.MainTab;
                    ShowMainTab();
                }));
            }
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
                    SelectedAction.CurrentSelectedAction = Actions.Play;
                    StartPlay();
                }));
            }
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
                    SelectedAction.CurrentSelectedAction = Actions.ShowProfile;
                    ShowProfile();
                }));
            }
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
                    SelectedAction.CurrentSelectedAction = Actions.ShowRecords;
                    ShowRecords();
                }));
            }
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
                    SelectedAction.CurrentSelectedAction = Actions.ShowAuthorization;

                    if (SavingRegistryData.GetCurrentUser() is not null)
                    {
                        ExitFromAccount();
                        return;
                    }
                    ExitGame();
                }));
            }
        }

        /// <summary>
        ///     Hide panels excepting main tab.
        /// </summary>
        private void ShowMainTab()
        {
            _chimpWindow.accountNameTextBlock.Text = "";
            _chimpWindow.mainText.Visibility = Visibility.Visible;
            _chimpWindow.playGrid.Visibility = Visibility.Hidden;
            _chimpWindow.accountPanel.Visibility = Visibility.Hidden;
            _chimpWindow.authorizationPanel.Visibility = Visibility.Hidden;
            _chimpWindow.recordsGrid.Visibility = Visibility.Hidden;
        }

        /// <summary>
        ///     Hide panels excepting play tab.
        /// </summary>
        private void StartPlay()
        {
            // Hide panels excepting play tab
            PrepareInterfaceToPlay();
        }

        /// <summary>
        ///     Hide panels excepting profile tab.
        /// </summary>
        private void ShowProfile()
        {
            // show welcome message to user
            _chimpWindow.accountNameTextBlock.Text = $"Hello, {SavingRegistryData.GetCurrentUser()}!";

            _chimpWindow.accountPanel.Visibility = Visibility.Visible;
            _chimpWindow.playGrid.Visibility = Visibility.Hidden;
            _chimpWindow.mainText.Visibility = Visibility.Hidden;
            _chimpWindow.authorizationPanel.Visibility = Visibility.Hidden;
            _chimpWindow.recordsGrid.Visibility = Visibility.Hidden;

            // Get current user login from registry and get all user data from the database
            var user = _database.GetUser(SavingRegistryData.GetCurrentUser());
            if (user is null)
            {
                MessageBox.Show("User is not found", "Error");
            }
            // show score message in the profile
            _chimpWindow.scoreText.Text = $"Your best score is {user?.Score}";
        }

        /// <summary>
        ///     Hide panels excepting records tab.
        /// </summary>
        private void ShowRecords()
        {
            _chimpWindow.accountNameTextBlock.Text = "";
            _chimpWindow.recordsGrid.Visibility = Visibility.Visible;
            _chimpWindow.authorizationPanel.Visibility = Visibility.Hidden;
            _chimpWindow.accountPanel.Visibility = Visibility.Hidden;
            _chimpWindow.playGrid.Visibility = Visibility.Hidden;
            _chimpWindow.mainText.Visibility = Visibility.Hidden;
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

                _chimpWindow.loginTextBox.Text = "";
                _chimpWindow.accountNameTextBlock.Text = "";
                _chimpWindow.authorizationPanel.Visibility = Visibility.Visible;
                _chimpWindow.accountPanel.Visibility = Visibility.Hidden;
                _chimpWindow.playGrid.Visibility = Visibility.Hidden;
                _chimpWindow.mainText.Visibility = Visibility.Hidden;
                _chimpWindow.recordsGrid.Visibility = Visibility.Hidden;

                DisableLeftButtonsBeforeAuth();
            }
        }

        /// <summary>
        ///     Hide panels excepting play tab.
        /// </summary>
        private void PrepareInterfaceToPlay()
        {
            _chimpWindow.accountNameTextBlock.Text = "";
            _chimpWindow.playGrid.Visibility = Visibility.Visible;
            _chimpWindow.mainText.Visibility = Visibility.Hidden;
            _chimpWindow.accountPanel.Visibility = Visibility.Hidden;
            _chimpWindow.authorizationPanel.Visibility = Visibility.Hidden;
            _chimpWindow.recordsGrid.Visibility = Visibility.Hidden;
        }

        /// <summary>
        ///     Disable left buttons.
        /// </summary>
        private void DisableLeftButtonsBeforeAuth()
        {
            _chimpWindow.recordsButton.IsEnabled = false;
            _chimpWindow.profileButton.IsEnabled = false;
            _chimpWindow.playButton.IsEnabled = false;
            _chimpWindow.mainTabButton.IsEnabled = false;
        }
    }
}
