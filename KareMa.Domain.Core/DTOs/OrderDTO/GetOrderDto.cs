namespace KareMa.Domain.Core.DTOs.OrderDTO
{
    public class GetOrderDto
    {
        public int Id { get; set; }
        [MaxLength(50)]
        public string Title { get; set; }
        [MaxLength(500)]
        public string Description { get; set; }
        public StatusEnum Status { get; set; }
        public Customer Customer { get; set; }
        public Entities.Expert? Expert { get; set; }
        public List<Suggestion>? Suggestions { get; set; }
        public Service Service { get; set; }
        public string? Image { get; set; }
    }
}
