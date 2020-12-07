using Core.Domain.Entities;
using System.Linq;

namespace Core.Application.Repositories
{
    public interface IAvailabilityRepository
    {
        IQueryable<Availability> GetByUser(int userId);
        void Update(Availability availability);
    }
}
