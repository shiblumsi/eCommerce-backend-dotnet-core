using eCommerce_backend.Data.DAL.Interfaces;
using eCommerce_backend.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace eCommerce_backend.Data.DAL.Repositories
{
    public class CartRepository : ICartRepository
    {

        private readonly AppDbContext _context;
        public CartRepository(AppDbContext context)
        {
            _context = context;
        }


        public async Task<Cart> AddCartAsync(int customerId)
        {
            var cart = new Cart
            {
                CustomerId = customerId
            };
            _context.Carts.Add(cart);
            await _context.SaveChangesAsync();
            return cart;
        }

        public async Task<CartItem> AddCartItemAsync(CartItem cartItem)
        {
            _context.CartItems.Add(cartItem);
            await _context.SaveChangesAsync();
            return cartItem;
        }

        public async Task DeleteCartAsync(int cartId)
        {
            var cart = await _context.Carts.FindAsync(cartId);
            if (cart != null)
            {
                _context.Carts.Remove(cart);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<Cart> GetCartByIdAsync(int cartId)
        {
            return await _context.Carts
                .Include(c => c.CartItems)
                .ThenInclude(ci => ci.ProductVariant)
                .FirstOrDefaultAsync(c => c.Id == cartId);
        }

        public async Task<CartItem> GetCartItemByIdAsync(int cartItemId)
        {
            return await _context.CartItems
                .Include(c=> c.Cart)
                .Include(ci => ci.ProductVariant)
                .FirstOrDefaultAsync(ci => ci.Id == cartItemId);
        }

        public Task<Cart?> GetCartByCustomerIdAsync(int customerId)
        {
            return _context.Carts
                .Include(c => c.CartItems)
                .ThenInclude(c=>c.ProductVariant)
                .FirstOrDefaultAsync(c=> c.CustomerId == customerId);
        }

        public async Task RemoveCartItemAsync(int cartItemId)
        {
            var cartItem = await _context.CartItems.FindAsync(cartItemId);
            if (cartItem != null)
            {
                _context.CartItems.Remove(cartItem);
                await _context.SaveChangesAsync();
            }
        }

        public Task UpdateCartAsync(Cart cart)
        {
            throw new NotImplementedException();
        }

        public async Task<CartItem> UpdateCartItemAsync(CartItem cartItem)
        {
            _context.CartItems.Update(cartItem);
            await _context.SaveChangesAsync();
            return cartItem;
        }


        public async Task<CartItem?> GetCartItemAsync(int cartId, int productVariantId)
        {
            return await _context.CartItems
                .FirstOrDefaultAsync(ci => ci.CartId == cartId && ci.ProductVariantId == productVariantId);
        }


    }
}
