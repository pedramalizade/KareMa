using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KareMa.Domain.Core.DTOs.CategoryDTO
{
    public class CategoryUpdateDto
    {
        public int Id { get; set; }

        [DisplayName("نام دسته بندی اصلی")]

        [MaxLength(100, ErrorMessage = "نام دسته بندی نمیتواند بیشتر از 100 کاراکتر باشد")]
        [MinLength(3, ErrorMessage = "نام دسته بندی نمیتواند کمتر از 3 کاراکتر باشد")]
        [Required(ErrorMessage = "نام دسته بندی نمی‌تواند بدون مقدار باشد")]
        public string? Name { get; set; }
        public string? Image { get; set; }
    }
}
