namespace eCommerce_backend.Data.Entities
{
    public class Product
    {
        public int Id { get; set; }

        public string Name { get; set; }
        public string Slug { get; set; }  // auto-generated
        public string? Description { get; set; }

        public string SKU { get; set; } // unique identifier
        public decimal BasePrice { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }

        // Relations
        public int VendorId { get; set; }
        public Vendor Vendor { get; set; }

        public int CategoryId { get; set; }
        public Category Category { get; set; }

        public ICollection<ProductVariant> Variants { get; set; }
        public ProductImage Images { get; set; }
    }
}
