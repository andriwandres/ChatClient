using Core.Application.Common;
using Core.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core.Application.Repositories
{
    public interface ITranslationRepository : IRepository<Translation>
    {
        Task<List<Translation>> GetByLanguage(int languageId);
        Task<List<Translation>> GetByLanguage(int languageId, string pattern);
    }
}
