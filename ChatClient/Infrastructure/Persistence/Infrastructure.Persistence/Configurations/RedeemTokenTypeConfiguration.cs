using Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Infrastructure.Persistence.Configurations
{
    public class RedeemTokenTypeConfiguration : IEntityTypeConfiguration<RedeemTokenType>
    {
        public void Configure(EntityTypeBuilder<RedeemTokenType> builder)
        {
            // Keys
            builder.HasKey(type => type.RedeemTokenTypeId);

            // Indexes
            builder.HasIndex(type => type.Name)
                .IsUnique();

            // Properties
            builder.Property(type => type.Name)
                .IsRequired();

            // Relationships
            builder.HasMany(type => type.Tokens)
                .WithOne(token => token.Type);
            
            // Seed data
            IEnumerable<RedeemTokenType> tokenTypes = Enum
                .GetValues(typeof(RedeemTokenTypeId))
                .Cast<RedeemTokenTypeId>()
                .Select(type => new RedeemTokenType()
                {
                    RedeemTokenTypeId = type,
                    Name = type.ToString()
                });

            builder.HasData(tokenTypes);
        }
    }
}
