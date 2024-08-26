using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GEC.Business.Contracts.Requests
{
    public class AddNewOrderRequest
    {
        public Guid UserId { get; set; }
        public ICollection<AddOrderItemsRequest> OrderItems { get; set; } = [];
    }
}