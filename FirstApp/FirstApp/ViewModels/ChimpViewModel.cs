using FirstApp.Models.DataBase;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace First_App.ViewModels
{
    public class ChimpViewModel : INotifyPropertyChanged
    {
        private ChimpDbContext _database;

        public ChimpViewModel()
        {

        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
