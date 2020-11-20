using System.Linq;
using Core.Domain.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace Core.Application.Repositories
{
    public interface IGroupRepository
    {
        IQueryable<Group> GetById(int groupId);
        Task Add(Group group, CancellationToken cancellationToken = default);
    }
}
