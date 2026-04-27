using FluentValidation;
using PhoneStore.Application.DTOs.Payment;
using System;
using System.Collections.Generic;
using System.Text;

namespace PhoneStore.Application.Validators.Payment
{
    public class PaymentRequestDtoValidator : AbstractValidator<PaymentRequestDto>
    {
        private bool BeValidMethod(string method)
        {
            var validMethods = new[] { "Visa", "MasterCard", "Cash" };
            return validMethods.Contains(method);
        }

        public PaymentRequestDtoValidator()
        {
            RuleFor(x => x.PaymentMethod)
                .NotEmpty().WithMessage("Payment method is required")
                .MaximumLength(50)
                .Must(BeValidMethod)
                .WithMessage("Invalid payment method");

        }
    }
}
