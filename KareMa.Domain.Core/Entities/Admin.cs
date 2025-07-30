namespace KareMa.Domain.Core.Entities
{
    public class Admin
    {
        public int Id { get; set; }
        public AppUser AppUser { get; set; }
        public int AppUserId { get; set; }
        [MaxLength(20)]
        public string FirstName { get; set; }
        public decimal Balance { get; set; }
        [MaxLength(50)]
        public string LastName { get; set; }
        public GenderEnum Gender { get; set; }
        public bool IsDeleted { get; set; } = false;
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }

}
