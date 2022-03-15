using Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations;

public class CountryConfiguration : IEntityTypeConfiguration<Country>
{
    public void Configure(EntityTypeBuilder<Country> builder)
    {
        // Keys
        builder.HasKey(country => country.CountryId);

        // Indexes
        builder.HasIndex(country => country.Code)
            .IsUnique();

        // Properties
        builder.Property(country => country.Code)
            .IsRequired()
            .HasMaxLength(2);

        builder.Property(country => country.Name)
            .IsRequired();

        builder.Property(country => country.FlagImage)
            .IsRequired(false);
    }
}