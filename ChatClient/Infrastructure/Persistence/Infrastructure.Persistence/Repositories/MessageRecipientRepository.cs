using Core.Application.Common;
using Core.Application.Database;
using Core.Application.Repositories;

namespace Infrastructure.Persistence.Repositories
{
    public class MessageRecipientRepository : RepositoryBase, IMessageRecipientRepository
    {
        public MessageRecipientRepository(IChatContext context) : base(context)
        {
        }
    }
}
