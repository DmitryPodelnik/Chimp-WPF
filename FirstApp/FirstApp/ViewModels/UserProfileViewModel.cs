using First_App.Models.Commands;
using First_App.Models.DataBase;
using First_App.Models.DataBase.Models;
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
        public string NewLogin { get; set; }
        public string NewPassword { get; set; }
        public string ConfirmNewPassword { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }

        // field that contains user records
        private List<Record> _records = new();
        public List<Record> Records
        {
            get => _records;
        }

        private string _currentUserMessage { get; set; }
        public string CurrentUserMessage
        {
            get => _currentUserMessage;
            set
            {
                _currentUserMessage = value;
            }
        }

        /// <summary>
        ///     UserProfileViewModel constructor().
        ///     Get current user from the registry.
        /// </summary>
        public UserProfileViewModel()
        {
            try
            {
                // Get current user login from registry and get all user data from the database
                var user = _database.GetUser(SavingRegistryData.GetCurrentUser());
                if (user is null)
                {
                    MessageBox.Show("User is not found", "Error");
                    return;
                }
                // welcome message in the profile
                _currentUserMessage = "Hello, " + SavingRegistryData.GetCurrentUser() + "!";

                var bestRecord = _database.GetCurrentUserRecords().OrderByDescending(r => r.Score).FirstOrDefault();
                // score message in the profile
                _currentUserScoreMessage = $"Your best score is {bestRecord?.Score}";
                _records = (List<Record>)_database.GetCurrentUserRecords();
            }
            catch (ArgumentNullException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        // field that stores current user best score message
        private string _currentUserScoreMessage { get; set; }
        public string CurrentUserScoreMessage
        {
            get => _currentUserScoreMessage;
            set
            {
                _currentUserScoreMessage = value;
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
        ///     Save new user data into registry and database.
        /// </summary>
        private void SaveProfile()
        {
            // save new data into database
            bool res = _database.SaveNewData(
                     SavingRegistryData.GetCurrentUser(),
                     NewLogin,
                     NewPassword,
                     ConfirmNewPassword
                 );

            // if ok then show success message and welcome message
            if (res == true)
            {
                MessageBox.Show("You have been successfully changed the user data", "Saving User Data", MessageBoxButton.OK);
                // welcome message in the profile
                _currentUserMessage = $"Hello, {SavingRegistryData.GetCurrentUser()}!";
                // update CurrentUserMessage property
                OnPropertyChanged("CurrentUserMessage");
            }
            // clear fields of new user data
            NewLogin = "";
            NewPassword = "";
            ConfirmNewPassword = "";

            return;
        }
    }
}
