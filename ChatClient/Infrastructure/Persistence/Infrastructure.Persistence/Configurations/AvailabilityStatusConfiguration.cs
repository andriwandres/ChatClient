using Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations
{
    public class AvailabilityStatusConfiguration : IEntityTypeConfiguration<AvailabilityStatus>
    {
        public void Configure(EntityTypeBuilder<AvailabilityStatus> builder)
        {
            // Keys
            builder.HasKey(status => status.AvailabilityStatusId);

            // Properties
            builder.Property(status => status.Name)
                .IsRequired();

            builder.Property(status => status.IndicatorColor)
                .IsRequired();

            builder.Property(status => status.IndicatorOverlay);

            // Relationships
            builder.HasMany(status => status.Availabilities)
                .WithOne(availability => availability.Status);
        }
    }
}
