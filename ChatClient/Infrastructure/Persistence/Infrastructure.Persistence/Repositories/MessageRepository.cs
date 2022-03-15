using Core.Application.Common;
using Core.Application.Database;
using Core.Application.Repositories;
using Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Infrastructure.Persistence.Repositories;

public class MessageRepository : RepositoryBase<Message>, IMessageRepository
{
    public MessageRepository(IChatContext context) : base(context)
    {
    }

    public async Task<bool> Exists(int messageId, CancellationToken cancellationToken = default)
    {
        return await Context.Messages
            .AsNoTracking()
            .AnyAsync(message => message.MessageId == messageId, cancellationToken);
    }

    public async Task<bool> CanAccess(int messageId, int userId, CancellationToken cancellationToken = default)
    {
        return await Context.Messages
            .AsNoTracking()
            .Where(message => message.MessageId == messageId)
            .AnyAsync(message =>
                    message.AuthorId == userId ||
                    message.MessageRecipients.Any(mr => (mr.Recipient.UserId ?? mr.Recipient.GroupMembership.UserId) == userId),
                cancellationToken
            );
    }

        

    public async Task Add(Message message, CancellationToken cancellationToken = default)
    {
        await Context.Messages.AddAsync(message, cancellationToken);
    }

    public void Update(Message message)
    {
        Context.Messages.Update(message);
    }
}