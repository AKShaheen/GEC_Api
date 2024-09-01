using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using GEC.Business.Contracts.Dtos;
using GEC.Business.Contracts.Exceptions;
using GEC.Business.Contracts.Requests;
using GEC.Business.Contracts.Response;
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
    public class OrderController(IOrderService _orderService) : BaseApiController
    {
        [HttpGet("GetAllOrders")]
        public async Task<IActionResult> GetAll()
        {
            var orders = await _orderService.GetAllOrdersAsync();
            if(orders.Count == 0){
                var badResponse = new BaseResponse<string>{
                    StatusCode = StatusCodes.Status404NotFound,
                    Message = "No Orders Found"
                };
                return NotFound(badResponse);
            }
            var response = new BaseResponse<List<OrdersViewModel>>{
                StatusCode = StatusCodes.Status200OK,
                Message = "Orders Retrieved Successfully",
                Data = orders.Adapt<List<OrdersViewModel>>()
            };
            return Ok(response);
        }
        // [HttpGet("User")]
        // public async Task<IActionResult> GetAllUserOrdersAsync(){
        //     var name = User.FindFirst(ClaimTypes.Name)?.Value;
        //     var orders = await _orderService.GetAllUserOrdersAsync(name);
        //     return orders?.Count == 0 ? NotFound("No Orders Found") : Ok(orders.Adapt<List<OrdersViewModel>>());
        // }
        [HttpPost("AddOrder")]
        public async Task<IActionResult> AddNewOrder(AddNewOrderRequest request){
            try{
                var order = await _orderService.AddOrderAsync(request.Adapt<OrderDto>());
                var orderVM = order.Adapt<OrdersViewModel>();
                if (orderVM == null){
                    var badResponse = new BaseResponse<string>{
                    StatusCode = StatusCodes.Status404NotFound,
                    Message = "User Or Product Are not found"
                };
                return NotFound(badResponse);
                }
                var response = new BaseResponse<OrdersViewModel>{
                    StatusCode = StatusCodes.Status200OK,
                    Message = "Order Added successfully",
                    Data = orderVM
                };
                return NotFound(response);
            }catch(UserNotFoundException ex){
                var badResponse = new BaseResponse<string>{
                    StatusCode = StatusCodes.Status404NotFound,
                    Message = ex.Message
                };
                return NotFound(badResponse);
            }catch(ProductNotFoundException ex){
                var badResponse = new BaseResponse<string>{
                    StatusCode = StatusCodes.Status404NotFound,
                    Message = ex.Message
                };
                return NotFound(badResponse);
            }catch(InvalidUserOperationException ex){
                    var badResponse = new BaseResponse<string>{
                    StatusCode = StatusCodes.Status404NotFound,
                    Message = ex.Message
                };
                return BadRequest(badResponse);
            }
        }
        [HttpDelete("DeleteOrder/{OrderId}")]
        public async Task<IActionResult> DeleteOrder([FromRoute] Guid OrderId){
            var status = await _orderService.DeleteOrder(OrderId);
            if(!status){
                var badResponse = new BaseResponse<string>{
                    StatusCode = StatusCodes.Status404NotFound,
                    Message = "Invalid Order Id"
                };
                return BadRequest(badResponse);
            }
            var response = new BaseResponse<string>{
                StatusCode = StatusCodes.Status200OK,
                Message = "Order Deleted successfully"
            };
            return Ok(response);
        }
    }
}