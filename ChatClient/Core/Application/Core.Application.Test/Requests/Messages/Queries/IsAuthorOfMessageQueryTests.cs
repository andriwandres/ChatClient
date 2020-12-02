using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Core.Application.Database;
using Core.Application.Requests.Messages.Queries;
using Core.Domain.Entities;
using Microsoft.AspNetCore.Http;
using MockQueryable.Moq;
using Moq;
using Xunit;

namespace Core.Application.Test.Requests.Messages.Queries
{
    public class IsAuthorOfMessageQueryTests
    {
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;
        private readonly Mock<IHttpContextAccessor> _httpContextAccessor;

        public IsAuthorOfMessageQueryTests()
        {
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _httpContextAccessor = new Mock<IHttpContextAccessor>();

            Claim nameIdentifierClaim = new Claim(ClaimTypes.NameIdentifier, 1.ToString());

            _httpContextAccessor
                .Setup(m => m.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier))
                .Returns(nameIdentifierClaim);
        }

        [Fact]
        public async Task IsAuthorOfMessageQuery_ShouldReturnTrue_WhenUserIsAuthor()
        {
            // Arrange
            IsAuthorOfMessageQuery request = new IsAuthorOfMessageQuery { MessageId = 1 };

            IQueryable<Message> databaseMessage = new[]
            {
                new Message { MessageId = 1, AuthorId = 1 }
            }
            .AsQueryable()
            .BuildMock()
            .Object;

            _unitOfWorkMock
                .Setup(m => m.Messages.GetById(request.MessageId))
                .Returns(databaseMessage);

            IsAuthorOfMessageQuery.Handler handler = new IsAuthorOfMessageQuery.Handler(_unitOfWorkMock.Object, _httpContextAccessor.Object);

            // Act
            bool isAuthor = await handler.Handle(request);

            // Assert
            Assert.True(isAuthor);
        }

        [Fact]
        public async Task IsAuthorOfMessageQuery_ShouldReturnFalse_WhenUserIsNotAuthor()
        {
            // Arrange
            IsAuthorOfMessageQuery request = new IsAuthorOfMessageQuery { MessageId = 1 };

            IQueryable<Message> databaseMessage = new[]
                {
                    new Message { MessageId = 1, AuthorId = 2 }
                }
                .AsQueryable()
                .BuildMock()
                .Object;

            _unitOfWorkMock
                .Setup(m => m.Messages.GetById(request.MessageId))
                .Returns(databaseMessage);

            IsAuthorOfMessageQuery.Handler handler = new IsAuthorOfMessageQuery.Handler(_unitOfWorkMock.Object, _httpContextAccessor.Object);

            // Act
            bool isAuthor = await handler.Handle(request);

            // Assert
            Assert.False(isAuthor);
        }
    }
}
