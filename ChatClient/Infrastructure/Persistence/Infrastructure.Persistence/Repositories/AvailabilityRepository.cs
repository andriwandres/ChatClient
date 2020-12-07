using Core.Application.Common;
using Core.Application.Database;
using Core.Application.Repositories;
using Core.Domain.Entities;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Infrastructure.Persistence.Repositories
{
    public class AvailabilityRepository : RepositoryBase, IAvailabilityRepository
    {
        public AvailabilityRepository(IChatContext context) : base(context)
        {
        }

        public IQueryable<Availability> GetByUser(int userId)
        {
            return Context.Availabilities
                .Where(availability => availability.UserId == userId);
        }

        public async Task Add(Availability availability, CancellationToken cancellationToken = default)
        {
            await Context.Availabilities.AddAsync(availability, cancellationToken);
        }

        public void Update(Availability availability)
        {
            Context.Availabilities.Update(availability);
        }
    }
}
