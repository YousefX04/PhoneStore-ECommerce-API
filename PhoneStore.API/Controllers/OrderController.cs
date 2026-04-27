using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PhoneStore.Application.Services.Interfaces;
using PhoneStore.Domain.Constants;

namespace PhoneStore.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpGet("{userId}")]
        [Authorize(Roles = Role.User)]
        public async Task<IActionResult> GetAllOrders(string userId, int pageNumber = 1)
        {
            try
            {
                var orders = await _orderService.GetAllOrders(userId, pageNumber);
                return Ok(orders);
            }
            catch (Exception ex)
            {
                return BadRequest(new { errors = ex.Message });
            }
        }
        
        [HttpPost]
        [Authorize(Roles = Role.User)]
        public async Task<IActionResult> CreateOrder(string userId)
        {
            try
            {
                await _orderService.CreateOrder(userId);
                return Ok("Order created successfully");
            }
            catch (Exception ex)
            {
                return BadRequest(new { errors = ex.Message });
            }
        }
    }
}
