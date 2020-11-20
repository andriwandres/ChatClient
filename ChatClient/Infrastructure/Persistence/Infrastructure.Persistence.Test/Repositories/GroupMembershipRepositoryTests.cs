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
    }
}
