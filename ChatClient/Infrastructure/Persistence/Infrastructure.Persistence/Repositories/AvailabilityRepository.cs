using Core.Application.Common;
using Core.Application.Database;
using Core.Application.Repositories;
using Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace Infrastructure.Persistence.Repositories
{
    public class AvailabilityRepository : RepositoryBase<Availability>, IAvailabilityRepository
    {
        public AvailabilityRepository(IChatContext context) : base(context)
        {
        }

        public async Task<Availability> GetByUser(int userId)
        {
            return await Context.Availabilities.SingleOrDefaultAsync(availability => availability.UserId == userId);
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
