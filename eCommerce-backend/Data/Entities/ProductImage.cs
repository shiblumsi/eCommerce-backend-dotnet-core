namespace eCommerce_backend.Data.Entities
{
    public class ProductImage
    {
        public int Id { get; set; }

        public int? ProductId { get; set; }
        public Product? Product { get; set; }

        public int? ProductVarientId { get; set; }
        public ProductVariant? ProductVariant { get; set; }

        public string? ImageUrl { get; set; }
    }
}
