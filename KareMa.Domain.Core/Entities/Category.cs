using System.ComponentModel.DataAnnotations;

namespace KareMa.Domain.Core.Entities
{
    public class Category
    {
        public int Id { get; set; }
        [MaxLength(100)]
        public string Name { get; set; }
        public string? Image { get; set; }
        public List<SubCategory>? SubCategories { get; set; }
        public bool IsDeleted { get; set; } = false;
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
