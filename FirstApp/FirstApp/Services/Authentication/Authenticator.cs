using First_App.Models.RegistryData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace First_App.Services.Authentication
{
    /// <summary>
    ///     Class of authentication to user.
    /// </summary>
    class Authenticator
    {
        private static Authenticator _instance = null;
        public static Authenticator Create()
        {
            if (_instance == null)
            {
                _instance = new Authenticator();
            }
            return _instance;
        }
        private string _currentUser { get; set; }
        public string CurrentUser
        {
            get => _currentUser;
            set
            {
                _currentUser = value;
            }
        }

        /// <summary>
        ///     Authenticatior constructor().
        ///     Get current user from the registry.
        /// </summary>
        protected Authenticator()
        {
            CurrentUser = SavingRegistryData.GetCurrentUser();
        }
        public bool IsLoggedIn => CurrentUser != null;
    }
}
