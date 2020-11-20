using Core.Domain.Entities;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Core.Application.Repositories
{
    public interface IGroupRepository
    {
        IQueryable<Group> GetById(int groupId);
        Task<bool> Exists(int groupId, CancellationToken cancellationToken = default);
        Task Add(Group group, CancellationToken cancellationToken = default);
        void Update(Group group);
    }
}
