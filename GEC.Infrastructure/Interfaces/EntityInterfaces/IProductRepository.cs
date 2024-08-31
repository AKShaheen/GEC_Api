using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GEC.Infrastructure.Models;

namespace GEC.Infrastructure.Interfaces.EntityInterfaces
{
    public interface IProductRepository
    {
        Task<List<Product>> GetAllAsync();
        Task<Product?> GetByIdAsync(Guid id);
        Task<decimal> GetPriceByIdAsync(Guid id);
        Task<Product?> GetByNameAsync(string Name);
        Task<Product> CreateAsync(Product productModel);
        Task<Product?> UpdateAsync(Product productModel);
        Task<bool> DeleteAsync(Guid id);
    }
}