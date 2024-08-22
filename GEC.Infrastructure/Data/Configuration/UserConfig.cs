using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GEC.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace GEC.Infrastructure.Data.Configuration
{
    public class UserConfig : IEntityTypeConfiguration<User>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<User> builder)
        {
            builder.Property(p => p.Name)
                .IsRequired()
                .HasColumnType("varchar(50)");
            builder.Property(p => p.Phone)
                .HasColumnType("varchar(14)");
        }
    }
}
