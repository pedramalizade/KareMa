using KareMa.Domain.Core.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

public class AddressConfiguration : IEntityTypeConfiguration<Address>
{
    public void Configure(EntityTypeBuilder<Address> builder)
    {
        builder.HasKey(c => c.Id);

        builder.HasOne(c => c.Customer)
            .WithOne(c => c.Address)
              .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne(c => c.Expert)
            .WithOne(c => c.Address)
          .OnDelete(DeleteBehavior.NoAction);


        builder.HasOne(c => c.City)
            .WithMany(c => c.Address)
            .OnDelete(DeleteBehavior.NoAction);
    }
}