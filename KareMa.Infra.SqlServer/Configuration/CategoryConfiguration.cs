using KareMa.Domain.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KareMa.Infra.SqlServer.Configuration
{
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.HasKey(s => s.Id);

            builder.HasMany(s => s.SubCategories)
                .WithOne(s => s.Category)
                .OnDelete(DeleteBehavior.NoAction);


            builder.HasData
                (
                new Category() { Id = 1, Name = "تمیزکاری", CreatedAt = new DateTime(2024, 2, 12), IsDeleted = false, Image = "~/Images/Category/1.jpg" },
                new Category() { Id = 2, Name = "ساختمان", CreatedAt = new DateTime(2024, 2, 12), IsDeleted = false, Image = "~/Images/Category/2.jpg" },
                new Category() { Id = 3, Name = "تعمیرات اشیاء", CreatedAt = new DateTime(2024, 2, 12), IsDeleted = false, Image = "~/Images/Category/3.jpg" },
                new Category() { Id = 4, Name = "اسباب‌کشی و حمل بار", CreatedAt = new DateTime(2024, 2, 12), IsDeleted = false, Image = "~/Images/Category/4.jpg" },
                new Category() { Id = 5, Name = "خودرو", CreatedAt = new DateTime(2024, 2, 12), IsDeleted = false, Image = "~/Images/Category/5.jpg" },
                new Category() { Id = 6, Name = "سازمان‌ها و مجتمع‌ها", CreatedAt = new DateTime(2024, 2, 12), IsDeleted = false, Image = "~/Images/Category/6.jpg" },
                new Category() { Id = 7, Name = "سلامت و زیبایی", CreatedAt = new DateTime(2024, 2, 12), IsDeleted = false, Image = "~/Images/Category/7.jpg" }
                );
        }
    }
}
