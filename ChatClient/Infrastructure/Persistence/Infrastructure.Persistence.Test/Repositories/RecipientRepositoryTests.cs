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
    public class RecipientRepositoryTests
    {
        private readonly Mock<IChatContext> _contextMock;

        public RecipientRepositoryTests()
        {
            _contextMock = new Mock<IChatContext>();
        }

        #region GetById()

        [Fact]
        public async Task GetById_ShouldReturnEmptyQueryable_WhenIdDoesNotMatch()
        {
            // Arrange
            const int recipientId = 5431;

            DbSet<Recipient> databaseRecipients = Enumerable
                .Empty<Recipient>()
                .AsQueryable()
                .BuildMockDbSet()
                .Object;

            _contextMock
                .Setup(m => m.Recipients)
                .Returns(databaseRecipients);

            RecipientRepository repository = new RecipientRepository(_contextMock.Object);

            // Act
            Recipient recipient = await repository
                .GetById(recipientId)
                .SingleOrDefaultAsync();

            // Assert
            Assert.Null(recipient);
        }

        [Fact]
        public async Task GetById_ShouldReturnQueryableWithSingleRecipient_WhenIdMatches()
        {
            // Arrange
            const int recipientId = 2;

            DbSet<Recipient> databaseRecipients = new[]
            {
                new Recipient { RecipientId = 1 },
                new Recipient { RecipientId = 2 },
            }
            .AsQueryable()
            .BuildMockDbSet()
            .Object;

            _contextMock
                .Setup(m => m.Recipients)
                .Returns(databaseRecipients);

            RecipientRepository repository = new RecipientRepository(_contextMock.Object);

            // Act
            Recipient recipient = await repository
                .GetById(recipientId)
                .SingleOrDefaultAsync();

            // Assert
            Assert.NotNull(recipient);
            Assert.Equal(recipientId, recipient.RecipientId);
        }

        #endregion

        #region Exists()

        [Fact]
        public async Task Exists_ShouldReturnTrue_WhenRecipientExists()
        {
            // Arrange
            const int recipientId = 1;

            DbSet<Recipient> databaseRecipients = new[]
            {
                new Recipient {RecipientId = 1},
                new Recipient {RecipientId = 2},
            }
            .AsQueryable()
            .BuildMockDbSet()
            .Object;


            _contextMock
                .Setup(m => m.Recipients)
                .Returns(databaseRecipients);

            RecipientRepository repository = new RecipientRepository(_contextMock.Object);

            // Act
            bool exists = await repository.Exists(recipientId);

            // Assert
            Assert.True(exists);
        }

        [Fact]
        public async Task Exists_ShouldReturnFalse_WhenRecipientDoesNotExist()
        {
            // Arrange
            const int recipientId = 341;

            DbSet<Recipient> databaseRecipients = new[]
            {
                new Recipient {RecipientId = 1},
                new Recipient {RecipientId = 2},
            }
            .AsQueryable()
            .BuildMockDbSet()
            .Object;


            _contextMock
                .Setup(m => m.Recipients)
                .Returns(databaseRecipients);

            RecipientRepository repository = new RecipientRepository(_contextMock.Object);

            // Act
            bool exists = await repository.Exists(recipientId);

            // Assert
            Assert.False(exists);
        }

        #endregion

        #region Add()

        [Fact]
        public async Task Add_ShouldAddRecipientsToTheDbContext()
        {
            // Arrange
            Recipient recipient = new Recipient();

            Mock<DbSet<Recipient>> membershipDbSetMock = Enumerable
                .Empty<Recipient>()
                .AsQueryable()
                .BuildMockDbSet();

            _contextMock
                .Setup(m => m.Recipients)
                .Returns(membershipDbSetMock.Object);

            RecipientRepository repository = new RecipientRepository(_contextMock.Object);

            // Act
            await repository.Add(recipient);

            // Assert
            _contextMock.Verify(m => m.Recipients.AddAsync(recipient, It.IsAny<CancellationToken>()));
        }

        #endregion
    }
}
