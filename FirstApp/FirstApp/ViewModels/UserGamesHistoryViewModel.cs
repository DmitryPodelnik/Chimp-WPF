using First_App.Models.Commands;
using First_App.Models.DataBase;
using First_App.Models.DataBase.Models;
using First_App.Navigation;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace First_App.ViewModels
{
    public class UserGamesHistoryViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }

        private Navigator _nav = Navigator.Create();
        // field to work with database
        private ChimpDataBase _database = new();
        // field that contains user records
        private List<Record> _records = new();
        public List<Record> Records
        {
            get => _records;
        }

        /// <summary>
        /// 
        /// </summary>
        public UserGamesHistoryViewModel()
        {
            try
            {
                _records = (List<Record>)_database.GetCurrentUserRecords();
            }
            catch (ArgumentNullException ex)
            {
                MessageBox.Show(ex.Message, "Eror", MessageBoxButton.OK, MessageBoxImage.Error);
            }
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
