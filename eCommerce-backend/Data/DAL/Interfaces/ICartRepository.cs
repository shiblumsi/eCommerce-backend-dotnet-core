using eCommerce_backend.Data.Entities;

namespace eCommerce_backend.Data.DAL.Interfaces
{
    public interface ICartRepository
    {
        Task<Cart> GetCartByIdAsync(int cartId);
        Task<Cart?> GetCartByCustomerIdAsync(int customerId);

        Task<Cart> AddCartAsync(int customerId);
        Task UpdateCartAsync(Cart cart);
        Task DeleteCartAsync(int cartId);
        Task<CartItem> AddCartItemAsync(CartItem cartItem);
        Task RemoveCartItemAsync(int cartItemId);
        Task<CartItem> GetCartItemByIdAsync(int cartItemId);

        Task<CartItem> UpdateCartItemAsync(CartItem cartItem);

        Task<CartItem?> GetCartItemAsync(int cartId, int productVariantId);
    }
}
