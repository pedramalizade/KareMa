using KareMa.Domain.Core.Entities;
using KareMa.Domain.Core.Enums;
using System.ComponentModel.DataAnnotations;

namespace KareMa.Domain.Core.DTOs.SuggestionDTO
{
    public class SuggestionsByExpertIdDto
    {
        public int Id { get; set; }
        [MaxLength(500)]
        public string? Description { get; set; }
        public int? ExpertId { get; set; }
        public Order? Order { get; set; }
        public int? OrderId { get; set; }
        public int? Price { get; set; }
        public DateTime? SuggestedDate { get; set; }
        public string? SuggestedDateString { get; set; }
        public StatusEnum? Status { get; set; }
    }
}
