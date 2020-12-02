using Core.Application.Database;
using Core.Application.Requests.Recipients.Queries;
using Core.Domain.Entities;
using Microsoft.AspNetCore.Http;
using MockQueryable.Moq;
using Moq;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Xunit;

namespace Core.Application.Test.Requests.Recipients.Queries
{
    public class IsOwnRecipientQueryTests
    {
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;
        private readonly Mock<IHttpContextAccessor> _httpContextAccessorMock;

        public IsOwnRecipientQueryTests()
        {
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _httpContextAccessorMock = new Mock<IHttpContextAccessor>();

            Claim nameIdentifierClaim = new Claim(ClaimTypes.NameIdentifier, 1.ToString());

            _httpContextAccessorMock
                .Setup(m => m.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier))
                .Returns(nameIdentifierClaim);
        }

        [Fact]
        public async Task IsOwnRecipientQueryHandler_ShouldReturnTrue_WhenRecipientIsOwnUser()
        {
            // Arrange
            IsOwnRecipientQuery request = new IsOwnRecipientQuery { RecipientId = 1 };

            IQueryable<Recipient> databaseRecipient = new[]
            {
                new Recipient { RecipientId = 1, UserId = 1 }
            }
            .AsQueryable()
            .BuildMock()
            .Object;

            _unitOfWorkMock
                .Setup(m => m.Recipients.GetById(request.RecipientId))
                .Returns(databaseRecipient);

            IsOwnRecipientQuery.Handler handler = new IsOwnRecipientQuery.Handler(_unitOfWorkMock.Object, _httpContextAccessorMock.Object);

            // Act
            bool isOwnRecipient = await handler.Handle(request);

            // Assert
            Assert.True(isOwnRecipient);
        }

        [Fact]
        public async Task IsOwnRecipientQueryHandler_ShouldReturnFalse_WhenRecipientIsForeignUser()
        {
            // Arrange
            IsOwnRecipientQuery request = new IsOwnRecipientQuery { RecipientId = 1 };

            IQueryable<Recipient> databaseRecipient = new[]
            {
                new Recipient { RecipientId = 1, UserId = 2 }
            }
            .AsQueryable()
            .BuildMock()
            .Object;

            _unitOfWorkMock
                .Setup(m => m.Recipients.GetById(request.RecipientId))
                .Returns(databaseRecipient);

            IsOwnRecipientQuery.Handler handler = new IsOwnRecipientQuery.Handler(_unitOfWorkMock.Object, _httpContextAccessorMock.Object);

            // Act
            bool isOwnRecipient = await handler.Handle(request);

            // Assert
            Assert.False(isOwnRecipient);
        }

        [Fact]
        public async Task IsOwnRecipientQueryHandler_ShouldReturnFalse_WhenRecipientIsForeignGroup()
        {
            // Arrange
            IsOwnRecipientQuery request = new IsOwnRecipientQuery { RecipientId = 1 };

            IQueryable<Recipient> databaseRecipient = new[]
            {
                new Recipient { RecipientId = 1, GroupMembershipId = 1 }
            }
            .AsQueryable()
            .BuildMock()
            .Object;

            _unitOfWorkMock
                .Setup(m => m.Recipients.GetById(request.RecipientId))
                .Returns(databaseRecipient);

            IsOwnRecipientQuery.Handler handler = new IsOwnRecipientQuery.Handler(_unitOfWorkMock.Object, _httpContextAccessorMock.Object);

            // Act
            bool isOwnRecipient = await handler.Handle(request);

            // Assert
            Assert.False(isOwnRecipient);
        }
    }
}
