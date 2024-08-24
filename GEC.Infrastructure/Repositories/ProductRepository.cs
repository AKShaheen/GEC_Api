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
    public class ProductRepository(ApplicationDBContext _context) : IProductRepository
    {
        
        public async Task<bool> CreateAsync(Product productModel)
        {
            if (await _context.Products.AnyAsync(p => p.Name == productModel.Name))
                return false;
            productModel.Status = true;
            await _context.Products.AddAsync(productModel);
            await _context.SaveChangesAsync();
            return true;
        }
        
        public async Task<List<Product>> GetAllAsync()
        {
            return await _context.Products
                                .Where(p => p.Status == true)
                                .ToListAsync();
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
        public async Task<bool> DeleteAsync(string name){
            var existingProduct = await _context.Products.FirstOrDefaultAsync(p => p.Name == name);
            if(existingProduct == null) return false;
            existingProduct.Status = false; 
            await _context.SaveChangesAsync();
            return true;
        }
    }
}