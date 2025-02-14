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

        builder.HasData(
            new Address
            {
                Id = 1,
                CustomerId = 1,
                ExpertId = 2,
                CityId = 4,
                Street = "سهروردی",
                Area = "منطقه 7",
                PostalCode = "174735364",
                CreatedAt = new DateTime(2024, 5, 3),
                IsDeleted = false
            }
        );
    }
}