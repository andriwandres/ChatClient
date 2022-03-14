using Core.Application.Database;
using Core.Application.Repositories;
using Core.Domain.Entities;
using Infrastructure.Persistence.Repositories;
using Infrastructure.Persistence.Test.Helpers;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Infrastructure.Persistence.Test.Repositories
{
    public class FriendshipChangeRepositoryTests
    {
        private readonly IChatContext _context;

        public FriendshipChangeRepositoryTests()
        {
            _context = TestContextFactory.Create();
        }

        [Fact]
        public async Task Add_ShouldAddFriendshipChange()
        {
            // Arrange
            FriendshipChange change = new();

            IFriendshipChangeRepository repository = new FriendshipChangeRepository(_context);

            // Act
            await repository.Add(change);

            // Assert
            Assert.NotEqual(0, change.FriendshipChangeId);
            FriendshipChange addedChange = await _context.FriendshipChanges.FindAsync(change.FriendshipChangeId);

            Assert.NotNull(addedChange);
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

            await _context.FriendshipChanges.AddRangeAsync(changes);
            await _context.SaveChangesAsync();

            IFriendshipChangeRepository repository = new FriendshipChangeRepository(_context);

            // Act
            IEnumerable<FriendshipChange> actualChanges = await repository.GetByFriendship(friendshipId);

            // Assert
            Assert.Equal(2, actualChanges.Count());
            Assert.All(actualChanges, change => Assert.Equal(friendshipId, change.FriendshipId));
        }
    }
}
