using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GEC.Business.Contracts.Dtos;
using GEC.Business.Interfaces;
using GEC.Infrastructure.Interfaces.EntityInterfaces;
using GEC.Infrastructure.Models;
using GEC.Infrastructure.Repositories;
using Mapster;

namespace GEC.Business.Services
{
    public class OrderService(IOrderRepository _orderRepo, IProductRepository _productRepo, IUserRepository _userRepo) : IOrderService
    {
        public async Task<OrderDto?> AddOrderAsync(OrderDto order)
        {
            if (!await _userRepo.IsExist(order.UserId)) return null;
            decimal total = 0;
            foreach (var item in order.OrderItems)
            {
                var productPrice = await _productRepo.GetPriceByIdAsync(item.ProductId);
                item.Cost = item.Quantity * productPrice ;
                total += item.Cost;
            }
            order.Amount = total;
            order.Tax = 0.14M*total;
            order.TotalAmount = order.Amount + order.Tax;
            var result = await _orderRepo.AddAsync(order.Adapt<Order>());

            return result.Adapt<OrderDto>() ;
        }

        public async Task<List<OrderDto>> GetAllOrdersAsync()
        {
            var orders = await _orderRepo.GetAllAsync();
            return orders.Adapt<List<OrderDto>>();
        }
        public async Task<List<OrderDto>?> GetAllUserOrdersAsync(string name){
            var userModel = await _userRepo.GetUserByNameAsync(name);
            if(userModel == null) return null;
            var userOrders = await _orderRepo.GetByIdAsync(userModel.UserId);
            return userOrders.Adapt<List<OrderDto>?>();
        }
    }
}