using PhoneStore.Application.DTOs.Order;
using System;
using System.Collections.Generic;
using System.Text;

namespace PhoneStore.Application.Services.Interfaces
{
    public interface IOrderService
    {
        Task CreateOrder(string userId);
        Task<List<OrderDto>> GetAllOrders(string userId, int pageNumber = 1);
    }
}
