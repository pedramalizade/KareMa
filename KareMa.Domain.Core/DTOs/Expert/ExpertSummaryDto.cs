using KareMa.Domain.Core.Entities;
using KareMa.Domain.Core.Enums;
using System.ComponentModel.DataAnnotations;

namespace KareMa.Domain.Core.DTOs.Expert
{
    public class ExpertSummaryDto
    {
        public int Id { get; set; }
        [MaxLength(20)]
        public string FirstName { get; set; }
        [MaxLength(50)]
        public string LastName { get; set; }
        public GenderEnum Gender { get; set; }
        [MaxLength(11)]
        public string? ProfileImage { get; set; }
        public List<Service>? Services { get; set; }
        public List<Comment>? Comments { get; set; }
        public decimal Balance { get; set; }
    }
}
