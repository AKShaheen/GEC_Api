using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GEC.Business.Contracts.Requests
{
    public class AddOrderItemsRequest
    {
        
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
    }
}