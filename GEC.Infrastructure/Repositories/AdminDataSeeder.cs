using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GEC.Infrastructure.Data;
using GEC.Infrastructure.Interfaces.DataSeeding;
using GEC.Infrastructure.Models;

namespace GEC.Infrastructure.Repositories
{
    public class AdminDataSeeder(ApplicationDBContext _context) : ISeeder
    {
        public async Task SeedAsync(string passwordHash, string passwordSalt)
        {
            if(!_context.Users.Any(u => u.IsAdmin)){
                var adminUser = new User
                {
                    UserId = Guid.NewGuid(),
                    Name = "Admin",
                    Address = "Address",
                    Phone = "00000000000000",
                    Email = "admin@ldc.com",
                    PasswordHash = passwordHash,
                    PasswordSalt = passwordSalt,
                    IsAdmin = true,
                    CreatedOn = DateTime.Now,
                    UpdatedOn = DateTime.Now
                };
                await _context.Users.AddAsync(adminUser);
                await _context.SaveChangesAsync();
            }
        }
    }
}