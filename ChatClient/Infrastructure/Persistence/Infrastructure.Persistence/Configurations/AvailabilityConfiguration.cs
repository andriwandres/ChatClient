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

            builder.HasAlternateKey(availability => availability.UserId);

            // Properties
            builder.Property(availability => availability.ModifiedManually)
                .IsRequired()
                .HasDefaultValue(false);

            builder.Property(availability => availability.Modified)
                .IsRequired();

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
