using First_App.Models.DataBase;
using FirstApp.Models.DataBase;
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
    public class ChimpViewModel : INotifyPropertyChanged
    {
        private ChimpDataBase _database = new();
        private Chimp _chimpWindow = (Chimp)Application.Current.MainWindow;

        public ChimpViewModel()
        {

        }



        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
