using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GEC.Infrastructure.Interfaces.DataSeeding
{
    public interface ISeeder
    {
        Task SeedAsync(string passwordHash, string passwordSalt);
    }
}