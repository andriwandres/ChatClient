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
        public async Task DisposeAsync_ShouldDisposeContextAsynchronously()
        {
            // Arrange
            Mock<IChatContext> contextMock = new Mock<IChatContext>();

            IUnitOfWork unitOfWork = new UnitOfWork(contextMock.Object);

            // Act
            await unitOfWork.DisposeAsync();

            // Assert
            contextMock.Verify(m => m.DisposeAsync());
        }

        [Fact]
        public void Countries_ShouldLazyLoadRepository()
        {
            // Arrange
            IUnitOfWork unitOfWork = new UnitOfWork(null);

            // Act
            ICountryRepository firstRepository = unitOfWork.Countries;
            ICountryRepository secondRepository = unitOfWork.Countries;

            // Assert
            Assert.NotNull(firstRepository);
            Assert.NotNull(secondRepository);
            Assert.Equal(firstRepository, secondRepository);
        }

        [Fact]
        public void FriendshipChanges_ShouldLazyLoadRepository()
        {
            // Arrange
            IUnitOfWork unitOfWork = new UnitOfWork(null);

            // Act
            IFriendshipChangeRepository firstRepository = unitOfWork.FriendshipChanges;
            IFriendshipChangeRepository secondRepository = unitOfWork.FriendshipChanges;

            // Assert
            Assert.NotNull(firstRepository);
            Assert.NotNull(secondRepository);
            Assert.Equal(firstRepository, secondRepository);
        }

        [Fact]
        public void Friendships_ShouldLazyLoadRepository()
        {
            // Arrange
            IUnitOfWork unitOfWork = new UnitOfWork(null);

            // Act
            IFriendshipRepository firstRepository = unitOfWork.Friendships;
            IFriendshipRepository secondRepository = unitOfWork.Friendships;

            // Assert
            Assert.NotNull(firstRepository);
            Assert.NotNull(secondRepository);
            Assert.Equal(firstRepository, secondRepository);
        }

        [Fact]
        public void GroupMemberships_ShouldLazyLoadRepository()
        {
            // Arrange
            IUnitOfWork unitOfWork = new UnitOfWork(null);

            // Act
            IGroupMembershipRepository firstRepository = unitOfWork.GroupMemberships;
            IGroupMembershipRepository secondRepository = unitOfWork.GroupMemberships;

            // Assert
            Assert.NotNull(firstRepository);
            Assert.NotNull(secondRepository);
            Assert.Equal(firstRepository, secondRepository);
        }

        [Fact]
        public void Groups_ShouldLazyLoadRepository()
        {
            // Arrange
            IUnitOfWork unitOfWork = new UnitOfWork(null);

            // Act
            IGroupRepository firstRepository = unitOfWork.Groups;
            IGroupRepository secondRepository = unitOfWork.Groups;

            // Assert
            Assert.NotNull(firstRepository);
            Assert.NotNull(secondRepository);
            Assert.Equal(firstRepository, secondRepository);
        }

        [Fact]
        public void Recipients_ShouldLazyLoadRepository()
        {
            // Arrange
            IUnitOfWork unitOfWork = new UnitOfWork(null);

            // Act
            IRecipientRepository firstRepository = unitOfWork.Recipients;
            IRecipientRepository secondRepository = unitOfWork.Recipients;

            // Assert
            Assert.NotNull(firstRepository);
            Assert.NotNull(secondRepository);
            Assert.Equal(firstRepository, secondRepository);
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
    }
}
