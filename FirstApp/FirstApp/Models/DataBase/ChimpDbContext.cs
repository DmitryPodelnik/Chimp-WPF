using FirstApp.Configurations;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstApp.Models.DataBase
{
    public class ChimpDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public ChimpDbContext(DbContextOptions<ChimpDbContext> options) : base(options)
        {
            // If database already exists then delete it
            ConnectToDatabase();
        }

        private void ConnectToDatabase()
        {
               // if (Database.CanConnect())
               // Database.EnsureDeleted();

            // Create database
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // Выводим в режиме отладки запросы, отправляемые EF, в окно Output (меню Visual Studio: View -> Output).
            // Method DbContextOptionsBuilder.LogTo was added only from EF Core 5.0.
            optionsBuilder.LogTo(s => System.Diagnostics.Debug.WriteLine(s));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UsersConfiguration());

        }
    }
}
