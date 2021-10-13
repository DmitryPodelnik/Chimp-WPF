using First_App.Models.DataBase;
using First_App.Models.DataBase.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace First_App.ViewModels
{
    /// <summary>
    ///     Class of user records view model.
    /// </summary>
    class UserRecordsViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
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

        public UserRecordsViewModel()
        {
            // get from database records from all users ordering them by descending and cast to list
            _records = _database.GetAllRecords().OrderByDescending(r => r.Date).ToList();
        }
    }
}
