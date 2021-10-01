using FirstApp.Configurations;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstApp.Models.DataBase
{
    public class ChimpContext : DbContext
    {
        public DbSet<User> Records { get; set; }

        public ChimpContext(DbContextOptions<ChimpContext> options) : base(options)
        {
            // Если такая БД уже есть, то удаляем ее
            ConnectToDatabase();
        }

        private void ConnectToDatabase()
        {
            if (Database.CanConnect())
                Database.EnsureDeleted();

            // Создаем БД
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // Выводим в режиме отладки запросы, отправляемые EF, в окно Output (меню Visual Studio: View -> Output).
            // Метод DbContextOptionsBuilder.LogTo был добавлен только начиная с EF Core 5.0.
            optionsBuilder.LogTo(s => System.Diagnostics.Debug.WriteLine(s));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UsersConfiguration());

        }
    }
}
