using AutoMapper;
using Core.Application.Database;
using Core.Application.Requests.Users.Queries;
using Core.Domain.Entities;
using Core.Domain.Resources.Users;
using Moq;
using System.Threading.Tasks;
using Xunit;

namespace Core.Application.Test.Requests.Users.Queries
{
    public class GetUserProfileQueryTests
    {
        private readonly IMapper _mapperMock;
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;

        public GetUserProfileQueryTests()
        {
            _unitOfWorkMock = new Mock<IUnitOfWork>();

            MapperConfiguration mapperConfiguration = new(config =>
            {
                config.CreateMap<User, UserProfileResource>();
            });

            _mapperMock = mapperConfiguration.CreateMapper();
        }

        [Fact]
        public async Task GetUserProfileQueryHandler_ShouldReturnUserProfile_WhenIdIsValid()
        {
            // Arrange
            GetUserProfileQuery request = new() { UserId = 1 };

            User expectedUser = new()
            {
                UserId = 1,
                Availability = new Availability { StatusId = AvailabilityStatusId.Online }
            };

            _unitOfWorkMock
                .Setup(m => m.Users.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(expectedUser);

            GetUserProfileQuery.Handler handler = new(_mapperMock, _unitOfWorkMock.Object);

            // Act
            UserProfileResource userProfile = await handler.Handle(request);

            // Assert
            Assert.NotNull(userProfile);
            Assert.Equal(request.UserId, userProfile.UserId);
        }

        [Fact]
        public async Task GetUserProfileQueryHandler_ShouldReturnNull_WhenIdIsInvalid()
        {
            // Arrange
            GetUserProfileQuery request = new() { UserId = 2181 };

            _unitOfWorkMock
                .Setup(m => m.Users.GetByIdAsync(request.UserId))
                .ReturnsAsync(null as User);

            GetUserProfileQuery.Handler handler = new(_mapperMock, _unitOfWorkMock.Object);

            // Act
            UserProfileResource userProfile = await handler.Handle(request);

            // Assert
            Assert.Null(userProfile);
        }
    }
}
