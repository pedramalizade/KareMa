using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KareMa.Domain.Core.Enums;

public class AdminUpdateDto
{

    public int Id { get; set; }
    public string FirstName { get; set; }
    [DisplayName("نام خانوادگی")]
    public string LastName { get; set; }
    [DisplayName("ایمیل")]
    public string Email { get; set; }
    [DisplayName("رمز عبور")]
    public string Password { get; set; }
    [DisplayName("شماره تلفن")]
    public string PhoneNumber { get; set; }
    [DisplayName("جنسیت")]
    public GenderEnum Gender { get; set; }

    //public int Id { get; set; }
    //[DisplayName("نام")]
    //[MaxLength(20, ErrorMessage = "نام نمی‌تواند بیشتر از 50 کاراکتر باشد")]
    //[MinLength(3, ErrorMessage = "نام نمی‌تواند کمتر از 3 کاراکتر باشد")]
    //[Required(ErrorMessage = "نام نمی‌تواند بدون مقدار باشد")]
    //public string? FirstName { get; set; }
    //[DisplayName("نام خانوادگی")]
    //[MaxLength(50, ErrorMessage = "نام خانوادگی نمی‌تواند بیشتر از 50 کاراکتر باشد")]
    //[MinLength(3, ErrorMessage = "نام خانوادگی نمی‌تواند کمتر از 3 کاراکتر باشد")]
    //[Required(ErrorMessage = "نام خانوادگی نمی‌تواند بدون مقدار باشد")]
    //public string? LastName { get; set; }
    //[DisplayName("ایمیل")]
    //[MaxLength(50, ErrorMessage = "ایمیل نمی‌تواند بیشتر از 50 کاراکتر باشد")]
    //[MinLength(3, ErrorMessage = "ایمیل نمی‌تواند کمتر از 3 کاراکتر باشد")]
    //[Required(ErrorMessage = "ایمیل نمی‌تواند بدون مقدار باشد")]
    //public string? Email { get; set; }
    //[DisplayName("شماره تلفن")]
    //[Length(11, 11, ErrorMessage = " شماره تلفن نمی‌تواند کمتر یا بیشتر از 11 کاراکتر باشد")]
    //[RegularExpression(@"^09\d{9}$", ErrorMessage = "فرمت شماره تلفن اشتباه است و باید با 09 شروع شود")]
    //[Required(ErrorMessage = "شماره تلفن نمی‌تواند بدون مقدار باشد")]
    //public string? PhoneNumber { get; set; }
}