using PhoneStore.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace PhoneStore.Domain.Entities
{
    public class Payment
    {
        public Guid Id { get; set; }
        public decimal Amount { get; set; }
        public string PaymentMethod { get; set; } // "Visa", "Cash" , "MasterCard"
        public PaymentStatus Status { get; set; } // Pending, Paid, Failed
        public Order Order { get; set; }
        public Guid OrderId { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
