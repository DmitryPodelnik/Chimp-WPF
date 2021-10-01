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

        public bool IsAuthorized (string login, string password)
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
    }
}
