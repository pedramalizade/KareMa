using KareMa.Domain.Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KareMa.Domain.Core.DTOs.SubCategoryDTO
{
    public class SubCategoryUpdateDto
    {
        public int Id { get; set; }
        [DisplayName("نام دسته بندی")]
        [MaxLength(100, ErrorMessage = "نام دسته بندی نمیتواند بیشتر از 100 کاراکتر باشد")]
        [MinLength(10, ErrorMessage = "نام دسته بندی نمیتواند کمتر از 10 کاراکتر باشد")]
        [Required(ErrorMessage = "وارد کردن نام دسته بندی اجباری است.")]
        public string Name { get; set; }
        //public int CategoryId { get; set; }
        //public Category Category { get; set; }
        //public List<Service> Services { get; set; }
        public string Image { get; set; }
    }
}
