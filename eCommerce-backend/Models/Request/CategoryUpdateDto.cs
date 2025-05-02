namespace eCommerce_backend.Models.Request
{
    public class CategoryUpdateDto
    {
        public string Name { get; set; }
        public string Slug { get; set; }
        public string? IconUrl { get; set; }
        public int? ParentCategoryId { get; set; }
    }
}
