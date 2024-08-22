using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GEC.Infrastructure.Models;

namespace GEC.Infrastructure.Interfaces.EntityInterfaces
{
    public interface IUserRepository
    {
        Task<User> CreateAsync(User userModel);
        Task<User?> FindByEmailAsync(string email);
    }
}