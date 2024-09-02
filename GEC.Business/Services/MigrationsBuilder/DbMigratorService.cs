using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GEC.Infrastructure.Data;
using GEC.Business.Services.MigrationsBuilder;
using Microsoft.EntityFrameworkCore;

namespace GEC.Business.Services.Repositories
{
    public class DbMigratorService(ApplicationDBContext _context) : IDbMigratorService
    {
        public void MigrateDatabase()
        {
            _context.Database.Migrate();
        }
    }
}