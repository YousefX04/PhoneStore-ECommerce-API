using PhoneStore.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace PhoneStore.Application.DTOs.Payment
{
    public class PaymentDto
    {
        public Guid Id { get; set; }
        public string PaymentMethod { get; set; }
        public string Status { get; set; }
        public decimal Amount { get; set; }
    }
}
