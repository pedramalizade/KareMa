using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KareMa.Domain.Core.DTOs.SubCategoryDTO
{
    public class ServiceSubCategoryUpdateDto
    {
        public int Id { get; set; }

        [MaxLength(100, ErrorMessage = "نام دسته بندی نمیتواند بیشتر از 100 کاراکتر باشد")]
        [MinLength(3, ErrorMessage = "نام دسته بندی نمیتواند کمتر از 3 کاراکتر باشد")]
        [Required(ErrorMessage = "نام دسته بندی نمی‌تواند بدون مقدار باشد")]
        public string? CategoryName { get; set; }
        public string? Image { get; set; }
        public int ServiceCategoryId { get; set; }
    }
}
