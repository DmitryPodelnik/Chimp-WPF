using First_App.Models.DataBase;
using First_App.Models.RegistryData;
using First_App.Navigation;
using First_App.ViewModels;
using First_App.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace First_App.Services.Authentication
{
    /// <summary>
    ///     Class of authentication to user.
    /// </summary>
    public class Authenticator : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }

        // singleton instance of Navigator
        private Navigator _nav = Navigator.Create();
        // field to work with database
        private ChimpDataBase _database = new();
        // singleton instance of Authenticator
        private static Authenticator _instance;
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
                OnPropertyChanged(nameof(CurrentUser));
                OnPropertyChanged(nameof(IsLoggedIn));
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
        /// <param name="login">User login.</param>
        /// <param name="password">User password.</param>
        /// <returns>True if correct or false.</returns>
        public bool CheckAuthorization(string login, string password)
        {
            // if login or password box is null or empty then error message
            if (String.IsNullOrEmpty(login) || String.IsNullOrEmpty(password))
            {
                MessageBox.Show("Incorrect login or password!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            // verify whether login and password are correct and exists and compares in the database
            bool result = _database.IsAuthorized(login, password);
            // if correct then save user data in the registry
            if (result)
            {
                SavingRegistryData registry = new();
                registry.SaveUserData(login, password);

                // show success message box and welcome message and show game user interface
                LoginSuccessActions(login);
            }
            else // or show error message
            {
                MessageBox.Show("Incorrect login or password!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            // clear password box field
            password = "";

            return true;
        }

        /// <summary>
        ///     Show success message box and welcome message and show game user interface.
        /// </summary>
        private void LoginSuccessActions(string login)
        {
            // get current user from registry
            CurrentUser = SavingRegistryData.GetCurrentUser();
            // show game user interface
            _nav.CurrentViewModel = new UserProfileViewModel();
        }
    }
}
