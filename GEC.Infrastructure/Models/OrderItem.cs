using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GEC.Infrastructure.Models
{
    public class OrderItem
    {
        public Guid OrderItemId { get; set; }
        public int Quantity { get; set; }
        public decimal Cost { get; set; } 
        public Guid? OrderId { get; set; }
        public Order? Order { get; set; }
        public Guid? ProductId { get; set; }
        public Product? Product { get; set; }
    }
}