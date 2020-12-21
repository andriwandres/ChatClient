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

        public async Task<bool> Exists(int userId, CancellationToken cancellationToken = default)
        {
            return await Context.Users
                .AsNoTracking()
                .AnyAsync(user => user.UserId == userId, cancellationToken);
        }

        public async Task<bool> UserNameOrEmailExists(string userName, string email, CancellationToken cancellationToken = default)
        {
            email = email?.ToLower();
            userName = userName?.ToLower();

            return await Context.Users
                .AsNoTracking()
                .AnyAsync(user => user.UserName.ToLower() == userName || user.Email.ToLower() == email, cancellationToken);
        }

        public async Task Add(User user, CancellationToken cancellationToken = default)
        {
            await Context.Users.AddAsync(user, cancellationToken);
        }
    }
}
