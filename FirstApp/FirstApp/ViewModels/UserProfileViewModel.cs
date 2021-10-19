using First_App.Models.Commands;
using First_App.Models.DataBase;
using First_App.Models.DataBase.Models;
using First_App.Models.RegistryData;
using First_App.Navigation;
using First_App.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
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

        private Navigator _nav = Navigator.Create();
        // field to work with database
        private ChimpDataBase _database = new();

        // field that stores
        private string _currentUserMessage { get; set; }
        public string CurrentUserMessage
        {
            get => _currentUserMessage;
            set => _currentUserMessage = value;
        }
        // field that stores
        private string _lastSeenMessage { get; set; }
        public string LastSeenMessage
        {
            get => _lastSeenMessage;
            set => _lastSeenMessage = value;
        }
        // field that stores
        private string _wasRegisteredMessage { get; set; }
        public string WasRegisteredMessage
        {
            get => _wasRegisteredMessage;
            set => _wasRegisteredMessage = value;
        }
        // field that stores
        private string _currentUserMaxScoreMessage { get; set; }
        public string CurrentUserMaxScoreMessage
        {
            get => _currentUserMaxScoreMessage;
            set => _currentUserMaxScoreMessage = value;
        }
        // field that stores
        private string _currentUserAverageScoreMessage { get; set; }
        public string CurrentUserAverageScoreMessage
        {
            get => _currentUserAverageScoreMessage;
            set => _currentUserAverageScoreMessage = value;
        }
        // field that stores
        private string _currentUserGameCountMessage { get; set; }
        public string CurrentUserGameCountMessage
        {
            get => _currentUserGameCountMessage;
            set => _currentUserGameCountMessage = value;
        }
        // field that stores
        private string _currentUserRateMessage { get; set; }
        public string CurrentUserRateMessage
        {
            get => _currentUserRateMessage;
            set => _currentUserRateMessage = value;
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
                    MessageBox.Show("User is not found", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                // welcome message in the profile
                _currentUserMessage = SavingRegistryData.GetCurrentUser();


                // var userProfile = _database.GetUser(SavingRegistryData.GetCurrentUser());
                if (user.Profile is not null)
                {
                    // max score message in the profile
                    _currentUserMaxScoreMessage = $"Best score: {user.Profile.MaxScore}";
                    // average score message in the profile
                    _currentUserAverageScoreMessage = $"Average score: {Math.Round(user.Profile.AverageScore, 1)}";
                    // game count message in the profile
                    _currentUserGameCountMessage = $"Game count: {user.Profile.GameCount}";
                    _wasRegisteredMessage = $"Registered: {user.Profile.RegisterDate}";
                    _currentUserRateMessage = $"Rate: {user.Profile.Rate}";
                    _lastSeenMessage = CalculateLastSeenMessage(user);
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

        /// <summary>
        ///
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        string CalculateLastSeenMessage(User user)
        {
            string result = "Last seen ";
            try
            {

                TimeSpan diff = DateTime.Now - Convert.ToDateTime(user.Profile.LastSeen);

                if (diff.Days > 0)
                {
                    result += diff.Days.ToString() + " days ago";
                }
                else if (diff.Hours > 0)
                {
                    result += diff.Hours.ToString() + " hours ago";
                }
                else if (diff.Minutes > 0)
                {
                    result += diff.Minutes.ToString() + " minutes ago";
                }
                else
                {
                    result += diff.Seconds.ToString() + " seconds ago";
                }
            }
            catch (InvalidCastException ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            return result;
        }

        /// <summary>
        ///
        /// </summary>
        private RelayCommand _gamesHistoryCommand;
        public RelayCommand GamesHistoryCommand
        {
            get
            {
                return _gamesHistoryCommand ??=
                new RelayCommand(obj =>
                {
                    // change to user games history tab
                    _nav.CurrentViewModel = new UserGamesHistoryViewModel();
                });
            }
        }

        /// <summary>
        ///
        /// </summary>
        private RelayCommand _editProfileCommand;
        public RelayCommand EditProfileCommand
        {
            get
            {
                return _editProfileCommand ??=
                new RelayCommand(obj =>
                {
                    // change to user games history tab
                    _nav.CurrentViewModel = new EditProfileViewModel();
                });
            }
        }
    }
}
