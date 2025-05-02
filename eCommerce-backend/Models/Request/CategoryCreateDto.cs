namespace eCommerce_backend.Models.Request
{
    public class CategoryCreateDto
    {
        public string Name { get; set; }
        public int? ParentCategoryId { get; set; }
        public IFormFile? IconImage { get; set; } // image file
    }
}
