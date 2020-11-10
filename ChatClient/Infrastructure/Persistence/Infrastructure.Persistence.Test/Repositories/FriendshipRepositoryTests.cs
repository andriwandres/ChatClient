using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Core.Application.Database;
using Core.Application.Repositories;
using Core.Domain.Entities;
using Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using MockQueryable.Moq;
using Moq;
using Xunit;

namespace Infrastructure.Persistence.Test.Repositories
{
    public class FriendshipRepositoryTests
    {
        [Fact]
        public async Task GetById_ShouldReturnFriendship_WhenIdMatches()
        {
            // Arrange
            const int friendshipId = 1;

            IEnumerable<Friendship> expectedFriendship = new[]
            {
                new Friendship {FriendshipId = 1}
            };

            Mock<DbSet<Friendship>> friendshipDbSetMock = expectedFriendship
                .AsQueryable()
                .BuildMockDbSet();

            Mock<IChatContext> contextMock = new Mock<IChatContext>();
            contextMock
                .Setup(m => m.Friendships)
                .Returns(friendshipDbSetMock.Object);

            IFriendshipRepository repository = new FriendshipRepository(contextMock.Object);

            // Act
            IEnumerable<Friendship> friendships = await repository
                .GetById(friendshipId)
                .ToListAsync();

            // Assert
            Assert.Single(friendships);
            Assert.Equal(friendshipId, friendships.First().FriendshipId);
        }

        [Fact]
        public async Task GetById_ShouldReturnEmptyQueryable_WhenIdDoesNotExist()
        {
            // Arrange
            const int friendshipId = 21231;

            IEnumerable<Friendship> expectedFriendship = new[]
            {
                new Friendship {FriendshipId = 1}
            };

            Mock<DbSet<Friendship>> friendshipDbSetMock = expectedFriendship
                .AsQueryable()
                .BuildMockDbSet();

            Mock<IChatContext> contextMock = new Mock<IChatContext>();
            contextMock
                .Setup(m => m.Friendships)
                .Returns(friendshipDbSetMock.Object);

            IFriendshipRepository repository = new FriendshipRepository(contextMock.Object);

            // Act
            IEnumerable<Friendship> friendships = await repository
                .GetById(friendshipId)
                .ToListAsync();

            // Assert
            Assert.Empty(friendships);
        }

        [Fact]
        public async Task Exists_ShouldReturnTrue_WhenFriendshipExists()
        {
            // Arrange
            const int friendshipId = 1;

            IEnumerable<Friendship> expectedFriendship = new[]
            {
                new Friendship {FriendshipId = 1}
            };

            Mock<DbSet<Friendship>> friendshipDbSetMock = expectedFriendship
                .AsQueryable()
                .BuildMockDbSet();

            Mock<IChatContext> contextMock = new Mock<IChatContext>();
            contextMock
                .Setup(m => m.Friendships)
                .Returns(friendshipDbSetMock.Object);

            IFriendshipRepository repository = new FriendshipRepository(contextMock.Object);

            // Act
            bool exists = await repository.Exists(friendshipId);

            // Assert
            Assert.True(exists);
        }

        [Fact]
        public async Task Exists_ShouldReturnFalse_WhenFriendshipDoesNotExist()
        {
            // Arrange
            const int friendshipId = 9881641;

            IEnumerable<Friendship> expectedFriendship = new[]
            {
                new Friendship {FriendshipId = 1}
            };

            Mock<DbSet<Friendship>> friendshipDbSetMock = expectedFriendship
                .AsQueryable()
                .BuildMockDbSet();

            Mock<IChatContext> contextMock = new Mock<IChatContext>();
            contextMock
                .Setup(m => m.Friendships)
                .Returns(friendshipDbSetMock.Object);

            IFriendshipRepository repository = new FriendshipRepository(contextMock.Object);

            // Act
            bool exists = await repository.Exists(friendshipId);

            // Assert
            Assert.False(exists);
        }

        [Fact]
        public async Task Add_ShouldAddFriendship()
        {
            // Arrange
            Friendship friendship = new Friendship
            {
                RequesterId = 1, AddresseeId = 1
            };

            Mock<DbSet<Friendship>> friendshipDbSetMock = Enumerable
                .Empty<Friendship>()
                .AsQueryable()
                .BuildMockDbSet();

            Mock<IChatContext> contextMock = new Mock<IChatContext>();
            contextMock
                .Setup(m => m.Friendships)
                .Returns(friendshipDbSetMock.Object);

            IFriendshipRepository repository = new FriendshipRepository(contextMock.Object);

            // Act
            await repository.Add(friendship);

            // Assert
            contextMock.Verify(m => m.Friendships.AddAsync(friendship, It.IsAny<CancellationToken>()));
        }

        [Fact]
        public void Remove_ShouldRemoveFriendship()
        {
            // Arrange
            Friendship friendship = new Friendship { FriendshipId = 1 };

            IEnumerable<Friendship> expectedFriendship = new[]
            {
                new Friendship {FriendshipId = 1}
            };

            Mock<DbSet<Friendship>> friendshipDbSetMock = expectedFriendship
                .AsQueryable()
                .BuildMockDbSet();

            Mock<IChatContext> contextMock = new Mock<IChatContext>();
            contextMock
                .Setup(m => m.Friendships)
                .Returns(friendshipDbSetMock.Object);

            IFriendshipRepository repository = new FriendshipRepository(contextMock.Object);

            // Act
            repository.Remove(friendship);

            // Assert
            contextMock.Verify(m => m.Friendships.Remove(friendship));
        }
    }
}