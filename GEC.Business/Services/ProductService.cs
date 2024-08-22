using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GEC.Business.Contracts.Dtos;
using GEC.Business.Interfaces;
using GEC.Infrastructure.Interfaces.EntityInterfaces;
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

        public async Task<ProductDto> GetAllAsync()
        {
            var products = await _productRepo.GetAllAsync();
            var productsDto = products.Adapt<ProductDto>();
            return productsDto;
        }
    }
}