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

            builder.HasMany(c => c.Addresses)
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
                    //PhoneNumber = "09192365988",
                    BackUpPhoneNumber = "09123669858",
                    BankCardNumber = "1234123412341234",
                    CreatedAt = new DateTime(2024, 2, 12),
                    IsDeleted = false,
                },
                new Customer
                {
                    Id = 2,
                    FirstName = "امیر",
                    LastName = "تقوایی",
                    Gender = GenderEnum.Male,
                    //PhoneNumber = "09014839264",
                    BackUpPhoneNumber = "09123623258",
                    BankCardNumber = "1239684412341234",
                    CreatedAt = new DateTime(2024, 2, 12),
                    IsDeleted = false,
                }
                ) ;
        }
    }
}

