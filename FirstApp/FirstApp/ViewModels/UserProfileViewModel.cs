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

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }

        // field to work with database
        private ChimpDataBase _database = new();
        public string NewLogin { get; set; }
        public string NewPassword { get; set; }
        public string ConfirmNewPassword { get; set; }
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
            set => _currentUserMessage = value;
        }


        // field that stores current user best score message
        private string _currentUserMaxScoreMessage { get; set; }
        public string CurrentUserMaxScoreMessage
        {
            get => _currentUserMaxScoreMessage;
            set => _currentUserMaxScoreMessage = value;
        }
        // field that stores current user best score message
        private string _currentUserAverageScoreMessage { get; set; }
        public string CurrentUserAverageScoreMessage
        {
            get => _currentUserAverageScoreMessage;
            set => _currentUserAverageScoreMessage = value;
        }
        // field that stores current user best score message
        private string _currentUserGameCountMessage { get; set; }
        public string CurrentUserGameCountMessage
        {
            get => _currentUserGameCountMessage;
            set => _currentUserGameCountMessage = value;
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
                    MessageBox.Show("User is not found", "Eror", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                // welcome message in the profile
                _currentUserMessage = "Hello, " + SavingRegistryData.GetCurrentUser() + "!";

                //var bestRecord = _database.GetCurrentUserRecords().OrderByDescending(r => r.Score).FirstOrDefault();
                //// score message in the profile
                //if (bestRecord is not null)
                //{
                //    _currentUserMaxScoreMessage = $"Your best score is {bestRecord.Score}";
                //}
                //else
                //{
                //    _currentUserMaxScoreMessage = $"Your best score is 0";
                //}

                var userProfile = _database.GetUser(SavingRegistryData.GetCurrentUser());
                if (userProfile.Profile is not null)
                {
                    // max score message in the profile
                    _currentUserMaxScoreMessage = $"Best score: {userProfile.Profile.MaxScore}";
                    // average score message in the profile
                    _currentUserAverageScoreMessage = $"Average score: {userProfile.Profile.AverageScore}";
                    // game count message in the profile
                    _currentUserGameCountMessage = $"Game count: {userProfile.Profile.GameCount}";
                }
                else
                {
                    // max score message in the profile
                    _currentUserMaxScoreMessage = $"Best score: 0";
                    // average score message in the profile
                    _currentUserAverageScoreMessage = $"Average score: 0";
                    // game count message in the profile
                    _currentUserGameCountMessage = $"Game count: 0";
                }
                _records = (List<Record>)_database.GetCurrentUserRecords();
            }
            catch (ArgumentNullException ex)
            {
                MessageBox.Show(ex.Message, "Eror", MessageBoxButton.OK, MessageBoxImage.Error);
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
                return _saveProfileCommand ??=
                new RelayCommand(obj =>
                {
                    // save new profile data in database and registry
                    SaveProfile();
                });
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
                MessageBox.Show("You have been successfully changed the user data", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
                // welcome message in the profile
                _currentUserMessage = $"Hello, {SavingRegistryData.GetCurrentUser()}!";
                // update CurrentUserMessage property
                OnPropertyChanged(nameof(CurrentUserMessage));
            }
            // clear fields of new user data
            NewLogin = "";
            NewPassword = "";
            ConfirmNewPassword = "";

            return;
        }
    }
}
