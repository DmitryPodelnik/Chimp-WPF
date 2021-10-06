using First_App.Models.Commands;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace First_App.Navigation
{
    class Navigator : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }

        private object _currentViewModel;
        public object CurrentViewModel
        {
            get => _currentViewModel;
            set
            {
                _currentViewModel = value;
                OnPropertyChanged("CurrentViewModel");
            }
        }
        public RelayCommand UpdateCurrentViewModelCommand { get; }

        private static Navigator _navigator;
        public static Navigator Create()
        {
            if (_navigator == null)
            {
                _navigator = new();
            }
            return _navigator;
        }

        private Navigator()
        {
            UpdateCurrentViewModelCommand = new RelayCommand(o => CurrentViewModel = o);
        }
    }
}
