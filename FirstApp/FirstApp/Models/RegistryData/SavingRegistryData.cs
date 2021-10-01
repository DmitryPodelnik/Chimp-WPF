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
        private void SaveUserData(string login, string password)
        {
            RegistryKey currentUserKey = Registry.CurrentUser;
            RegistryKey helloKey = currentUserKey.CreateSubKey("ChimpAuthData");
            helloKey.SetValue("login", login);
            helloKey.SetValue("password", password);
            helloKey.Close();
        }
    }
}
