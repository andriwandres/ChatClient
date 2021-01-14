using Core.Application.Database;
using Core.Application.Requests.Recipients.Queries;
using Core.Application.Services;
using Core.Domain.Entities;
using Core.Domain.Resources.Recipients;
using MockQueryable.Moq;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Core.Application.Test.Requests.Recipients.Queries
{
    public class GetOwnRecipientsQueryTests
    {
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;
        private readonly Mock<IUserProvider> _userProviderMock;

        public GetOwnRecipientsQueryTests()
        {
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _userProviderMock = new Mock<IUserProvider>();
            _userProviderMock
                .Setup(m => m.GetCurrentUserId())
                .Returns(2);
        }

        [Fact]
        public async Task GetOwnRecipientsQueryHandler_ShouldReturnRelevantRecipients()
        {
            // Arrange
            IEnumerable<MessageRecipient> databaseRecipients = new[]
            {
                new MessageRecipient
                {
                    MessageRecipientId = 1, 
                    MessageId = 1,
                    RecipientId = 1,
                    IsRead = false,
                    IsForwarded = false,
                    Message = new Message
                    {
                        MessageId = 1,
                        AuthorId = 2,
                        Author = new User
                        {
                            UserId = 2,
                            UserName = "someUsername"
                        },
                        HtmlContent = "messageContent",
                        Created = new DateTime(2020, 2, 2, 15, 0, 0),
                    },
                    Recipient = new Recipient
                    {
                        RecipientId = 1,
                        UserId = 1,
                        User = new User
                        {
                            UserId = 1,
                            UserName = "someOtherUsername",
                            Availability = new Availability
                            {
                                StatusId = AvailabilityStatusId.Online
                            }
                        },
                        Pins = new List<PinnedRecipient>(),
                        ReceivedMessages = new List<MessageRecipient>(),
                    },
                },
                new MessageRecipient
                {
                    MessageRecipientId = 2,
                    MessageId = 2,
                    RecipientId = 2,
                    IsRead = true,
                    IsForwarded = false,
                    Message = new Message
                    {
                        MessageId = 2,
                        AuthorId = 1,
                        Author = new User
                        {
                            UserId = 1,
                            UserName = "someOtherUsername"
                        },
                        HtmlContent = "messageContent",
                        Created = new DateTime(2020, 2, 2, 15, 1, 0),
                    },
                    Recipient = new Recipient
                    {
                        RecipientId = 2,
                        GroupMembershipId = 1,
                        GroupMembership = new GroupMembership
                        {
                            GroupId = 1,
                            Group = new Group
                            {
                                GroupId = 1,
                                Name = "someGroupName"
                            }
                        },
                        Pins = new List<PinnedRecipient>(),
                        ReceivedMessages = new List<MessageRecipient>(),
                    },
                }
            };

            IQueryable<MessageRecipient> queryableMock = databaseRecipients
                .AsQueryable()
                .BuildMock()
                .Object;

            _unitOfWorkMock
                .Setup(m => m.MessageRecipients.GetLatestGroupedByRecipients(It.IsAny<int>()))
                .Returns(queryableMock);

            GetOwnRecipientsQuery.Handler handler = new GetOwnRecipientsQuery.Handler(_unitOfWorkMock.Object, _userProviderMock.Object);
            
            // Act
            IEnumerable<RecipientResource> recipients = await handler.Handle(new GetOwnRecipientsQuery());

            // Assert
            Assert.Equal(2, recipients.Count());
        }
    }
}
