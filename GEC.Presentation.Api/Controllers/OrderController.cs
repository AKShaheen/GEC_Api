using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using GEC.Business.Contracts.Dtos;
using GEC.Business.Contracts.Exceptions;
using GEC.Business.Contracts.Requests;
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
            return orders.Count == 0 ? NotFound("No Orders Found") : Ok(orders.Adapt<List<OrdersViewModel>>());
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
                return orderVM == null ? NotFound("User Or Product Are not found") : Ok("Product Added successfully");
            }catch(UserNotFoundException ex){
                return NotFound(ex.Message);
            }catch(ProductNotFoundException ex){
                return NotFound(ex.Message);
            }catch(InvalidUserOperationException ex){
                return BadRequest(ex.Message);
            }catch(Exception){
                return BadRequest();
            }
        }
        [HttpDelete("DeleteOrder/{OrderId}")]
        public async Task<IActionResult> DeleteOrder([FromRoute] Guid OrderId){
            var status = await _orderService.DeleteOrder(OrderId);
            return status ? Ok("Deleted") : NotFound("Cannot Found the Specified Order");
        }
    }
}