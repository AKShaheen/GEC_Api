using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GEC.Business.Contracts.Dtos
{
    public class OrderDto
    {
        public Guid OrderId { get; set; }
        public decimal Amount { get; set; }
        public decimal Tax { get; set; }
        public decimal TotalAmount { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime UpdatedOn { get; set; }
        public Guid UserId { get; set; }
        public bool IsDeleted { get; set; }
        public ICollection<OrderItemDto> OrderItems { get; set; } = [];
    }
}