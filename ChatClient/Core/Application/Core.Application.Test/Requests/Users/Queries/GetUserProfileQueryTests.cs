using AutoMapper;
using Core.Application.Database;
using Core.Application.Requests.Users.Queries;
using Core.Domain.Entities;
using Core.Domain.Resources.Users;
using MockQueryable.Moq;
using Moq;
using System.Collections.Generic;
using System.Linq;
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

            MapperConfiguration mapperConfiguration = new MapperConfiguration(config =>
            {
                config.CreateMap<User, UserProfileResource>();
            });

            _mapperMock = mapperConfiguration.CreateMapper();
        }

        [Fact]
        public async Task GetUserProfileQueryHandler_ShouldReturnUserProfile_WhenIdIsValid()
        {
            // Arrange
            GetUserProfileQuery request = new GetUserProfileQuery { UserId = 1 };

            IEnumerable<User> expectedUser = new[]
            {
                new User
                {
                    UserId = 1,
                    Availability = new Availability { StatusId = AvailabilityStatusId.Online }
                }
            };

            Mock<IQueryable<User>> userQueryableMock = expectedUser.AsQueryable().BuildMock();

            _unitOfWorkMock
                .Setup(m => m.Users.GetById(It.IsAny<int>()))
                .Returns(userQueryableMock.Object);

            GetUserProfileQuery.Handler handler = new GetUserProfileQuery.Handler(_mapperMock, _unitOfWorkMock.Object);

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
            GetUserProfileQuery request = new GetUserProfileQuery { UserId = 2181 };

            Mock<IQueryable<User>> userQueryableMock = Enumerable
                .Empty<User>()
                .AsQueryable()
                .BuildMock();

            _unitOfWorkMock
                .Setup(m => m.Users.GetById(request.UserId))
                .Returns(userQueryableMock.Object);

            GetUserProfileQuery.Handler handler = new GetUserProfileQuery.Handler(_mapperMock, _unitOfWorkMock.Object);

            // Act
            UserProfileResource userProfile = await handler.Handle(request);

            // Assert
            Assert.Null(userProfile);
        }
    }
}
