using eCommerce_backend.BLL.Interfaces;
using eCommerce_backend.Data.DAL.Interfaces;
using eCommerce_backend.Data.Entities;
using eCommerce_backend.Models.Request;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace eCommerce_backend.BLL.Implementations
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _authRepository;
        private readonly IConfiguration _config;
        public AuthService(IUserRepository authRepository, IConfiguration config)
        {
            _authRepository = authRepository;
            _config = config;
        }


        private string GenerateToken(User user)
        {
            var claims = new[]
            {
                new Claim("UserId", user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.Email?.ToString() ?? ""),
                new Claim("PhoneNumber", user.PhoneNumber ?? ""),
                new Claim("role", user.Role.ToString()),
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
            issuer: _config["Jwt:Issuer"],
            audience: _config["Jwt:Audience"],
            claims: claims,
            expires: DateTime.UtcNow.AddDays(7),
            signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }


        public async Task<string> CustomerRegisterAsync(CustomerRegistrationDto dto)
        {
            var existingUser = await _authRepository.GetUserByEmailOrPhoneAsync(dto.PhoneNumber);
            if (existingUser != null) throw new Exception("User already exists.");

            var newUser = new User
            {
                PhoneNumber = dto.PhoneNumber,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password),
                Email = dto.Email,
                Role = UserRole.Customer,
                Customer = new Customer
                {
                    FullName = dto.FullName,
                    Address = dto.Address,
                }
            };

            await _authRepository.AddUserAsync(newUser);

            return GenerateToken(newUser);
        }

        
        public async Task<string> VendorRegisterAsync(VendorRegistrationDto dto)
        {
            var existingUser = await _authRepository.GetUserByEmailOrPhoneAsync(dto.Email);
            if (existingUser != null) throw new Exception("User already exist");

            var newUser = new User
            {
                Email = dto.Email,
                PhoneNumber = dto.PhoneNumber,
                Role = UserRole.Vendor,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password),
                Vendor = new Vendor
                {
                    ShopLogoUrl = dto.ShopLogoUrl,
                    ShopName = dto.ShopName,
                    Description = dto.Description,
                }
            };

            await _authRepository.AddUserAsync(newUser);

            return GenerateToken(newUser);
        }


        public async Task<string> LoginAsync(LoginDto dto)
        {
            var user = await _authRepository.GetUserByEmailOrPhoneAsync(dto.PhoneOrEmail);
            if (user == null || !BCrypt.Net.BCrypt.Verify(dto.Password, user.PasswordHash))
                throw new Exception("Invalid credintials!");
            return GenerateToken(user);
        }
    }
}
