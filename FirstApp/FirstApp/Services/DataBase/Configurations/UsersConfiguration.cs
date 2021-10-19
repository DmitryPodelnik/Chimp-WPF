using First_App.Models.DataBase.Models;
using FirstApp.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace FirstApp.Configurations
{
    /// <summary>
    ///     Class of user configuration.
    /// </summary>
    public class UsersConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            SHA256Managed sha256 = new SHA256Managed();

            builder.HasData(
              new User[]
              {
                    new User { Id = 1,  Username = "admin",
                        Password = Convert.ToBase64String(sha256.ComputeHash(Encoding.UTF8.GetBytes("123")))},
                    new User { Id = 2,  Username = "badAdmin",
                        Password = Convert.ToBase64String(sha256.ComputeHash(Encoding.UTF8.GetBytes("123")))},
                    new User { Id = 3,  Username = "Dim4ik",
                        Password = Convert.ToBase64String(sha256.ComputeHash(Encoding.UTF8.GetBytes("123")))},
                    new User { Id = 4,  Username = "AWP_SHNIK",
                        Password = Convert.ToBase64String(sha256.ComputeHash(Encoding.UTF8.GetBytes("123")))},
                    new User { Id = 5,  Username = "dYndio",
                        Password = Convert.ToBase64String(sha256.ComputeHash(Encoding.UTF8.GetBytes("123")))},
                    new User { Id = 6,  Username = "AK-47_Nagibator",
                        Password = Convert.ToBase64String(sha256.ComputeHash(Encoding.UTF8.GetBytes("123")))},
                    new User { Id = 7,  Username = "top4ez",
                        Password = Convert.ToBase64String(sha256.ComputeHash(Encoding.UTF8.GetBytes("123")))},
                    new User { Id = 8,  Username = "sniperElite",
                        Password = Convert.ToBase64String(sha256.ComputeHash(Encoding.UTF8.GetBytes("123")))},
                    new User { Id = 9,  Username = "Inkognito",
                        Password = Convert.ToBase64String(sha256.ComputeHash(Encoding.UTF8.GetBytes("123")))},
                    new User { Id = 10,  Username = "Line6Winner",
                        Password = Convert.ToBase64String(sha256.ComputeHash(Encoding.UTF8.GetBytes("123")))},
              });
        }
    }
}
