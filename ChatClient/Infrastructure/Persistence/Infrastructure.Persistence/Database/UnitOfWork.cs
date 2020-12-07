using Core.Application.Database;
using Core.Application.Repositories;
using Infrastructure.Persistence.Repositories;
using System.Threading;
using System.Threading.Tasks;

namespace Infrastructure.Persistence.Database
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IChatContext _context;

        private AvailabilityRepository _availabilityRepository;
        private AvailabilityStatusRepository _availabilityStatusRepository;
        private CountryRepository _countryRepository;
        private FriendshipChangeRepository _friendshipChangeRepository;
        private FriendshipRepository _friendshipRepository;
        private GroupMembershipRepository _groupMembershipRepository;
        private GroupRepository _groupRepository;
        private LanguageRepository _languageRepository;
        private MessageRecipientRepository _messageRecipientRepository;
        private MessageRepository _messageRepository;
        private RecipientRepository _recipientRepository;
        private TranslationRepository _translationRepository;
        private UserRepository _userRepository;

        public UnitOfWork(IChatContext context)
        {
            _context = context;
        }

        public IAvailabilityRepository Availabilities => _availabilityRepository ??= new AvailabilityRepository(_context);
        public IAvailabilityStatusRepository AvailabilityStatuses => _availabilityStatusRepository ??= new AvailabilityStatusRepository(_context);
        public ICountryRepository Countries => _countryRepository ??= new CountryRepository(_context);
        public IFriendshipChangeRepository FriendshipChanges => _friendshipChangeRepository ??= new FriendshipChangeRepository(_context);
        public IFriendshipRepository Friendships => _friendshipRepository ??= new FriendshipRepository(_context);
        public IGroupMembershipRepository GroupMemberships => _groupMembershipRepository ??= new GroupMembershipRepository(_context);
        public IGroupRepository Groups => _groupRepository ??= new GroupRepository(_context);
        public ILanguageRepository Languages => _languageRepository ??= new LanguageRepository(_context);
        public IMessageRecipientRepository MessageRecipients => _messageRecipientRepository ??= new MessageRecipientRepository(_context);
        public IMessageRepository Messages => _messageRepository ??= new MessageRepository(_context);
        public IRecipientRepository Recipients => _recipientRepository ??= new RecipientRepository(_context);
        public ITranslationRepository Translations => _translationRepository ??= new TranslationRepository(_context);
        public IUserRepository Users => _userRepository ??= new UserRepository(_context);

        public int Commit()
        {
            return _context.SaveChanges();
        }

        public async Task<int> CommitAsync(CancellationToken cancellationToken = default)
        {
            return await _context.SaveChangesAsync(cancellationToken);
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public async ValueTask DisposeAsync()
        {
            await _context.DisposeAsync();
        }
    }
}
