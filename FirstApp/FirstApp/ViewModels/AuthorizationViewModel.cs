﻿using First_App.Models.Commands;
using First_App.Models.DataBase;
using First_App.Services.Authentication;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace First_App.ViewModels
{
    /// <summary>
    ///     Class of authorization view model.
    /// </summary>
    class AuthorizationViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
        private Authenticator _authenticator = Authenticator.Create();
        public string Login { get; set; }
        public string Password { get; set; }

        /// <summary>
        ///     Command after clicking login button.
        /// </summary>
        private RelayCommand _loginCommand;
        public RelayCommand LoginCommand
        {
            get
            {
                return _loginCommand ??=
                new RelayCommand(obj =>
                {
                    // verify whether the data of user are correct
                    bool result = _authenticator.CheckAuthorization(Login, Password);
                });
            }
        }
    }
}
