namespace KareMa.Domain.Core.Entities
{
    public class AppUser : IdentityUser<int>
    {
        public Admin? Admin { get; set; }
        public Customer? Customer { get; set; }
        public Expert? Expert { get; set; }
    }
}
