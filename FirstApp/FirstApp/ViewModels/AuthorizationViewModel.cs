using First_App.Models.Commands;
using First_App.Models.DataBase;
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
    ///     Class of authorization view model.
    /// </summary>
    class AuthorizationViewModel : INotifyPropertyChanged
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

        private Authenticator _authenticator = new();

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
                    _authenticator.CheckAuthorization();
                }));
            }
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
        ///     Enable left buttons.
        /// </summary>
        private void EnableLeftButtonBeforeAuth()
        {
            //_chimpWindow.recordsButton.IsEnabled = true;
            //_chimpWindow.profileButton.IsEnabled = true;
            //_chimpWindow.playButton.IsEnabled = true;
            //_chimpWindow.mainTabButton.IsEnabled = true;
        }
    }
}
