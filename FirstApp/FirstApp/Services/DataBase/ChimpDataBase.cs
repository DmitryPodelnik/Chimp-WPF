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
            set
            {
                _context = value;
            }
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
                MessageBox.Show(ex.Message);
            }
            catch (NotSupportedException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        ///     Verify whether login and password are correct and exist and match in database.
        /// </summary>
        /// <param name="login">User login.</param>
        /// <param name="password">User password.</param>
        /// <returns>True if login and password are correct or false.</returns>
        public bool IsAuthorized (string login, string password)
        {
            try
            {
                var result = _context.Users.FirstOrDefault(
                     u => u.Username == login &&
                     u.Password == Convert.ToBase64String(sha256.ComputeHash(Encoding.UTF8.GetBytes(password))));

                // if correct
                if (result != null)
                {
                    return true;
                }
                // if incorrect
                return false;
            }
            catch (ArgumentNullException ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
            catch (ArgumentOutOfRangeException ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
            catch (ObjectDisposedException ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
            catch (EncoderFallbackException ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }

        /// <summary>
        ///     Get user by login from database.
        /// </summary>
        /// <param name="login">User login.</param>
        /// <returns>User if exists or null.</returns>
        public User GetUser (string login)
        {
            try
            {
                if (String.IsNullOrEmpty(login))
                {
                    return null;
                }
                var user = _context.Users.FirstOrDefault(u => u.Username == login);
                return user;
            }
            catch (ArgumentNullException ex)
            {
                MessageBox.Show(ex.Message);
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
        public bool SaveNewData (string previousLogin, string newLogin, string password, string confirmPassword)
        {
            if (String.IsNullOrEmpty(previousLogin))
            {
                MessageBox.Show("Inccorect values!", "Error", MessageBoxButton.OK);
                return false;
            }
            if (String.IsNullOrEmpty(newLogin) &&
                (String.IsNullOrEmpty(password) || String.IsNullOrEmpty(confirmPassword)))
            {
                MessageBox.Show("Inccorect values!", "Error", MessageBoxButton.OK);
                return false;
            }
            if (password != confirmPassword)
            {
                MessageBox.Show("Passwords are not equal!", "Error", MessageBoxButton.OK);
                return false;
            }

            try
            {
                var user = _context.Users.FirstOrDefault(u => u.Username == previousLogin);
                if (user == null)
                {
                    MessageBox.Show("User is not found!", "Error", MessageBoxButton.OK);
                    return false;
                }
                if (!String.IsNullOrEmpty(newLogin))
                {
                    var existedUser = _context.Users.FirstOrDefault(u => u.Username == newLogin);
                    if (existedUser == null)
                    {
                        user.Username = newLogin;
                    }
                }
                // if password and confirm password are not null or empty - set new password
                if (!(String.IsNullOrEmpty(password) || String.IsNullOrEmpty(confirmPassword))) {
                    user.Password = Convert.ToBase64String(sha256.ComputeHash(Encoding.UTF8.GetBytes(password)));
                }
                _context.Users.Update(user);
                _context.SaveChanges();

                SavingRegistryData registry = new();

                // if new login is not null or empty
                if (!String.IsNullOrEmpty(newLogin))
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
                MessageBox.Show(ex.Message);
                return false;
            }
            catch (DbUpdateConcurrencyException ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
            catch (DbUpdateException ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }

        /// <summary>
        ///     Get all records from database.
        /// </summary>
        /// <returns>IList of Records if exist at least one instance or null.</returns>
        public IList<Record> GetAllRecords()
        {
            return _context.Records
                .Include(r => r.User)
                .OrderByDescending(r => r.Date)
                .ToList();
        }

        /// <summary>
        ///     Add new user record to database.
        /// </summary>
        /// <param name="record">New user game record.</param>
        public void AddRecord(Record record)
        {
            _context.Records.Add(record);
            _context.SaveChanges();
        }

        /// <summary>
        ///     Get all current user records from database.
        /// </summary>
        /// <returns>IList of Records if exists or null</returns>
        public IList<Record> GetCurrentUserRecords()
        {
            return  _context.Records
                            .Where(u => u.User.Username == SavingRegistryData.GetCurrentUser())
                            .OrderByDescending(r => r.Date)
                            .ToList();
        }
    }
}
