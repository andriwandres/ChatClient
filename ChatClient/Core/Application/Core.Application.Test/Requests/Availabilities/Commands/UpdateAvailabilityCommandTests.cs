using Core.Application.Database;
using Core.Application.Requests.Availabilities.Commands;
using Core.Application.Services;
using Core.Domain.Entities;
using Moq;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Core.Application.Test.Requests.Availabilities.Commands
{
    public class UpdateAvailabilityCommandTests
    {
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;
        private readonly Mock<IUserProvider> _userProviderMock;
        private readonly Mock<IDateProvider> _dateProviderMock;

        public UpdateAvailabilityCommandTests()
        {
            _unitOfWorkMock = new Mock<IUnitOfWork>();

            _userProviderMock = new Mock<IUserProvider>();
            _userProviderMock
                .Setup(m => m.GetCurrentUserId())
                .Returns(1);

            _dateProviderMock = new Mock<IDateProvider>();
            _dateProviderMock
                .Setup(m => m.UtcNow())
                .Returns(new DateTime(2020, 1, 1));
        }

        [Fact]
        public async Task UpdateAvailabilityCommandHandler_ShouldUpdateAvailabilityStatus()
        {
            // Arrange
            UpdateAvailabilityCommand request = new() { AvailabilityStatusId = AvailabilityStatusId.Busy };

            _unitOfWorkMock
                .Setup(m => m.Availabilities.GetByUser(1))
                .ReturnsAsync(new Availability { AvailabilityId = 1, UserId = 1 });

            Availability passedAvailability = null;

            _unitOfWorkMock
                .Setup(m => m.Availabilities.Update(It.IsAny<Availability>()))
                .Callback<Availability>(a => passedAvailability = a);

            _unitOfWorkMock
                .Setup(m => m.CommitAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(1);

            UpdateAvailabilityCommand.Handler handler = new(_unitOfWorkMock.Object, _userProviderMock.Object, _dateProviderMock.Object);

            // Act
            await handler.Handle(request);

            // Assert
            _unitOfWorkMock.Verify(m => m.Availabilities.Update(It.IsAny<Availability>()), Times.Once);
            _unitOfWorkMock.Verify(m => m.CommitAsync(It.IsAny<CancellationToken>()), Times.AtLeastOnce);

            Assert.NotNull(passedAvailability);
            Assert.Equal(request.AvailabilityStatusId, passedAvailability.StatusId);
        }
    }
}