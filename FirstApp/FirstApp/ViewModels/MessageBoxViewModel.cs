using First_App.Models.Commands;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace First_App.ViewModels
{
    class MessageBoxViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }

        public string Title { get; set; }
        public string Message { get; set; }

        /// <summary>
        ///     Command after clicking yes button.
        /// </summary>
        private RelayCommand _yesCommand;
        public RelayCommand YesCommand
        {
            get
            {
                return _yesCommand ??=
                new RelayCommand(obj =>
                {
                    Application.Current.MainWindow.OwnedWindows[0].Close();
                });
            }
        }

        /// <summary>
        ///     Command after clicking no button.
        /// </summary>
        private RelayCommand _noCommand;
        public RelayCommand NoCommand
        {
            get
            {
                return _noCommand ??=
                new RelayCommand(obj =>
                {
                    Application.Current.MainWindow.OwnedWindows[0].Close();
                });
            }
        }

        /// <summary>
        ///     Command after clicking OK button.
        /// </summary>
        private RelayCommand _OKCommand;
        public RelayCommand OKCommand
        {
            get
            {
                return _OKCommand ??=
                new RelayCommand(obj =>
                {
                    Application.Current.MainWindow.OwnedWindows[0].Close();
                });
            }
        }
    }
}
