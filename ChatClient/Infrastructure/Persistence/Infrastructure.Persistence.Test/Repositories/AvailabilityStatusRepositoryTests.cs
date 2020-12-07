using Core.Application.Database;
using Core.Application.Repositories;
using Core.Domain.Entities;
using Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using MockQueryable.Moq;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Infrastructure.Persistence.Test.Repositories
{
    public class AvailabilityStatusRepositoryTests
    {
        private readonly Mock<IChatContext> _contextMock;

        public AvailabilityStatusRepositoryTests()
        {
            _contextMock = new Mock<IChatContext>();
        }

        [Fact]
        public async Task GetAll_ShouldReturnAllAvailabilityStatuses()
        {
            // Arrange
            IEnumerable<AvailabilityStatus> databaseAvailabilityStatuses = new []
            {
                new AvailabilityStatus { AvailabilityStatusId = AvailabilityStatusId.Online },
                new AvailabilityStatus { AvailabilityStatusId = AvailabilityStatusId.Busy },
            };

            DbSet<AvailabilityStatus> dbSetMock = databaseAvailabilityStatuses
                .AsQueryable()
                .BuildMockDbSet()
                .Object;

            _contextMock
                .Setup(m => m.AvailabilityStatuses)
                .Returns(dbSetMock);

            IAvailabilityStatusRepository repository = new AvailabilityStatusRepository(_contextMock.Object);

            // Act
            IEnumerable<AvailabilityStatus> statuses = await repository
                .GetAll()
                .ToListAsync();

            // Assert
            Assert.NotNull(statuses);
            Assert.Equal(2, statuses.Count());
        }
    }
}
