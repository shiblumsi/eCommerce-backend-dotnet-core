namespace eCommerce_backend.Models.Response
{
    public class CartWithItemsDto
    {
        public int Id { get; set; }
        public List<CartItemDto>? Items { get; set; }
    }
}
