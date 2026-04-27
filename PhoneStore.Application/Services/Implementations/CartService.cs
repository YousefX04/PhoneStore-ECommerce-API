using Microsoft.EntityFrameworkCore;
using PhoneStore.Application.DTOs.Cart;
using PhoneStore.Application.Services.Interfaces;
using PhoneStore.Domain.Entities;
using PhoneStore.Domain.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace PhoneStore.Application.Services.Implementations
{
    public class CartService : ICartService
    {
        private readonly IUnitOfWork _unitOfWork;

        public CartService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task AddToCart(string userId, Guid productId, int quantity)
        {
            var cart = await _unitOfWork.Carts
                .GetAsync(
                filter: c => c.UserId == userId
                );

            if (cart == null)
            {
                cart = new Cart
                {
                    UserId = userId,
                    CartItems = new List<CartItem>()
                };

                await _unitOfWork.Carts.AddAsync(cart);
            }

            var existingItem = await _unitOfWork.CartItems
                .GetAsync(
                filter: ci => ci.CartId == cart.Id && ci.ProductId == productId
                );

            if (existingItem != null)
            {
                existingItem.Quantity += quantity;
            }
            else
            {
                var cartItem = new CartItem
                {
                    ProductId = productId,
                    Quantity = quantity,
                    CartId = cart.Id
                };

                await _unitOfWork.CartItems.AddAsync(cartItem);
            }

            await _unitOfWork.SaveChangesAsync();
        }

        public async Task ClearCart(string userId)
        {
            var cart = await _unitOfWork.Carts
                .GetAsync(
                filter: c => c.UserId == userId
                );

            if (cart == null)
                throw new Exception("Cart not found");

            var cartItems = await _unitOfWork.CartItems
                .GetAllAsync(
                filter: ci => ci.CartId == cart.Id
                );

            if (cartItems == null)
                throw new Exception("No items found in the cart");

            foreach (var item in cartItems)
                await _unitOfWork.CartItems.Delete(item);
            
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<CartDto> GetUserCart(string userId)
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

            if (cart == null)
            {
                return new CartDto
                {
                    Items = new List<CartItemDto>(),
                    TotalPrice = 0
                };
            }

            return cart;
        }

        public async Task RemoveFromCart(string userId, Guid productId)
        {
            var cart = await _unitOfWork.Carts
                .GetAsync(
                filter: c => c.UserId == userId
                );

            if (cart == null)
                throw new Exception("Cart not found");

            var existingItem = await _unitOfWork.CartItems
                .GetAsync(
                filter: ci => ci.CartId == cart.Id && ci.ProductId == productId
                );

            if (existingItem == null)
                throw new Exception("Cart item not found");

            await _unitOfWork.CartItems.Delete(existingItem);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
