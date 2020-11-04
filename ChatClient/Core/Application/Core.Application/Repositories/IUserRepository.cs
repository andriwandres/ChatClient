using Core.Domain.Entities;
using System.Linq;

namespace Core.Application.Repositories
{
    public interface IUserRepository
    {
        IQueryable<User> GetUserById(int userId);
        IQueryable<User> GetUserByUserNameOrEmail(string userNameOrEmail);
    }
}
