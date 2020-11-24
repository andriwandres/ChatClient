using Core.Domain.Entities;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Core.Application.Repositories
{
    public interface IGroupMembershipRepository
    {
        IQueryable<GroupMembership> GetByGroup(int groupId);
        Task<bool> CombinationExists(int groupId, int userId, CancellationToken cancellationToken = default);
        Task Add(GroupMembership membership, CancellationToken cancellationToken = default);
    }
}
