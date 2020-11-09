using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Core.Application.Database;
using Core.Application.Repositories;
using Core.Application.Requests.Users.Commands;
using Core.Application.Services;
using Core.Domain.Entities;
using Moq;
using Xunit;

namespace Core.Application.Test.Requests.Users.Commands
{
    public class RegisterUserCommandTests
    {
        [Fact]
        public async Task RegisterUserCommandHandler_ShouldReturnGeneratedUserId()
        {
            // Arrange
            RegisterUserCommand request = new RegisterUserCommand
            {
                Email = "new@user.account",
                UserName = "newUser",
                Password = "secretPassword"
            };

            byte[] expectedSalt = new byte[16];
            byte[] expectedHash = new byte[16];
            DateTime expectedDate = new DateTime(2020, 1, 1, 0, 0, 0);

            Mock<IUnitOfWork> unitOfWorkMock = new Mock<IUnitOfWork>();

            unitOfWorkMock
                .Setup(m => m.Users.Add(It.IsAny<User>(), It.IsAny<CancellationToken>()))
                .Callback<User, CancellationToken>((user, _) => user.UserId = 1)
                .Returns(Task.CompletedTask);

            Mock<ICryptoService> cryptoServiceMock = new Mock<ICryptoService>();
            cryptoServiceMock
                .Setup(m => m.GenerateSalt())
                .Returns(expectedSalt);
            
            cryptoServiceMock
                .Setup(m => m.HashPassword(request.Password, expectedSalt))
                .Returns(expectedHash);

            Mock<IDateProvider> dateProviderMock = new Mock<IDateProvider>();
            dateProviderMock
                .Setup(m => m.UtcNow())
                .Returns(expectedDate);

            RegisterUserCommand.RegisterUserCommandHandler handler = 
                new RegisterUserCommand.RegisterUserCommandHandler(cryptoServiceMock.Object, unitOfWorkMock.Object, dateProviderMock.Object);

            // Act
            int id = await handler.Handle(request);

            // Assert
            Assert.NotInRange(id, int.MinValue, 0);

            unitOfWorkMock.Verify(m => m.Users.Add(It.IsAny<User>(), It.IsAny<CancellationToken>()));
            unitOfWorkMock.Verify(m => m.CommitAsync(It.IsAny<CancellationToken>()));
        }
    }
}
