using Core.Domain.Entities;
using System.Threading;
using System.Threading.Tasks;
using Core.Application.Common;

namespace Core.Application.Repositories
{
    public interface IRecipientRepository : IRepository<Recipient>
    {
        Task<Recipient> GetByIdIncludingMemberships(int recipientId, CancellationToken cancellationToken = default);

        Task<bool> Exists(int recipientId, CancellationToken cancellationToken = default);

        Task Add(Recipient recipient, CancellationToken cancellationToken = default);
    }
}
