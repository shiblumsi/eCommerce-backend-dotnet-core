using eCommerce_backend.Data.DAL.Interfaces;
using eCommerce_backend.Data.Entities;
using eCommerce_backend.Models.Response;

namespace eCommerce_backend.Data.DAL.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly AppDbContext _context;
        public ProductRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Product> AddProductAsync(Product product)
        {
            _context.Products.Add(product);
            await _context.SaveChangesAsync();
            return product;
        }

        public async Task<ProductVariant> AddProductVariantAsync(ProductVariant variant)
        {
            _context.ProductVariants.Add(variant);
            await _context.SaveChangesAsync();
            return variant;
        }

        public Task DeleteProductAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task DeleteProductVariantAsync(int variantId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<ProductListItemDto>> GetAllAlProductAsync()
        {
            throw new NotImplementedException();
        }

        public Task<ProductDto?> GetProductByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<ProductVariant?> GetProductVariantByIdAsync(int variantId)
        {
            throw new NotImplementedException();
        }

        public Task<Product?> GetVendorProductByIdAsync(int productId, int vendorId)
        {
            throw new NotImplementedException();
        }

        public Task UpdateProductAsync(Product product)
        {
            throw new NotImplementedException();
        }

        public Task UpdateProductVariantAsync(ProductVariant variant)
        {
            throw new NotImplementedException();
        }


        //Image
        public async Task<ProductImage> AddProductImageAsync(ProductImage image)
        {
            _context.ProductImages.Add(image);
            await _context.SaveChangesAsync();
            return image;
        }
    }
}
