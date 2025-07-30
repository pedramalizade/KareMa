namespace KareMa.Domain.Core.Entities
{
    public class Customer
    {
        public int Id { get; set; }
        public AppUser AppUser { get; set; }
        public int AppUserId { get; set; }
        [MaxLength(20)]
        public string FirstName { get; set; }
        public string? Image { get; set; }
        [MaxLength(50)]
        public string LastName { get; set; }
        public GenderEnum? Gender { get; set; }
        [MaxLength(11)]
        public string? PhoneNumber { get; set; }
        public decimal Balance { get; set; }
        [MaxLength(16)]
        public string? BankCardNumber { get; set; }
        public Address? Addresses { get; set; }
        public List<Comment>? Comments { get; set; }
        public List<Order>? Orders { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public bool IsDeleted { get; set; } = false;

    }
}
