using eCommerce_backend.Data.DAL.Interfaces;
using eCommerce_backend.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace eCommerce_backend.Data.DAL.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;
        public UserRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<User?> GetUserByEmailOrPhoneAsync(string emailOrPhone)
        {
            var existingUser = await _context.Users
                .Include(u => u.Customer)
                .Include(u => u.Vendor)
                .FirstOrDefaultAsync(u => u.Email == emailOrPhone || u.PhoneNumber == emailOrPhone);

            return existingUser;
        }

        public async Task AddUserAsync(User user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
        }

        public async Task<Vendor?> GetVendorByUserIdAsync(int userId)
        {
            return await _context.Vendors.FirstOrDefaultAsync(u => u.UserId == userId);
        }

        public async Task<Customer?> GetCustomerByUserIdAsync(int userId)
        {
            return await _context.Customers.FirstOrDefaultAsync(u => u.UserId == userId);
        }
    }
}
