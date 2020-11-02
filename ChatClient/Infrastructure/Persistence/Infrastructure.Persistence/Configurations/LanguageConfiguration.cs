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

            builder.HasAlternateKey(language => language.Code);

            // Properties
            builder.Property(language => language.Name)
                .IsRequired();

            // Relationships
            builder.HasMany(language => language.Translations)
                .WithOne(translation => translation.Language);
        }
    }
}
