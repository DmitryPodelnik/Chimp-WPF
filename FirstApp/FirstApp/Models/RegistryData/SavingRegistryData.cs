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
            RegistryKey currentUserKey = Registry.CurrentUser;
            RegistryKey authKey = currentUserKey.CreateSubKey("ChimpAuthData");
            authKey.SetValue("login", login);
            authKey.SetValue("password", password);
            authKey.Close();
        }

        public bool IsExistsKey(string key)
        {
            RegistryKey currentUserKey = Registry.CurrentUser;
            RegistryKey authKey = currentUserKey.OpenSubKey(key);
            authKey.Close();

            if (authKey != null)
            {
                return true;
            }
            return false;
        }
    }
}
