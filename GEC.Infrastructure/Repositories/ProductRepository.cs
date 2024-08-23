using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GEC.Infrastructure.Data;
using GEC.Infrastructure.Interfaces.EntityInterfaces;
using GEC.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace GEC.Infrastructure.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationDBContext _context;

        public ProductRepository(ApplicationDBContext dBContext)
        {
            _context = dBContext;
        }

        public async Task<bool> CreateAsync(Product productModel)
        {
            await _context.Products.AddAsync(productModel);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<Product?> DeleteAsync(Guid id)
        {
            var productModel = await _context.Products.FirstOrDefaultAsync(p => p.ProductId == id);
            if(productModel == null) return null;
            _context.Products.Remove(productModel);
            await _context.SaveChangesAsync();
            return productModel;
        }

        public async Task<List<Product>> GetAllAsync()
        {
            return await _context.Products.ToListAsync();
        }

        public async Task<Product?> GetByIdAsync(Guid id)
        {
            return await _context.Products.FindAsync(id);
        }
        public async Task<Product?> GetByNameAsync(string Name){
            return await _context.Products.FirstOrDefaultAsync(p => p.Name == Name);
        }

        public async Task<Product?> UpdateAsync(Product productModel)
        {

            var existingProduct = await _context.Products.FirstOrDefaultAsync(p => p.Name == productModel.Name);
            if(existingProduct == null) return null;
            
            existingProduct.Name = productModel.Name;
            existingProduct.Description = productModel.Description;
            existingProduct.Price = productModel.Price;
            existingProduct.Stock = productModel.Stock;
            existingProduct.Status = true;
            existingProduct.UpdatedOn = DateTime.Now;

            await _context.SaveChangesAsync();
            return existingProduct;
        }
    }
}