using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GEC.Business.Interfaces;
using GEC.Business.Services.PasswordHash;
using GEC.Infrastructure.Data;
using GEC.Infrastructure.Interfaces.DataSeeding;

namespace GEC.Business.Services.Seeder
{
    public class AdminDataSeeder(ISeeder _seeder) : IDataSeeder
    {
        public async Task SeedAdminDataAsync()
        {
            var passwordHasher = PasswordHasher.Hash("AdminAdmin1#");
            await _seeder.SeedAsync(passwordHasher.PasswordHash, passwordHasher.PasswordSalt);
        }
    }
}