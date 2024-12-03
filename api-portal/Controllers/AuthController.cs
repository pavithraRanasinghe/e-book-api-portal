using api_portal.Services;
using Microsoft.AspNetCore.Mvc;

namespace api_portal.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController(AuthService authService) : ControllerBase
    {
        private readonly AuthService _authService = authService;

        [HttpPost]
        public IActionResult Login([FromBody] LoginRequest request)
        {
            if (string.IsNullOrEmpty(request.Email) || string.IsNullOrEmpty(request.Password))
            {
                throw new BadHttpRequestException("Email and Password are required.");
            }

            var token = _authService.Login(request.Email, request.Password);

            if (token == null)
            {
                throw new UnauthorizedAccessException("Invalid credentials.");
            }

            return Ok(new { Token = token });
        }
    }

    public class LoginRequest
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
}
