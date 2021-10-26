using First_App.Models;
using First_App.Models.Commands;
using First_App.Models.DataBase;
using First_App.Models.DataBase.Models;
using First_App.Models.Game;
using First_App.Models.RegistryData;
using First_App.Navigation;
using First_App.Services.Authentication;
using First_App.Views;
using First_App.Views.Windows;
using FirstApp.Models.DataBase;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Automation.Peers;
using System.Windows.Automation.Provider;
using System.Windows.Controls;

namespace First_App.ViewModels
{
    /// <summary>
    ///     Class of chimp view model.
    /// </summary>
    public class ChimpViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }

        public Navigator Navigator { get; }
        // field to work with database
        private ChimpDataBase _database = new();
        public Authenticator Authenticator { get; }

        /// <summary>
        ///     ChimpViewModel constructor().
        ///     Checks if user is logged in.
        /// </summary>
        public ChimpViewModel()
        {
            Navigator = Navigator.Create();
            Authenticator = Authenticator.Create();

            SavingRegistryData registry = new();
            if (registry.IsExistsKey("ChimpAuthData"))
            {
                string[] loginPassword = SavingRegistryData.GetCurrentUser(true).Split(";");
                _database.IsAuthorized(loginPassword[0], loginPassword[1]);
                // change to user profile tab
                Navigator.CurrentViewModel = new UserProfileViewModel();
            }
            else
            {
                // change to authorization tab
                Navigator.CurrentViewModel = new AuthorizationViewModel();
            }
        }

        /// <summary>
        ///     Event occurs while chimp window is closing.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void OnWindowClosing(object sender, CancelEventArgs e)
        {
            // if game is started then
            if (Game.IsGameStarted)
            {
                // are you sure to finish current game?
                if (IsSureToFinishGame())
                {
                    // if yes then is not current user null
                    if (SavingRegistryData.GetCurrentUser() is not null)
                    {
                        // save current game record to database
                        SaveNewRecord();
                        // assign to Game.IsGameStarted - false
                        Game.IsGameStarted = false;
                    }
                    ExitGame(e);

                    return;
                }
                // cancel closing chimp window
                e.Cancel = true;
                return;
            }
            ExitGame(e);
        }

        /// <summary>
        ///     Saves a new record into database.
        /// </summary>
        private void SaveNewRecord()
        {
            Record newRecord = new();
            newRecord.Date = DateTime.Now.ToString();
            newRecord.UserId = _database.GetUser(SavingRegistryData.GetCurrentUser()).Id;
            newRecord.Score = Counter.Score;
            Counter.Score = 4;

            _database.AddRecord(newRecord);
            // update current user data in the database: game count, max score and average score
            _database.UpdateCurrentUserData();
        }

        /// <summary>
        ///     Closes game if you are sure.
        /// </summary>
        private void ExitGame(CancelEventArgs e)
        {
            //MessageBoxWindow.Create().ShowMessageBox("Are you sure to exit the game?", "Exit", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            var res = MessageBox.Show("Are you sure to exit the game?", "Exit", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (res == MessageBoxResult.No)
            {
                // cancel closing chimp window
                e.Cancel = true;

                if (Navigator.CurrentViewModel is PlayFieldViewModel)
                {
                    // change to user profile tab
                    Navigator.CurrentViewModel = new UserProfileViewModel();
                }
            }
            else
            {
                ExitFromAccount();
            }
        }

        /// <summary>
        ///     Hides panels excepting authorization tab.
        ///     Exits from account and forward to authorization tab.
        /// </summary>
        private void ExitFromAccount()
        {
            // update current user data in the database: game count, max score and average score
            _database.UpdateLastSeenTime();
        }

        /// <summary>
        ///     If user wants to finish current game.
        /// </summary>
        /// <returns>True if yes or false.</returns>
        private bool IsSureToFinishGame()
        {
            MessageBoxResult res =
                MessageBox.Show("Are you sure to finish the game and save current result?",
                                "Warning",
                                MessageBoxButton.YesNo,
                                MessageBoxImage.Warning
                );
            if (res == MessageBoxResult.Yes)
            {
                return true;
            }
            return false;
        }
    }
}
