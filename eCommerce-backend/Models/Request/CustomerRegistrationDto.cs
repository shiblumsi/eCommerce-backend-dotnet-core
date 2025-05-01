namespace eCommerce_backend.Models.Request
{
    public class CustomerRegistrationDto
    {
        public string? Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Password { get; set; }

        public string FullName { get; set; } 
        public string? Address { get; set; } 

    }
}
