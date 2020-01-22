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

        public UnitOfWork(ChatContext context)
        {
            _context = context;
        }

        public IUserRepository UserRepository => _userRepository = _userRepository ?? new UserRepository(_context);

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
