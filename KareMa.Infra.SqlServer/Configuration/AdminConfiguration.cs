public class AdminConfiguration : IEntityTypeConfiguration<Admin>
{
    public void Configure(EntityTypeBuilder<Admin> builder)
    {
        builder.HasKey(a => a.Id);

        builder.HasData(
            new Admin
            {
                Id = 1,
                FirstName = "پدرام",
                LastName = "علیزاده",
                Gender = GenderEnum.Male,
                Balance = 0,
                CreatedAt = new DateTime(2024, 2, 12),
                IsDeleted = false,
                AppUserId = 1
            }
        );
    }
}
