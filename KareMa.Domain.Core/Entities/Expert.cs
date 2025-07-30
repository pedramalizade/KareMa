namespace KareMa.Domain.Core.Entities
{
    public class Expert
    {
        public int Id { get; set; }
        public AppUser AppUser { get; set; }
        public int AppUserId { get; set; }
        [MaxLength(20)]
        public string FirstName { get; set; }
        [MaxLength(50)]
        public string LastName { get; set; }
        public GenderEnum Gender { get; set; }
        [MaxLength(11)]
        public string? PhoneNumber { get; set; }
        public string? Bio { get; set; }
        public decimal Balance { get; set; }
        public DateTime BirthDate { get; set; }
        [MaxLength(16)]
        public string? BankCardNumber { get; set; }
        public List<Order>? Orders { get; set; }
        public List<Service>? Services { get; set; }
        public List<Suggestion>? Suggestions { get; set; }
        public List<Comment>? Comments { get; set; }
        public string? Image { get; set; }
        [DisplayName("آدرس")]
        public Address Address { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public bool IsConfirm { get; set; } = false;
        public bool IsDeleted { get; set; } = false;
    }
}
