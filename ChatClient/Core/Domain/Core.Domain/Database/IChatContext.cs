using Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Core.Domain.Database
{
    public interface IChatContext : IDisposable
    {
        DbSet<ArchivedRecipient> ArchivedRecipients { get; set; }
        DbSet<Availability> Availabilities { get; set; }
        DbSet<AvailabilityStatus> AvailabilityStatuses { get; set; }
        DbSet<Country> Countries { get; set; }
        DbSet<DisplayImage> DisplayImages { get; set; }
        DbSet<Friendship> Friendships { get; set; }
        DbSet<FriendshipChange> FriendshipChanges { get; set; }
        DbSet<FriendshipStatus> FriendshipStatuses { get; set; }
        DbSet<Group> Groups { get; set; }
        DbSet<GroupMembership> GroupMemberships { get; set; }
        DbSet<Language> Languages { get; set; }
        DbSet<PinnedRecipient> PinnedRecipients { get; set; }
        DbSet<Translation> Translations { get; set; }
        DbSet<User> Users { get; set; }

        int SaveChanges();
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
