using PhoneStore.Application.DTOs.Payment;
using System;
using System.Collections.Generic;
using System.Text;

namespace PhoneStore.Application.Services.Interfaces
{
    public interface IPaymentService
    {
        Task Pay(Guid orderId, PaymentRequestDto model);
        Task<List<PaymentDto>> GetByOrderId(Guid orderId);
    }
}
