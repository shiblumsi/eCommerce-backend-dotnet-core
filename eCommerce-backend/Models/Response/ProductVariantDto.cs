namespace eCommerce_backend.Models.Response
{
    public class ProductVariantDto
    {
        public int Id { get; set; }
        public string? Size { get; set; }
        public string? Color { get; set; }
        public decimal? Price { get; set; }
        public int Stock { get; set; }
        public bool IsActive { get; set; }
        public string VarientImage { get; set; }
        public int VendorId { get; set; }
    }
}
