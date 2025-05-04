using eCommerce_backend.BLL.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace eCommerce_backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerProductController : ControllerBase
    {
        private readonly IProductService _productService;
        public CustomerProductController(IProductService productService)
        {
            _productService = productService;
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
    }
}
