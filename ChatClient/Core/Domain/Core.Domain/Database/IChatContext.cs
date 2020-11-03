using System;
using System.Threading;
using System.Threading.Tasks;
using Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Core.Domain.Database
{
    public interface IChatContext : IDisposable
    {
        DbSet<User> Users { get; set; }
        DbSet<Country> Countries { get; set; }
        DbSet<Friendship> Friendships { get; set; }
        DbSet<FriendshipChange> FriendshipChanges { get; set; }
        DbSet<FriendshipStatus> FriendshipStatuses { get; set; }
        DbSet<Language> Languages { get; set; }
        DbSet<Translation> Translations { get; set; }

        int SaveChanges();
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
