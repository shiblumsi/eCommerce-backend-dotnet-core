using eCommerce_backend.BLL.Interfaces;
using eCommerce_backend.Data.DAL.Interfaces;
using eCommerce_backend.Models.Request;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace eCommerce_backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerProductController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly ICartService _cartService;
        private readonly IUserRepository _userRepository;
        public CustomerProductController(IProductService productService, ICartService cartService, IUserRepository userRepository)
        {
            _productService = productService;
            _cartService = cartService;
            _userRepository = userRepository;
        }

        [HttpGet("all-product")]
        public async Task<IActionResult> GetAllProuct()
        {
            var allProduct = await _productService.GetAllProductAsync();
            return Ok(allProduct);
        }

        [HttpGet("product/{id}")]
        public async Task<IActionResult> GetProductById(int id)
        {
            var product = await _productService.GetProductByIdAsync(id);
            return Ok(product);
        }

        [Authorize(Policy = "CustomerOnly")]
        [HttpPost("add-to-cart")]
        public async Task<IActionResult> AddToCart(CartItemAddToCartDto dto)
        {
            var userId = int.Parse(User.FindFirst("UserId")?.Value);
            var customer = await _userRepository.GetCustomerByUserIdAsync(userId);
            var item = await _cartService.ADDItemToCartAsync(dto, customer.Id);
            return Ok(item);
        }


        [Authorize(Policy = "CustomerOnly")]
        [HttpGet("get-cart")]
        public async Task<IActionResult> GetCart()
        {
            var userId = int.Parse(User.FindFirst("UserId")?.Value);
            var customer = await _userRepository.GetCustomerByUserIdAsync(userId);
            var cart = await _cartService.GetCustomerCart(customer.Id);
            return Ok(cart);
        }

    }
}
