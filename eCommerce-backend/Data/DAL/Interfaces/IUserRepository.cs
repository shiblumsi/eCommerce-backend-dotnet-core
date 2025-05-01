using eCommerce_backend.Data.Entities;

namespace eCommerce_backend.Data.DAL.Interfaces
{
    public interface IUserRepository
    {
        public Task<User?> GetUserByEmailOrPhoneAsync(string emailOrPhone);
        public Task AddUserAsync(User user);
    }
}
