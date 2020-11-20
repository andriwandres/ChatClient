using Core.Application.Database;
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
    }
}
