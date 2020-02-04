using ChatClient.Core.Models.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ChatClient.Core.Services
{
    public interface IUserService
    {
        Task<User> GetUserByCode(string code);
    }
}
