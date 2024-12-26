using api_portal.Dto;
using api_portal.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;

namespace api_portal.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class CustomerController(CustomerService customerService) : ControllerBase
    {
        private readonly CustomerService _customerService = customerService;

        [HttpPost("register")]
        public IActionResult Register([FromBody] CustomerRequest request)
        {
            if (string.IsNullOrEmpty(request.Email) || string.IsNullOrEmpty(request.Password) || string.IsNullOrEmpty(request.Name))
            {
                return BadRequest("Name, Email, and Password are required.");
            }

            var isRegistered = _customerService.Register(request);

            if (!isRegistered)
            {
                return Conflict("A user with this email already exists.");
            }

            return Ok("Registration successful.");
        }

        [HttpGet]
        public IActionResult FindAll()
        {
            var customers = _customerService.FindAll();
            return Ok(customers);
        }

        [HttpGet("{id}")]
        public IActionResult FindById(int id)
        {
            var customer = _customerService.FindById(id);

            if (customer == null)
            {
                return NotFound($"Customer with ID {id} not found.");
            }

            return Ok(customer);
        }
    }
}
