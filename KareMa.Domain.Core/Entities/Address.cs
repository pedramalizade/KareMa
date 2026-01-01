namespace KareMa.Domain.Core.Entities
{
    public class Address
    {
        public Address()
        {
        }
        public int Id { get; set; }

        /// <summary>
        /// شناسه مشتری مرتبط با این آدرس (در صورت تعلق به مشتری)
        /// </summary>
        public int? CustomerId { get; set; }

        /// <summary>
        /// اطلاعات مشتری مرتبط با این آدرس
        /// </summary>
        public Customer? Customer { get; set; }

        /// <summary>
        /// شناسه متخصص مرتبط با این آدرس (در صورت تعلق به متخصص)
        /// </summary>
        public int? ExpertId { get; set; }

        /// <summary>
        /// اطلاعات متخصص مرتبط با این آدرس
        /// </summary>
        public Expert? Expert { get; set; }

        /// <summary>
        /// عنوان آدرس (مانند منزل، محل کار و ...)
        /// </summary>
        [MaxLength(50)]
        public string Title { get; set; }

        /// <summary>
        /// شناسه شهر مربوط به آدرس
        /// </summary>
        public int CityId { get; set; }

        /// <summary>
        /// اطلاعات شهر مربوط به آدرس
        /// </summary>
        [MaxLength(50)]
        public City? City { get; set; }

        /// <summary>
        /// نام خیابان
        /// </summary>
        [MaxLength(50, ErrorMessage = "نام خیابان  نمی‌تواند بیشتر از 50 کاراکتر باشد")]
        [MinLength(2, ErrorMessage = "نام خیابان نمی‌تواند کمتر از 2 کاراکتر باشد")]
        [Required(ErrorMessage = "نام خیابان نمی‌تواند بدون مقدار باشد")]
        public string Street { get; set; }

        /// <summary>
        /// نام محله
        /// </summary>
        [MaxLength(500, ErrorMessage = "محله نمی‌تواند بیشتر از 500 کاراکتر باشد")]
        [MinLength(3, ErrorMessage = "محله نمی‌تواند کمتر از 3 کاراکتر باشد")]
        [Required(ErrorMessage = "محله نمی‌تواند بدون مقدار باشد")]
        public string Area { get; set; }

        /// <summary>
        /// کد پستی آدرس (۱۰ رقمی)
        /// </summary>
        [DisplayName("کدپستی")]
        [Length(10, 10, ErrorMessage = "کدپستی نمی‌تواند کمتر یا بیشتر از 10 کاراکتر باشد")]
        [Required(ErrorMessage = "کدپستی نمی‌تواند بدون مقدار باشد")]
        public string PostalCode { get; set; }

        /// <summary>
        /// مشخص می‌کند آیا این آدرس، آدرس پیش‌فرض کاربر است یا خیر
        /// </summary>
        public bool IsDefault { get; set; }

        /// <summary>
        /// مشخص می‌کند آیا آدرس حذف منطقی شده است یا خیر
        /// </summary>
        public bool IsDeleted { get; set; }

        /// <summary>
        /// تاریخ و زمان ایجاد آدرس
        /// </summary>
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
