using Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations
{
    public class CountryConfiguration : IEntityTypeConfiguration<Country>
    {
        public void Configure(EntityTypeBuilder<Country> builder)
        {
            // Keys
            builder.HasKey(country => country.Code);

            // Properties
            builder.Property(country => country.Name);

            // Relationships
            builder.HasMany(country => country.Users)
                .WithOne(user => user.Country);
        }
    }
}
