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
                Gender = GenderEnum.Male,
                CreatedAt = new DateTime(2024, 2, 12),
                IsDeleted = false,
                AppUserId = 1
            }
        );
    }
}
