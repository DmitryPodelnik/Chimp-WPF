﻿using First_App.Models.DataBase.Models;
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
            builder.Property(u => u.MaxScore).HasDefaultValue(0);
            builder.Property(u => u.AverageScore).HasDefaultValue(0);
            builder.Property(u => u.GameCount).HasDefaultValue(0);
            builder.Property(u => u.Rate).HasDefaultValue(0.0);
            builder.Property(u => u.RegisterDate).HasDefaultValue(DateTime.Now.ToShortDateString());
            builder.Property(u => u.LastSeen).HasDefaultValue(DateTime.Now);

            builder.HasData(
              new UserProfile[]
              {
                  new UserProfile { Id = 1, UserId = 1, GameCount = 11 },
                  new UserProfile { Id = 2, UserId = 2, GameCount = 9 },
                  new UserProfile { Id = 3, UserId = 3, GameCount = 7 },
                  new UserProfile { Id = 4, UserId = 4, GameCount = 9 },
                  new UserProfile { Id = 5, UserId = 5, GameCount = 9 },
                  new UserProfile { Id = 6, UserId = 6, GameCount = 11 },
                  new UserProfile { Id = 7, UserId = 7, GameCount = 9 },
                  new UserProfile { Id = 8, UserId = 8, GameCount = 7 },
                  new UserProfile { Id = 9, UserId = 9, GameCount = 9 },
                  new UserProfile { Id = 10, UserId = 10, GameCount = 9 },
              });
        }
    }
}
