namespace eCommerce_backend.Data.Entities
{
    public class ProductVariant
    {
        public int Id { get; set; }

        public int ProductId { get; set; }
        public Product Product { get; set; }

        public string? Size { get; set; }  // optional
        public string? Color { get; set; } // optional

        public decimal? Price { get; set; }  // override base price
        public int Stock { get; set; }

        public bool IsActive { get; set; } = true;

        public ProductImage ProductImage { get; set; }
    }
}
