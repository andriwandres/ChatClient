using Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Core.Application.Database;

public interface IChatContext : IDisposable, IAsyncDisposable
{
    ChangeTracker ChangeTracker { get; }

    DbSet<ArchivedRecipient> ArchivedRecipients { get; set; }
    DbSet<Availability> Availabilities { get; set; }
    DbSet<Country> Countries { get; set; }
    DbSet<DisplayImage> DisplayImages { get; set; }
    DbSet<Friendship> Friendships { get; set; }
    DbSet<FriendshipChange> FriendshipChanges { get; set; }
    DbSet<Group> Groups { get; set; }
    DbSet<GroupMembership> GroupMemberships { get; set; }
    DbSet<Message> Messages { get;set; }
    DbSet<MessageReaction> MessageReactions { get;set; }
    DbSet<MessageRecipient> MessageRecipients { get;set; }
    DbSet<NicknameAssignment> NicknameAssignments { get; set; }
    DbSet<PinnedRecipient> PinnedRecipients { get; set; }
    DbSet<Recipient> Recipients { get; set; }
    DbSet<RedeemToken> RedeemTokens { get; set; }
    DbSet<User> Users { get; set; }

    int SaveChanges();
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    DbSet<TEntity> Set<TEntity>() where TEntity : class;
}