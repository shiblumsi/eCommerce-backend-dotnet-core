namespace eCommerce_backend.Models.Response
{
    public class CartItemDto
    {
        public int Id { get; set; }
        public int ProductVarientId { get; set; }
        public int Quantity { set; get; }
        public decimal Price { set; get; }
        
        
    }
}
