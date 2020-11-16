using Core.Application.Common;
using Core.Application.Database;
using Core.Application.Repositories;
using Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Infrastructure.Persistence.Repositories
{
    public class UserRepository : RepositoryBase, IUserRepository
    {
        public UserRepository(IChatContext context) : base(context)
        {
        }

        public IQueryable<User> GetById(int userId)
        {
            return Context.Users
                .AsNoTracking()
                .Where(user => user.UserId == userId);
        }

        public IQueryable<User> GetByUserNameOrEmail(string userNameOrEmail)
        {
            userNameOrEmail = userNameOrEmail.ToLower();

            return Context.Users
                .AsNoTracking()
                .Where(user => user.UserName.ToLower() == userNameOrEmail || user.Email.ToLower() == userNameOrEmail);
        }

        public async Task<bool> EmailExists(string email, CancellationToken cancellationToken = default)
        {
            email = email.ToLower();

            return await Context.Users
                .AsNoTracking()
                .AnyAsync(user => user.Email.ToLower() == email, cancellationToken);
        }

        public async Task<bool> UserNameExists(string userName, CancellationToken cancellationToken = default)
        {
            userName = userName.ToLower();

            return await Context.Users
                .AsNoTracking()
                .AnyAsync(user => user.UserName.ToLower() == userName, cancellationToken);
        }

        public async Task<bool> UserNameOrEmailExists(string userName, string email, CancellationToken cancellationToken = default)
        {
            email = email.ToLower();
            userName = userName.ToLower();

            return await Context.Users
                .AsNoTracking()
                .AnyAsync(user => user.UserName.ToLower() == userName || user.Email.ToLower() == email, cancellationToken);
        }

        public async Task Add(User user, CancellationToken cancellationToken = default)
        {
            await Context.Users.AddAsync(user, cancellationToken);
        }

        public IQueryable<Friendship> GetFriendshipsOfUser(int userId)
        {
            return Context.Friendships
                .AsNoTracking()
                .Where(friendship => friendship.RequesterId == userId || friendship.AddresseeId == userId);
        }
    }
}
