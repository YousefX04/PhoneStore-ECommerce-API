using PhoneStore.Application.DTOs.Cart;
using PhoneStore.Application.DTOs.Order;
using PhoneStore.Application.Services.Interfaces;
using PhoneStore.Domain.Entities;
using PhoneStore.Domain.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace PhoneStore.Application.Services.Implementations
{
    public class OrderService : IOrderService
    {
        private readonly IUnitOfWork _unitOfWork;

        public OrderService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task CreateOrder(string userId)
        {
            var cart = await _unitOfWork.Carts.GetAsync(
                filter: c => c.UserId == userId,
                selector: c => new CartDto
                {
                    Id = c.Id,
                    Items = c.CartItems.Select(ci => new CartItemDto
                    {
                        ProductId = ci.ProductId,
                        ProductName = ci.Product.Name,
                        Price = ci.Product.Price,
                        Quantity = ci.Quantity,
                        SubTotal = ci.Quantity * ci.Product.Price
                    }).ToList(),
                    TotalPrice = c.CartItems.Sum(ci => ci.Quantity * ci.Product.Price)
                }
            );

            if (cart == null || cart.Items.Count == 0)
                throw new InvalidOperationException("Cart is empty");
            

            await _unitOfWork.Orders.AddAsync(new Order
            {
                UserId = userId,
                OrderDate = DateTime.UtcNow,
                OrderItems = cart.Items.Select(ci => new OrderItem
                {
                    ProductId = ci.ProductId,
                    Quantity = ci.Quantity,
                    Price = ci.Price,
                }).ToList(),
                TotalPrice = cart.TotalPrice
            });

            await _unitOfWork.Carts.DeleteWhereAsync(c => c.UserId == userId);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<List<OrderDto>> GetAllOrders(string userId, int pageNumber = 1)
        {
            var orders = await _unitOfWork.Orders
                .GetAllAsync(
                filter: o => o.UserId == userId,
                selector: o => new OrderDto
                {
                    Id = o.Id,
                    OrderDate = o.OrderDate,
                    TotalPrice = o.TotalPrice,
                    OrderItems = o.OrderItems.Select(oi => new OrderItemDto
                    {
                        ProductId = oi.ProductId,
                        ProductName = oi.Product.Name,
                        Price = oi.Price,
                        Quantity = oi.Quantity,
                        SubTotal = oi.Quantity * oi.Price
                    }).ToList()
                },
                pageNumber: pageNumber,
                pageSize: 10
                );

            return orders;
        }
    }
}
