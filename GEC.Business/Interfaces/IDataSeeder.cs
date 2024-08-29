using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GEC.Business.Interfaces
{
    public interface IDataSeeder
    {
        Task SeedAdminDataAsync();
    }
}