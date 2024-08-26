using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using GEC.Infrastructure.Interfaces.Authentication;
using GEC.Infrastructure.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Security.Cryptography;

namespace GEC.Business.Services.Authentication
{
    public class JwtTokenGenerator : IJwtTokenGenerator
    {
        public readonly IConfiguration _configuration;

        public JwtTokenGenerator(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        // public static string GenerateSecureKey(int length = 64)
        // {
        //     byte[] keyBytes = new byte[length];
        //     using (var rng = RandomNumberGenerator.Create())
        //     {
        //         rng.GetBytes(keyBytes);
        //     }
        //     return Convert.ToBase64String(keyBytes);
        // }
        public string GenerateToken(User user)
        {
            List<Claim> claims = new List<Claim> {
                new(ClaimTypes.Sid, user.UserId.ToString()),
                new(ClaimTypes.Name, user.Name),
                new(ClaimTypes.Email, user.Email),
                new(ClaimTypes.MobilePhone, user.Phone),
                new(ClaimTypes.Role, user.IsAdmin ? "Admin": "Customer"),
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetSection("JwtSettings:Secret").Value));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddMinutes(60),
                signingCredentials: creds 
                );
            var jwt = new JwtSecurityTokenHandler().WriteToken(token);
            return jwt;
        }
    }
}