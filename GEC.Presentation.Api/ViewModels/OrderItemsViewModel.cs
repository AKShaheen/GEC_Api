using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GEC.Presentation.Api.ViewModels
{
    public class OrderItemsViewModel
    {
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal Cost { get; set; }  
    }
}