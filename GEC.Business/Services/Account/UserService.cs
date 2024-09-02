using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GEC.Business.Contracts.Dtos;
using GEC.Business.Contracts.Requests;
using GEC.Business.Interfaces;
using GEC.Infrastructure.Interfaces.EntityInterfaces;
using Mapster;
using GEC.Business.Services.PasswordHash;
using GEC.Infrastructure.Models;
using GEC.Infrastructure.Interfaces.Authentication;
using GEC.Business.Services.Authentication;

namespace GEC.Business.Services.Account
{
    public class UserService(IUserRepository _userRepository, IJwtTokenGenerator _jwtTokenGenerator) : IUserService
    {
        public async Task<UserDto?> RegisterAsync(UserDto request)
        {
            var user = request.Adapt<User>();
            var passwordHash = PasswordHasher.Hash(request.Password);

            user.UserId = Guid.NewGuid();
            user.Name = request.Name;
            user.Phone = request.Phone;
            user.Email = request.Email;
            user.PasswordHash = passwordHash.PasswordHash;
            user.PasswordSalt = passwordHash.PasswordSalt;
            user.CreatedOn = DateTime.Now;
            user.UpdatedOn = DateTime.Now;
            user.Status = request.Status;
            user.IsAdmin = false;
            user.IsDeleted = false;
            try{
                var userModel = await _userRepository.CreateAsync(user);
                return userModel.Adapt<UserDto>();
            }
            catch (ApplicationException){
                return null;
            }
        }
        public async Task<UserDto?> LoginAsync(string email, string password){
            var userModel = await _userRepository.FindByEmailAsync(email) ?? throw new KeyNotFoundException("Could not find user");
            if(!PasswordHasher.VerifyPassword(userModel.PasswordHash, userModel.PasswordSalt ,password))
                throw new InvalidOperationException("Password Not Valid");
            var userDto = userModel.Adapt<UserDto>();
            #if AuthMode
            userDto.Token = _jwtTokenGenerator.GenerateToken(userModel);
            #endif
            return userDto;
        }
        public async Task<bool?> GetUserRoleByIdAsync(Guid id){
            var userModel = await _userRepository.GetUserByIdAsync(id);
            return userModel?.IsAdmin;
        }
    }
}