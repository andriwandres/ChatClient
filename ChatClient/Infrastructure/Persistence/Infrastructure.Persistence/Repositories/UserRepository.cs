using Core.Application.Common;
using Core.Application.Database;
using Core.Application.Repositories;
using Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq;

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
    }
}
