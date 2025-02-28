using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KareMa.Domain.Core.Entities;

namespace KareMa.Infra.SqlServer.Configuration
{
    public class SubCategoryConfiguration : IEntityTypeConfiguration<SubCategory>
    {
        public void Configure(EntityTypeBuilder<SubCategory> builder)
        {
            builder.HasKey(s => s.Id);

            builder.HasMany(s => s.Services)
                 .WithOne(s => s.SubCategory)
                 .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(s => s.Category)
                .WithMany(s => s.SubCategories)
                .OnDelete(DeleteBehavior.NoAction);


            builder.HasData
                (
                new SubCategory { Id = 1, Name = "نظافت و پذیرایی", CategoryId = 1, CreatedAt = new DateTime(2024, 2, 12), IsDeleted = false, Image = "\\Images\\SubCategory\\1.jpg" },
                new SubCategory { Id = 2, Name = "شستشو", CategoryId = 1, CreatedAt = new DateTime(2024, 2, 12), IsDeleted = false, Image = "\\Images\\SubCategory\\2.jpg" },
                new SubCategory { Id = 3, Name = "کارواش و دیتیلینگ", CategoryId = 1, CreatedAt = new DateTime(2024, 2, 12), IsDeleted = false, Image = "\\Images\\SubCategory\\3.jpg" },
                new SubCategory { Id = 4, Name = "سرمایش و گرمایش", CategoryId = 3, CreatedAt = new DateTime(2024, 2, 12), IsDeleted = false, Image = "\\Images\\SubCategory\\4.jpg" },
                new SubCategory { Id = 5, Name = "نصب وتعمیر لوازم خانگی", CategoryId = 3, CreatedAt = new DateTime(2024, 2, 12), IsDeleted = false, Image = "\\Images\\SubCategory\\5.jpg" },
                new SubCategory { Id = 7, Name = "خذمات کامپیوتری", CategoryId = 3, CreatedAt = new DateTime(2024, 2, 12), IsDeleted = false, Image = "\\Images\\SubCategory\\7.jpg" },
                new SubCategory { Id = 8, Name = "تعمیرات موبایل", CategoryId = 3, CreatedAt = new DateTime(2024, 2, 12), IsDeleted = false, Image = "\\Images\\SubCategory\\8.jpg" },
                new SubCategory { Id = 9, Name = "سرمایش و گرمایش", CategoryId = 2, CreatedAt = new DateTime(2024, 2, 12), IsDeleted = false, Image = "\\Images\\SubCategory\\9.jpg" },
                new SubCategory { Id = 10, Name = "تعمیرا ساختمان", CategoryId = 2, CreatedAt = new DateTime(2024, 2, 12), IsDeleted = false, Image = "\\Images\\SubCategory\\10.jpg" },
                new SubCategory { Id = 11, Name = "لوله کشی", CategoryId = 2, CreatedAt = new DateTime(2024, 2, 12), IsDeleted = false, Image = "\\Images\\SubCategory\\11.jpg" },
                new SubCategory { Id = 12, Name = "طراحی و بازسازی ساختمان", CategoryId = 2, CreatedAt = new DateTime(2024, 2, 12), IsDeleted = false, Image = "\\Images\\SubCategory\\12.jpg" },
                new SubCategory { Id = 13, Name = "برق کاری", CategoryId = 2, CreatedAt = new DateTime(2024, 2, 12), IsDeleted = false, Image = "\\Images\\SubCategory\\13.jpg" },
                new SubCategory { Id = 14, Name = "چوب و کابینت", CategoryId = 2, CreatedAt = new DateTime(2024, 2, 12), IsDeleted = false, Image = "\\Images\\SubCategory\\14.jpg" },
                new SubCategory { Id = 15, Name = "خدمات شیشه ای ساختمان", CategoryId = 2, CreatedAt = new DateTime(2024, 2, 12), IsDeleted = false, Image = "\\Images\\SubCategory\\15.jpg" },
                new SubCategory { Id = 16, Name = "باغبانی و فضای سبز", CategoryId = 2, CreatedAt = new DateTime(2024, 2, 12), IsDeleted = false, Image = "\\Images\\SubCategory\\16.jpg" },
                new SubCategory { Id = 17, Name = "باربری و جا به جایی", CategoryId = 4, CreatedAt = new DateTime(2024, 2, 12), IsDeleted = false, Image = "\\Images\\SubCategory\\17.jpg" },
                new SubCategory { Id = 18, Name = "خدمات و تعمیرات خودرو", CategoryId = 5, CreatedAt = new DateTime(2024, 2, 12), IsDeleted = false, Image = "\\Images\\SubCategory\\18.jpg" },
                new SubCategory { Id = 19, Name = "کارواش و دیتیلینگ", CategoryId = 5, CreatedAt = new DateTime(2024, 2, 12), IsDeleted = false, Image = "\\Images\\SubCategory\\19.jpg" },
                new SubCategory { Id = 20, Name = "خدمات شرکتی", CategoryId = 6, CreatedAt = new DateTime(2024, 2, 12), IsDeleted = false, Image = "\\Images\\SubCategory\\20.jpg" },
                new SubCategory { Id = 21, Name = "زیبایی بانوان", CategoryId = 7, CreatedAt = new DateTime(2024, 2, 12), IsDeleted = false, Image = "\\Images\\SubCategory\\21.jpg" },
                new SubCategory { Id = 22, Name = "پیرایش و زیبایی آقایان", CategoryId = 7, CreatedAt = new DateTime(2024, 2, 12), IsDeleted = false, Image = "\\Images\\SubCategory\\22.jpg" },
                new SubCategory { Id = 23, Name = "پزشکی و پرستاری", CategoryId = 7, CreatedAt = new DateTime(2024, 2, 12), IsDeleted = false, Image = "\\Images\\SubCategory\\23.jpg" },
                new SubCategory { Id = 24, Name = "حیوانات خانگی", CategoryId = 7, CreatedAt = new DateTime(2024, 2, 12), IsDeleted = false, Image = "\\Images\\SubCategory\\24.jpg" },
                new SubCategory { Id = 25, Name = "تندرستی و ورزش", CategoryId = 7, CreatedAt = new DateTime(2024, 2, 12), IsDeleted = false, Image = "\\Images\\SubCategory\\25.jpg" }
                );
        }
    }
}
