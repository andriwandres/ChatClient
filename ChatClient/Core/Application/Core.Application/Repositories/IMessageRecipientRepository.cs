using Core.Application.Common;
using Core.Domain.Dtos.Messages;
using Core.Domain.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Core.Application.Repositories
{
    public interface IMessageRecipientRepository : IRepository<MessageRecipient>
    {
        Task<List<MessageRecipient>> GetMessagesWithRecipient(int userId, int recipientId, MessageBoundaries boundaries);
        Task<List<MessageRecipient>> GetLatestGroupedByRecipients(int userId);
        Task Add(MessageRecipient messageRecipient, CancellationToken cancellationToken = default);
        Task AddRange(IEnumerable<MessageRecipient> messageRecipients, CancellationToken cancellationToken = default);
    }
}