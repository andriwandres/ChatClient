using ChatClient.Core.Models.Domain;
using ChatClient.Data.Configurations;
using ChatClient.Data.Seeds;
using Microsoft.EntityFrameworkCore;

namespace ChatClient.Data.Database
{
    public class ChatContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<UserRelationship> UserRelationships { get; set; }
        public DbSet<GroupMembership> GroupMemberships { get; set; }
        public DbSet<MessageRecipient> MessageRecipients { get; set; }

        public ChatContext(DbContextOptions<ChatContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            // Entity Type Configurations
            builder.ApplyConfiguration(new UserConfiguration());
            builder.ApplyConfiguration(new GroupConfiguration());
            builder.ApplyConfiguration(new GroupMembershipConfiguration());
            builder.ApplyConfiguration(new MessageConfiguration());
            builder.ApplyConfiguration(new MessageRecipientConfiguration());
            builder.ApplyConfiguration(new UserRelationshipConfiguration());

            // Data Seeds
            builder.ApplyConfiguration(new UserSeed());
            builder.ApplyConfiguration(new GroupSeed());
            builder.ApplyConfiguration(new MessageSeed());
            builder.ApplyConfiguration(new GroupMembershipSeed());
            builder.ApplyConfiguration(new MessageRecipientSeed());
        }
    }
}
