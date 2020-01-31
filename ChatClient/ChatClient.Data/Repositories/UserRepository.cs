using ChatClient.Core.Models.Domain;
using ChatClient.Core.Repositories;
using ChatClient.Data.Database;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace ChatClient.Data.Repositories
{
    public class UserRepository : Repository<ChatContext>, IUserRepository
    {
        public UserRepository(ChatContext context) : base(context) { }

        public async Task<User> GetUserByEmail(string email)
        {
            email = email.ToLower();

            return await Context.Users.SingleOrDefaultAsync(user => user.Email.ToLower() == email);
        }

        public async Task<bool> IsEmailTaken(string email)
        {
            email = email.ToLower();
            return await Context.Users.AnyAsync(user => user.Email.ToLower() == email);
        }

        public async Task<bool> UserCodeExists(string code)
        {
            code = code.ToLower();
            return await Context.Users.AnyAsync(user => user.UserCode.ToLower() == code);
        }
    }
}
