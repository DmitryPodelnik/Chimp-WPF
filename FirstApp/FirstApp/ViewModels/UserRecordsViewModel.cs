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

        private ObservableCollection<Record> _records = new();
        public ObservableCollection<Record> Records
        {
            get => _records;
        }
        public UserRecordsViewModel()
        {

        }
    }
}
