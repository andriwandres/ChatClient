using System.Threading;
using System.Threading.Tasks;
using Core.Application.Common;
using Core.Domain.Entities;

namespace Core.Application.Repositories
{
    public interface ILanguageRepository : IRepository<Language>
    {
        Task<bool> Exists(int languageId, CancellationToken cancellationToken = default);
    }
}
