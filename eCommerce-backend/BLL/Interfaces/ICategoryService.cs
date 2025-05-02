using eCommerce_backend.Data.Entities;
using eCommerce_backend.Models.Request;
using eCommerce_backend.Models.Response;

namespace eCommerce_backend.BLL.Interfaces
{
    public interface ICategoryService
    {
        public Task<Category> AddCategoryAsync(CategoryCreateDto dto);
        public Task<List<CategoryWithSubCategoryDto>> GetAllCategoryAsync();
        public Task<Category?> GetCategoryByIdAsync(int id);

        public Task<Category> UpdateCategoryAsync(int id, CategoryUpdateDto categoryUpdateDto);

        Task DeleteAsync(int id);
    }
}
