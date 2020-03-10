using ChatClient.Core.Models.Domain;
using ChatClient.Core.Repositories;
using ChatClient.Data.Database;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatClient.Data.Repositories
{
    public class GroupMembershipRepository : Repository<ChatContext>, IGroupMembershipRepository
    {
        public GroupMembershipRepository(ChatContext context) : base(context) { }

        public async Task<IEnumerable<GroupMembership>> GetMemberships(int groupId)
        {
            IEnumerable<GroupMembership> memberships = await Context.GroupMemberships
                .Where(membership => membership.GroupId == groupId)
                .ToListAsync();

            return memberships;
        }
    }
}
