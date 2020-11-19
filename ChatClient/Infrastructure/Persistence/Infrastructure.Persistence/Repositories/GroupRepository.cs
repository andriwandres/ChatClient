using Core.Application.Common;
using Core.Application.Database;
using Core.Application.Repositories;
using Core.Domain.Entities;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Infrastructure.Persistence.Repositories
{
    public class GroupRepository : RepositoryBase, IGroupRepository
    {
        public GroupRepository(IChatContext context) : base(context)
        {
        }

        public async Task Add(Group @group, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
    }
}
