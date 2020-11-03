using Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations
{
    public class TranslationConfiguration : IEntityTypeConfiguration<Translation>
    {
        public void Configure(EntityTypeBuilder<Translation> builder)
        {
            // Keys
            builder.HasKey(translation => translation.TranslationId);

            // Indexes
            builder.HasIndex(translation => new { translation.LanguageId, translation.Key });

            // Properties
            builder.Property(translation => translation.Key)
                .IsRequired();

            builder.Property(translation => translation.Value)
                .IsRequired();

            // Relationships
            builder.HasOne(translation => translation.Language)
                .WithMany(language => language.Translations)
                .HasForeignKey(translation => translation.LanguageId);
        }
    }
}
