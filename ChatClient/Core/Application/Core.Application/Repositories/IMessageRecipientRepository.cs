using Core.Domain.Entities;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Core.Application.Repositories
{
    public interface IMessageRecipientRepository
    {
        Task Add(MessageRecipient messageRecipient, CancellationToken cancellationToken = default);
        Task AddRange(IEnumerable<MessageRecipient> messageRecipients, CancellationToken cancellationToken = default);
    }
}