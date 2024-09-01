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
            return await _context.Orders.Include(c => c.OrderItems).Where(p => !p.IsDeleted).ToListAsync();
        }
        public async Task<List<Order>?> GetByIdAsync(Guid id){
            return await _context.Orders
                    .Include(c => c.OrderItems)
                    .Where(s => s.UserId == id)
                    .ToListAsync();
        }
        public async Task<Order> AddAsync(Order order){
            order.IsDeleted = false;
            order.CreatedOn = DateTime.Now;
            await _context.Orders.AddAsync(order);
            await _context.SaveChangesAsync();
            return order;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var existingOrder = await _context.Orders.FirstOrDefaultAsync(p => p.OrderId == id);
            if(existingOrder == null) return false;
            existingOrder.IsDeleted = true; 
            await _context.SaveChangesAsync();
            return true;
        }
    }
}