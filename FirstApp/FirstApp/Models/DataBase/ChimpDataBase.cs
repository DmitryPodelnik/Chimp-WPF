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

namespace First_App.Models.DataBase
{
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


        public ChimpDataBase()
        {
            try
            {
                var configuration =
                     new ConfigurationBuilder()
                         .SetBasePath(Directory.GetCurrentDirectory())
                         .AddJsonFile("appsettings.json")
                         .Build();


                // Получаем строку подключения из файла appsettings.json
                ConnectionString = configuration.GetConnectionString("DefaultConnection");

                // Создаем объект контекста EF, указываем ему строку соединения и
                // получаем объект настроек для конструктора объекта контекста EF
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

        public bool IsAuthorized (string login, string password)
        {
            try
            {
                var result = _context.Users.FirstOrDefault(
                     u => u.Username == login &&
                     u.Password == Convert.ToBase64String(sha256.ComputeHash(Encoding.UTF8.GetBytes(password))));

                if (result != null)
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

        public async Task<bool> SaveNewDataAsync (string previousLogin, string newLogin, string password, string confirmPassword)
        {
            if (previousLogin == null && (password == null || confirmPassword == null))
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

                var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == previousLogin);
                if (user == null)
                {
                    MessageBox.Show("User is not found!", "Error", MessageBoxButton.OK);
                    return false;
                }

                if (newLogin != null)
                {
                    var existedUser = await _context.Users.FirstOrDefaultAsync(u => u.Username == newLogin);
                    if (existedUser == null)
                    {
                        user.Username = newLogin;
                    }
                }
                user.Password = password;
                await _context.SaveChangesAsync();

                SavingRegistryData registry = new();
                registry.SaveUserData(newLogin, password);

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
    }
}
