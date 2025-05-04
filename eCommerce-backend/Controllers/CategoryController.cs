using eCommerce_backend.BLL.Interfaces;
using eCommerce_backend.Data.Entities;
using eCommerce_backend.Models.Request;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace eCommerce_backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateCategory(CategoryCreateDto dto)
        {
            var createdCategory = await _categoryService.AddCategoryAsync(dto);
            return Ok(createdCategory);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCategoies()
        {
            var result = await _categoryService.GetAllCategoryAsync();
            return Ok(result);
        }

        [HttpGet("category/{id}")]
        public async Task<IActionResult> GetCategory(int id)
        {
            var category = await _categoryService.GetCategoryByIdAsync(id);
            return Ok(category);
        }

        [HttpPut("update/{id}")]
        public async Task<IActionResult> UpdateCategory(int id, CategoryUpdateDto categoryUpdateDto)
        {
            var result = await _categoryService.UpdateCategoryAsync(id, categoryUpdateDto);
            return Ok(result);
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _categoryService.DeleteAsync(id);
            return Ok("204 No Content");
        }

    }
}
