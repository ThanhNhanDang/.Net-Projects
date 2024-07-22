namespace OnlineStoreManagement.Entities
{
    public class Customer
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string? Email { get; set; } = null!;
        public string? Address { get; set; }
        public string? PhoneNumber { get; set; }
        public ICollection<Order> Orders { get; set; } = null!;
    }
}
