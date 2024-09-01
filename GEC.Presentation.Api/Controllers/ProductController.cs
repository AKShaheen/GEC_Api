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
    public class ProductController(IProductService _productService
                                    ,IValidator<AddProductRequest> _addProductValidator,
                                    IValidator<UpdateRequest> _updateProductValidator) : BaseApiController
    {

        [HttpGet("GetAllProducts")]
        public async Task<IActionResult> GetAllProducts()
        {
            var products = await _productService.GetAllAsync();
            if(products.Count == 0){
                var badResponse = new BaseResponse<string>{
                    StatusCode = StatusCodes.Status404NotFound,
                    Message = "No Products Available",
                };
                return NotFound(badResponse);
            } 
            var response = new BaseResponse<List<ProductsViewModel>>{
                    StatusCode = StatusCodes.Status200OK,
                    Message = "Product Retrieved Successfully",
                    Data = products.Adapt<List<ProductsViewModel>>()
                };
            return Ok(response);
        }

        [HttpGet("GetProductsById/{Id}")]
        public async Task<IActionResult> GetById([FromRoute] Guid Id){
            var product = await _productService.GetByIdAsync(Id);
            if(product == null){
                var badResponse = new BaseResponse<string>{
                    StatusCode = StatusCodes.Status404NotFound,
                    Message = "No Product Found",
                };
                return NotFound(badResponse);
            }
            var response = new BaseResponse<ProductsViewModel>{
                StatusCode = StatusCodes.Status200OK,
                Message = "Product Retrieved Successfully",
                Data = product.Adapt<ProductsViewModel>()
            };
            return Ok(response);
        }
        #if AuthMode
        [HttpPost("AddProduct"), Authorize(Roles = "Admin")]
        #else
        [HttpPost("AddProduct")]
        #endif
        public async Task<IActionResult> AddNewProduct(AddProductRequest product)
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
                Message =  "Product Added Successfully",
                Data = responseProduct.Adapt<ProductsViewModel>()
            };
            return Ok(response);
        }
        #if AuthMode
        [HttpPut("UpdateProduct"), Authorize(Roles = "Admin")]
        #else
        [HttpPut("UpdateProduct")]
        #endif
        public async Task<IActionResult> UpdateProduct(UpdateRequest product)
        {
            var result = await _updateProductValidator.ValidateAsync(product);
            if(!result.IsValid){
                var badResponse = new BaseResponse<string>{
                    StatusCode = StatusCodes.Status400BadRequest,
                    Message = "Invalid Input",
                    Errors = string.Join("," , result.Errors.Select(x => x.ErrorMessage))
                };
                return BadRequest(badResponse);
            }
            var ProductResponse = await _productService.UpdateProductAsync(product.Adapt<ProductDto>());
            if(ProductResponse == null){
                var badResponse = new BaseResponse<string>{
                StatusCode = StatusCodes.Status404NotFound,
                Message = "The Target Product Is Not Found"
                };
                return NotFound(badResponse);
            }
            var response = new BaseResponse<ProductsViewModel>{
                StatusCode = StatusCodes.Status200OK,
                Message =  "Product Updated Successfully",
                Data = ProductResponse.Adapt<ProductsViewModel>()
            };
            return Ok(response);
        }
        #if AuthMode
        [HttpDelete("DeleteProduct/{productId}"),Authorize(Roles = "Admin")]
        #else
        [HttpDelete("DeleteProduct/{productId}")]
        #endif
        public async Task<IActionResult> DeleteProduct([FromRoute] Guid productId)
        {
            var DeleteResponse = await _productService.DeleteProductAsync(productId);
            if(!DeleteResponse){
                var badResponse = new BaseResponse<string>{
                StatusCode = StatusCodes.Status404NotFound,
                Message = "The Target Product Is Not Found"
                };
                return NotFound(badResponse);
            }
            var response = new BaseResponse<ProductsViewModel>{
                StatusCode = StatusCodes.Status200OK,
                Message =  "Product Deleted successfully",
            };
            return Ok(response);
        }
    }
}