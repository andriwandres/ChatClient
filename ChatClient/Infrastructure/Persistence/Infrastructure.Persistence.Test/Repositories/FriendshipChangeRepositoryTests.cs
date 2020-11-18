using System.Collections.Generic;
using System.Diagnostics.Contracts;
using Core.Application.Database;
using Core.Application.Repositories;
using Core.Domain.Entities;
using Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using MockQueryable.Moq;
using Moq;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Infrastructure.Persistence.Test.Repositories
{
    public class FriendshipChangeRepositoryTests
    {
        [Fact]
        public async Task Add_ShouldAddFriendshipChange()
        {
            // Arrange
            FriendshipChange change = new FriendshipChange();

            Mock<DbSet<FriendshipChange>> friendshipChangeDbSetMock = Enumerable
                .Empty<FriendshipChange>()
                .AsQueryable()
                .BuildMockDbSet();

            Mock<IChatContext> contextMock = new Mock<IChatContext>();
            contextMock
                .Setup(m => m.FriendshipChanges)
                .Returns(friendshipChangeDbSetMock.Object);

            IFriendshipChangeRepository repository = new FriendshipChangeRepository(contextMock.Object);

            // Act
            await repository.Add(change);

            // Assert
            contextMock.Verify(m => m.FriendshipChanges.AddAsync(change, It.IsAny<CancellationToken>()));
        }

        [Fact]
        public async Task GetByFriendship_ShouldGetFriendships()
        {
            // Arrange
            const int friendshipId = 1;

            IEnumerable<FriendshipChange> changes = new[]
            {
                new FriendshipChange { FriendshipChangeId = 1, FriendshipId = 1 },
                new FriendshipChange { FriendshipChangeId = 2, FriendshipId = 1 },
                new FriendshipChange { FriendshipChangeId = 3, FriendshipId = 2 },
                new FriendshipChange { FriendshipChangeId = 4, FriendshipId = 2 },
            };

            Mock<DbSet<FriendshipChange>> friendshipChangeDbSetMock = changes
                .AsQueryable()
                .BuildMockDbSet();

            Mock<IChatContext> contextMock = new Mock<IChatContext>();
            contextMock
                .Setup(m => m.FriendshipChanges)
                .Returns(friendshipChangeDbSetMock.Object);

            FriendshipChangeRepository repository = new FriendshipChangeRepository(contextMock.Object);

            // Act
            IEnumerable<FriendshipChange> actualChanges = await repository
                .GetByFriendship(friendshipId)
                .ToListAsync();

            // Assert
            Assert.Equal(2, actualChanges.Count());
            Assert.All(actualChanges, change => Assert.Equal(friendshipId, change.FriendshipId));
        }
    }
}
