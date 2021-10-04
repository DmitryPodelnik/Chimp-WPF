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
                    ShowLoginTab();
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
            }
            else
            {
                MessageBox.Show("Incorrect login or password!", "Error", MessageBoxButton.OK);
            }
        }

        private void LoginSuccessActions()
        {
            MessageBox.Show("You have been successfully logged in!", "Authorization", MessageBoxButton.OK);
            _chimpWindow.authorizationPanel.Visibility = Visibility.Hidden;
        }

        private void ShowLoginTab()
        {

        }

        private void StartPlay()
        {

        }

        private void ShowProfile()
        {

        }

        private void ShowRecords()
        {

        }

        private void ExitGame()
        {
            _chimpWindow.authorizationPanel.Visibility = Visibility.Visible;
        }

        private void SaveProfile()
        {

        }


        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
