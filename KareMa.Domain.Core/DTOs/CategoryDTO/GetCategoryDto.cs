namespace KareMa.Domain.Core.DTOs.CategoryDTO
{
    public class GetCategoryDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Image { get; set; }
        public bool IsDeleted { get; set; }
    }
}
