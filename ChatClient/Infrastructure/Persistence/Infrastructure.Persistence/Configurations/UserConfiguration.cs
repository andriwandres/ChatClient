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

            // Indexes
            builder.HasIndex(user => user.DisplayId)
                .IsUnique();

            builder.HasIndex(user => user.UserName)
                .IsUnique();

            builder.HasIndex(user => user.Email)
                .IsUnique();

            // Properties
            builder.Property(user => user.CountryId);

            builder.Property(user => user.ProfileImageId);

            builder.Property(user => user.DisplayId)
                .HasMaxLength(8)
                .IsRequired()
                .ValueGeneratedOnAdd()
                .HasValueGenerator<DisplayIdGenerator>();

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
            builder.HasOne(user => user.StatusMessage)
                .WithOne(statusMessage => statusMessage.User);

            builder.HasOne(user => user.Recipient)
                .WithOne(recipient => recipient.User);

            builder.HasOne(user => user.ProfileImage)
                .WithOne()
                .HasForeignKey<User>(user => user.ProfileImageId);

            builder.HasOne(user => user.Availability)
                .WithOne(availability => availability.User);

            builder.HasOne(user => user.Country)
                .WithMany(country => country.Users)
                .HasForeignKey(user => user.CountryId);

            builder.HasMany(user => user.AddressedFriendships)
                .WithOne(friendship => friendship.Addressee);

            builder.HasMany(user => user.RequestedFriendships)
                .WithOne(friendship => friendship.Requester);

            builder.HasMany(user => user.ArchivedRecipients)
                .WithOne(archive => archive.User);

            builder.HasMany(user => user.PinnedRecipients)
                .WithOne(pin => pin.User);

            builder.HasMany(user => user.AuthoredMessages)
                .WithOne(message => message.Author);

            builder.HasMany(user => user.MessageReactions)
                .WithOne(reaction => reaction.User);

            builder.HasMany(user => user.AddressedNicknames)
                .WithOne(nicknameAssignment => nicknameAssignment.Addressee);

            builder.HasMany(user => user.RequestedNicknames)
                .WithOne(nicknameAssignment => nicknameAssignment.Requester);

            builder.HasMany(user => user.RedeemTokens)
                .WithOne(token => token.User);
        }
    }
}
