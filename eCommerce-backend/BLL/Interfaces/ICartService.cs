using eCommerce_backend.Data.Entities;
using eCommerce_backend.Models.Request;
using eCommerce_backend.Models.Response;

namespace eCommerce_backend.BLL.Interfaces
{
    public interface ICartService
    {
        Task<CartItemDto> ADDItemToCartAsync(CartItemAddToCartDto dto, int customerId);
        Task<CartWithItemsDto> GetCustomerCart(int customerId);

        Task<CartWithItemsDto> ChangeCartItemQuantityAsync(int cartItemId, int change, int customerId);

    }
}
