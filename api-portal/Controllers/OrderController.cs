using api_portal.Dto;
using api_portal.Services;
using Microsoft.AspNetCore.Mvc;

namespace api_portal.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly OrderService _orderService;

        public OrderController(OrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpPost]
        public IActionResult CheckoutCart([FromBody] CheckoutRequest request)
        {
            if (string.IsNullOrEmpty(request.PaymentMethod))
            {
                return BadRequest("Payment method is required.");
            }

            var order = _orderService.CheckoutCart(request);

            if (order == null)
            {
                return BadRequest("Cart is empty or invalid.");
            }

            return Ok();
        }

        [HttpPut("{orderId}/status")]
        public IActionResult UpdateOrderStatus(int orderId, [FromBody] UpdateOrderStatusRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.NewStatus))
            {
                return BadRequest("Order status cannot be empty.");
            }

            var isUpdated = _orderService.UpdateOrderStatus(orderId, request.NewStatus);

            if (!isUpdated)
            {
                return NotFound($"Order with ID {orderId} was not found.");
            }

            return Ok($"Order ID {orderId} status updated to '{request.NewStatus}'.");
        }

        [HttpGet]
        public IActionResult FindAllOrders()
        {
            var orders = _orderService.FindAllOrders();
            return Ok(orders);
        }

        [HttpGet("user/{userId}")]
        public IActionResult FindOrdersByUserId(int userId)
        {
            var orders = _orderService.FindOrdersByUserId(userId);

            if (!orders.Any())
            {
                return NotFound($"No orders found for user ID {userId}.");
            }

            return Ok(orders);
        }

        [HttpGet("status/{status}")]
        public IActionResult FindOrdersByStatus(string status)
        {
            var orders = _orderService.FindOrdersByStatus(status);

            if (!orders.Any())
            {
                return NotFound($"No orders found with status '{status}'.");
            }

            return Ok(orders);
        }

        [HttpDelete("{orderId}")]
        public IActionResult RemoveOrderById(int orderId)
        {
            var result = _orderService.RemoveOrderById(orderId);

            if (!result)
            {
                return NotFound($"Order with ID {orderId} not found.");
            }

            return Ok($"Order with ID {orderId} has been removed successfully.");
        }

    }

    public class UpdateOrderStatusRequest
    {
        public string NewStatus { get; set; }
    }
}
