using eCommerce_backend.Models.Request;

namespace eCommerce_backend.BLL.Interfaces
{
    public interface IAuthService
    {
        public Task<string> CustomerRegisterAsync(CustomerRegistrationDto dto);
        public Task<string> VendorRegisterAsync(VendorRegistrationDto dto);

        public Task<string> LoginAsync(LoginDto dto);
    }
}
