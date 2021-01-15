using Core.Domain.Dtos.Messages;
using Core.Domain.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Core.Application.Repositories
{
    public interface IMessageRecipientRepository
    {
        IQueryable<MessageRecipient> GetMessagesWithRecipient(int userId, int recipientId, MessageBoundaries boundaries);
        IQueryable<MessageRecipient> GetLatestGroupedByRecipients(int userId);
        Task Add(MessageRecipient messageRecipient, CancellationToken cancellationToken = default);
        Task AddRange(IEnumerable<MessageRecipient> messageRecipients, CancellationToken cancellationToken = default);
    }
}