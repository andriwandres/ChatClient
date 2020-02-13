using ChatClient.Core.Models.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ChatClient.Data.Configurations
{
    public class DisplayImageConfiguration : IEntityTypeConfiguration<DisplayImage>
    {
        public void Configure(EntityTypeBuilder<DisplayImage> builder)
        {
            builder.HasKey(di => di.DisplayImageId);

            builder.Property(di => di.Image)
                .IsRequired();

            builder.HasOne(di => di.User)
                .WithOne(u => u.ProfileImage)
                .HasForeignKey<User>(u => u.ProfileImageId);

            builder.HasOne(di => di.Group)
                .WithOne(u => u.GroupImage)
                .HasForeignKey<Group>(u => u.GroupImageId);
        }
    }
}
