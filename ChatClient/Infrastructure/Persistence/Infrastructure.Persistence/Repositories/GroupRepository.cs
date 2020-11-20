using Core.Application.Common;
using Core.Application.Database;
using Core.Application.Repositories;
using Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Infrastructure.Persistence.Repositories
{
    public class GroupRepository : RepositoryBase, IGroupRepository
    {
        public GroupRepository(IChatContext context) : base(context)
        {
        }

        public IQueryable<Group> GetById(int groupId)
        {
            return Context.Groups
                .AsNoTracking()
                .Where(group => group.GroupId == groupId);
        }

        public async Task Add(Group group, CancellationToken cancellationToken = default)
        {
            await Context.Groups.AddAsync(group, cancellationToken);
        }
    }
}
