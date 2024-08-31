using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GEC.Infrastructure.Data;
using GEC.Infrastructure.Interfaces.EntityInterfaces;
using GEC.Infrastructure.Models;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace GEC.Infrastructure.Repositories
{
    public class UserRepository (ApplicationDBContext _context) : IUserRepository
    {
        public async Task<User> CreateAsync(User userModel)
        {
            await _context.AddAsync(userModel);
            await _context.SaveChangesAsync();
            return userModel;
        }
        public async Task<User?> FindByEmailAsync(string email){
            return await _context.Users.SingleOrDefaultAsync(e => e.Email == email);
        }
        public async Task<bool> IsExist(Guid id){
            return await _context.Users.AnyAsync(e => e.UserId == id);
        }
        public async Task<User?> GetUserByIdAsync(Guid id){
            return await _context.Users.FirstOrDefaultAsync(e => e.UserId == id);
        }
    }
}