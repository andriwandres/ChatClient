using ChatClient.Core;
using ChatClient.Core.Repositories;
using ChatClient.Data.Database;
using ChatClient.Data.Repositories;
using System.Threading.Tasks;

namespace ChatClient.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ChatContext _context;
        private UserRepository _userRepository;
        private GroupRepository _groupRepository;
        private MessageRepository _messageRepository;
        private GroupMembershipRepository _groupMembershipRepository;
        private MessageRecipientRepository _messageRecipientRepository;

        public UnitOfWork(ChatContext context)
        {
            _context = context;
        }

        public IUserRepository UserRepository => 
            _userRepository = _userRepository ?? new UserRepository(_context);

        public IGroupRepository GroupRepository => 
            _groupRepository = _groupRepository ?? new GroupRepository(_context);

        public IMessageRepository MessageRepository =>
            _messageRepository = _messageRepository ?? new MessageRepository(_context);

        public IGroupMembershipRepository GroupMembershipRepository =>
            _groupMembershipRepository = _groupMembershipRepository ?? new GroupMembershipRepository(_context);

        public IMessageRecipientRepository MessageRecipientRepository =>
            _messageRecipientRepository = _messageRecipientRepository ?? new MessageRecipientRepository(_context);

        public async Task<int> Commit()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
