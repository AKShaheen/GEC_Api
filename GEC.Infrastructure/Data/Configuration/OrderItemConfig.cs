using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GEC.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GEC.Infrastructure.Data.Configuration
{
    public class OrderItemConfig : IEntityTypeConfiguration<OrderItem>
    {
        public void Configure(EntityTypeBuilder<OrderItem> builder)
        {
            builder.Property(p => p.Cost).HasColumnType("decimal(10,2)");
        }
    }
}