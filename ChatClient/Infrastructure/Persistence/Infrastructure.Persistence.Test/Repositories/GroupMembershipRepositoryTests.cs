using Core.Application.Database;
using Core.Domain.Entities;
using Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using MockQueryable.Moq;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Infrastructure.Persistence.Test.Repositories
{
    public class GroupMembershipRepositoryTests
    {
        [Fact]
        public async Task Add_ShouldAddGroupMembershipsToTheDbContext()
        {
            // Arrange
            GroupMembership membership = new GroupMembership();

            Mock<DbSet<GroupMembership>> membershipDbSetMock = Enumerable
                .Empty<GroupMembership>()
                .AsQueryable()
                .BuildMockDbSet();

            Mock<IChatContext> contextMock = new Mock<IChatContext>();
            contextMock
                .Setup(m => m.GroupMemberships)
                .Returns(membershipDbSetMock.Object);

            GroupMembershipRepository repository = new GroupMembershipRepository(contextMock.Object);

            // Act
            await repository.Add(membership);

            // Assert
            contextMock.Verify(m => m.GroupMemberships.AddAsync(membership, It.IsAny<CancellationToken>()));
        }

        [Fact]
        public async Task GetByGroup_ShouldGetMemberships()
        {
            // Arrange
            const int groupId = 1;

            IEnumerable<GroupMembership> databaseMemberships = new []
            {
                new GroupMembership { GroupMembershipId = 1, GroupId = 1 },
                new GroupMembership { GroupMembershipId = 2, GroupId = 1 },
                new GroupMembership { GroupMembershipId = 3, GroupId = 2 },
                new GroupMembership { GroupMembershipId = 4, GroupId = 3 },
            };

            DbSet<GroupMembership> membershipDbSetMock = databaseMemberships
                .AsQueryable()
                .BuildMockDbSet()
                .Object;

            Mock<IChatContext> contextMock = new Mock<IChatContext>();
            contextMock
                .Setup(m => m.GroupMemberships)
                .Returns(membershipDbSetMock);

            GroupMembershipRepository repository = new GroupMembershipRepository(contextMock.Object);

            // Act
            IEnumerable<GroupMembership> actualMemberships = await repository
                .GetByGroup(groupId)
                .ToListAsync();

            // Assert
            Assert.NotNull(actualMemberships);
            Assert.Equal(2, actualMemberships.Count());
            Assert.All(actualMemberships, m => Assert.Equal(groupId, m.GroupId));
        }

        [Fact]
        public async Task CombinationExists_ShouldReturnTrue_WhenCombinationExists()
        {
            // Arrange
            const int userId = 1;
            const int groupId = 2;

            IEnumerable<GroupMembership> databaseMemberships = new[]
            {
                new GroupMembership { GroupMembershipId = 1, GroupId = 1, UserId = 1 },
                new GroupMembership { GroupMembershipId = 2, GroupId = 1, UserId = 2},
                new GroupMembership { GroupMembershipId = 3, GroupId = 2, UserId = 1 },
                new GroupMembership { GroupMembershipId = 4, GroupId = 3, UserId = 1},
            };

            DbSet<GroupMembership> membershipDbSetMock = databaseMemberships
                .AsQueryable()
                .BuildMockDbSet()
                .Object;

            Mock<IChatContext> contextMock = new Mock<IChatContext>();
            contextMock
                .Setup(m => m.GroupMemberships)
                .Returns(membershipDbSetMock);

            GroupMembershipRepository repository = new GroupMembershipRepository(contextMock.Object);

            // Act
            bool exists = await repository.CombinationExists(groupId, userId);

            // Assert
            Assert.True(exists);
        }

        [Fact]
        public async Task CombinationExists_ShouldReturnFalse_WhenCombinationDoesNotExist()
        {
            // Arrange
            const int userId = 2131;
            const int groupId = 412;

            IEnumerable<GroupMembership> databaseMemberships = new[]
            {
                new GroupMembership { GroupMembershipId = 1, GroupId = 1, UserId = 1 },
                new GroupMembership { GroupMembershipId = 2, GroupId = 1, UserId = 2},
                new GroupMembership { GroupMembershipId = 3, GroupId = 2, UserId = 1 },
                new GroupMembership { GroupMembershipId = 4, GroupId = 3, UserId = 1},
            };

            DbSet<GroupMembership> membershipDbSetMock = databaseMemberships
                .AsQueryable()
                .BuildMockDbSet()
                .Object;

            Mock<IChatContext> contextMock = new Mock<IChatContext>();
            contextMock
                .Setup(m => m.GroupMemberships)
                .Returns(membershipDbSetMock);

            GroupMembershipRepository repository = new GroupMembershipRepository(contextMock.Object);

            // Act
            bool exists = await repository.CombinationExists(groupId, userId);

            // Assert
            Assert.False(exists);
        }

        [Fact]
        public async Task GetById_ShouldReturnEmptyQueryable_WhenIdDoesNotMatch()
        {
            // Arrange
            const int membershipId = 43289;

            IEnumerable<GroupMembership> databaseMemberships = new[]
            {
                new GroupMembership { GroupMembershipId = 1, GroupId = 1, UserId = 1 },
                new GroupMembership { GroupMembershipId = 2, GroupId = 1, UserId = 2},
                new GroupMembership { GroupMembershipId = 3, GroupId = 2, UserId = 1 },
                new GroupMembership { GroupMembershipId = 4, GroupId = 3, UserId = 1},
            };

            DbSet<GroupMembership> membershipDbSetMock = databaseMemberships
                .AsQueryable()
                .BuildMockDbSet()
                .Object;

            Mock<IChatContext> contextMock = new Mock<IChatContext>();
            contextMock
                .Setup(m => m.GroupMemberships)
                .Returns(membershipDbSetMock);

            GroupMembershipRepository repository = new GroupMembershipRepository(contextMock.Object);

            // Act
            IEnumerable<GroupMembership> memberships = await repository
                .GetById(membershipId)
                .ToListAsync();

            // Assert
            Assert.NotNull(memberships);
            Assert.Empty(memberships);
        }

        [Fact]
        public async Task GetById_ShouldReturnMembership_WhenIdMatches()
        {
            // Arrange
            const int membershipId = 2;

            IEnumerable<GroupMembership> databaseMemberships = new[]
            {
                new GroupMembership { GroupMembershipId = 1, GroupId = 1, UserId = 1 },
                new GroupMembership { GroupMembershipId = 2, GroupId = 1, UserId = 2},
                new GroupMembership { GroupMembershipId = 3, GroupId = 2, UserId = 1 },
                new GroupMembership { GroupMembershipId = 4, GroupId = 3, UserId = 1},
            };

            DbSet<GroupMembership> membershipDbSetMock = databaseMemberships
                .AsQueryable()
                .BuildMockDbSet()
                .Object;

            Mock<IChatContext> contextMock = new Mock<IChatContext>();
            contextMock
                .Setup(m => m.GroupMemberships)
                .Returns(membershipDbSetMock);

            GroupMembershipRepository repository = new GroupMembershipRepository(contextMock.Object);

            // Act
            GroupMembership membership = await repository
                .GetById(membershipId)
                .SingleOrDefaultAsync();

            // Assert
            Assert.NotNull(membership);
            Assert.Equal(membershipId, membership.GroupMembershipId);
        }

        [Fact]
        public async Task Exists_ShouldReturnTrue_WhenIdMatches()
        {
            // Arrange
            const int membershipId = 2;

            IEnumerable<GroupMembership> databaseMemberships = new[]
            {
                new GroupMembership { GroupMembershipId = 1, GroupId = 1, UserId = 1 },
                new GroupMembership { GroupMembershipId = 2, GroupId = 1, UserId = 2},
                new GroupMembership { GroupMembershipId = 3, GroupId = 2, UserId = 1 },
                new GroupMembership { GroupMembershipId = 4, GroupId = 3, UserId = 1},
            };

            DbSet<GroupMembership> membershipDbSetMock = databaseMemberships
                .AsQueryable()
                .BuildMockDbSet()
                .Object;

            Mock<IChatContext> contextMock = new Mock<IChatContext>();
            contextMock
                .Setup(m => m.GroupMemberships)
                .Returns(membershipDbSetMock);

            GroupMembershipRepository repository = new GroupMembershipRepository(contextMock.Object);

            // Act
            bool exists = await repository.Exists(membershipId);

            // Assert
            Assert.True(exists);
        }

        [Fact]
        public async Task Exists_ShouldReturnFalse_WhenIdDoesNotMatch()
        {
            // Arrange
            const int membershipId = 5452;

            IEnumerable<GroupMembership> databaseMemberships = new[]
            {
                new GroupMembership { GroupMembershipId = 1, GroupId = 1, UserId = 1 },
                new GroupMembership { GroupMembershipId = 2, GroupId = 1, UserId = 2},
                new GroupMembership { GroupMembershipId = 3, GroupId = 2, UserId = 1 },
                new GroupMembership { GroupMembershipId = 4, GroupId = 3, UserId = 1},
            };

            DbSet<GroupMembership> membershipDbSetMock = databaseMemberships
                .AsQueryable()
                .BuildMockDbSet()
                .Object;

            Mock<IChatContext> contextMock = new Mock<IChatContext>();
            contextMock
                .Setup(m => m.GroupMemberships)
                .Returns(membershipDbSetMock);

            GroupMembershipRepository repository = new GroupMembershipRepository(contextMock.Object);

            // Act
            bool exists = await repository.Exists(membershipId);

            // Assert
            Assert.False(exists);
        }
    }
}
