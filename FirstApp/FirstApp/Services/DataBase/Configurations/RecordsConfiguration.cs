using First_App.Models.DataBase.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace First_App.Services.DataBase.Configurations
{
    public class RecordsConfiguration : IEntityTypeConfiguration<Record>
    {
        public void Configure(EntityTypeBuilder<Record> builder)
        {
            builder.HasData(
              new Record[]
              {
                  new Record { Id = 1, Date = DateTime.Now.ToString(), Score = 7, UserId = 1 },
                  new Record { Id = 2, Date = DateTime.Now.ToString(), Score = 15, UserId = 1 },
                  new Record { Id = 3, Date = DateTime.Now.ToString(), Score = 5, UserId = 1 },
                  new Record { Id = 4, Date = DateTime.Now.ToString(), Score = 4, UserId = 1 },
                  new Record { Id = 5, Date = DateTime.Now.ToString(), Score = 22, UserId = 1 },
                  new Record { Id = 6, Date = DateTime.Now.ToString(), Score = 13, UserId = 1 },
                  new Record { Id = 7, Date = DateTime.Now.ToString(), Score = 18, UserId = 1 },
                  new Record { Id = 8, Date = DateTime.Now.ToString(), Score = 22, UserId = 1 },
                  new Record { Id = 9, Date = DateTime.Now.ToString(), Score = 13, UserId = 1 },
                  new Record { Id = 10, Date = DateTime.Now.ToString(), Score = 22, UserId = 1 },
                  new Record { Id = 11, Date = DateTime.Now.ToString(), Score = 13, UserId = 1 },

                  new Record { Id = 12, Date = DateTime.Now.ToString(), Score = 8, UserId = 2 },
                  new Record { Id = 13, Date = DateTime.Now.ToString(), Score = 12, UserId = 2 },
                  new Record { Id = 14, Date = DateTime.Now.ToString(), Score = 4, UserId = 2 },
                  new Record { Id = 15, Date = DateTime.Now.ToString(), Score = 9, UserId = 2 },
                  new Record { Id = 16, Date = DateTime.Now.ToString(), Score = 19, UserId = 2 },
                  new Record { Id = 17, Date = DateTime.Now.ToString(), Score = 10, UserId = 2 },
                  new Record { Id = 18, Date = DateTime.Now.ToString(), Score = 12, UserId = 2 },
                  new Record { Id = 19, Date = DateTime.Now.ToString(), Score = 12, UserId = 2 },
                  new Record { Id = 20, Date = DateTime.Now.ToString(), Score = 12, UserId = 2 },

                  new Record { Id = 21, Date = DateTime.Now.ToString(), Score = 4, UserId = 3 },
                  new Record { Id = 22, Date = DateTime.Now.ToString(), Score = 13, UserId = 3 },
                  new Record { Id = 23, Date = DateTime.Now.ToString(), Score = 5, UserId = 3 },
                  new Record { Id = 24, Date = DateTime.Now.ToString(), Score = 6, UserId = 3 },
                  new Record { Id = 25, Date = DateTime.Now.ToString(), Score = 7, UserId = 3 },
                  new Record { Id = 26, Date = DateTime.Now.ToString(), Score = 8, UserId = 3 },
                  new Record { Id = 27, Date = DateTime.Now.ToString(), Score = 9, UserId = 3 },

                  new Record { Id = 28, Date = DateTime.Now.ToString(), Score = 7, UserId = 4 },
                  new Record { Id = 29, Date = DateTime.Now.ToString(), Score = 8, UserId = 4 },
                  new Record { Id = 30, Date = DateTime.Now.ToString(), Score = 4, UserId = 4 },
                  new Record { Id = 31, Date = DateTime.Now.ToString(), Score = 4, UserId = 4 },
                  new Record { Id = 32, Date = DateTime.Now.ToString(), Score = 4, UserId = 4 },
                  new Record { Id = 33, Date = DateTime.Now.ToString(), Score = 5, UserId = 4 },
                  new Record { Id = 34, Date = DateTime.Now.ToString(), Score = 9, UserId = 4 },
                  new Record { Id = 35, Date = DateTime.Now.ToString(), Score = 6, UserId = 4 },
                  new Record { Id = 36, Date = DateTime.Now.ToString(), Score = 6, UserId = 4 },

                  new Record { Id = 37, Date = DateTime.Now.ToString(), Score = 4, UserId = 5 },
                  new Record { Id = 38, Date = DateTime.Now.ToString(), Score = 4, UserId = 5 },
                  new Record { Id = 39, Date = DateTime.Now.ToString(), Score = 4, UserId = 5 },
                  new Record { Id = 40, Date = DateTime.Now.ToString(), Score = 4, UserId = 5 },
                  new Record { Id = 41, Date = DateTime.Now.ToString(), Score = 5, UserId = 5 },
                  new Record { Id = 42, Date = DateTime.Now.ToString(), Score = 10, UserId = 5 },
                  new Record { Id = 43, Date = DateTime.Now.ToString(), Score = 12, UserId = 5 },
                  new Record { Id = 44, Date = DateTime.Now.ToString(), Score = 12, UserId = 5 },
                  new Record { Id = 45, Date = DateTime.Now.ToString(), Score = 11, UserId = 5 },

                  new Record { Id = 46, Date = DateTime.Now.ToString(), Score = 5, UserId = 6 },
                  new Record { Id = 47, Date = DateTime.Now.ToString(), Score = 4, UserId = 6 },
                  new Record { Id = 48, Date = DateTime.Now.ToString(), Score = 7, UserId = 6 },
                  new Record { Id = 49, Date = DateTime.Now.ToString(), Score = 5, UserId = 6 },
                  new Record { Id = 50, Date = DateTime.Now.ToString(), Score = 14, UserId = 6 },
                  new Record { Id = 51, Date = DateTime.Now.ToString(), Score = 13, UserId = 6 },
                  new Record { Id = 52, Date = DateTime.Now.ToString(), Score = 12, UserId = 6 },
                  new Record { Id = 53, Date = DateTime.Now.ToString(), Score = 21, UserId = 6 },
                  new Record { Id = 54, Date = DateTime.Now.ToString(), Score = 25, UserId = 6 },
                  new Record { Id = 55, Date = DateTime.Now.ToString(), Score = 23, UserId = 6 },
                  new Record { Id = 56, Date = DateTime.Now.ToString(), Score = 28, UserId = 6 },

                  new Record { Id = 57, Date = DateTime.Now.ToString(), Score = 7, UserId = 7 },
                  new Record { Id = 58, Date = DateTime.Now.ToString(), Score = 12, UserId = 7 },
                  new Record { Id = 59, Date = DateTime.Now.ToString(), Score = 15, UserId = 7 },
                  new Record { Id = 60, Date = DateTime.Now.ToString(), Score = 20, UserId = 7 },
                  new Record { Id = 61, Date = DateTime.Now.ToString(), Score = 20, UserId = 7 },
                  new Record { Id = 62, Date = DateTime.Now.ToString(), Score = 19, UserId = 7 },
                  new Record { Id = 63, Date = DateTime.Now.ToString(), Score = 14, UserId = 7 },
                  new Record { Id = 64, Date = DateTime.Now.ToString(), Score = 17, UserId = 7 },
                  new Record { Id = 65, Date = DateTime.Now.ToString(), Score = 13, UserId = 7 },

                  new Record { Id = 66, Date = DateTime.Now.ToString(), Score = 4, UserId = 8 },
                  new Record { Id = 67, Date = DateTime.Now.ToString(), Score = 12, UserId = 8 },
                  new Record { Id = 68, Date = DateTime.Now.ToString(), Score = 9, UserId = 8 },
                  new Record { Id = 69, Date = DateTime.Now.ToString(), Score = 16, UserId = 8 },
                  new Record { Id = 70, Date = DateTime.Now.ToString(), Score = 17, UserId = 8 },
                  new Record { Id = 71, Date = DateTime.Now.ToString(), Score = 18, UserId = 8 },
                  new Record { Id = 72, Date = DateTime.Now.ToString(), Score = 19, UserId = 8 },

                  new Record { Id = 73, Date = DateTime.Now.ToString(), Score = 17, UserId = 9 },
                  new Record { Id = 74, Date = DateTime.Now.ToString(), Score = 18, UserId = 9 },
                  new Record { Id = 75, Date = DateTime.Now.ToString(), Score = 14, UserId = 9 },
                  new Record { Id = 76, Date = DateTime.Now.ToString(), Score = 14, UserId = 9 },
                  new Record { Id = 77, Date = DateTime.Now.ToString(), Score = 14, UserId = 9 },
                  new Record { Id = 78, Date = DateTime.Now.ToString(), Score = 15, UserId = 9 },
                  new Record { Id = 79, Date = DateTime.Now.ToString(), Score = 19, UserId = 9 },
                  new Record { Id = 80, Date = DateTime.Now.ToString(), Score = 16, UserId = 9 },
                  new Record { Id = 81, Date = DateTime.Now.ToString(), Score = 16, UserId = 9 },

                  new Record { Id = 82, Date = DateTime.Now.ToString(), Score = 24, UserId = 10 },
                  new Record { Id = 83, Date = DateTime.Now.ToString(), Score = 24, UserId = 10 },
                  new Record { Id = 84, Date = DateTime.Now.ToString(), Score = 34, UserId = 10 },
                  new Record { Id = 85, Date = DateTime.Now.ToString(), Score = 44, UserId = 10 },
                  new Record { Id = 86, Date = DateTime.Now.ToString(), Score = 25, UserId = 10 },
                  new Record { Id = 87, Date = DateTime.Now.ToString(), Score = 10, UserId = 10 },
                  new Record { Id = 88, Date = DateTime.Now.ToString(), Score = 12, UserId = 10 },
                  new Record { Id = 89, Date = DateTime.Now.ToString(), Score = 4, UserId = 10 },
                  new Record { Id = 90, Date = DateTime.Now.ToString(), Score = 11, UserId = 10 },
              });
        }
    }
}
