using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GEC.Business.Services.Authentication
{
    public class JwtSettings
    {
        public const string SectionName = "JwtSettings";
        public string Secret {get; init; } = null!;
        public int ExpirationMinutes {get; init; }
        public string Issuer {get; init; } = null!;
        public string Audience {get; init; } = null!;
    }
}