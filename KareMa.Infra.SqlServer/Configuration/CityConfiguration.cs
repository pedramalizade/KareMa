using KareMa.Domain.Core.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KareMa.Infra.SqlServer.Configuration
{
    public class CityConfiguration : IEntityTypeConfiguration<City>
    {
        public void Configure(EntityTypeBuilder<City> builder)
        {
            builder.HasKey(c => c.Id);

            builder.HasMany(c => c.Address)
                .WithOne(c => c.City)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasData(
               new City { Id = 1, Name = "تهران", CreatedAt = new DateTime(2024, 2, 12) },
               new City { Id = 2, Name = "آذربایجان غربی", CreatedAt = new DateTime(2024, 2, 12) },
               new City { Id = 3, Name = "اردبیل", CreatedAt = new DateTime(2024, 2, 12) },
               new City { Id = 4, Name = "اصفهان", CreatedAt = new DateTime(2024, 2, 12) },
               new City { Id = 5, Name = "البرز", CreatedAt = new DateTime(2024, 2, 12) },
               new City { Id = 6, Name = "ایلام", CreatedAt = new DateTime(2024, 2, 12) },
               new City { Id = 7, Name = "بوشهر", CreatedAt = new DateTime(2024, 2, 12) },
               new City { Id = 8, Name = "آذربایجان شرقی", CreatedAt = new DateTime(2024, 2, 12), },
               new City { Id = 9, Name = "چهارمحال و بختیاری", CreatedAt = new DateTime(2024, 2, 12) },
               new City { Id = 10, Name = "خراسان جنوبی", CreatedAt = new DateTime(2024, 2, 12) },
               new City { Id = 11, Name = "خراسان رضوی", CreatedAt = new DateTime(2024, 2, 12) },
               new City { Id = 12, Name = "خراسان شمالی", CreatedAt = new DateTime(2024, 2, 12) },
               new City { Id = 13, Name = "خوزستان", CreatedAt = new DateTime(2024, 2, 12) },
               new City { Id = 14, Name = "زنجان", CreatedAt = new DateTime(2024, 2, 12) },
               new City { Id = 15, Name = "سمنان", CreatedAt = new DateTime(2024, 2, 12) },
               new City { Id = 16, Name = "سیستان و بلوچستان", CreatedAt = new DateTime(2024, 2, 12) },
               new City { Id = 17, Name = "فارس", CreatedAt = new DateTime(2024, 2, 12) },
               new City { Id = 18, Name = "قزوین", CreatedAt = new DateTime(2024, 2, 12) },
               new City { Id = 19, Name = "قم", CreatedAt = new DateTime(2024, 2, 12) },
               new City { Id = 20, Name = "کردستان", CreatedAt = new DateTime(2024, 2, 12) },
               new City { Id = 21, Name = "کرمان", CreatedAt = new DateTime(2024, 2, 12) },
               new City { Id = 22, Name = "کرمانشاه", CreatedAt = new DateTime(2024, 2, 12) },
               new City { Id = 23, Name = "کهگیلویه و بویراحمد", CreatedAt = new DateTime(2024, 2, 12) },
               new City { Id = 24, Name = "گلستان", CreatedAt = new DateTime(2024, 2, 12) },
               new City { Id = 25, Name = "گیلان", CreatedAt = new DateTime(2024, 2, 12) },
               new City { Id = 26, Name = "لرستان", CreatedAt = new DateTime(2024, 2, 12) },
               new City { Id = 27, Name = "مازندران", CreatedAt = new DateTime(2024, 2, 12) },
               new City { Id = 28, Name = "مرکزی", CreatedAt = new DateTime(2024, 2, 12) },
               new City { Id = 29, Name = "هرمزگان", CreatedAt = new DateTime(2024, 2, 12) },
               new City { Id = 30, Name = "همدان", CreatedAt = new DateTime(2024, 2, 12) },
               new City { Id = 31, Name = "یزد", CreatedAt = new DateTime(2024, 2, 12) }
               );
        }
    }
}
