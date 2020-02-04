using ChatClient.Core.Models.Domain;
using ChatClient.Core.Repositories;
using ChatClient.Data.Database;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatClient.Data.Repositories
{
    public class UserRelationshipRepository : Repository<ChatContext>, IUserRelationshipRepository
    {
        public UserRelationshipRepository(ChatContext context) : base(context) { }

        public async Task<IEnumerable<UserRelationship>> GetContacts(int userId)
        {
            IEnumerable<UserRelationship> contacts = await Context.UserRelationships
                .Include(relationship => relationship.Target)
                .Include(relationship => relationship.Initiator)
                .Where(relationship => relationship.InitiatorId == userId || relationship.TargetId == userId)
                .OrderByDescending(relationship => relationship.CreatedAt)
                .ToListAsync();

            return contacts;
        }
    }
}
