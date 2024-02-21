using Entity.Concrete;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.EntityFrameworkCore.Configurations
{
    internal class LogConfiguration : IEntityTypeConfiguration<Log>
    {
        public void Configure(EntityTypeBuilder<Log> builder)
    {
        builder.ToTable("Logs", "dbo");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.LogMessage).IsRequired();
        builder.Property(x => x.LogDate).IsRequired();
   
    }
}
    
}
