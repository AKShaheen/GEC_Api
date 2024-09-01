using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GEC.Infrastructure.Models
{
    public class Order
    {
        public Guid OrderId { get; set; }
        public decimal Amount { get; set; }
        public decimal Tax { get; set; }
        public decimal TotalAmount { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime UpdatedOn { get; set; } = DateTime.Now;
        public bool IsDeleted { get; set; }
        public Guid UserId { get; set; }
        public User? User { get; set; }
        public ICollection<OrderItem>? OrderItems { get; set; }
    }
}