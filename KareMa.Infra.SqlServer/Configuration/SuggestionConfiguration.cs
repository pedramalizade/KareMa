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
    public class SuggestionConfiguration : IEntityTypeConfiguration<Suggestion>
    {
        public void Configure(EntityTypeBuilder<Suggestion> builder)
        {
            builder.HasKey(s => s.Id);

            builder.HasOne(s => s.Order)
                .WithMany(o => o.Suggestions)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(s => s.Expert)
                .WithMany(e => e.Suggestions)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasData(
            new Suggestion
            {
                Id = 1,
                Description = "نظافت خانه",
                ExpertId = 1,
                CreateAt = new DateTime(2024, 6, 8),
                Price = 4000,
                OrderId = 1,
                IsDeleted = false
            }
        );
        }
    }
}
