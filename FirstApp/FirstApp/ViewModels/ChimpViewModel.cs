using First_App.Models;
using First_App.Models.Commands;
using First_App.Models.DataBase;
using First_App.Models.Game;
using First_App.Models.RegistryData;
using FirstApp.Models.DataBase;
using System;
using System.Collections.Generic;
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
    ///     Class of chimp view model.
    /// </summary>
    public class ChimpViewModel : INotifyPropertyChanged
    {
        // field to work with database
        private ChimpDataBase _database = new();
        // field of main window
        private Chimp _chimpWindow = (Chimp)Application.Current.MainWindow;
        // field of game play
        private Game _game;

        public ChimpViewModel()
        {

        }

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
        ///     Command after clicking login button.
        /// </summary>
        private RelayCommand _loginCommand;
        public RelayCommand LoginCommand
        {
            get
            {
                return _loginCommand =
                (_loginCommand = new RelayCommand(obj =>
                {
                    // verify whether the data of user are correct
                    CheckAuthorization();
                }));
            }
        }

        /// <summary>
        ///     Command after clicking save profile button.
        /// </summary>
        private RelayCommand _saveProfileCommand;
        public RelayCommand SaveProfileCommand
        {
            get
            {
                return _saveProfileCommand =
                (_saveProfileCommand = new RelayCommand(obj =>
                {
                    // save new profile data in database and registry
                    SaveProfile();
                }));
            }
        }

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
                    _chimpWindow.startGamePanel.Visibility = Visibility.Hidden;
                    //  call Game constructor() and initialize game cubes
                    InitializeGameField();

                    _game.StartGame();
                }));
            }
        }

        /// <summary>
        ///     Verify whether the data of user are correct.
        /// </summary>
        private void CheckAuthorization()
        {
            // if login or password box is null or empty then error message
            if (String.IsNullOrEmpty(_chimpWindow.loginTextBox.Text) ||
                String.IsNullOrEmpty(_chimpWindow.passwordBox.Password))
            {
                MessageBox.Show("Incorrect login or password!", "Error", MessageBoxButton.OK);

                return;
            }

            // verify whether login and password are correct and exists and compares in the database
            bool result = _database.IsAuthorized(
                _chimpWindow.loginTextBox.Text,
                _chimpWindow.passwordBox.Password
                );

            // if correct then save user data in the registry
            if (result)
            {
                SavingRegistryData registry = new();
                registry.SaveUserData(
                    _chimpWindow.loginTextBox.Text,
                    _chimpWindow.passwordBox.Password
                    );

                // show success message box and welcome message and hide authorization panel
                LoginSuccessActions();
                EnableLeftButtonBeforeAuth();
            }
            else // or show error message
            {
                MessageBox.Show("Incorrect login or password!", "Error", MessageBoxButton.OK);
            }
            // clear password box field
            _chimpWindow.passwordBox.Password = "";
        }

        /// <summary>
        ///     Show success message box and welcome message and hide authorization panel.
        /// </summary>
        private void LoginSuccessActions()
        {
            MessageBox.Show("You have been successfully logged in!", "Authorization", MessageBoxButton.OK);
            _chimpWindow.accountNameTextBlock.Text = $"Hello, {_chimpWindow.loginTextBox.Text}!";
            _chimpWindow.authorizationPanel.Visibility = Visibility.Hidden;
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
        private void StartPlay ()
        {
            // Hide panels excepting play tab
            PrepareInterfaceToPlay();
        }

        private void InitializeGameField()
        {
            _game = new();
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
        ///     Hide panels excepting profile tab.
        /// </summary>
        private void ShowProfile ()
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
        private void ShowRecords ()
        {
            _chimpWindow.accountNameTextBlock.Text = "";
            _chimpWindow.recordsGrid.Visibility = Visibility.Visible;
            _chimpWindow.authorizationPanel.Visibility = Visibility.Hidden;
            _chimpWindow.accountPanel.Visibility = Visibility.Hidden;
            _chimpWindow.playGrid.Visibility = Visibility.Hidden;
            _chimpWindow.mainText.Visibility = Visibility.Hidden;
        }

        /// <summary>
        ///     Hide panels excepting authorization tab.
        ///     Exit from account and forward to authorization tab.
        /// </summary>
        private void ExitFromAccount ()
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
        ///     Close game if you sure.
        /// </summary>
        private void ExitGame ()
        {
            var res = MessageBox.Show("Are you sure to exit the game?", "Exit", MessageBoxButton.YesNo);

            if (res == MessageBoxResult.Yes)
            {
                _chimpWindow.Close();
            }
        }

        /// <summary>
        ///     Disable left buttons.
        /// </summary>
        private void DisableLeftButtonsBeforeAuth ()
        {
            //_chimpWindow.recordsButton.IsEnabled = false;
            //_chimpWindow.profileButton.IsEnabled = false;
            //_chimpWindow.playButton.IsEnabled = false;
            //_chimpWindow.mainTabButton.IsEnabled = false;
        }

        /// <summary>
        ///     Enable left buttons.
        /// </summary>
        private void EnableLeftButtonBeforeAuth ()
        {
            //_chimpWindow.recordsButton.IsEnabled = true;
            //_chimpWindow.profileButton.IsEnabled = true;
            //_chimpWindow.playButton.IsEnabled = true;
            //_chimpWindow.mainTabButton.IsEnabled = true;
        }

        /// <summary>
        ///     Save new user data into registry and database.
        /// </summary>
        private void SaveProfile ()
        {
           // save new data into database
           bool res = _database.SaveNewData(
                    SavingRegistryData.GetCurrentUser(),
                    _chimpWindow.accountLoginTextBox.Text,
                    _chimpWindow.accountPasswordBox.Password,
                    _chimpWindow.accountPasswordBoxConfirm.Password
                );

            // if ok then show success message and welcome message
            if (res == true)
            {
                MessageBox.Show("You have been successfully changed the user data", "Saving User Data", MessageBoxButton.OK);
                _chimpWindow.accountNameTextBlock.Text = $"Hello, {SavingRegistryData.GetCurrentUser()}!";
            }
            // clear fields of new user data
            _chimpWindow.accountLoginTextBox.Text = "";
            _chimpWindow.accountPasswordBox.Password = "";
            _chimpWindow.accountPasswordBoxConfirm.Password = "";

            return;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged ([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
