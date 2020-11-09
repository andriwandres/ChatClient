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
        [Fact]
        public async Task GetUserProfileQueryHandler_ShouldReturnUserProfile_WhenIdIsValid()
        {
            // Arrange
            GetUserProfileQuery request = new GetUserProfileQuery { UserId = 1 };

            IEnumerable<User> expectedUser = new[]
            {
                new User {UserId = 1}
            };

            Mock<IQueryable<User>> userQueryableMock = expectedUser.AsQueryable().BuildMock();

            Mock<IUnitOfWork> unitOfWorkMock = new Mock<IUnitOfWork>();
            unitOfWorkMock
                .Setup(m => m.Users.GetById(request.UserId))
                .Returns(userQueryableMock.Object);

            MapperConfiguration mapperConfiguration = new MapperConfiguration(config =>
            {
                config.CreateMap<User, UserProfileResource>();
            });

            IMapper mapperMock = mapperConfiguration.CreateMapper();

            GetUserProfileQuery.GetUserProfileQueryHandler handler = new GetUserProfileQuery.GetUserProfileQueryHandler(mapperMock, unitOfWorkMock.Object);

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

            Mock<IUnitOfWork> unitOfWorkMock = new Mock<IUnitOfWork>();
            unitOfWorkMock
                .Setup(m => m.Users.GetById(request.UserId))
                .Returns(userQueryableMock.Object);

            MapperConfiguration mapperConfiguration = new MapperConfiguration(config =>
            {
                config.CreateMap<User, UserProfileResource>();
            });

            IMapper mapperMock = mapperConfiguration.CreateMapper();

            GetUserProfileQuery.GetUserProfileQueryHandler handler = new GetUserProfileQuery.GetUserProfileQueryHandler(mapperMock, unitOfWorkMock.Object);

            // Act
            UserProfileResource userProfile = await handler.Handle(request);

            // Assert
            Assert.Null(userProfile);
        }
    }
}
