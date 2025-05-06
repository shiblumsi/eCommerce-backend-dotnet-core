using eCommerce_backend.Models.Request;
using eCommerce_backend.Models.Response;

namespace eCommerce_backend.BLL.Interfaces
{
    public interface IProductService
    {
        //---------------------Customer/Public ------------------------

        public Task<List<ProductListDto>> GetAllProductAsync();
        Task<ProductWithVarientsDto?> GetProductByIdAsync(int id);


        // -------------------- Vendor Endpoints --------------------

        Task<ProductWithVarientsDto> AddProductAsync(int vendorId, ProductWithVarientCreateDto dto);
        Task<List<ProductWithVarientsDto>> GetAllProductsForVendorWithVarients(int vendorId);
        Task<ProductWithVarientsDto?> GetVendorProductByIdAsync(int id, int vendorId);
        Task<ProductUpdateDto?> UpdateProductAsync(int productId,int vendorId, ProductUpdateDto updateDto);
    }
}
