﻿using First_App.Models.Commands;
using First_App.Models.DataBase;
using First_App.Models.DataBase.Models;
using First_App.Models.RegistryData;
using First_App.Navigation;
using First_App.Views;
using First_App.Views.Windows;
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
        /// </summary>
        /// <param name="username">Username</param>
        public UserProfileViewModel(string username)
        {
            InitializeUserProfile(username);
        }

        /// <summary>
        ///     UserProfileViewModel constructor().
        ///     Gets current user from the registry.
        /// </summary>
        public UserProfileViewModel()
        {
            InitializeUserProfile();
        }

        /// <summary>
        ///     Gets current user from the registry and fill user profile data.
        /// </summary>
        /// <param name="username"></param>
        private void InitializeUserProfile(string username = null)
        {
            try
            {
                User user;
                // Gets current user login from registry and gets all user data from the database
                if (username == null)
                {
                    user = _database.GetUser(SavingRegistryData.GetCurrentUser());
                    // welcome message in the profile
                    _currentUserMessage = SavingRegistryData.GetCurrentUser();
                }
                else
                {
                    user = _database.GetUser(username);
                    // welcome message in the profile
                    _currentUserMessage = username;
                }
                if (user is null)
                {
                    MessageBoxWindow.Create().ShowMessageBox("User is not found", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

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
                    if (user.IsOnline == false)
                    {
                        _lastSeenMessage = CalculateLastSeenMessage(user);
                    }
                    else
                    {
                        _lastSeenMessage = "User is online";
                    }
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
                MessageBoxWindow.Create().ShowMessageBox(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (NullReferenceException ex)
            {
                MessageBoxWindow.Create().ShowMessageBox(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (Exception ex)
            {
                MessageBoxWindow.Create().ShowMessageBox(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        /// <summary>
        ///     Calculates timespan from the last seen of user.
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        private string CalculateLastSeenMessage(User user)
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
                MessageBoxWindow.Create().ShowMessageBox(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (NullReferenceException ex)
            {
                MessageBoxWindow.Create().ShowMessageBox(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (Exception ex)
            {
                MessageBoxWindow.Create().ShowMessageBox(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            return result;
        }

        /// <summary>
        ///     Command after clicking games history button.
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
        ///     Command after clicking edit profile button.
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
