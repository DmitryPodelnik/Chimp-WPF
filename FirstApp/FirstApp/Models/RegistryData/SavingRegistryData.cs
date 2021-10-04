using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace First_App.Models.RegistryData
{
    public class SavingRegistryData
    {
        public void SaveUserData (string login, string password)
        {
            try
            {
                using RegistryKey currentUserKey = Registry.CurrentUser;
                using RegistryKey authKey = currentUserKey.CreateSubKey("ChimpAuthData");
                authKey.SetValue("login", login);
                authKey.SetValue("password", password);
            }
            catch (ArgumentNullException ex)
            {
                MessageBox.Show(ex.Message);
            }
            catch (IOException ex)
            {
                MessageBox.Show(ex.Message);
            }
            catch (ObjectDisposedException ex)
            {
                MessageBox.Show(ex.Message);
            }
            catch (SecurityException ex)
            {
                MessageBox.Show(ex.Message);
            }
            catch (UnauthorizedAccessException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void RemoveUserData ()
        {
            try
            {
                using RegistryKey currentUserKey = Registry.CurrentUser;
                using RegistryKey authKey = currentUserKey.OpenSubKey("ChimpAuthData", true);
                // value deleting from the key
                authKey.DeleteValue("login");
                authKey.DeleteValue("password");
                // key deleting
                currentUserKey.DeleteSubKey("ChimpAuthData");
            }
            catch (ArgumentNullException ex)
            {
                MessageBox.Show(ex.Message);
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show(ex.Message);
            }
            catch (ObjectDisposedException ex)
            {
                MessageBox.Show(ex.Message);
            }
            catch (SecurityException ex)
            {
                MessageBox.Show(ex.Message);
            }
            catch (UnauthorizedAccessException ex)
            {
                MessageBox.Show(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public bool IsExistsKey (string key)
        {
            try
            {
                using RegistryKey currentUserKey = Registry.CurrentUser;
                using RegistryKey authKey = currentUserKey.OpenSubKey(key);

                if (authKey != null)
                {
                    return true;
                }
                return false;
            }
            catch (ArgumentNullException ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
            catch (ObjectDisposedException ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
            catch (SecurityException ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }
    }
}
