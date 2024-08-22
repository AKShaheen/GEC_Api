using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GEC.Infrastructure.Interfaces.EntityInterfaces;
using GEC.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace GEC.Runtime.DependencyInjection
{
    public static class InfrastructureDI
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services){
            
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IUserRepository, UserRepository>();

            return services;
        }
    }
}