using eCommerce_backend.BLL.Interfaces;
using eCommerce_backend.Models.Request;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace eCommerce_backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        public AuthController( IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("customer-registration")]
        public async Task<IActionResult> CustomerRegistration(CustomerRegistrationDto dto)
        {
            var token = await _authService.CustomerRegisterAsync(dto);
            return Ok(new { Token = token });
        }

        [HttpPost("vendor-registration")]
        public async Task<IActionResult> VendorRegistration(VendorRegistrationDto dto)
        {
            var token = await _authService.VendorRegisterAsync(dto);
            return Ok(new { Token = token });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto dto)
        {
            var token = await _authService.LoginAsync(dto);
            return Ok(new { Token = token });
        }
    }
}
