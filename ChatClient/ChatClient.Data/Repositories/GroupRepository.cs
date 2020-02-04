using ChatClient.Core.Repositories;
using ChatClient.Data.Database;

namespace ChatClient.Data.Repositories
{
    public class GroupRepository : Repository<ChatContext>, IGroupRepository
    {
        public GroupRepository(ChatContext context) : base(context) { }
    }
}
