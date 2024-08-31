using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GEC.Business.Contracts.Dtos;
using GEC.Business.Contracts.Requests;

namespace GEC.Business.Interfaces
{
    public interface IUserService
    {
        Task<UserDto> RegisterAsync(UserDto request);
        Task<UserDto?> LoginAsync(string username, string password);
        Task<bool?> GetUserRoleByIdAsync(Guid id);
    }
}