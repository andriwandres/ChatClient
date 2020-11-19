using Core.Domain.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace Core.Application.Repositories
{
    public interface IGroupRepository
    {
        Task Add(Group group, CancellationToken cancellationToken = default);
    }
}
