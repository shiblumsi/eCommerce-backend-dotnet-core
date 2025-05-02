using eCommerce_backend.BLL.Interfaces;
using eCommerce_backend.Data.DAL.Interfaces;
using eCommerce_backend.Data.Entities;
using eCommerce_backend.Helper;
using eCommerce_backend.Models.Request;
using eCommerce_backend.Models.Response;
using static eCommerce_backend.Models.Response.CategoryWithSubCategoryDto;

namespace eCommerce_backend.BLL.Implementations
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        public CategoryService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        private string GenerateSlug(string name)
        {
            return name.ToLower().Replace(" ", "-").Replace("'", "");
        }

        public async Task<Category> AddCategoryAsync(CategoryCreateDto dto)
        {
            var imageUrl = await FileHelper.SaveImageAsync(dto.IconImage, "uploads/image/category");
            var category = new Category
            {
                Name = dto.Name,
                IconUrl = imageUrl,
                ParentCategoryId = dto.ParentCategoryId,
                Slug = GenerateSlug(dto.Name),
            };

            return await _categoryRepository.AddCategoryAsync(category);
        }

        

        public async Task<Category?> GetCategoryByIdAsync(int id)
        {
            return await _categoryRepository.GetCategoryByIdAsync(id);
        }


        public async Task<List<CategoryWithSubCategoryDto>> GetAllCategoryAsync()
        {
            var allCategories = await _categoryRepository.GetAllCategoryAsync();

            // Only root categories
            var rootCategories = allCategories
                .Where(c => c.ParentCategoryId == null)
                .Select(c => MapToDtoRecursive(c, allCategories))
                .ToList();

            return rootCategories;
        }

        private CategoryWithSubCategoryDto MapToDtoRecursive(Category category, List<Category> allCategories)
        {
            return new CategoryWithSubCategoryDto
            {
                Id = category.Id,
                Name = category.Name,
                Slug = category.Slug,
                IconUrl = category.IconUrl,
                SubCategories = allCategories
                    .Where(c => c.ParentCategoryId == category.Id)
                    .Select(c => MapToDtoRecursive(c, allCategories))
                    .ToList()
            };
        }

        public async Task<Category?> UpdateCategoryAsync(int id, CategoryUpdateDto categoryUpdateDto)
        {
            var category = await _categoryRepository.GetCategoryByIdAsync (id);
            if (category == null) throw new Exception("Category Not found");

            category.Name = categoryUpdateDto.Name;
            category.Slug = categoryUpdateDto.Slug;
            category.IconUrl = categoryUpdateDto.IconUrl;
            category.ParentCategoryId = categoryUpdateDto.ParentCategoryId;

            await _categoryRepository.UpdateCategoryAsync (category);
            return category;
        }

        public async Task DeleteAsync(int id)
        {
            var category = await _categoryRepository.GetCategoryByIdAsync(id);
            if (category == null) throw new Exception("Not found");

            await _categoryRepository.DeleteAsync(category);
        }

    }
}
