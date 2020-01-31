using ChatClient.Core.Repositories;
using ChatClient.Data.Database;

namespace ChatClient.Data.Repositories
{
    public class GroupMembershipRepository : Repository<ChatContext>, IGroupMembershipRepository
    {
        public GroupMembershipRepository(ChatContext context) : base(context) { }
    }
}
