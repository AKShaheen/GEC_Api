using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GEC.Business.Contracts.Dtos;

namespace GEC.Business.Interfaces
{
    public interface IProductService
    {
        Task<List<ProductDto>> GetAllAsync();
        Task<ProductDto> GetByIdAsync(Guid id);
        Task<ProductDto> GetByNameAsync(string name);
        Task<bool> AddNewProductAsync(ProductDto product);
        Task<ProductDto?> UpdateProductAsync(ProductDto product);
        Task<bool> DeleteProductAsync(Guid id);
    }
}