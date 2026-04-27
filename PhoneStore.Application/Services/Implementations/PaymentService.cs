using FluentValidation;
using Microsoft.AspNetCore.Identity;
using PhoneStore.Application.DTOs.Payment;
using PhoneStore.Application.Services.Interfaces;
using PhoneStore.Domain.Entities;
using PhoneStore.Domain.Enums;
using PhoneStore.Domain.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace PhoneStore.Application.Services.Implementations
{
    public class PaymentService : IPaymentService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IValidator<PaymentRequestDto> _validator;

        public PaymentService(IUnitOfWork unitOfWork, IValidator<PaymentRequestDto> validator)
        {
            _unitOfWork = unitOfWork;
            _validator = validator;
        }

        public async Task<List<PaymentDto>> GetByOrderId(Guid orderId)
        {
            var payments = await _unitOfWork.Payments
                .GetAllAsync(
                filter: p => p.OrderId == orderId,
                selector: p => new PaymentDto
                {
                    Id = p.Id,
                    PaymentMethod = p.PaymentMethod,
                    Status = p.Status.ToString(),
                    Amount = p.Amount
                });

            return payments;
        }

        public async Task Pay(Guid orderId, PaymentRequestDto model)
        {
            var result = _validator.Validate(model);

            if(!result.IsValid)
                throw new ValidationException(result.ToString(","));

            var order = await _unitOfWork.Orders.FindByIdAsync(orderId);

            if (order == null)
                throw new Exception("Order not found");

            var payment = new Payment
            {
                OrderId = orderId,
                Amount = order.TotalPrice,
                CreatedAt = DateTime.UtcNow,
                PaymentMethod = model.PaymentMethod
            };

            if(model.PaymentMethod == "Visa" || model.PaymentMethod == "MasterCard")
            {
                payment.Status = PaymentStatus.Paid;
            }
            else
            {
                payment.Status = PaymentStatus.Pending;
            }

            await _unitOfWork.Payments.AddAsync(payment);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
