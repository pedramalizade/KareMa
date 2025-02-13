using KareMa.Domain.Core.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class AccountRegisterDto
{
    [Required(ErrorMessage = "وارد کردن نام اجباری است")]
    [Length(3, 20, ErrorMessage = "نام نمی‌تواند کمتر 3 و بیشتر از 20 کاراکتر باشد")]
    public string FirstName { get; set; }
    [Required(ErrorMessage = "وارد کردن نام خانوادگی اجباری است")]
    [Length(3, 50, ErrorMessage = "نام خانوادگی نمی‌تواند کمتر 3 و بیشتر از 50 کاراکتر باشد")]
    public string LastName { get; set; }
    [Required(ErrorMessage = "وارد کردن رمزعبور اجباری است")]
    [MinLength(6, ErrorMessage = "رمزعبور نمی‌تواند کمتر 6 کاراکتر باشد")]
    public string Password { get; set; }
    [Required(ErrorMessage = "وارد کردن ایمیل اجباری است")]
    [EmailAddress(ErrorMessage = "فرمت ایمیل اشتباه است")]
    [Length(10, 50, ErrorMessage = "ایمیل نمی‌تواند کمتر 10 و بیشتر از 50 کاراکتر باشد")]
    public string Email { get; set; }
    public bool isExpert { get; set; }
    [Required]
    public GenderEnum Gender { get; set; }
}
