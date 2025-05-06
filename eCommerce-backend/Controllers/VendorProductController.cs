using eCommerce_backend.BLL.Implementations;
using eCommerce_backend.BLL.Interfaces;
using eCommerce_backend.Data.DAL.Interfaces;
using eCommerce_backend.Data.Entities;
using eCommerce_backend.Models.Request;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace eCommerce_backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Policy = "VendorOnly")]
    public class VendorProductController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly IUserRepository _userRepository;
        public VendorProductController(IProductService productService,IUserRepository userRepository)
        {
            _productService = productService;
            _userRepository = userRepository;
        }

        

        [HttpPost("create")]
        public async Task<IActionResult> ProductCreate([FromForm] ProductWithVarientCreateDto dto)
        {
            var userIdFromToken = User.FindFirst("UserId");
            if (userIdFromToken == null) return Unauthorized("User ID not found in token.");

            int userId = int.Parse(userIdFromToken.Value);

            var vendor = await _userRepository.GetVendorByUserIdAsync(userId);
            if (vendor == null) return NotFound("Vendor Not Found");

            var newProduct = await _productService.AddProductAsync(vendor.Id,dto);
            return Ok(newProduct);
        }



        [HttpGet("all-products")]
        public async Task<IActionResult> GetVendorsAllProduct()
        {
            var userIdFromToken = User.FindFirst("UserId");
            if (userIdFromToken == null) return Unauthorized("User ID not found in token.");

            int userId = int.Parse(userIdFromToken.Value);

            var vendor = await _userRepository.GetVendorByUserIdAsync(userId);
            if (vendor == null) return NotFound("Vendor Not Found");

            var products = await _productService.GetAllProductsForVendorWithVarients(vendor.Id);

            return Ok(products);
        }




        [HttpGet("product/{id}")]
        public async Task<IActionResult> GetVendorProduct(int id)
        {
            var userIdFromToken = User.FindFirst("UserId");
            if (userIdFromToken == null) return Unauthorized("User ID not found in token.");

            int userId = int.Parse(userIdFromToken.Value);

            var vendor = await _userRepository.GetVendorByUserIdAsync(userId);
            if (vendor == null) return NotFound("Vendor Not Found");

            var product = await _productService.GetVendorProductByIdAsync(id, vendor.Id);
            if (product == null)
            {
                return Unauthorized("You are not authorized to View another vendor's product. You can only view your own products.");
            }

            return Ok(product);

        }


        
        [HttpPatch("update-product/{id}")]
        public async Task<IActionResult> UpdateProduct(int id, [FromForm] ProductUpdateDto updateDto)
        {
            var userIdFromToken = User.FindFirst("UserId");
            if (userIdFromToken == null) return Unauthorized("User ID not found in token.");

            int userId = int.Parse(userIdFromToken.Value);

            var vendor = await _userRepository.GetVendorByUserIdAsync(userId);
            if (vendor == null) return NotFound("Vendor Not Found");

            var updatedProduct = await _productService.UpdateProductAsync(id,vendor.Id, updateDto);

            if (updatedProduct == null)
            {
                return Unauthorized("You are not authorized to update this product. You can only update your own products.");
            }
            return Ok(updatedProduct);
        }


        //varients
        [HttpGet("product-varient/{id}")]
        public async Task<IActionResult> GetProductVarient(int id)
        {
            var userIdFromToken = User.FindFirst("UserId");
            if (userIdFromToken == null) return Unauthorized("User ID not found in token.");

            int userId = int.Parse(userIdFromToken.Value);

            var vendor = await _userRepository.GetVendorByUserIdAsync(userId);
            if (vendor == null) return NotFound("Vendor Not Found");

            var pVarient = await _productService.GetProductVariantByIdAsync(id);
            if (pVarient == null) return NotFound("not Found");

            if(pVarient.VendorId != vendor.Id)
            {
                return Unauthorized("You are not authorized to access this variant.");
            }
            return Ok(pVarient);
        }
    }
}
