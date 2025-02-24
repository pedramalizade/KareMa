using KareMa.Domain.Core.Entities;
using KareMa.Domain.Core.Enums;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                //Email = "pedramalizade@gmail.com",
                Gender = GenderEnum.Male,
                //Password = "0909",
                //PhoneNumber = "09127575839",
                CreatedAt = new DateTime(2024, 2, 12),
                IsDeleted = false,
                AppUserId = 1
            }
        );
    }
}
