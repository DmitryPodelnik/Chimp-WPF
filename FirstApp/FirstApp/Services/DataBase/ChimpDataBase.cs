using FirstApp.Models.DataBase;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using System.Security.Cryptography;
using System.Windows;
using First_App.Models.RegistryData;
using FirstApp.Models;
using First_App.Models.DataBase.Models;
using First_App.Views.Windows;

namespace First_App.Models.DataBase
{
    /// <summary>
    ///     Class of database interaction.
    /// </summary>
    public class ChimpDataBase
    {
        private SHA256Managed sha256 = new SHA256Managed();
        private ChimpDbContext _context;
        public ChimpDbContext Context
        {
            get => _context;
            set => _context = value;
        }

        public string ConnectionString { get; set; }
        public DbContextOptions<ChimpDbContext> Options { get; set; }

        /// <summary>
        ///     ChimpDataBase constructor()
        ///     Get connection string from the appsettings.json.
        ///     Create EF context object and point out connect string and
        ///     get option object for EF context object constructor.
        /// </summary>
        public ChimpDataBase()
        {
            try
            {
                var configuration =
                     new ConfigurationBuilder()
                         .SetBasePath(Directory.GetCurrentDirectory())
                         .AddJsonFile("appsettings.json")
                         .Build();

                // Get connection string from the appsettings.json
                ConnectionString = configuration.GetConnectionString("DefaultConnection");

                // Create EF context object and point out connect string and
                // get option object for EF context object constructor
                var options =
                    new DbContextOptionsBuilder<ChimpDbContext>()
                        .UseSqlServer(ConnectionString)
                        .Options;
                Options = options;

                _context = new ChimpDbContext(options);
            }
            catch (UnauthorizedAccessException ex)
            {
                MessageBoxWindow.Create().ShowMessageBox(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (NotSupportedException ex)
            {
                MessageBoxWindow.Create().ShowMessageBox(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (NullReferenceException ex)
            {
                MessageBoxWindow.Create().ShowMessageBox(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        /// <summary>
        ///     Verify whether login and password are correct and exist and match in database.
        /// </summary>
        /// <param name="login">User login.</param>
        /// <param name="password">User password.</param>
        /// <returns>True if login and password are correct or false.</returns>
        public bool IsAuthorized(string login, string password)
        {
            try
            {
                var user = _context.Users.FirstOrDefault(
                     u => u.Username == login &&
                     u.Password == Convert.ToBase64String(sha256.ComputeHash(Encoding.UTF8.GetBytes(password))));

                // if correct
                if (user != null)
                {
                    user.IsOnline = true;
                    _context.Users.Update(user);
                    _context.SaveChanges();

                    return true;
                }
                // if incorrect
                return false;
            }
            catch (ArgumentNullException ex)
            {
                MessageBoxWindow.Create().ShowMessageBox(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            catch (ArgumentOutOfRangeException ex)
            {
                MessageBoxWindow.Create().ShowMessageBox(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            catch (ObjectDisposedException ex)
            {
                MessageBoxWindow.Create().ShowMessageBox(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            catch (EncoderFallbackException ex)
            {
                MessageBoxWindow.Create().ShowMessageBox(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            catch (NullReferenceException ex)
            {
                MessageBoxWindow.Create().ShowMessageBox(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
        }

        /// <summary>
        ///     Get user by login from database.
        /// </summary>
        /// <param name="login">User login.</param>
        /// <returns>User if exists or null.</returns>
        public User GetUser(string login)
        {
            try
            {
                if (string.IsNullOrEmpty(login))
                {
                    return null;
                }
                return _context.Users
                    .Include(u => u.Profile)
                    .Include(u => u.Records)
                    .FirstOrDefault(u => u.Username == login);
            }
            catch (ArgumentNullException ex)
            {
                MessageBoxWindow.Create().ShowMessageBox(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return null;
            }
            catch (NullReferenceException ex)
            {
                MessageBoxWindow.Create().ShowMessageBox(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return null;
            }
        }

        /// <summary>
        ///     Save new user data to database.
        /// </summary>
        /// <param name="previousLogin">Previous login.</param>
        /// <param name="newLogin">New login.</param>
        /// <param name="password">New password.</param>
        /// <param name="confirmPassword">Confirm new password.</param>
        /// <returns>True if saved or false.</returns>
        public bool SaveNewData(string previousLogin, string newLogin, string password, string confirmPassword)
        {
            if (string.IsNullOrEmpty(previousLogin))
            {
                MessageBoxWindow.Create().ShowMessageBox("Inccorect values!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            if (string.IsNullOrEmpty(newLogin) &&
                (string.IsNullOrEmpty(password) || string.IsNullOrEmpty(confirmPassword)))
            {
                MessageBoxWindow.Create().ShowMessageBox("Inccorect values!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            if (password != confirmPassword)
            {
                MessageBoxWindow.Create().ShowMessageBox("Passwords are not equal!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            try
            {
                var user = _context.Users.FirstOrDefault(u => u.Username == previousLogin);
                if (user == null)
                {
                    MessageBoxWindow.Create().ShowMessageBox("User is not found!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return false;
                }
                if (!string.IsNullOrEmpty(newLogin))
                {
                    var existedUser = _context.Users.FirstOrDefault(u => u.Username == newLogin);
                    if (existedUser == null)
                    {
                        user.Username = newLogin;
                    }
                }
                // if password and confirm password are not null or empty - set new password
                if (!(string.IsNullOrEmpty(password) || string.IsNullOrEmpty(confirmPassword)))
                {
                    user.Password = Convert.ToBase64String(sha256.ComputeHash(Encoding.UTF8.GetBytes(password)));
                }
                _context.Users.Update(user);
                _context.SaveChanges();

                SavingRegistryData registry = new();

                // if new login is not null or empty
                if (!string.IsNullOrEmpty(newLogin))
                {
                    registry.SaveUserData(newLogin, password);
                }
                else // or
                {
                    registry.SaveUserData(previousLogin, password);
                }
                return true;
            }
            catch (ArgumentNullException ex)
            {
                MessageBoxWindow.Create().ShowMessageBox(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            catch (DbUpdateConcurrencyException ex)
            {
                MessageBoxWindow.Create().ShowMessageBox(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            catch (DbUpdateException ex)
            {
                MessageBoxWindow.Create().ShowMessageBox(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            catch (NullReferenceException ex)
            {
                MessageBoxWindow.Create().ShowMessageBox(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
        }

        /// <summary>
        ///     Get all records from database.
        /// </summary>
        /// <returns>IList of Records if exist at least one instance or null.</returns>
        public IList<Record> GetAllRecords()
        {
            try
            {
                return _context.Records
                    .Include(r => r.User)
                    .ThenInclude(u => u.Profile)
                    .ToList();
            }
            catch (ArgumentNullException ex)
            {
                MessageBoxWindow.Create().ShowMessageBox(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return null;
            }
            catch (NullReferenceException ex)
            {
                MessageBoxWindow.Create().ShowMessageBox(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return null;
            }
        }

        /// <summary>
        ///     Add new user record to database.
        /// </summary>
        /// <param name="record">New user game record.</param>
        public void AddRecord(Record record)
        {
            try
            {
                _context.Records.Add(record);
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                MessageBoxWindow.Create().ShowMessageBox(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (DbUpdateException ex)
            {
                MessageBoxWindow.Create().ShowMessageBox(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (NullReferenceException ex)
            {
                MessageBoxWindow.Create().ShowMessageBox(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        /// <summary>
        ///     Update current user data in the database: game count, max score and average score.
        /// </summary>
        public bool UpdateCurrentUserData()
        {
            try
            {
                var user = GetUser(SavingRegistryData.GetCurrentUser());
                if (user is not null)
                {
                    user.Profile.GameCount++;
                    if (user.Records.Count != 0)
                    {
                        user.Profile.MaxScore = GetCurrentUserRecords().Max(u => u.Score);
                        user.Profile.AverageScore = GetCurrentUserRecords().Average(u => u.Score);
                    }
                    if (user.Profile.AverageScore != 0 && user.Profile.GameCount != 0)
                    {
                        user.Profile.Rate = Math.Round((user.Profile.AverageScore / user.Profile.GameCount) * 10, 1);
                    }
                    _context.Users.Update(user);
                    _context.SaveChanges();

                    return true;
                }
                return false;
            }
            catch (NullReferenceException ex)
            {
                MessageBoxWindow.Create().ShowMessageBox(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
        }

        /// <summary>
        ///     Update last seen of current user into database.
        ///     Sets current user IsOnline value to false;
        /// </summary>
        /// <returns></returns>
        public bool UpdateLastSeenTime()
        {
            try
            {
                var user = GetUser(SavingRegistryData.GetCurrentUser());
                if (user is not null)
                {
                    user.Profile.LastSeen = DateTime.Now;
                    user.IsOnline = false;
                    _context.Users.Update(user);
                    _context.SaveChanges();

                    return true;
                }
                return false;
            }
            catch (NullReferenceException ex)
            {
                MessageBoxWindow.Create().ShowMessageBox(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
        }

        /// <summary>
        ///     Get all current user records from database.
        /// </summary>
        /// <returns>IList of Records if exists or null</returns>
        public IList<Record> GetCurrentUserRecords()
        {
            try
            {
                return _context.Records
                .Include(r => r.User)
                .ThenInclude(u => u.Profile)
                .Where(u => u.User.Username == SavingRegistryData.GetCurrentUser(false))
                .OrderByDescending(r => r.Date)
                .ToList();
            }
            catch (ArgumentNullException ex)
            {
                MessageBoxWindow.Create().ShowMessageBox(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return null;
            }
            catch (NullReferenceException ex)
            {
                MessageBoxWindow.Create().ShowMessageBox(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return null;
            }
        }
    }
}
