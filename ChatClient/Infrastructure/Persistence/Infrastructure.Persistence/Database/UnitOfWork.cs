using Core.Application.Database;
using Core.Application.Repositories;
using Infrastructure.Persistence.Repositories;
using System.Threading;
using System.Threading.Tasks;

namespace Infrastructure.Persistence.Database
{
    public class UnitOfWork : IUnitOfWork
    {
        private UserRepository _userRepository;

        private readonly IChatContext _context;

        public UnitOfWork(IChatContext context)
        {
            _context = context;
        }

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
    }
}
