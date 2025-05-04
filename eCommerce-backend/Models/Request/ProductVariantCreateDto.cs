using eCommerce_backend.Models.Response;

namespace eCommerce_backend.Models.Request
{
    public class ProductVariantCreateDto
    {
        public string? Size { get; set; }
        public string? Color { get; set; }
        public decimal? Price { get; set; }
        public int Stock { get; set; }
        public IFormFile? ProductVarientImage { get; set; }
    }
}
