using Core.Application.Database;
using Core.Domain.Entities;
using Infrastructure.Persistence.Repositories;
using Infrastructure.Persistence.Test.Helpers;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Infrastructure.Persistence.Test.Repositories
{
    public class AvailabilityRepositoryTests
    {
        private readonly IChatContext _context;

        public AvailabilityRepositoryTests()
        {
            _context = TestContextFactory.Create();
            
            // Insert Test Data
            for (int index = 1; index <= 3; index++)
            {
                _context.Availabilities.Add(new Availability
                {
                    AvailabilityId = index,
                    UserId = index,
                    StatusId = AvailabilityStatusId.Online,
                    Modified = DateTime.Now,
                    ModifiedManually = false,
                });
            }

            _context.SaveChanges();
            _context.ChangeTracker.Clear();
        }

        #region GetByUser

        [Fact]
        public async Task GetByUser_ShouldReturnNull_WhenAvailabilityWithGivenUserDoesNotExist()
        {
            // Arrange
            const int userId = 5782;

            AvailabilityRepository repository = new(_context);

            // Act
            Availability availability = await repository.GetByUser(userId);

            // Assert
            Assert.Null(availability);
        }

        [Fact]
        public async Task GetByUser_ShouldReturnAvailability_WhenUserIdMatches()
        {
            // Arrange
            const int userId = 1;

            AvailabilityRepository repository = new(_context);

            // Act
            Availability availability = await repository.GetByUser(userId);

            // Assert
            Assert.NotNull(availability);
            Assert.Equal(userId, availability.UserId);
        }

        #endregion

        #region Add

        [Fact]
        public async Task Add_ShouldAddNewAvailability()
        {
            // Arrange
            const int userId = 55;
            Availability availability = new() { UserId = userId };

            AvailabilityRepository repository = new(_context);

            // Act
            await repository.Add(availability);

            // Assert
            Assert.NotEqual(0, availability.AvailabilityId);

            Availability addedAvailability = await _context.Availabilities.FindAsync(availability.AvailabilityId);

            Assert.NotNull(addedAvailability);
            Assert.Equal(userId, addedAvailability.UserId);
        }

        #endregion

        #region Update

        [Fact]
        public async Task Update_ShouldUpdateAvailability()
        {
            // Arrange
            const int userId = 55;
            const int availabilityId = 3;
            Availability availability = new() { AvailabilityId = availabilityId, UserId = userId };

            AvailabilityRepository repository = new(_context);

            // Act
            repository.Update(availability);

            // Assert
            Availability updatedAvailability = await _context.Availabilities.FindAsync(availabilityId);

            Assert.NotNull(updatedAvailability);
            Assert.Equal(userId, availability.UserId);
        }

        #endregion
    }
}
