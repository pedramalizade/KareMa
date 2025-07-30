public class CommentCreateDto
{
    [Required(ErrorMessage = "عنوان نمی‌تواند خالی باشد")]
    [MaxLength(100, ErrorMessage = "عنوان نمی‌تواند بیشتر از 100 کاراکتر باشد")]
    public string Title { get; set; }

    [Required(ErrorMessage = "متن کامنت نمی‌تواند خالی باشد")]
    [MaxLength(500, ErrorMessage = "متن کامنت نمی‌تواند بیشتر از 500 کاراکتر باشد")]
    [DisplayName("متن کامنت")]
    public string Description { get; set; }

    [Range(1, 5, ErrorMessage = "امتیاز باید بین 1 تا 5 باشد")]
    [DisplayName("رضایت")]
    public int Score { get; set; }

    public int CustomerId { get; set; }
    public int ExpertId { get; set; }
}