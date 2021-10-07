using First_App.Models.RegistryData;
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
        private Chimp _chimpWindow = (Chimp)Application.Current.MainWindow;

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
    }
}
