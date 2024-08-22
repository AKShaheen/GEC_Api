using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GEC.Infrastructure.Models
{
    public class Product
    {
        public Guid ProductId { get; set; }
        public required string Name { get; set; }
        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public DateTime CreatedOn { get; set;} = DateTime.Now;
        public DateTime UpdatedOn { get; set;} = DateTime.Now;
        public bool Status { get; set; }
        public ICollection<OrderItem>? OrderItems { get; set; }
    }
}