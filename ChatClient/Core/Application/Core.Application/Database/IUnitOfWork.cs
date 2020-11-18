using Core.Application.Repositories;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Core.Application.Database
{
    public interface IUnitOfWork : IDisposable
    {
        ICountryRepository Countries { get; }
        IFriendshipChangeRepository FriendshipChanges { get; }
        IFriendshipRepository Friendships { get; }
        ILanguageRepository Languages { get; }
        ITranslationRepository Translations { get; }
        IUserRepository Users { get; }

        int Commit();
        Task<int> CommitAsync(CancellationToken cancellationToken = default);
    }
}
