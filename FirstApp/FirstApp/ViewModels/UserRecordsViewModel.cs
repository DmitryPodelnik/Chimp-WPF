using First_App.Models.Commands;
using First_App.Models.DataBase;
using First_App.Models.DataBase.Models;
using First_App.Services.Comparers;
using First_App.Views.Windows;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace First_App.ViewModels
{
    /// <summary>
    ///     Class of user records view model.
    /// </summary>
    class UserRecordsViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }

        // field to work with database
        private ChimpDataBase _database = new();
        // field that contains user records
        private List<Record> _records = new();
        public List<Record> Records
        {
            get => _records;
        }

        // field that stores last seen message
        private string _lastSeenMessage { get; set; }
        public string LastSeenMessage
        {
            get => _lastSeenMessage;
            set => _lastSeenMessage = value;
        }

        /// <summary>
        ///     UserRecordsViewModel constructor().
        ///     Get all records from the database.
        /// </summary>
        public UserRecordsViewModel()
        {
            try
            {
                // gets from database records from all users and orders them by descending then casts to list
                _records = _database.GetAllRecords()
                    .OrderByDescending(r => r.Score)
                    .Distinct(new RecordsComparer())
                    .OrderByDescending(r => r.User.Profile.Rate)
                    .ToList();
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
        ///     Command after clicking username button.
        /// </summary>
        private RelayCommand _showUserProfileCommand;
        public RelayCommand ShowUserProfileCommand
        {
            get
            {
                return _showUserProfileCommand ??=
                new RelayCommand(obj =>
                {
                    MessageBox.Show("Hello");
                    SelectedUserProfile userProfile = new();
                    userProfile.ShowDialog();
                });
            }
        }

    }
}
