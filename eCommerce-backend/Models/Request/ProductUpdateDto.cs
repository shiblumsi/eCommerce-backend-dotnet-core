namespace eCommerce_backend.Models.Request
{
    public class ProductUpdateDto
    {
        public string? Name { get; set; }
        public string? Description { get; set; }

        public decimal? BasePrice { get; set; }
        

        public int? CategoryId { get; set; }

        public IFormFile? ProductImage { get; set; }
    }
}
