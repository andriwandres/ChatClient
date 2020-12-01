using Core.Domain.Entities;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Core.Application.Repositories
{
    public interface IRecipientRepository
    {
        IQueryable<Recipient> GetById(int recipientId);

        Task<bool> Exists(int recipientId, CancellationToken cancellationToken = default);

        Task Add(Recipient recipient, CancellationToken cancellationToken = default);
    }
}
