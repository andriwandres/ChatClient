using Core.Domain.Entities;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Core.Application.Repositories
{
    public interface IGroupMembershipRepository
    {
        IQueryable<GroupMembership> GetById(int membershipId);
        IQueryable<GroupMembership> GetByGroup(int groupId);
        IQueryable<GroupMembership> GetByCombination(int groupId, int userId);

        Task<bool> Exists(int membershipId, CancellationToken cancellationToken = default);
        Task<bool> CombinationExists(int groupId, int userId, CancellationToken cancellationToken = default);
        Task<bool> CanUpdateMembership(int userId, int membershipIdToUpdate, CancellationToken cancellationToken = default);
        Task<bool> CanDeleteMembership(int userId, int membershipIdToDelete, CancellationToken cancellationToken = default);

        Task Add(GroupMembership membership, CancellationToken cancellationToken = default);
        void Update(GroupMembership membership);
        void Delete(GroupMembership membership);
    }
}
