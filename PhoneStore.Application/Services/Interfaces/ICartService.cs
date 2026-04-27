using PhoneStore.Application.DTOs.Cart;
using System;
using System.Collections.Generic;
using System.Text;

namespace PhoneStore.Application.Services.Interfaces
{
    public interface ICartService
    {
        Task AddToCart(string userId, Guid productId, int quantity);
        Task RemoveFromCart(string userId, Guid productId);
        Task ClearCart(string userId);
        Task<CartDto> GetUserCart(string userId);
    }
}
