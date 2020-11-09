using Core.Domain.Entities;
using System.Linq;

namespace Core.Application.Repositories
{
    public interface ITranslationRepository
    {
        IQueryable<Translation> GetByLanguage(int languageId);
        IQueryable<Translation> GetByLanguage(int languageId, string pattern);
    }
}
