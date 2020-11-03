using Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations
{
    public class StatusMessageConfiguration : IEntityTypeConfiguration<StatusMessage>
    {
        public void Configure(EntityTypeBuilder<StatusMessage> builder)
        {
            // Keys
            builder.HasKey(message => message.StatusMessageId);

            // Indexes
            builder.HasIndex(message => message.UserId)
                .IsUnique();

            // Properties
            builder.Property(message => message.UserId);

            builder.Property(message => message.Message);

            // Relationships
            builder.HasOne(message => message.User)
                .WithOne(user => user.StatusMessage)
                .HasForeignKey<StatusMessage>(message => message.UserId);
        }
    }
}
