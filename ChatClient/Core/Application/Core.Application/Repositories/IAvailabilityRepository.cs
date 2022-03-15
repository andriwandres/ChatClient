using Core.Domain.Entities;
using System.Threading;
using System.Threading.Tasks;
using Core.Application.Common;

namespace Core.Application.Repositories;

public interface IAvailabilityRepository : IRepository<Availability>
{
    Task<Availability> GetByUser(int userId);
    Task Add(Availability availability, CancellationToken cancellationToken = default);
    void Update(Availability availability);
}