using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GEC.Business.Contracts.Dtos;
using GEC.Business.Interfaces;
using GEC.Infrastructure.Interfaces.EntityInterfaces;
using GEC.Infrastructure.Repositories;
using Mapster;

namespace GEC.Business.Services
{
    public class OrderService(IOrderRepository _orderRepo) : IOrderService
    {
        public async Task<List<OrderDto>> GetAllOrdersAsync()
        {
            var orders = await _orderRepo.GetAllAsync();
            return orders.Adapt<List<OrderDto>>();
        }
        public async Task<List<OrderDto>> GetAllUserOrdersAsync(string id){
            var userOrders = await _orderRepo.GetByIdAsync(id);
            return userOrders.Adapt<List<OrderDto>>();
        }
    }
}