using First_App.Models.Tables;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace First_App.Models.DataBase.Configurations
{
    public class StatisticsConfiguration : IEntityTypeConfiguration<Statistics>
    {
        public void Configure(EntityTypeBuilder<Statistics> builder)
        {
            builder.Property(s => s.Score).HasDefaultValue(0);

            builder.HasData(
              new Statistics[]
              {
                  new Statistics { Id = 1, Score = 0 },
                  new Statistics { Id = 2, Score = 0 },
              });
        }
    }
}
