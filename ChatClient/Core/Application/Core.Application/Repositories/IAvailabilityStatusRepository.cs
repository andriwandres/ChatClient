using Core.Domain.Entities;
using System.Linq;

namespace Core.Application.Repositories
{
    public interface IAvailabilityStatusRepository
    {
        IQueryable<AvailabilityStatus> GetAll();
    }
}
