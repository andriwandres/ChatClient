using Core.Domain.Entities;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Core.Application.Repositories
{
    public interface IAvailabilityRepository
    {
        IQueryable<Availability> GetByUser(int userId);
        Task Add(Availability availability, CancellationToken cancellationToken = default);
        void Update(Availability availability);
    }
}
