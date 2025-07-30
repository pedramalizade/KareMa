public class CustomerCreateDto
{
    [Required(ErrorMessage = "نام اجباری است")]
    [DisplayName("نام")]
    public string FirstName { get; set; }

    [Required(ErrorMessage = "نام خانوادگی اجباری است")]
    [DisplayName("نام خانوادگی")]
    public string LastName { get; set; }

    [DisplayName("جنسیت")]
    public GenderEnum? Gender { get; set; }

    [Required(ErrorMessage = "شماره تلفن اجباری است")]
    [Phone(ErrorMessage = "شماره تلفن نامعتبر است")]
    [DisplayName("شماره تلفن")]
    public string PhoneNumber { get; set; }

    [DisplayName("موجودی")]
    public decimal? Balance { get; set; } 

    [DisplayName("شماره کارت بانکی")]
    public string BankCardNumber { get; set; } 

    public string? Image { get; set; } 

    [Required(ErrorMessage = "آدرس اجباری است")]
    [DisplayName("آدرس")]
    public Address Addresses { get; set; }
    public int AppUserId { get; set; }
}