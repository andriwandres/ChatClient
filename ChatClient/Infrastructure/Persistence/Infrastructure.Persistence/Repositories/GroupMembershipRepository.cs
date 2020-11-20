using Core.Application.Common;
using Core.Application.Database;
using Core.Application.Repositories;
using Core.Domain.Entities;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Infrastructure.Persistence.Repositories
{
    public class GroupMembershipRepository : RepositoryBase, IGroupMembershipRepository
    {
        public GroupMembershipRepository(IChatContext context) : base(context)
        {
        }

        public IQueryable<GroupMembership> GetByGroup(int groupId)
        {
            return Context.GroupMemberships.Where(membership => membership.GroupId == groupId);
        }

        public async Task Add(GroupMembership membership, CancellationToken cancellationToken = default)
        {
            await Context.GroupMemberships.AddAsync(membership, cancellationToken);
        }
    }
}
