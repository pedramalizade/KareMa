using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
