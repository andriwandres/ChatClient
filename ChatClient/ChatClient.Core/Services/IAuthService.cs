using ChatClient.Core.Models.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ChatClient.Core.Services
{
    public interface IAuthService
    {
        Task<User> GetUser();
        Task<bool> IsEmailTaken(string email);
        Task<AuthenticatedUser>
    }
}
