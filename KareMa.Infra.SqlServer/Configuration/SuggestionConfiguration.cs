using KareMa.Domain.Core.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

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
        }
    }
}
