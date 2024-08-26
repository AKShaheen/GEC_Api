using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using GEC.Business.Interfaces;
using GEC.Presentation.Api.ViewModels;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GEC.Presentation.Api.Controllers
{
    [Authorize]
    public class OrderController(IOrderService _orderService) : BaseApiController
    {
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var orders = await _orderService.GetAllOrdersAsync();
            return orders.Count == 0 ? NotFound("No Orders Found") : Ok(orders.Adapt<List<OrdersViewModel>>());
        }
        [HttpGet("User")]
        public async Task<ActionResult> GetAllUserOrdersAsync(){
            var UserId = User.FindFirst(ClaimTypes.Sid)?.Value;
            var orders = await _orderService.GetAllUserOrdersAsync(UserId);
            return orders.Count == 0 ? NotFound("No Orders Found") : Ok(orders.Adapt<List<OrdersViewModel>>());
        }
    }
}