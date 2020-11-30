using Core.Application.Database;
using Core.Application.Requests.Users.Commands;
using Core.Application.Services;
using Core.Domain.Entities;
using Moq;
using System;
using System.Net.Mail;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Core.Application.Test.Requests.Users.Commands
{
    public class CreateAccountCommandTests
    {
        [Fact]
        public async Task CreateAccountCommandHandler_ShouldReturnGeneratedUserId()
        {
            // Arrange
            CreateAccountCommand request = new CreateAccountCommand
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
                .Returns(Task.CompletedTask);

            unitOfWorkMock
                .Setup(m => m.Recipients.Add(It.IsAny<Recipient>(), It.IsAny<CancellationToken>()))
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

            CreateAccountCommand.RegisterUserCommandHandler handler = 
                new CreateAccountCommand.RegisterUserCommandHandler(cryptoServiceMock.Object, unitOfWorkMock.Object, dateProviderMock.Object);

            // Act
            await handler.Handle(request);

            // Assert
            unitOfWorkMock.Verify(m => m.Users.Add(It.IsAny<User>(), It.IsAny<CancellationToken>()));
            unitOfWorkMock.Verify(m => m.Recipients.Add(It.IsAny<Recipient>(), It.IsAny<CancellationToken>()));
            unitOfWorkMock.Verify(m => m.CommitAsync(It.IsAny<CancellationToken>()));
        }
    }
}
