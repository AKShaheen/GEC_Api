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

namespace GEC.Business.Services.Account
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IJwtTokenGenerator _jwtTokenGenerator;

        public UserService(IUserRepository userRepository, IJwtTokenGenerator jwtTokenGenerator)
        {
            _userRepository = userRepository;
            _jwtTokenGenerator = jwtTokenGenerator;
        }

        public async Task<UserDto> RegisterAsync(UserDto request)
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
            user.IsAdmin = true;
            user.IsDeleted = false;
            
            var userModel = await _userRepository.CreateAsync(user);
            return userModel.Adapt<UserDto>();
        }
        public async Task<UserDto?> LoginAsync(string email, string password){
            var userModel = await _userRepository.FindByEmailAsync(email) ?? throw new KeyNotFoundException("Could not find user");
            if(!PasswordHasher.VerifyPassword(userModel.PasswordHash, userModel.PasswordSalt ,password))
                throw new InvalidOperationException("Password Not Valid");
            var userDto = userModel.Adapt<UserDto>();
            //userDto.Token = _jwtTokenGenerator.GenerateToken(userModel);
            return userDto;
        }
    }
}