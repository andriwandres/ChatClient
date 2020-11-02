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

        int SaveChanges();
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
