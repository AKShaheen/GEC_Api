using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GEC.Infrastructure.Models;

namespace GEC.Infrastructure.Interfaces.EntityInterfaces
{
    public interface IOrderRepository
    {
        Task<List<Order>> GetAllAsync();
        Task<List<Order>?> GetByIdAsync(Guid id);
        Task<Order> AddAsync(Order order);
        Task<bool> DeleteAsync(Guid id);
    }
}