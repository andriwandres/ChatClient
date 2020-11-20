using Core.Domain.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace Core.Application.Repositories
{
    public interface IGroupMembershipRepository
    {
        Task Add(GroupMembership membership, CancellationToken cancellationToken = default);
    }
}
