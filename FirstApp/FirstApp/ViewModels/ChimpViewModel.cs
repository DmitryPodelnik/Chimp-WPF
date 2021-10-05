using First_App.Models;
using First_App.Models.Commands;
using First_App.Models.DataBase;
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
    public class ChimpViewModel : INotifyPropertyChanged
    {
        private ChimpDataBase _database = new();
        private Chimp _chimpWindow = (Chimp)Application.Current.MainWindow;

        public ChimpViewModel()
        {

        }


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

        private RelayCommand _loginCommand;
        public RelayCommand LoginCommand
        {
            get
            {
                return _loginCommand =
                (_loginCommand = new RelayCommand(obj =>
                {
                    CheckAuthorization();
                }));
            }
        }

        private RelayCommand _saveProfileCommand;
        public RelayCommand SaveProfileCommand
        {
            get
            {
                return _saveProfileCommand =
                (_saveProfileCommand = new RelayCommand(obj =>
                {
                    SaveProfile();
                }));
            }
        }

        private void CheckAuthorization()
        {
            if ((_chimpWindow.loginTextBox.Text == null || _chimpWindow.loginTextBox.Text.Length == 0) 
                || _chimpWindow.passwordBox.Password == null || _chimpWindow.passwordBox.Password.Length == 0)
            {
                MessageBox.Show("Incorrect login or password!", "Error", MessageBoxButton.OK);

                return;
            }

            bool result = _database.IsAuthorized(
                _chimpWindow.loginTextBox.Text,
                _chimpWindow.passwordBox.Password
                );

            if (result)
            {
                SavingRegistryData registry = new();
                registry.SaveUserData(
                    _chimpWindow.loginTextBox.Text,
                    _chimpWindow.passwordBox.Password
                    );

                LoginSuccessActions();
                _chimpWindow.passwordBox.Password = "";
                EnableLeftButtonBeforeAuth();
            }
            else
            {
                MessageBox.Show("Incorrect login or password!", "Error", MessageBoxButton.OK);
                _chimpWindow.passwordBox.Password = "";
            }
        }

        private void LoginSuccessActions()
        {
            MessageBox.Show("You have been successfully logged in!", "Authorization", MessageBoxButton.OK);
            _chimpWindow.accountNameTextBlock.Text = $"Hello, {_chimpWindow.loginTextBox.Text}!";
            _chimpWindow.authorizationPanel.Visibility = Visibility.Hidden;
        }

        private void ShowMainTab()
        {
            _chimpWindow.accountNameTextBlock.Text = "";
            _chimpWindow.mainText.Visibility = Visibility.Visible;
            _chimpWindow.playGrid.Visibility = Visibility.Hidden;
            _chimpWindow.accountPanel.Visibility = Visibility.Hidden;
            _chimpWindow.authorizationPanel.Visibility = Visibility.Hidden;
            _chimpWindow.recordsGrid.Visibility = Visibility.Hidden;
        }

        private void StartPlay ()
        {
            _chimpWindow.accountNameTextBlock.Text = "";
            _chimpWindow.playGrid.Visibility = Visibility.Visible;
            _chimpWindow.mainText.Visibility = Visibility.Hidden;
            _chimpWindow.accountPanel.Visibility = Visibility.Hidden;
            _chimpWindow.authorizationPanel.Visibility = Visibility.Hidden;
            _chimpWindow.recordsGrid.Visibility = Visibility.Hidden;
        }

        private void ShowProfile ()      
        {
            _chimpWindow.accountNameTextBlock.Text = $"Hello, {SavingRegistryData.GetCurrentUser()}!";
            _chimpWindow.accountPanel.Visibility = Visibility.Visible;
            _chimpWindow.playGrid.Visibility = Visibility.Hidden;
            _chimpWindow.mainText.Visibility = Visibility.Hidden;
            _chimpWindow.authorizationPanel.Visibility = Visibility.Hidden;
            _chimpWindow.recordsGrid.Visibility = Visibility.Hidden;

            var user = _database.GetUser(SavingRegistryData.GetCurrentUser());
            if (user is null)
            {
                MessageBox.Show("User is not found", "Error");
            }

            _chimpWindow.scoreText.Text = $"Your best score is {user?.Score}";
        }

        private void ShowRecords ()
        {
            _chimpWindow.accountNameTextBlock.Text = "";
            _chimpWindow.recordsGrid.Visibility = Visibility.Visible;
            _chimpWindow.authorizationPanel.Visibility = Visibility.Hidden;
            _chimpWindow.accountPanel.Visibility = Visibility.Hidden;
            _chimpWindow.playGrid.Visibility = Visibility.Hidden;
            _chimpWindow.mainText.Visibility = Visibility.Hidden;
        }

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

        private void ExitGame ()
        {
            var res = MessageBox.Show("Are you sure to exit the game?", "Exit", MessageBoxButton.YesNo);

            if (res == MessageBoxResult.Yes)
            {
                _chimpWindow.Close();
            }
        }

        private void DisableLeftButtonsBeforeAuth ()
        {
            _chimpWindow.recordsButton.IsEnabled = false;
            _chimpWindow.profileButton.IsEnabled = false;
            _chimpWindow.playButton.IsEnabled = false;
            _chimpWindow.mainTabButton.IsEnabled = false;
        }
        private void EnableLeftButtonBeforeAuth ()
        {
            _chimpWindow.recordsButton.IsEnabled = true;
            _chimpWindow.profileButton.IsEnabled = true;
            _chimpWindow.playButton.IsEnabled = true;
            _chimpWindow.mainTabButton.IsEnabled = true;
        }

        private void SaveProfile ()
        {
           bool res = _database.SaveNewData(
                    SavingRegistryData.GetCurrentUser(),
                    _chimpWindow.accountLoginTextBox.Text,
                    _chimpWindow.accountPasswordBox.Password,
                    _chimpWindow.accountPasswordBoxConfirm.Password
                );
            
            if (res == true)
            {
                MessageBox.Show("You have been successfully changed the user data", "Saving User Data", MessageBoxButton.OK);
                _chimpWindow.accountNameTextBlock.Text = $"Hello, {SavingRegistryData.GetCurrentUser()}!";
            }
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
