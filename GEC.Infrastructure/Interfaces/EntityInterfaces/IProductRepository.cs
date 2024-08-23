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
        Task<Product?> GetByNameAsync(string Name);
        Task<bool> CreateAsync(Product productModel);
        Task<Product?> UpdateAsync(Guid id, Product productModel);
        Task<Product?> DeleteAsync(Guid id);
    }
}