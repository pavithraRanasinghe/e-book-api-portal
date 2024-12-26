using api_portal.Dto;
using api_portal.Services;
using Microsoft.AspNetCore.Mvc;

namespace api_portal.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CartController : ControllerBase
    {
        private readonly CartService _cartService;

        public CartController(CartService cartService)
        {
            _cartService = cartService;
        }

        [HttpPost]
        public IActionResult AddToCart([FromBody] CartRequest request)
        {
            if (request == null)
            {
                return BadRequest("Invalid cart data.");
            }

            var cart = _cartService.AddToCart(request);
            return Ok(cart);
        }

        [HttpGet("user/{userId}")]
        public IActionResult GetCartByUserId(int userId)
        {
            var cart = _cartService.GetCartByUserId(userId);

            if (cart == null)
            {
                return NotFound($"Cart for user ID {userId} not found.");
            }

            return Ok(cart);
        }
    }
}
