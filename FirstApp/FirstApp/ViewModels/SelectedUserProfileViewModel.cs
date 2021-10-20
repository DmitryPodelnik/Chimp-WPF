using First_App.Models.DataBase;
using First_App.Models.RegistryData;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace First_App.ViewModels
{
    class SelectedUserProfileViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }

        // field to work with database
        private ChimpDataBase _database = new();

        // field that stores user name message
        private string _currentUserMessage { get; set; }
        public string CurrentUserMessage
        {
            get => _currentUserMessage;
            set => _currentUserMessage = value;
        }
        // field that stores last seen message
        private string _lastSeenMessage { get; set; }
        public string LastSeenMessage
        {
            get => _lastSeenMessage;
            set => _lastSeenMessage = value;
        }
        // field that stores register date message
        private string _wasRegisteredMessage { get; set; }
        public string WasRegisteredMessage
        {
            get => _wasRegisteredMessage;
            set => _wasRegisteredMessage = value;
        }
        // field that stores max score message
        private string _currentUserMaxScoreMessage { get; set; }
        public string CurrentUserMaxScoreMessage
        {
            get => _currentUserMaxScoreMessage;
            set => _currentUserMaxScoreMessage = value;
        }
        // field that stores average score message
        private string _currentUserAverageScoreMessage { get; set; }
        public string CurrentUserAverageScoreMessage
        {
            get => _currentUserAverageScoreMessage;
            set => _currentUserAverageScoreMessage = value;
        }
        // field that stores game count message
        private string _currentUserGameCountMessage { get; set; }
        public string CurrentUserGameCountMessage
        {
            get => _currentUserGameCountMessage;
            set => _currentUserGameCountMessage = value;
        }
        // field that stores rate message
        private string _currentUserRateMessage { get; set; }
        public string CurrentUserRateMessage
        {
            get => _currentUserRateMessage;
            set => _currentUserRateMessage = value;
        }

        /// <summary>
        ///     UserProfileViewModel constructor().
        ///     Gets current user from the registry.
        /// </summary>
        public SelectedUserProfileViewModel()
        {
            try
            {
                // Gets current user login from registry and gets all user data from the database
                var user = _database.GetUser(SavingRegistryData.GetCurrentUser());
                if (user is null)
                {
                    MessageBox.Show("User is not found", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                // welcome message in the profile
                _currentUserMessage = SavingRegistryData.GetCurrentUser();

                if (user.Profile is not null)
                {
                    // max score message at the profile
                    _currentUserMaxScoreMessage = $"Best score: {user.Profile.MaxScore}";
                    // average score message at the profile
                    _currentUserAverageScoreMessage = $"Average score: {Math.Round(user.Profile.AverageScore, 1)}";
                    // game count message at the profile
                    _currentUserGameCountMessage = $"Game count: {user.Profile.GameCount}";
                    // register date message at the profile
                    _wasRegisteredMessage = $"Registered: {user.Profile.RegisterDate}";
                    // user rate message
                    _currentUserRateMessage = $"Rate: {user.Profile.Rate}";
                    // last seen message
                    // _lastSeenMessage = CalculateLastSeenMessage(user);
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
            }
            catch (ArgumentNullException ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
