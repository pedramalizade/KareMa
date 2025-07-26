using KareMa.Domain.Core.Entities;
using KareMa.Domain.Core.Enums;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace KareMa.Infra.SqlServer.Configuration
{
    public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.HasMany(c => c.Orders)
           .WithOne(c => c.Customer)
           .OnDelete(DeleteBehavior.NoAction);

            builder.HasMany(c => c.Comments)
                .WithOne(c => c.Customer)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasData
                (
                new Customer
                {
                    Id = 1,
                    FirstName = "تارا",
                    LastName = "بابایی",
                    Gender = GenderEnum.Female,
                    Image = "\\AdminTemplate\\images\\user\\placeholder.jpg",
                    PhoneNumber = "09192365988",
                    BankCardNumber = "1234123412341234",
                    CreatedAt = new DateTime(2024, 2, 12),
                    Balance = 1500,
                    IsDeleted = false,
                    AppUserId = 3

                },
                new Customer
                {
                    Id = 2,
                    FirstName = "امیر",
                    LastName = "تقوایی",
                    Gender = GenderEnum.Male,
                    Image = "\\AdminTemplate\\images\\user\\placeholder.jpg",
                    Balance = 1500,
                    PhoneNumber = "09014839264",
                    BankCardNumber = "1239684412341234",
                    CreatedAt = new DateTime(2024, 2, 12),
                    IsDeleted = false,
                    AppUserId = 5

                }
                ) ;
        }
    }
}

