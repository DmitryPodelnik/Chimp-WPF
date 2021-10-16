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
                    new User { Id = 1,  Username = "user1",
                        Password = Convert.ToBase64String(sha256.ComputeHash(Encoding.UTF8.GetBytes("12341234qwe")))},
                    new User { Id = 2,  Username = "user2",
                        Password = Convert.ToBase64String(sha256.ComputeHash(Encoding.UTF8.GetBytes("12341234qwe")))},
              });
        }
    }
}
