using Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations;

public class RedeemTokenConfiguration : IEntityTypeConfiguration<RedeemToken>
{
    public void Configure(EntityTypeBuilder<RedeemToken> builder)
    {
        // Keys
        builder.HasKey(token => token.RedeemTokenId);

        // Properties
        builder.Property(token => token.UserId);

        builder.Property(token => token.Type);

        builder.Property(token => token.Token)
            .IsRequired()
            .ValueGeneratedOnAdd();

        builder.Property(token => token.IsUsed)
            .IsRequired()
            .HasDefaultValue(false);

        builder.Property(token => token.ValidUntil)
            .IsRequired();

        // Relationships
        builder.HasOne(token => token.User)
            .WithMany(user => user.RedeemTokens)
            .HasForeignKey(token => token.UserId);
    }
}