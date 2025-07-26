using KareMa.Domain.Core.Entities;

namespace KareMa.Domain.Core.DTOs.ServiceDTO
{
    public class GetServiceDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public SubCategory? SubCategory { get; set; }
        public int SubCategoryId { get; set; }
        public int Price { get; set; }
        public string Image { get; set; }
        public bool IsDeleted { get; set; }
    }
}
