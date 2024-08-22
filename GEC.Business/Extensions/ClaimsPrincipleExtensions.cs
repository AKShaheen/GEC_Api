using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace GEC.Business.Extensions
{
    public static class ClaimsPrincipleExtensions
    {
        public static bool IsClientAdmin(this ClaimsPrincipal claimsPrincipal){
            var role = claimsPrincipal.FindFirst(ClaimTypes.Role)?.Value
                ?? throw new Exception("Cannot get username from token");
            return role == "Admin" ;
        }
    }
}