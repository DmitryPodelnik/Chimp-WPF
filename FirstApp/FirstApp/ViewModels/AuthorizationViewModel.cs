using First_App.Models.Commands;
using First_App.Models.DataBase;
using First_App.Services.Authentication;
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
    /// <summary>
    ///     Class of authorization view model.
    /// </summary>
    class AuthorizationViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }

        // field of main window
        private Chimp _chimpWindow = (Chimp)Application.Current.MainWindow;

        private Authenticator _authenticator = Authenticator.Create();

        /// <summary>
        ///     Command after clicking login button.
        /// </summary>
        private RelayCommand _loginCommand;
        public RelayCommand LoginCommand
        {
            get
            {
                return _loginCommand =
                (_loginCommand = new RelayCommand(obj =>
                {
                    // verify whether the data of user are correct
                    _authenticator.CheckAuthorization();
                }));
            }
        }
    }
}
