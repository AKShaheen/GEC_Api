using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GEC.Infrastructure.Models;

namespace GEC.Infrastructure.Interfaces.Authentication
{
    public interface IJwtTokenGenerator
    {
        string GenerateToken(User user);
    }
}