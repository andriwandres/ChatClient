using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Core.Application.Database;
using Core.Application.Repositories;
using Infrastructure.Persistence.Database;
using Moq;
using Xunit;

namespace Infrastructure.Persistence.Test.Database
{
    public class UnitOfWorkTests
    {
        [Fact]
        public void Commit_ShouldSaveChangesOnContext()
        {
            // Arrange
            Mock<IChatContext> contextMock = new Mock<IChatContext>();

            IUnitOfWork unitOfWork = new UnitOfWork(contextMock.Object);

            // Act
            unitOfWork.Commit();

            // Assert
            contextMock.Verify(m => m.SaveChanges());
        }

        [Fact]
        public async Task CommitAsync_ShouldSaveChangesOnContext()
        {
            // Arrange
            Mock<IChatContext> contextMock = new Mock<IChatContext>();

            IUnitOfWork unitOfWork = new UnitOfWork(contextMock.Object);

            // Act
            await unitOfWork.CommitAsync();

            // Assert
            contextMock.Verify(m => m.SaveChangesAsync(It.IsAny<CancellationToken>()));
        }

        [Fact]
        public void Dispose_ShouldDisposeContext()
        {
            // Arrange
            Mock<IChatContext> contextMock = new Mock<IChatContext>();

            IUnitOfWork unitOfWork = new UnitOfWork(contextMock.Object);

            // Act
            unitOfWork.Dispose();

            // Assert
            contextMock.Verify(m => m.Dispose());
        }

        [Fact]
        public void Users_ShouldLazyLoadUserRepository()
        {
            // Arrange
            IUnitOfWork unitOfWork = new UnitOfWork(null);

            // Act
            IUserRepository firstUserRepository = unitOfWork.Users;
            IUserRepository secondUserRepository = unitOfWork.Users;

            // Assert
            Assert.NotNull(firstUserRepository);
            Assert.NotNull(secondUserRepository);
            Assert.Equal(firstUserRepository, secondUserRepository);
        }
    }
}
