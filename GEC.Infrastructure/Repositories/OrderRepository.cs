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
        public async Task<Order?> GetByIdAsync(string id){
            return await _context.Orders.Include(c => c.OrderItems).FirstOrDefaultAsync(p => p.UserId.ToString() == id);
        }
    }
}