using FirstApp.Models.DataBase;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace First_App.Models.DataBase
{
    public class ChimpDataBase
    {
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
    }
}
