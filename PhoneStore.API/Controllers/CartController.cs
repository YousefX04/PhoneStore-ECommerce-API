using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PhoneStore.Application.Services.Interfaces;
using PhoneStore.Domain.Constants;

namespace PhoneStore.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly ICartService _cartService;

        public CartController(ICartService cartService)
        {
            _cartService = cartService;
        }

        [HttpGet("{userId}")]
        [Authorize(Roles = Role.User)]
        public async Task<IActionResult> GetUserCart(string userId)
        {
            try
            {
                var cart = await _cartService.GetUserCart(userId);
                return Ok(cart);
            }
            catch (Exception ex)
            {
                return BadRequest(new { errors = ex.Message });
            }
        }

        [HttpPost("{userId}")]
        [Authorize(Roles = Role.User)]
        public async Task<IActionResult> AddToCart(string userId, Guid productId, int quantity)
        {
            try
            {
                await _cartService.AddToCart(userId, productId, quantity);
                return Ok("Product added to cart successfully.");
            }
            catch (Exception ex)
            {
                return BadRequest(new { errors = ex.Message });
            }
        }

        [HttpDelete("{userId}/items/{productId}")]
        [Authorize(Roles = Role.User)]
        public async Task<IActionResult> RemoveFromCart(string userId, Guid productId)
        {
            try
            {
                await _cartService.RemoveFromCart(userId, productId);
                return Ok("Product removed from cart successfully.");
            }
            catch (Exception ex)
            {
                return BadRequest(new { errors = ex.Message });
            }
        }

        [HttpDelete("{userId}")]
        [Authorize(Roles = Role.User)]
        public async Task<IActionResult> ClearCart(string userId)
        {
            try
            {
                await _cartService.ClearCart(userId);
                return Ok("Cart cleared successfully.");
            }
            catch (Exception ex)
            {
                return BadRequest(new { errors = ex.Message });
            }
        }
    }
}
