using System;
using System.Threading;
using System.Threading.Tasks;
using Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Core.Application.Database
{
    public interface IChatContext : IDisposable
    {
        DbSet<ArchivedRecipient> ArchivedRecipients { get; set; }
        DbSet<Availability> Availabilities { get; set; }
        DbSet<AvailabilityStatus> AvailabilityStatuses { get; set; }
        DbSet<Country> Countries { get; set; }
        DbSet<DisplayImage> DisplayImages { get; set; }
        DbSet<Emoji> Emojis { get; set; }
        DbSet<Friendship> Friendships { get; set; }
        DbSet<FriendshipChange> FriendshipChanges { get; set; }
        DbSet<FriendshipStatus> FriendshipStatuses { get; set; }
        DbSet<Group> Groups { get; set; }
        DbSet<GroupMembership> GroupMemberships { get; set; }
        DbSet<Language> Languages { get; set; }
        DbSet<Message> Messages { get;set; }
        DbSet<MessageReaction> MessageReactions { get;set; }
        DbSet<MessageRecipient> MessageRecipients { get;set; }
        DbSet<NicknameAssignment> NicknameAssignments { get; set; }
        DbSet<PinnedRecipient> PinnedRecipients { get; set; }
        DbSet<Recipient> Recipients { get; set; }
        DbSet<RedeemToken> RedeemTokens { get; set; }
        DbSet<StatusMessage> StatusMessages { get; set; }
        DbSet<RedeemTokenType> RedeemTokenTypes { get; set; }
        DbSet<Translation> Translations { get; set; }
        DbSet<User> Users { get; set; }

        int SaveChanges();
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
