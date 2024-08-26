using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using GEC.Business.Contracts.Requests;
using GEC.Business.Services.Validations;
using Microsoft.Extensions.DependencyInjection;

namespace GEC.Runtime.DependencyInjection
{
    public static class Validators
    {
        public static IServiceCollection AddValidationServices(this IServiceCollection services){
            
            services.AddScoped<IValidator<RegisterRequest>, RegisterRequestValidator>();    
            services.AddScoped<IValidator<LoginRequest>, LoginRequestValidator>();  
            services.AddScoped<IValidator<AddProductRequest>, AddProductValidator>();  
            services.AddScoped<IValidator<UpdateRequest>, UpdateProductValidator>();  

            return services;
        }
    }
}