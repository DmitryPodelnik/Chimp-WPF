using First_App.Models.Commands;
using First_App.Models.DataBase;
using First_App.Models.RegistryData;
using First_App.Views;
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
    ///     Class of user profile view model.
    /// </summary>
    public class UserProfileViewModel : INotifyPropertyChanged
    {
        // field of main window
        private Chimp _chimpWindow = (Chimp)Application.Current.MainWindow;
        // field to work with database
        private ChimpDataBase _database = new();

        private UserProfile _userProfileUserControl = new();

        public string NewLogin { get; set; }
        public string NewPassword { get; set; }
        public string ConfirmNewPassword { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
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
        ///     Hide panels excepting profile tab.
        /// </summary>
        private void ShowProfile()
        {
            // show welcome message to user
            _userProfileUserControl.accountNameTextBlock.Text = $"Hello, {SavingRegistryData.GetCurrentUser()}!";

            // Get current user login from registry and get all user data from the database
            var user = _database.GetUser(SavingRegistryData.GetCurrentUser());
            if (user is null)
            {
                MessageBox.Show("User is not found", "Error");
            }
            // show score message in the profile
            _userProfileUserControl.scoreText.Text = $"Your best score is {user?.Score}";
        }

        /// <summary>
        ///     Save new user data into registry and database.
        /// </summary>
        private void SaveProfile()
        {
            // save new data into database
            bool res = _database.SaveNewData(
                     SavingRegistryData.GetCurrentUser(),
                     _userProfileUserControl.accountLoginTextBox.Text,
                     _userProfileUserControl.accountPasswordBox.Password,
                     _userProfileUserControl.accountPasswordBoxConfirm.Password
                 );

            // if ok then show success message and welcome message
            if (res == true)
            {
                MessageBox.Show("You have been successfully changed the user data", "Saving User Data", MessageBoxButton.OK);
                _userProfileUserControl.accountNameTextBlock.Text = $"Hello, {SavingRegistryData.GetCurrentUser()}!";
            }
            // clear fields of new user data
            _userProfileUserControl.accountLoginTextBox.Text = "";
            _userProfileUserControl.accountPasswordBox.Password = "";
            _userProfileUserControl.accountPasswordBoxConfirm.Password = "";

            return;
        }
    }
}
