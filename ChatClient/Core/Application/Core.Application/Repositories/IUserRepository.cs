using Core.Domain.Entities;
using System.Linq;

namespace Core.Application.Repositories
{
    public interface IUserRepository
    {
        IQueryable<User> GetById(int userId);
        IQueryable<User> GetByUserNameOrEmail(string userNameOrEmail);
    }
}
