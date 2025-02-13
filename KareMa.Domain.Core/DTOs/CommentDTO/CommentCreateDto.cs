using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class CommentCreateDto
{
    [DisplayName("عنوان")]
    [MaxLength(50, ErrorMessage = "عنوان نمی‌تواند بیشتر از 50 کاراکتر باشد")]
    [MinLength(3, ErrorMessage = "عنوان نمی‌تواند کمتر از 3 کاراکتر باشد")]
    [Required(ErrorMessage = "عنوان نمی‌تواند بدون مقدار باشد")]
    public string Title { get; set; }
    [DisplayName("متن کامنت")]
    [MaxLength(500, ErrorMessage = "متن کامنت نمی‌تواند بیشتر از 500 کاراکتر باشد")]
    [MinLength(3, ErrorMessage = "متن کامنت نمی‌تواند کمتر از 3 کاراکتر باشد")]
    [Required(ErrorMessage = "متن کامنت نمی‌تواند بدون مقدار باشد")]
    public string Description { get; set; }
    [DisplayName("رضایت")]
    public int Score { get; set; }
    public int CustomerId { get; set; }
    public int ExpertId { get; set; }
}