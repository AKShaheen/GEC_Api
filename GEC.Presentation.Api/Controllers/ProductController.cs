using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using FluentValidation;
using GEC.Business.Contracts.Dtos;
using GEC.Business.Contracts.Requests;
using GEC.Business.Contracts.Response;
using GEC.Business.Extensions;
using GEC.Business.Interfaces;
using GEC.Presentation.Api.ViewModels;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GEC.Presentation.Api.Controllers
{
    #if AuthMode
    [Authorize]
    #endif
    public class ProductController(IProductService _productService, IUserService _userService
                                    ,IValidator<AddProductRequest> _addProductValidator,
                                    IValidator<UpdateRequest> _updateProductValidator) : BaseApiController
    {

        [HttpGet("GetAllProducts")]
        public async Task<IActionResult> GetAllProducts()
        {
            var products = await _productService.GetAllAsync();
            return products.Count == 0 ? NotFound("No products available") : Ok(products.Adapt<List<ProductsViewModel>>());
        }

        [HttpGet("GetProductsById/{Id}")]
        public async Task<IActionResult> GetById([FromRoute] Guid Id){
            var product = await _productService.GetByIdAsync(Id);
            return product == null ? NotFound() : Ok(product.Adapt<ProductsViewModel>());
        }
        #if AuthMode
        [HttpPost("AddProduct"), Authorize(Roles = "Admin")]
        #else
        [HttpPost("AddProduct/{UserId}")]
        #endif
        public async Task<IActionResult> AddNewProduct([FromRoute] Guid UserId,AddProductRequest product)
        {
            var result = await _addProductValidator.ValidateAsync(product);
            if(!result.IsValid){
                var badResponse = new BaseResponse<string>{
                    StatusCode = StatusCodes.Status400BadRequest,
                    Message = "Invalid Input",
                    Errors = string.Join("," , result.Errors.Select(x => x.ErrorMessage))
                };
                return BadRequest(badResponse);
            }
            var adminCred = await _userService.GetUserRoleByIdAsync(UserId);
            if(adminCred == true){
                var responseProduct = await _productService.AddNewProductAsync(product.Adapt<ProductDto>());
                if(responseProduct == null){
                    var badResponse = new BaseResponse<string>{
                    StatusCode = StatusCodes.Status400BadRequest,
                    Message = "Can't Add Product"
                };
                return NotFound(badResponse);
                }
                var response = new BaseResponse<ProductsViewModel>{
                    StatusCode = StatusCodes.Status200OK,
                    Message =  "Customer Added Successfully",
                    Data = responseProduct.Adapt<ProductsViewModel>()
                };
                return NotFound(response);
            }else{
                var badResponse = new BaseResponse<string>{
                    StatusCode = StatusCodes.Status400BadRequest,
                    Message = "Not Authorized"
                };
                return NotFound(badResponse);
            }
        }
        #if AuthMode
        [HttpPut("UpdateProduct"), Authorize(Roles = "Admin")]
        #else
        [HttpPut("UpdateProduct")]
        #endif
        public async Task<IActionResult> UpdateProduct(UpdateRequest product)
        {
            var result = await _updateProductValidator.ValidateAsync(product);
            if(!result.IsValid)
                return StatusCode(StatusCodes.Status400BadRequest, result.Errors.Select(x => x.ErrorMessage));
            var response = await _productService.UpdateProductAsync(product.Adapt<ProductDto>());
            return response == null ? NotFound("The Target Product Is Not Found") : Ok(response.Adapt<AdminProductVM>());
        }
        #if AuthMode
        [HttpDelete("DeleteProduct/{productId}"),Authorize(Roles = "Admin")]
        #else
        [HttpDelete("DeleteProduct/{productId}")]
        #endif
        public async Task<IActionResult> DeleteProduct([FromRoute] Guid productId)
        {
            var response = await _productService.DeleteProductAsync(productId);
            return response ? Ok("Deleted") : NotFound("The Target Product Is Not Found");
        }
    }
}