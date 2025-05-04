namespace eCommerce_backend.Models.Response
{
    public class ProductListDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal BasePrice { get; set; }
        public string ProductImage { get; set; }
    }
}
