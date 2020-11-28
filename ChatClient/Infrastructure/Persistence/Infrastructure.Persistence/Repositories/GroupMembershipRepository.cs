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
    public class GroupMembershipRepository : RepositoryBase, IGroupMembershipRepository
    {
        public GroupMembershipRepository(IChatContext context) : base(context)
        {
        }

        public IQueryable<GroupMembership> GetById(int membershipId)
        {
            return Context.GroupMemberships
                .AsNoTracking()
                .Where(membership => membership.GroupMembershipId == membershipId);
        }

        public IQueryable<GroupMembership> GetByGroup(int groupId)
        {
            return Context.GroupMemberships
                .AsNoTracking()
                .Where(membership => membership.GroupId == groupId);
        }

        public IQueryable<GroupMembership> GetByCombination(int groupId, int userId)
        {
            return Context.GroupMemberships
                .AsNoTracking()
                .Where(membership => membership.GroupId == groupId && membership.UserId == userId);
        }

        public async Task<bool> Exists(int membershipId, CancellationToken cancellationToken = default)
        {
            return await Context.GroupMemberships
                .AsNoTracking()
                .AnyAsync(membership => membership.GroupMembershipId == membershipId, cancellationToken);
        }

        public async Task<bool> CombinationExists(int groupId, int userId, CancellationToken cancellationToken = default)
        {
            return await Context.GroupMemberships
                .AsNoTracking()
                .AnyAsync(membership => membership.GroupId == groupId && membership.UserId == userId, cancellationToken);
        }

        public async Task<bool> CanUpdateMembership(int userId, int membershipIdToUpdate, CancellationToken cancellationToken = default)
        {
            return await Context.GroupMemberships
                .Where(membership => membership.GroupMembershipId == membershipIdToUpdate)
                .Select(membership => membership.Group.Memberships.Any(member => member.UserId == userId && member.IsAdmin))
                .SingleOrDefaultAsync(cancellationToken);
        }

        public async Task<bool> CanDeleteMembership(int userId, int membershipIdToDelete, CancellationToken cancellationToken = default)
        {
            return await Context.GroupMemberships
                .Where(membership => membership.GroupMembershipId == membershipIdToDelete)
                .Select(membership => membership.Group.Memberships.Any(member => member.UserId == userId && member.IsAdmin))
                .SingleOrDefaultAsync(cancellationToken);
        }

        public async Task Add(GroupMembership membership, CancellationToken cancellationToken = default)
        {
            await Context.GroupMemberships.AddAsync(membership, cancellationToken);
        }

        public void Update(GroupMembership membership)
        {
            Context.GroupMemberships.Update(membership);
        }

        public void Delete(GroupMembership membership)
        {
            Context.GroupMemberships.Remove(membership);
        }
    }
}
