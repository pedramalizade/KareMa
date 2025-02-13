using KareMa.Domain.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
