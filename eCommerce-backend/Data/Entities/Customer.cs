namespace eCommerce_backend.Data.Entities
{
    public class Customer
    {
        public int Id { get; set; }

        public int UserId {  get; set; }
        public User User { get; set; }

        public string FullName { get; set; }
        public string? Address { get; set; }

        public DateTime CreatedDate { get; set; }= DateTime.Now;
    }
}
