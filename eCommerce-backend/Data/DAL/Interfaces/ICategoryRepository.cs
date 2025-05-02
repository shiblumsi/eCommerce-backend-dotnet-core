using eCommerce_backend.Data.Entities;

namespace eCommerce_backend.Data.DAL.Interfaces
{
    public interface ICategoryRepository
    {
        public Task<Category> AddCategoryAsync(Category category);
        public Task<List<Category>> GetAllCategoryAsync();
        public Task<Category?> GetCategoryByIdAsync(int id);

        public Task<Category?> UpdateCategoryAsync(Category category);
        public Task DeleteAsync(Category category);
    }
}
