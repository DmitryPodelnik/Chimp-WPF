using First_App.Views.Windows;
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
    /// <summary>
    ///     Class of saving user data in the registry.
    /// </summary>
    public class SavingRegistryData
    {
        /// <summary>
        ///     Save user data in the registry.
        /// </summary>
        /// <param name="login">User login.</param>
        /// <param name="password">User password.</param>
        public void SaveUserData(string login, string password)
        {
            try
            {
                using RegistryKey currentUserKey = Registry.CurrentUser;
                using RegistryKey authKey = currentUserKey.CreateSubKey("ChimpAuthData");

                if (login is not null)
                {
                    authKey.SetValue("login", login);
                }
                if (password is not null)
                {
                    authKey.SetValue("password", password);
                }
            }
            catch (ArgumentNullException ex)
            {
                MessageBoxWindow.Create().ShowMessageBox(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (IOException ex)
            {
                MessageBoxWindow.Create().ShowMessageBox(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (ObjectDisposedException ex)
            {
                MessageBoxWindow.Create().ShowMessageBox(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (SecurityException ex)
            {
                MessageBoxWindow.Create().ShowMessageBox(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (UnauthorizedAccessException ex)
            {
                MessageBoxWindow.Create().ShowMessageBox(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        /// <summary>
        ///     Remove user data from the registry.
        /// </summary>
        public void RemoveUserData()
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
                MessageBoxWindow.Create().ShowMessageBox(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (ArgumentException ex)
            {
                MessageBoxWindow.Create().ShowMessageBox(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (ObjectDisposedException ex)
            {
                MessageBoxWindow.Create().ShowMessageBox(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (SecurityException ex)
            {
                MessageBoxWindow.Create().ShowMessageBox(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (UnauthorizedAccessException ex)
            {
                MessageBoxWindow.Create().ShowMessageBox(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (InvalidOperationException ex)
            {
                MessageBoxWindow.Create().ShowMessageBox(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (NullReferenceException ex)
            {
                MessageBoxWindow.Create().ShowMessageBox(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (Exception ex)
            {
                MessageBoxWindow.Create().ShowMessageBox(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        /// <summary>
        ///     Verify whether key exists in the registry.
        /// </summary>
        /// <param name="key">Registry key.</param>
        /// <returns>True if exists or false.</returns>
        public bool IsExistsKey(string key)
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
                MessageBoxWindow.Create().ShowMessageBox(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            catch (ObjectDisposedException ex)
            {
                MessageBoxWindow.Create().ShowMessageBox(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            catch (SecurityException ex)
            {
                MessageBoxWindow.Create().ShowMessageBox(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            catch (NullReferenceException ex)
            {
                MessageBoxWindow.Create().ShowMessageBox(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            catch (Exception ex)
            {
                MessageBoxWindow.Create().ShowMessageBox(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
        }

        /// <summary>
        ///     Get current user login from the registry.
        /// </summary>
        /// <returns>Login if exists or null.</returns>
        public static string GetCurrentUser(bool password = false)
        {
            try
            {
                using RegistryKey currentUserKey = Registry.CurrentUser;
                using RegistryKey authKey = currentUserKey.OpenSubKey("ChimpAuthData");
                if (password)
                {
                    return authKey?.GetValue("login", null).ToString() + ";" + authKey?.GetValue("password", null).ToString();
                }
                return authKey?.GetValue("login", null).ToString();
            }
            catch (ArgumentNullException ex)
            {
                MessageBoxWindow.Create().ShowMessageBox(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return null;
            }
            catch (ArgumentException ex)
            {
                MessageBoxWindow.Create().ShowMessageBox(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return null;
            }
            catch (ObjectDisposedException ex)
            {
                MessageBoxWindow.Create().ShowMessageBox(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return null;
            }
            catch (SecurityException ex)
            {
                MessageBoxWindow.Create().ShowMessageBox(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return null;
            }
            catch (UnauthorizedAccessException ex)
            {
                MessageBoxWindow.Create().ShowMessageBox(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return null;
            }
            catch (IOException ex)
            {
                MessageBoxWindow.Create().ShowMessageBox(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return null;
            }
            catch (NullReferenceException ex)
            {
                MessageBoxWindow.Create().ShowMessageBox(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return null;
            }
            catch (Exception ex)
            {
                MessageBoxWindow.Create().ShowMessageBox(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return null;
            }
        }
    }
}
