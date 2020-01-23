using ChatClient.Core.Models;
using ChatClient.Core.Repositories;
using ChatClient.Data.Database;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChatClient.Data.Repositories
{
    public class GroupMembershipRepository : Repository<GroupMembership>, IGroupMembershipRepository
    {
        public GroupMembershipRepository(ChatContext context) : base(context) { }
    }
}
