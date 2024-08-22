using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GEC.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace GEC.Infrastructure.Data.Configuration
{
    public class OrderConfig : IEntityTypeConfiguration<Order>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Order> builder)
        {
            builder.Property(p => p.Amount).HasColumnType("decimal(10,2)");
            builder.Property(p => p.Tax).HasColumnType("decimal(10,2)");
            builder.Property(p => p.TotalAmount).HasColumnType("decimal(10,2)");
        }
    }
}