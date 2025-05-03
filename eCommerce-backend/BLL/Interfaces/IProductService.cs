using eCommerce_backend.Models.Request;
using eCommerce_backend.Models.Response;

namespace eCommerce_backend.BLL.Interfaces
{
    public interface IProductService
    {
        // -------------------- Vendor Endpoints --------------------

        Task<ProductDto> AddProductAsync(ProductCreateDto dto);
    }
}
