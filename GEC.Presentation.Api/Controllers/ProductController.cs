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
    public class ProductController : BaseApiController
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllProducts()
        {
            var products = await _productService.GetAllAsync();
            if (products.Count == 0) return NotFound("No products available");
            return Ok(products.Adapt<List<ProductsViewModel>>());
        }

        [HttpPost("Add"), Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddNewProduct(ProductsViewModel product)
        {
            //if( !User.IsClientAdmin() ) return Ok("Forbid");
            return  Ok("Added");
        }
    }
}