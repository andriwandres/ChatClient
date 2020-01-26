using ChatClient.Core.Models.Domain;
using ChatClient.Core.Models.Dtos;
using ChatClient.Core.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ChatClient.Core.Services
{
    public interface IAuthService
    {
        Task<AuthenticatedUser> Authenticate();
        Task<User> GetUser();
        Task<bool> IsEmailTaken(string email);
        Task<AuthenticatedUser> Login(LoginDto credentials);
        Task Register(RegisterDto credentials);
    }
}
