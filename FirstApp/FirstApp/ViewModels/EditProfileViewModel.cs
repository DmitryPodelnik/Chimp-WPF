using First_App.Models.Commands;
using First_App.Models.DataBase;
using First_App.Models.RegistryData;
using First_App.Navigation;
using First_App.Views.Windows;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace First_App.ViewModels
{
    public class EditProfileViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }

        private Navigator _nav = Navigator.Create();
        // field to work with database
        private ChimpDataBase _database = new();

        public string NewLogin { get; set; }
        public string NewPassword { get; set; }
        public string ConfirmNewPassword { get; set; }

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
        ///     Saves new user data into registry and database.
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
                MessageBoxWindow.Create().ShowMessageBox("You have been successfully changed the user data", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            // clear fields of new user data
            NewLogin = "";
            NewPassword = "";
            ConfirmNewPassword = "";

            return;
        }

        /// <summary>
        ///     Command after clicking back button.
        /// </summary>
        private RelayCommand _backCommand;
        public RelayCommand BackCommand
        {
            get
            {
                return _backCommand ??=
                new RelayCommand(obj =>
                {
                    _nav.CurrentViewModel = new UserProfileViewModel();
                });
            }
        }
    }
}
