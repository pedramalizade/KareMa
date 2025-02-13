using KareMa.Domain.Core.Entities;
using KareMa.Domain.Core.Enums;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

            builder.Property(c => c.Balance)
                .HasColumnType("decimal(18,2)");
                

            builder.HasData
                (
                new Customer
                {
                    Id = 1,
                    FirstName = "تارا",
                    LastName = "بابایی",
                    Gender = GenderEnum.Female,
                    Balance = 0,
                    PhoneNumber = "09123669858",
                    CreatedAt = new DateTime(2024, 2, 12),
                    IsDeleted = false,
                    AppUserId = 3
                },
                new Customer
                {
                    Id = 2,
                    FirstName = "پارسا",
                    LastName = "تقوایی",
                    Balance = 0,
                    Gender = GenderEnum.Male,
                    PhoneNumber = "09123623258",
                    CreatedAt = new DateTime(2024, 2, 12),
                    IsDeleted = false,
                    AppUserId = 5
                }
                ); ;
        }
    }
}
