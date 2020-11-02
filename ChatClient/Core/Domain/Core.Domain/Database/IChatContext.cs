using System;
using System.Threading;
using System.Threading.Tasks;

namespace Core.Domain.Database
{
    public interface IChatContext : IDisposable
    {
        int SaveChanges();
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
