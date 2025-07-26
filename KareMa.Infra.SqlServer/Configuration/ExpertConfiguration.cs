using KareMa.Domain.Core.Entities;
using KareMa.Domain.Core.Enums;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace KareMa.Infra.SqlServer.Configuration
{
    public class ExpertConfiguration : IEntityTypeConfiguration<Expert>
    {
        public void Configure(EntityTypeBuilder<Expert> builder)
        {
            builder.HasKey(e => e.Id);

            builder.HasMany(e => e.Services)
                 .WithMany(e => e.Experts);

            builder.HasMany(e => e.Suggestions)
                .WithOne(e => e.Expert)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasData
                (
                new Expert
                {
                    Id = 1,
                    FirstName = "علی",
                    BirthDate = new DateTime(1999, 10, 03),
                    LastName = "کریمی",
                    PhoneNumber = "09362356998",
                    Gender = GenderEnum.Male,
                    BankCardNumber = "1234123412341234",
                    Image = "\\AdminTemplate\\images\\user\\placeholder.jpg",
                    IsConfirm = true,
                    Bio = "تخصص هام توی زمینه خودم عالیه",
                    CreatedAt = new DateTime(2024, 2, 12),
                    IsDeleted = false,
                    Balance = 0,
                    AppUserId = 2
                },
                 new Expert
                 {
                     Id = 2,
                     FirstName = "سارا",
                     LastName = "خاتمی",
                     BirthDate = new DateTime(1999, 4, 04),
                     PhoneNumber = "09362357998",
                     BankCardNumber = "1234123412341234",
                     Gender = GenderEnum.Female,
                     Image = "\\AdminTemplate\\images\\user\\placeholder.jpg",
                     IsConfirm = true,
                     Balance = 0,
                     Bio = "میتونید بهم اعتماد کنید توی انجام کار ها",
                     CreatedAt = new DateTime(2024, 2, 12),
                     IsDeleted = false,
                     AppUserId = 4
                 }
                );
            ;
        }
    }


}
