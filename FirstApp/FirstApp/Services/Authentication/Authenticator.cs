using First_App.Models.DataBase;
using First_App.Models.RegistryData;
using First_App.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace First_App.Services.Authentication
{
    /// <summary>
    ///     Class of authentication to user.
    /// </summary>
    public class Authenticator
    {
        // field of main window
        private Chimp _chimpWindow = (Chimp)Application.Current.MainWindow;

        private Authorization _authUserControl = new();
        private UserProfile _userProfileUserControl = new();

        // field to work with database
        private ChimpDataBase _database = new();
        private static Authenticator _instance = null;
        public static Authenticator Create()
        {
            if (_instance == null)
            {
                _instance = new Authenticator();
            }
            return _instance;
        }
        private string _currentUser { get; set; }
        public string CurrentUser
        {
            get => _currentUser;
            set
            {
                _currentUser = value;
            }
        }

        /// <summary>
        ///     Authenticatior constructor().
        ///     Get current user from the registry.
        /// </summary>
        protected Authenticator()
        {
            CurrentUser = SavingRegistryData.GetCurrentUser();
        }
        public bool IsLoggedIn => CurrentUser != null;

        /// <summary>
        ///     Verify whether the data of user are correct.
        /// </summary>
        public void CheckAuthorization()
        {
            // if login or password box is null or empty then error message
            if (String.IsNullOrEmpty(_authUserControl.loginTextBox.Text) ||
                String.IsNullOrEmpty(_authUserControl.passwordBox.Password))
            {
                MessageBox.Show("Incorrect login or password!", "Error", MessageBoxButton.OK);

                return;
            }

            // verify whether login and password are correct and exists and compares in the database
            bool result = _database.IsAuthorized(
                _authUserControl.loginTextBox.Text,
                _authUserControl.passwordBox.Password
                );

            // if correct then save user data in the registry
            if (result)
            {
                SavingRegistryData registry = new();
                registry.SaveUserData(
                    _authUserControl.loginTextBox.Text,
                    _authUserControl.passwordBox.Password
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
            _authUserControl.passwordBox.Password = "";
        }

        /// <summary>
        ///     Show success message box and welcome message and hide authorization panel.
        /// </summary>
        private void LoginSuccessActions()
        {
            MessageBox.Show("You have been successfully logged in!", "Authorization", MessageBoxButton.OK);
            _userProfileUserControl.accountNameTextBlock.Text = $"Hello, {_authUserControl.loginTextBox.Text}!";
            _authUserControl.authorizationPanel.Visibility = Visibility.Hidden;
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
