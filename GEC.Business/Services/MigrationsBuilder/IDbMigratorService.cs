using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GEC.Business.Services.MigrationsBuilder
{
    public interface IDbMigratorService
    {
        void MigrateDatabase();
    }
}