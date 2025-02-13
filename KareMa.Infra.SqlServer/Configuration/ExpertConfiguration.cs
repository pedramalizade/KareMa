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

            builder.Property(c => c.Balance)
                .HasColumnType("decimal(18,2)");

            //builder.HasOne(e => e.Address)
            //    .WithOne(a => a.Expert)
            //    .OnDelete(DeleteBehavior.NoAction);

            builder.HasData
                (
                new Expert
                {
                    Id = 1,
                    FirstName = "علی",
                    LastName = "کریمی",
                    BirthDate = new DateTime(1998, 12, 03),
                    PhoneNumber = "09362356998",
                    Gender = GenderEnum.Male,
                    Balance = 0,
                    IsConfirm = true,
                    CreatedAt = new DateTime(2024, 2, 12),
                    IsDeleted = false,
                    //AppUserId = 1
                },
                 new Expert
                 {
                     Id = 2,
                     FirstName = "سارا",
                     LastName = "خاتمی",
                     BirthDate = new DateTime(2004, 12, 07),
                     PhoneNumber = "09362357998",
                     Gender = GenderEnum.Female,
                     IsConfirm = true,
                     Balance = 0,
                     CreatedAt = new DateTime(2024, 2, 12),
                     IsDeleted = false,
                     //AppUserId = 1
                 }
                );
            ;
        }
    }


}
