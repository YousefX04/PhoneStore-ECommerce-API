using System;
using System.Collections.Generic;
using System.Text;

namespace PhoneStore.Application.DTOs.Order
{
    public class OrderDto
    {
        public Guid Id { get; set; }
        public List<OrderItemDto> OrderItems { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
