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
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.HasKey(o => o.Id);

            builder.HasOne(o => o.Customer)
                .WithMany(c => c.Orders)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(o => o.Expert)
                .WithMany(c => c.Orders)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(o => o.Service)
                .WithMany(c => c.Orders)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasMany(o => o.Suggestions)
                .WithOne(s => s.Order)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasData(
          new Order
          {
              Id = 1,
              CreatedAt = new DateTime(2024, 2, 12),
              CustomerId = 1,
              IsDeleted = false,
              Description = "نظافت خونه صد متری یه طور کامل",
              ExpertId = 1,
              DoneAt = new DateTime(2024, 2, 12),
              RequesteForTime = new DateTime(2024, 2, 12),
              ServiceId = 1,
              Title = "نظافت",
              Status = StatusEnum.Confirmed,
          }
          );
        }
    }
}

