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
    public class CommentConfiguration : IEntityTypeConfiguration<Comment>
    {
        public void Configure(EntityTypeBuilder<Comment> builder)
        {
            builder.HasKey(c => c.Id);

            builder.HasOne(c => c.Customer)
                 .WithMany(c => c.Comments)
                 .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(c => c.Expert)
                .WithMany(c => c.Comments)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasData(
                new Comment
                {
                    Id = 1,
                    CustomerId = 1,
                    ExpertId = 1,
                    IsAccept = false,
                    Description = "کارش بی نظیر بود",
                    IsDeleted = false,
                    Score = 4,
                    Title = "عالی",
                    CreatedAt = new DateTime(2024, 2, 12),
                }
                );
            ;
        }
    }
}
