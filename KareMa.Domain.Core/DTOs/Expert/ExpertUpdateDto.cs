using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using KareMa.Domain.Core.Enums;

public class ExpertUpdateDto
{
    public int Id { get; set; }
    [MaxLength(25, ErrorMessage = "نام نمی‌تواند بیشتر از 25 کاراکتر باشد"), MinLength(3), Required, DisplayName("نام")]
    public string FirstName { get; set; }
    [MaxLength(25, ErrorMessage = "نام خانوادگی نمی‌تواند بیشتر از 25 کاراکتر باشد"), MinLength(3), Required, DisplayName("نام خانوادگی")]
    public string LastName { get; set; }
    [DisplayName("جنسیت")]
    public GenderEnum Gender { get; set; }
    [Length(11, 11, ErrorMessage = "شماره تلفن باید 11 رقم باشد"), RegularExpression(@"^09\d{9}$", ErrorMessage = "شماره تلفن باید با 09 شروع شود"), Required, DisplayName("شماره تلفن")]
    public string PhoneNumber { get; set; }
    [DisplayName("موجودی")]
    public decimal Balance { get; set; }
    [Length(16, 16, ErrorMessage = "شماره کارت باید 16 رقم باشد"), RegularExpression(@"^\d{16}$", ErrorMessage = "شماره کارت باید فقط شامل اعداد باشد"), DisplayName("شماره کارت بانکی")]
    public string BankCardNumber { get; set; }
    public DateTime BirthDate { get; set; }
    [DisplayName("عکس پروفایل")]
    public string? Image { get; set; }
    public List<int>? ServiceIds { get; set; }
    [MaxLength(500, ErrorMessage = "توضیحات نمی‌تواند بیشتر از 500 کاراکتر باشد"), DisplayName("درباره من")]
    public string? Bio { get; set; }
}