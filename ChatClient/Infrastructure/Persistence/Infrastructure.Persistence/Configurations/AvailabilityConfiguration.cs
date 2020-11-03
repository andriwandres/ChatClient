using Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations
{
    public class AvailabilityConfiguration : IEntityTypeConfiguration<Availability>
    {
        public void Configure(EntityTypeBuilder<Availability> builder)
        {
            // Keys
            builder.HasKey(availability => availability.AvailabilityId);

            // Indexes
            builder.HasIndex(availability => availability.UserId)
                .IsUnique();

            // Properties
            builder.Property(availability => availability.UserId);

            builder.Property(availability => availability.StatusId);

            builder.Property(availability => availability.Modified)
                .IsRequired();

            builder.Property(availability => availability.ModifiedManually)
                .IsRequired()
                .HasDefaultValue(false);

            // Relationships
            builder.HasOne(availability => availability.Status)
                .WithMany(status => status.Availabilities)
                .HasForeignKey(availability => availability.StatusId);

            builder.HasOne(availability => availability.User)
                .WithOne(user => user.Availability)
                .HasForeignKey<Availability>(availability => availability.UserId);
        }
    }
}
