using AutoMapper;
using Core.Application.Database;
using Core.Application.Requests.Groups.Commands;
using Core.Application.Services;
using Core.Domain.Entities;
using Core.Domain.Resources.Groups;
using Microsoft.AspNetCore.Http;
using Moq;
using System;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Core.Application.Test.Requests.Groups.Commands
{
    public class CreateGroupCommandTests
    {
        [Fact]
        public async Task CreateGroupCommandHandler_ShouldReturnCreatedGroup()
        {
            // Arrange
            CreateGroupCommand request = new CreateGroupCommand
            {
                Name = "Some group name",
                Description = "Some group description"
            };

            Claim expectedNameIdentifierClaim = new Claim(ClaimTypes.NameIdentifier, "1");

            Mock<IDateProvider> dateProviderMock = new Mock<IDateProvider>();
            dateProviderMock
                .Setup(m => m.UtcNow())
                .Returns(new DateTime(2020, 1, 1, 0, 0, 0));

            Mock<IHttpContextAccessor> httpContextAccessorMock = new Mock<IHttpContextAccessor>();
            httpContextAccessorMock
                .Setup(m => m.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier))
                .Returns(expectedNameIdentifierClaim);

            Mock<IUnitOfWork> unitOfWorkMock = new Mock<IUnitOfWork>();
            unitOfWorkMock
                .Setup(m => m.Groups.Add(It.IsAny<Group>(), It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask);

            unitOfWorkMock
                .Setup(m => m.GroupMemberships.Add(It.IsAny<GroupMembership>(), It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask);

            unitOfWorkMock
                .Setup(m => m.Recipients.Add(It.IsAny<Recipient>(), It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask);

            MapperConfiguration mapperConfiguration = new MapperConfiguration(config =>
            {
                config.CreateMap<Group, GroupResource>();
            });

            IMapper mapperMock = mapperConfiguration.CreateMapper();

            CreateGroupCommand.Handler handler = 
                new CreateGroupCommand.Handler(mapperMock, unitOfWorkMock.Object, dateProviderMock.Object, httpContextAccessorMock.Object);

            // Act
            GroupResource group = await handler.Handle(request);

            // Assert
            Assert.NotNull(group);

            unitOfWorkMock.Verify(m => m.Groups.Add(It.IsAny<Group>(), It.IsAny<CancellationToken>()), Times.Once);
            unitOfWorkMock.Verify(m => m.GroupMemberships.Add(It.IsAny<GroupMembership>(), It.IsAny<CancellationToken>()), Times.Once);
            unitOfWorkMock.Verify(m => m.Recipients.Add(It.IsAny<Recipient>(), It.IsAny<CancellationToken>()), Times.Once);

            unitOfWorkMock.Verify(m => m.CommitAsync(It.IsAny<CancellationToken>()), Times.AtLeastOnce);
        }
    }
}
