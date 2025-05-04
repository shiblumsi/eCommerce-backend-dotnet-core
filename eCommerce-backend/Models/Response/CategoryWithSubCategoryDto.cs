namespace eCommerce_backend.Models.Response
{
    public class CategoryWithSubCategoryDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Slug { get; set; }
        public string? IconUrl { get; set; }

        public List<CategoryWithSubCategoryDto> SubCategories { get; set; } = new();
    }
}
