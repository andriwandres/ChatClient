using Core.Application.Database;
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
    public class AvailabilityRepositoryTests
    {
        private readonly Mock<IChatContext> _contextMock;

        public AvailabilityRepositoryTests()
        {
            _contextMock = new Mock<IChatContext>();
        }

        #region GetByUser()

        [Fact]
        public async Task GetByUser_ShouldReturnEmptyQueryable_WhenAvailabilityWithGivenUserDoesNotExist()
        {
            // Arrange
            const int userId = 5782;

            IEnumerable<Availability> databaseAvailabilities = new[]
            {
                new Availability { AvailabilityId = 1, UserId = 1, },
                new Availability { AvailabilityId = 2, UserId = 2, },
                new Availability { AvailabilityId = 3, UserId = 3, }
            };

            DbSet<Availability> dbSetMock = databaseAvailabilities
                .AsQueryable()
                .BuildMockDbSet()
                .Object;

            _contextMock
                .Setup(m => m.Availabilities)
                .Returns(dbSetMock);

            AvailabilityRepository repository = new AvailabilityRepository(_contextMock.Object);

            // Act
            Availability availability = await repository
                .GetByUser(userId)
                .SingleOrDefaultAsync();

            // Assert
            Assert.Null(availability);
        }

        [Fact]
        public async Task GetByUser_ShouldReturnAvailability_WhenUserIdMatches()
        {
            // Arrange
            const int userId = 1;

            IEnumerable<Availability> databaseAvailabilities = new[]
            {
                new Availability { AvailabilityId = 1, UserId = 1, },
                new Availability { AvailabilityId = 2, UserId = 2, },
                new Availability { AvailabilityId = 3, UserId = 3, }
            };

            DbSet<Availability> dbSetMock = databaseAvailabilities
                .AsQueryable()
                .BuildMockDbSet()
                .Object;

            _contextMock
                .Setup(m => m.Availabilities)
                .Returns(dbSetMock);

            AvailabilityRepository repository = new AvailabilityRepository(_contextMock.Object);

            // Act
            Availability availability = await repository
                .GetByUser(userId)
                .SingleOrDefaultAsync();

            // Assert
            Assert.NotNull(availability);
        }

        #endregion

        #region Update()

        [Fact]
        public async Task Update_ShouldUpdateAvailability()
        {
            // Arrange
            Availability availability = new Availability();

            _contextMock.Setup(m => m.Availabilities.Update(availability));

            AvailabilityRepository repository = new AvailabilityRepository(_contextMock.Object);

            // Act
            repository.Update(availability);

            // Assert
            _contextMock.Verify(m => m.Availabilities.Update(availability));
        }

        #endregion
    }
}
