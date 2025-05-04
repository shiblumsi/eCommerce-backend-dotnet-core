namespace eCommerce_backend.Models.Request
{
    public class VendorRegistrationDto
    {
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Password { get; set; }

        public string ShopName { get; set; }
        public string? Description { get; set; }
        public string? ShopLogoUrl { get; set; }
    }
}
