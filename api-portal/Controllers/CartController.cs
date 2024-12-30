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

            _cartService.AddToCart(request);
            return Ok();
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

        [HttpDelete("remove/{userId}/{bookId}")]
        public IActionResult RemoveFromCart(int userId, int bookId)
        {
            var isRemoved = _cartService.RemoveFromCart(userId, bookId);

            if (!isRemoved)
            {
                return NotFound($"Item with BookID {bookId} not found in the cart for UserID {userId}.");
            }

            return Ok($"Item with BookID {bookId} has been removed from the cart for UserID {userId}.");
        }
    }
}
