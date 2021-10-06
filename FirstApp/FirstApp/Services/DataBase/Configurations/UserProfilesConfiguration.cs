using FirstApp.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace First_App.Models.DataBase.Configurations
{
    /// <summary>
    ///     Class of user profile configuration.
    /// </summary>
    public class UserProfilesConfiguration : IEntityTypeConfiguration<UserProfile>
    {
        public void Configure(EntityTypeBuilder<UserProfile> builder)
        {
            SHA256Managed sha256 = new SHA256Managed();

            builder.HasData(
              new UserProfile[]
              {

              });
        }
    }
}
