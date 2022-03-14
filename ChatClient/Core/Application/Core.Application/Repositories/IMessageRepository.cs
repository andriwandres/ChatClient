using Core.Application.Common;
using Core.Domain.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace Core.Application.Repositories
{
    public interface IMessageRepository : IRepository<Message>
    {
        Task<bool> Exists(int messageId, CancellationToken cancellationToken = default);
        Task<bool> CanAccess(int messageId, int userId, CancellationToken cancellationToken = default);

        Task Add(Message message, CancellationToken cancellationToken = default);
        void Update(Message message);
    }
}
