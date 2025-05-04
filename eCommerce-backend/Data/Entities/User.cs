namespace eCommerce_backend.Data.Entities
{

    public enum UserRole
    {
        Admin,
        Vendor,
        Customer
    }


    public class User
    {
        public int Id { get; set; }

        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }

        public string? PasswordHash { get; set; }

        public bool IsEmailVerified { get; set; } = false;
        public bool IsPhoneVerified { get; set; } = false;

        public UserRole Role { get; set; }

        public DateTime? CreatedAt { get; set; } = DateTime.UtcNow;

        //Navigation Properties
        public Vendor? Vendor { get; set; }
        public Customer? Customer { get; set; }

    }
}
