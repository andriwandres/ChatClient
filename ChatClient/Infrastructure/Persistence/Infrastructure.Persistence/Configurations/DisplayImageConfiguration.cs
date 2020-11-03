using Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations
{
    public class DisplayImageConfiguration : IEntityTypeConfiguration<DisplayImage>
    {
        public void Configure(EntityTypeBuilder<DisplayImage> builder)
        {
            // Keys
            builder.HasKey(image => image.DisplayImageId);

            // Properties
            builder.Property(image => image.Bytes);
        }
    }
}
