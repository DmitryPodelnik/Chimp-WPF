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

                var userProfile = _database.GetUser(SavingRegistryData.GetCurrentUser());
                if (userProfile.Profile is not null)
                {
                    // max score message in the profile
                    _currentUserMaxScoreMessage = $"Best score: {userProfile.Profile.MaxScore}";
                    // average score message in the profile
                    _currentUserAverageScoreMessage = $"Average score: {Math.Round(userProfile.Profile.AverageScore, 1)}";
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
            }
            catch (ArgumentNullException ex)
            {
                MessageBox.Show(ex.Message, "Eror", MessageBoxButton.OK, MessageBoxImage.Error);
            }
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
