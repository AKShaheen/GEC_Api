using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using GEC.Business.Contracts.Dtos;
using GEC.Business.Extensions;
using GEC.Business.Interfaces;
using GEC.Presentation.Api.ViewModels;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GEC.Presentation.Api.Controllers
{
    [Authorize]
    public class ProductController(IProductService _productService) : BaseApiController
    {

        [HttpGet]
        public async Task<IActionResult> GetAllProducts()
        {
            var products = await _productService.GetAllAsync();
            if (products.Count == 0) return NotFound("No products available");
            return Ok(products.Adapt<List<ProductsViewModel>>());
        }

        [HttpGet("{name}")]
        public async Task<IActionResult> GetByName([FromRoute] string name){
            var product = await _productService.GetByNameAsync(name);
            return product == null ? NotFound(): Ok(product.Adapt<ProductsViewModel>());
        }

        [HttpPost("Add"), Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddNewProduct(ProductsViewModel product)
        {
            var responseStatus = await _productService.AddNewProductAsync(product.Adapt<ProductDto>());
            return  responseStatus? Ok("Added"): BadRequest("Check Your Inputs");
        }
    }
}