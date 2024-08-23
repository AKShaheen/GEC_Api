using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GEC.Business.Contracts.Dtos;
using GEC.Business.Interfaces;
using GEC.Infrastructure.Interfaces.EntityInterfaces;
using GEC.Infrastructure.Models;
using Mapster;

namespace GEC.Business.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepo;

        public ProductService(IProductRepository productRepo)
        {
            _productRepo = productRepo;
        }

        public async Task<List<ProductDto>> GetAllAsync()
        {
            var products = await _productRepo.GetAllAsync();
            return products.Adapt<List<ProductDto>>();
        }

        public async Task<ProductDto> GetByNameAsync(string name){
            var product = await _productRepo.GetByNameAsync(name);
            return product.Adapt<ProductDto>();
        }

        public async Task<bool> AddNewProductAsync(ProductDto product){
            var status = await _productRepo.CreateAsync(product.Adapt<Product>());
            return status;
        }
    }
}