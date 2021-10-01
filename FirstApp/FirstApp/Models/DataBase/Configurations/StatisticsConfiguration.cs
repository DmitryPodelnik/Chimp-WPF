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
            builder.HasData(
              new Statistics[]
              {

              });
        }
    }
}
