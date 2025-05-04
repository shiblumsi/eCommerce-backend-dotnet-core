using eCommerce_backend.Data.Entities;

namespace eCommerce_backend.Models.Request
{
    public class ProductCreateDto
    {
        public string Name { get; set; }
        public string Slug { get; set; }  
        public string? Description { get; set; }

        public string SKU { get; set; } 
        public decimal BasePrice { get; set; }

        public int VendorId { get; set; }
        public int CategoryId { get; set; }

        public List<ProductVariantCreateDto> Variants { get; set; }
        public IFormFile ProductImage { get; set; }
    }
}
