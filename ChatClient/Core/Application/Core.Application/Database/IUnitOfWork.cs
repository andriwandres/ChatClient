using Core.Application.Repositories;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Core.Application.Database
{
    public interface IUnitOfWork : IDisposable, IAsyncDisposable
    {
        IAvailabilityRepository Availabilities { get; }
        ICountryRepository Countries { get; }
        IFriendshipChangeRepository FriendshipChanges { get; }
        IFriendshipRepository Friendships { get; }
        IGroupMembershipRepository GroupMemberships { get; }
        IGroupRepository Groups { get; }
        ILanguageRepository Languages { get; }
        IMessageRecipientRepository MessageRecipients { get; }
        IMessageRepository Messages { get; }
        IRecipientRepository Recipients { get; }
        ITranslationRepository Translations { get; }
        IUserRepository Users { get; }

        int Commit();
        Task<int> CommitAsync(CancellationToken cancellationToken = default);
    }
}
