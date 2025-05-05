using eCommerce_backend.Data.Entities;
using eCommerce_backend.Models.Request;
using eCommerce_backend.Models.Response;

namespace eCommerce_backend.Data.DAL.Interfaces
{
    public interface IProductRepository
    {
        // -------------------- Public (Customer-facing) Endpoints --------------------
        Task<List<Product>> GetAllProductAsync();//
        Task<Product?> GetProductByIdAsync(int id);//


        // -------------------- Vendor Endpoints --------------------

        Task<Product> AddProductAsync(Product product);  //
        Task<List<Product>> GetAllProductsForVendorWithVarients(int vendorId); //
        Task<Product?> GetVendorProductByIdAsync(int id);//
        Task<Product> UpdateProductAsync(Product product);
        Task DeleteProductAsync(int id);


        // ----------- Variants (Vendor) -----------

        Task<ProductVariant> AddProductVariantAsync(ProductVariant variant);//
        Task<ProductVariant?> GetProductVariantByIdAsync(int variantId);
        Task UpdateProductVariantAsync(ProductVariant variant);
        Task DeleteProductVariantAsync(int variantId);


        // ----------- Images (Vendor) -----------

        Task<ProductImage> AddProductImageAsync(ProductImage image);//



    }

}
