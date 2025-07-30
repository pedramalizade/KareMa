public class ExpertCreateDto
{
    public int AppUserId { get; set; }
    [DisplayName("نام")]
    public string FirstName { get; set; }
    [DisplayName("نام خانوداگی")]
    public string LastName { get; set; }
    [DisplayName("جنسیت")]
    public GenderEnum Gender { get; set; }
    [DisplayName("شماره تلفن")]
    public string PhoneNumber { get; set; }
    public DateTime BirthDate { get; set; }
    public decimal Balance { get; set; }
    [DisplayName("عکس پروفایل")]
    public string Image { get; set; }
    [DisplayName("شماره کارت بانکی")]
    public string BankCardNumber { get; set; }
    public List<int> Services { get; set; }
    [DisplayName("آدرس")]
    public Address Address { get; set; }
}