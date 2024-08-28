using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GEC.Business.Contracts.Dtos;

namespace GEC.Business.Interfaces
{
    public interface IOrderService
    {
        Task<List<OrderDto>> GetAllOrdersAsync();
        Task<List<OrderDto>?> GetAllUserOrdersAsync(string id);
        Task<OrderDto?> AddOrderAsync(OrderDto order);
        Task<bool> DeleteOrder(Guid OrderId);
    }
}