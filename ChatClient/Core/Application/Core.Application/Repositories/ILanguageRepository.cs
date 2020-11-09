using Core.Domain.Entities;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Core.Application.Repositories
{
    public interface ILanguageRepository
    {
        IQueryable<Language> GetAll();
        Task<bool> Exists(int languageId, CancellationToken cancellationToken = default);
    }
}
