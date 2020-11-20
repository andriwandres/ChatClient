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
    public class GroupRepositoryTests
    {
        [Fact]
        public async Task Add_ShouldAddGroupToTheDbContext()
        {
            // Arrange
            Group group = new Group();

            Mock<DbSet<Group>> groupDbSetMock = Enumerable
                .Empty<Group>()
                .AsQueryable()
                .BuildMockDbSet();

            Mock<IChatContext> contextMock = new Mock<IChatContext>();
            contextMock
                .Setup(m => m.Groups)
                .Returns(groupDbSetMock.Object);

            GroupRepository repository = new GroupRepository(contextMock.Object);

            // Act
            await repository.Add(group);

            // Assert
            contextMock.Verify(m => m.Groups.AddAsync(group, It.IsAny<CancellationToken>()));
        }
    }
}
