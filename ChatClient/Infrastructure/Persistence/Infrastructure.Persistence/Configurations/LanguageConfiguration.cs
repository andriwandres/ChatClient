using Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations
{
    public class LanguageConfiguration : IEntityTypeConfiguration<Language>
    {
        public void Configure(EntityTypeBuilder<Language> builder)
        {
            // Keys
            builder.HasKey(language => language.LanguageId);

            // Indexes
            builder.HasIndex(language => language.Code)
                .IsUnique();

            // Properties
            builder.Property(language => language.Code)
                .IsRequired()
                .HasMaxLength(2);

            builder.Property(language => language.Name)
                .IsRequired();

            // Relationships
            builder.HasMany(language => language.Translations)
                .WithOne(translation => translation.Language);
        }
    }
}
