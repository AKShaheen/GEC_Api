using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GEC.Presentation.Api.ViewModels
{
    public class OrdersViewModel
    {        
        public Guid OrderId { get; set; }
        public decimal Amount { get; set; }
        public decimal Tax { get; set; }
        public decimal TotalAmount { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime UpdatedOn { get; set; }
        public Guid UserId { get; set; }
        public bool IsDeleted { get; set; }
        public ICollection<OrderItemsViewModel> OrderItems { get; set; } = [];
    }
}