using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KareMa.Domain.Core.Entities;
using KareMa.Domain.Core.Enums;

public class ExpertUpdateDto
{
    public int Id { get; set; }
    [MaxLength(25, ErrorMessage = "نام نمی‌تواند بیشتر از 25 کاراکتر باشد")]
    [MinLength(3, ErrorMessage = "نام نمی‌تواند کمتر از 3 کاراکتر باشد")]
    [Required(ErrorMessage = "نام نمی‌تواند بدون مقدار باشد")]
    [DisplayName("نام")]
    public string FirstName { get; set; }
    [DisplayName("نام خانوداگی")]
    [MaxLength(25, ErrorMessage = "نام خانوادگی نمیتواند بیشتر از 30 کاراکتر باشد")]
    [MinLength(3, ErrorMessage = "نام خانوادگی نمی‌تواند کمتر از 3 کاراکتر باشد")]
    [Required(ErrorMessage = "نام خانوادگی نمی‌تواند بدون مقدار باشد")]
    public string LastName { get; set; }
    [DisplayName("جنسیت")]
    public GenderEnum Gender { get; set; }
    [DisplayName("شماره تلفن")]
    [Length(11, 11, ErrorMessage = " شماره تلفن نمی‌تواند کمتر یا بیشتر از 11 کاراکتر باشد")]
    [RegularExpression(@"^09\d{9}$", ErrorMessage = "فرمت شماره تلفن اشتباه است و باید با 09 شروع شود")]
    [Required(ErrorMessage = "شماره تلفن نمی‌تواند بدون مقدار باشد")]
    public string PhoneNumber { get; set; }
    [DisplayName("موجودی")]
    public decimal Balance { get; set; }
    [DisplayName("شماره کارت بانکی")]
    public string BankCardNumber { get; set; }
    public DateTime BirthDate { get; set; }
    [DisplayName("عکس پروفایل")]
    public string? Image { get; set; }
    public List<int>? ServiceIds { get; set; }

}