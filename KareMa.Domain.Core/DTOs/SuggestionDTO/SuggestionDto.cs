namespace KareMa.Domain.Core.DTOs.SuggestionDTO
{
    public class SuggestionDto
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public int ExpertId { get; set; }
        public Entities.Expert Expert { get; set; } 
        public decimal Price { get; set; } 
        public string Description { get; set; }
        public DateTime SuggestedDate { get; set; } 
        public StatusEnum? Status { get; set; }
    }
}
