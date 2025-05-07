using eCommerce_backend.BLL.Interfaces;
using eCommerce_backend.Data.DAL.Interfaces;
using eCommerce_backend.Data.Entities;
using eCommerce_backend.Models.Request;
using eCommerce_backend.Models.Response;

namespace eCommerce_backend.BLL.Implementations
{
    public class CartService : ICartService
    {
        private readonly ICartRepository _cartRepository;
        public CartService(ICartRepository cartRepository)
        {
            _cartRepository = cartRepository;
        }

        public async Task<CartItemDto> ADDItemToCartAsync(CartItemAddToCartDto dto, int customerId)
        {
            var cart = await _cartRepository.GetCartByCustomerIdAsync(customerId)
                ??  await _cartRepository.AddCartAsync(customerId);



            var existingItem = await _cartRepository.GetCartItemAsync(cart.Id, dto.ProductVariantId);

            if (existingItem != null)
            {
                existingItem.Quantity += 1;
                existingItem.UpdatedAt = DateTime.UtcNow;
                var updatedItem =  await _cartRepository.UpdateCartItemAsync(existingItem);
                return new CartItemDto
                {
                    Id = existingItem.Id,
                    ProductVarientId = existingItem.ProductVariantId,
                    Quantity = existingItem.Quantity,
                    Price = existingItem.ProductVariant?.Price ?? 0m
                };
            }

            var newItem = new CartItem
            {
                CartId = cart.Id,
                ProductVariantId = dto.ProductVariantId,
                Quantity = 1,
                CreatedAt = DateTime.UtcNow
            };

            var item =  await _cartRepository.AddCartItemAsync(newItem);
            return new CartItemDto
            {
                Id = item.Id,
                ProductVarientId = item.ProductVariantId,
                Quantity = item.Quantity,
                Price= item.ProductVariant?.Price ?? 0m
            };
        }

        

        public async Task<CartWithItemsDto> GetCustomerCart(int customerId)
        {
            var cart = await _cartRepository.GetCartByCustomerIdAsync(customerId);
            if (cart == null) return null;

            return new CartWithItemsDto
            {
                Id = cart.Id,
                Items = cart.CartItems?.Select(i => new CartItemDto
                {
                    Id = i.Id,
                    ProductVarientId = i.ProductVariantId,
                    Quantity = i.Quantity,
                    Price = i.ProductVariant?.Price ?? 0m
                }).ToList() ?? new List<CartItemDto>()
            };
        }


        public async Task<CartWithItemsDto> ChangeCartItemQuantityAsync(int cartItemId, int change, int customerId)
        {
            var cartItem = await _cartRepository.GetCartItemByIdAsync(cartItemId);
            if (cartItem == null) throw new Exception("not found cartItem");

            cartItem.Quantity += change;

            if(cartItem.Quantity <= 0)
            {
                await _cartRepository.RemoveCartItemAsync(cartItemId);
            }
            else
            {
                await _cartRepository.UpdateCartItemAsync(cartItem);
            }

            var cart = await _cartRepository.GetCartByCustomerIdAsync(customerId);

            return new CartWithItemsDto
            {
                Id = cart.Id,
                Items = cart.CartItems?.Select(i => new CartItemDto
                {
                    Id = i.Id,
                    ProductVarientId = i.ProductVariantId,
                    Quantity = i.Quantity,
                    Price = i.ProductVariant?.Price ?? 0m
                }).ToList()
            };
        }

    }
}
