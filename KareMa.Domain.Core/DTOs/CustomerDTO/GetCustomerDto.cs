namespace KareMa.Domain.Core.DTOs.CustomerDTO
{
    public class GetCustomerDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string? Image { get; set; }
        public Decimal Balance { get; set; }

    }
}
