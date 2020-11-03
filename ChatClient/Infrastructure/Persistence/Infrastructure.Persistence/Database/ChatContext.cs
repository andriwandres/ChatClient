using Core.Domain.Database;
using Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Infrastructure.Persistence.Database
{
    public class ChatContext : DbContext, IChatContext
    {
        public DbSet<ArchivedRecipient> ArchivedRecipients { get; set; }
        public DbSet<Availability> Availabilities { get; set; }
        public DbSet<AvailabilityStatus> AvailabilityStatuses { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<DisplayImage> DisplayImages { get; set; }
        public DbSet<Emoji> Emojis { get; set; }
        public DbSet<Friendship> Friendships { get; set; }
        public DbSet<FriendshipChange> FriendshipChanges { get; set; }
        public DbSet<FriendshipStatus> FriendshipStatuses { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<GroupMembership> GroupMemberships { get; set; }
        public DbSet<Language> Languages { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<MessageReaction> MessageReactions { get; set; }
        public DbSet<MessageRecipient> MessageRecipients { get; set; }
        public DbSet<NicknameAssignment> NicknameAssignments { get; set; }
        public DbSet<PinnedRecipient> PinnedRecipients { get; set; }
        public DbSet<Recipient> Recipients { get; set; }
        public DbSet<RedeemToken> RedeemTokens { get; set; }
        public DbSet<RedeemTokenType> RedeemTokenTypes { get; set; }
        public DbSet<StatusMessage> StatusMessages { get; set; }
        public DbSet<Translation> Translations { get; set; }
        public DbSet<User> Users { get; set; }

        public ChatContext(DbContextOptions<ChatContext> options) : base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
