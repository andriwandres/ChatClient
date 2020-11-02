using Core.Domain.Entities;
using Infrastructure.Persistence.Generators;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            // Keys
            builder.HasKey(user => user.UserId);

            builder.HasAlternateKey(user => user.DisplayId);

            // Properties
            builder.Property(user => user.DisplayId)
                .HasMaxLength(8)
                .IsRequired()
                .ValueGeneratedOnAdd()
                .HasValueGenerator((property, type) => new DisplayIdGenerator(8));

            builder.Property(user => user.UserName)
                .IsRequired();

            builder.Property(user => user.Email)
                .IsRequired();

            builder.Property(user => user.PasswordHash)
                .IsRequired();

            builder.Property(user => user.PasswordSalt)
                .IsRequired();

            builder.Property(user => user.Created)
                .IsRequired();

            builder.Property(user => user.IsEmailConfirmed)
                .IsRequired()
                .HasDefaultValue(false);

            // Relationships
            builder.HasOne(user => user.Country)
                .WithMany(country => country.Users)
                .HasForeignKey(user => user.CountryId);
        }
    }
}
