using Core.Application.Common;
using Core.Application.Database;
using Core.Application.Repositories;
using Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Infrastructure.Persistence.Repositories
{
    public class AvailabilityStatusRepository : RepositoryBase<AvailabilityStatus>, IAvailabilityStatusRepository
    {
        public AvailabilityStatusRepository(IChatContext context) : base(context)
        {
        }
    }
}
