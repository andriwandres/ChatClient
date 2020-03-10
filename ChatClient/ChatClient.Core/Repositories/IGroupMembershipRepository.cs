using ChatClient.Core.Models.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ChatClient.Core.Repositories
{
    public interface IGroupMembershipRepository
    {
        Task<IEnumerable<GroupMembership>> GetMemberships(int groupId);
    }
}
