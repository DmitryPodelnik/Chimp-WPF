using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace First_App.Models.RegistryData
{
    public class SavingRegistryData
    {
        public void SaveUserData (string login, string password)
        {
            using RegistryKey currentUserKey = Registry.CurrentUser;
            using RegistryKey authKey = currentUserKey.CreateSubKey("ChimpAuthData");
            authKey.SetValue("login", login);
            authKey.SetValue("password", password);
        }

        public bool IsExistsKey(string key)
        {
            using RegistryKey currentUserKey = Registry.CurrentUser;
            using RegistryKey authKey = currentUserKey.OpenSubKey(key);

            if (authKey != null)
            {
                return true;
            }
            return false;
        }
    }
}
