using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using GEC.Business.Contracts.Dtos;
using GEC.Business.Contracts.Requests;
using GEC.Business.Extensions;
using GEC.Business.Interfaces;
using GEC.Presentation.Api.ViewModels;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GEC.Presentation.Api.Controllers
{
    //[Authorize]
    public class ProductController(IProductService _productService) : BaseApiController
    {

        [HttpGet]
        public async Task<IActionResult> GetAllProducts()
        {
            var products = await _productService.GetAllAsync();
            return products.Count == 0 ? NotFound("No products available") : Ok(products.Adapt<List<ProductsViewModel>>());
        }

        [HttpGet("{name}")]
        public async Task<IActionResult> GetByName([FromRoute] string name){
            var product = await _productService.GetByNameAsync(name);
            return product == null ? NotFound() : Ok(product.Adapt<ProductsViewModel>());
        }

        [HttpPost("Add")]
        public async Task<IActionResult> AddNewProduct(AddProductRequest product)
        {
            var responseStatus = await _productService.AddNewProductAsync(product.Adapt<ProductDto>());
            return  responseStatus ? Ok("Added") : BadRequest("Check Your Inputs");
        }
        [HttpPut("Update")/*, Authorize(Roles = "Admin")*/]
        public async Task<IActionResult> UpdateProduct(UpdateRequest product)
        {
            var response = await _productService.UpdateProductAsync(product.Adapt<ProductDto>());
            return response == null ? NotFound("The Target Product Is Not Found") : Ok(response.Adapt<AdminProductVM>());
        }
        [HttpDelete("{name}")/*, Authorize(Roles = "Admin")*/]
        public async Task<IActionResult> DeleteProduct([FromRoute] string name)
        {
            var response = await _productService.DeleteProductAsync(name);
            return response ? Ok("Deleted") : NotFound("The Target Product Is Not Found");
        }
    }
}