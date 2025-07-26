using KareMa.Domain.Core.Entities;

namespace KareMa.Domain.Core.DTOs.SubCategoryDTO
{
    public class GetSubCategoryDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Category? Category { get; set; }
        public int CategoryId { get; set; }
        public string? Image { get; set; }
        public bool IsDeleted { get; set; }
    }
}
