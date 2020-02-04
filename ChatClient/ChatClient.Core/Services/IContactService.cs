using ChatClient.Core.Models.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ChatClient.Core.Services
{
    public interface IContactService
    {
        Task<User> GetUserByCode(string code);
        Task<IEnumerable<UserRelationship>> GetContacts(int userId);
    }
}
