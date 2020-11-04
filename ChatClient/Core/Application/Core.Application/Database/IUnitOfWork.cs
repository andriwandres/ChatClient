using System;
using System.Threading;
using System.Threading.Tasks;
using Core.Application.Repositories;

namespace Core.Application.Database
{
    public interface IUnitOfWork : IDisposable
    {
        IUserRepository Users { get; }

        int Commit();
        Task<int> CommitAsync(CancellationToken cancellationToken = default);
    }
}
