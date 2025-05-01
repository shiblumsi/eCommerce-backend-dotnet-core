namespace eCommerce_backend.Data.Entities
{

    public enum VendorStatus
    {
        Pending,
        Approved,
        Rejected
    }
    public class Vendor
    {
        public int Id { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }

        public string ShopName { get; set; }
        public string? Description { get; set; }
        public string? ShopLogoUrl { get; set; }

        public VendorStatus Status { get; set; } = VendorStatus.Pending;
    }
}
