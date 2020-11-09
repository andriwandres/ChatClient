using Core.Application.Database;
using Core.Application.Repositories;
using Infrastructure.Persistence.Database;
using Moq;
using System.Threading;
using System.Threading.Tasks;
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
        public void Users_ShouldLazyLoadRepository()
        {
            // Arrange
            IUnitOfWork unitOfWork = new UnitOfWork(null);

            // Act
            IUserRepository firstRepository = unitOfWork.Users;
            IUserRepository secondRepository = unitOfWork.Users;

            // Assert
            Assert.NotNull(firstRepository);
            Assert.NotNull(secondRepository);
            Assert.Equal(firstRepository, secondRepository);
        }

        [Fact]
        public void Languages_ShouldLazyLoadRepository()
        {
            // Arrange
            IUnitOfWork unitOfWork = new UnitOfWork(null);

            // Act
            ILanguageRepository firstRepository = unitOfWork.Languages;
            ILanguageRepository secondRepository = unitOfWork.Languages;

            // Assert
            Assert.NotNull(firstRepository);
            Assert.NotNull(secondRepository);
            Assert.Equal(firstRepository, secondRepository);
        }

        [Fact]
        public void Translations_ShouldLazyLoadRepository()
        {
            // Arrange
            IUnitOfWork unitOfWork = new UnitOfWork(null);

            // Act
            ITranslationRepository firstRepository = unitOfWork.Translations;
            ITranslationRepository secondRepository = unitOfWork.Translations;

            // Assert
            Assert.NotNull(firstRepository);
            Assert.NotNull(secondRepository);
            Assert.Equal(firstRepository, secondRepository);
        }
    }
}
