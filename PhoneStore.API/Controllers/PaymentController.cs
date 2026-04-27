using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PhoneStore.Application.DTOs.Payment;
using PhoneStore.Application.Services.Interfaces;
using PhoneStore.Domain.Constants;

namespace PhoneStore.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly IPaymentService _paymentService;

        public PaymentController(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }

        [HttpGet("{orderId}")]
        [Authorize(Roles = Role.User)]
        public async Task<IActionResult> GetByOrderId(Guid orderId)
        {
            var payments = await _paymentService.GetByOrderId(orderId);
            return Ok(payments);
        }

        [HttpPost("{orderId}")]
        [Authorize(Roles = Role.User)]
        public async Task<IActionResult> Pay(Guid orderId, PaymentRequestDto model)
        {
            try
            {
                await _paymentService.Pay(orderId, model);
                return Ok("Payment successful");
            }
            catch (Exception ex)
            {
                return BadRequest(new { errors = ex.Message });
            }
        }
    }
}
