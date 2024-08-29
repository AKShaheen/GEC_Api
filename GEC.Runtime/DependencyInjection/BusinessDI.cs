using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GEC.Business.Interfaces;
using GEC.Business.Services;
using GEC.Business.Services.Account;
using GEC.Business.Services.Authentication;
using GEC.Business.Services.Seeder;
using GEC.Infrastructure.Interfaces.Authentication;
using Microsoft.Extensions.DependencyInjection;

namespace GEC.Runtime.DependencyInjection
{
    public static class BusinessDI
    {
        public static IServiceCollection AddBusinessServices(this IServiceCollection services){
            
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IOrderService, OrderService>();
            services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();
            services.AddScoped<IDataSeeder, AdminDataSeeder>();

            return services;
        }
    }
}