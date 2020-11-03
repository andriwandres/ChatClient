using Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations
{
    public class RedeemTokenTypeConfiguration : IEntityTypeConfiguration<RedeemTokenType>
    {
        public void Configure(EntityTypeBuilder<RedeemTokenType> builder)
        {
            // Keys
            builder.HasKey(type => type.RedeemTokenTypeId);

            // Properties
            builder.Property(type => type.Name)
                .IsRequired();

            // Relationships
            builder.HasMany(type => type.Tokens)
                .WithOne(token => token.Type);
        }
    }
}
