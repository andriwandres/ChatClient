using ChatClient.Core.Models.Domain;
using ChatClient.Core.Repositories;
using ChatClient.Data.Database;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ChatClient.Data.Repositories
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(ChatContext context) : base(context) { }

        public async Task<User> GetUserByEmail(string email)
        {
            email = email.ToLower();
            return await SingleOrDefault(user => user.Email.ToLower() == email);
        }

        public async Task<bool> IsEmailTaken(string email)
        {
            email = email.ToLower();
            return await Any(user => user.Email.ToLower() == email);
        }

        public async Task<bool> UserCodeExists(string code)
        {
            code = code.ToLower();
            return await Any(user => user.UserCode.ToLower() == code);
        }
    }
}
