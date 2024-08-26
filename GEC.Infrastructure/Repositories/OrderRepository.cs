using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GEC.Infrastructure.Data;
using GEC.Infrastructure.Interfaces.EntityInterfaces;
using GEC.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace GEC.Infrastructure.Repositories
{
    public class OrderRepository(ApplicationDBContext _context) : IOrderRepository
    {
        public async Task<List<Order>> GetAllAsync()
        {
            return await _context.Orders.Include(c => c.OrderItems).ToListAsync();
        }
        public async Task<List<Order>?> GetByIdAsync(Guid id){
            return await _context.Orders
                    .Include(c => c.OrderItems)
                    .Where(s => s.UserId == id)
                    .ToListAsync();
        }
        public async Task<Order> AddAsync(Order order){
            order.IsDeleted = false;
            order.OrderDate = DateTime.Now;
            await _context.Orders.AddAsync(order);
            await _context.SaveChangesAsync();
            return order;
        }
    }
}