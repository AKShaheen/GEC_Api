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
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDBContext _context;

        public UserRepository(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<User> CreateAsync(User userModel)
        {
            await _context.AddAsync(userModel);
            await _context.SaveChangesAsync();
            return userModel;
        }
        public async Task<User?> FindByEmailAsync(string email){
            return await _context.Users.SingleOrDefaultAsync(e => e.Email == email);
        }
    }
}