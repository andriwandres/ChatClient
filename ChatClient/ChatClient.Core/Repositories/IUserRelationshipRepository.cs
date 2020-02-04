using ChatClient.Core.Models.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ChatClient.Core.Repositories
{
    public interface IUserRelationshipRepository
    {
        Task<IEnumerable<UserRelationship>> GetContacts(int userId);
    }
}
