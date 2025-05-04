using eCommerce_backend.Models.Request;
using eCommerce_backend.Models.Response;

namespace eCommerce_backend.BLL.Interfaces
{
    public interface IProductService
    {
        //---------------------Customer/Public ------------------------

        public Task<List<ProductListDto>> GetAllProductAsync();
        Task<ProductDto?> GetProductByIdAsync(int id);


        // -------------------- Vendor Endpoints --------------------

        Task<ProductDto> AddProductAsync(ProductCreateDto dto);
    }
}
