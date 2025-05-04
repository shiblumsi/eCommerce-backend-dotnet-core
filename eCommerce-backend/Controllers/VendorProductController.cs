using eCommerce_backend.BLL.Interfaces;
using eCommerce_backend.Models.Request;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace eCommerce_backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VendorProductController : ControllerBase
    {
        private readonly IProductService _productService;
        public VendorProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpPost("create")]
        public async Task<IActionResult> ProductCreate([FromForm] ProductCreateDto dto)
        {
            var newProduct = await _productService.AddProductAsync(dto);
            return Ok(newProduct);
        }
    }
}
