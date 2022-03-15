using Core.Application.Database;
using Core.Application.Requests.Messages.Commands;
using Core.Domain.Entities;
using Moq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Core.Application.Test.Requests.Messages.Commands;

public class EditMessageCommandTests
{
    private readonly Mock<IUnitOfWork> _unitOfWorkMock;

    public EditMessageCommandTests()
    {
        _unitOfWorkMock = new Mock<IUnitOfWork>();
    }

    [Fact]
    public async Task EditMessageCommandHandler_ShouldUpdateMessagesContent()
    {
        // Arrange
        EditMessageCommand request = new()
        {
            MessageId = 1,
            HtmlContent = "<p>hello world</p>"
        };

        Message databaseMessage = new() { MessageId = 1, HtmlContent = "<p>original</p>", IsEdited = false };

        _unitOfWorkMock
            .Setup(m => m.Messages.GetByIdAsync(request.MessageId))
            .ReturnsAsync(databaseMessage);

        Message passedMessage = null;

        _unitOfWorkMock
            .Setup(m => m.Messages.Update(It.IsAny<Message>()))
            .Callback<Message>(m => passedMessage = m);

        _unitOfWorkMock
            .Setup(m => m.CommitAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(1);

        EditMessageCommand.Handler handler = new(_unitOfWorkMock.Object);

        // Act
        await handler.Handle(request);

        // Assert
        _unitOfWorkMock.Verify(m => m.Messages.Update(It.IsAny<Message>()), Times.Once);
        _unitOfWorkMock.Verify(m => m.CommitAsync(It.IsAny<CancellationToken>()), Times.AtLeastOnce);

        Assert.NotNull(passedMessage);
        Assert.Equal(request.HtmlContent, passedMessage.HtmlContent);
        Assert.True(passedMessage.IsEdited);
    }
}