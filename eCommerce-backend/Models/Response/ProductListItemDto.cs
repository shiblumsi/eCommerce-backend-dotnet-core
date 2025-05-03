namespace eCommerce_backend.Models.Response
{
    public class ProductListItemDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Slug { get; set; }
        public decimal BasePrice { get; set; }
        public string MainImageUrl { get; set; }
    }
}
