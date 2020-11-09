using Core.Application.Common;
using Core.Application.Database;
using Core.Application.Repositories;
using Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Infrastructure.Persistence.Repositories
{
    public class TranslationRepository : RepositoryBase, ITranslationRepository
    {
        public TranslationRepository(IChatContext context) : base(context)
        {
        }

        public IQueryable<Translation> GetByLanguage(int languageId)
        {
            return Context.Translations
                .AsNoTracking()
                .Where(translation => translation.LanguageId == languageId)
                .OrderBy(translation => translation.Key);
        }

        public IQueryable<Translation> GetByLanguage(int languageId, string pattern)
        {
            return Context.Translations
                .AsNoTracking()
                .Where(translation =>
                    translation.LanguageId == languageId && EF.Functions.Like(translation.Key, pattern))
                .OrderBy(translation => translation.Key);
        }
    }
}
