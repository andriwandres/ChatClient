using System.Collections.Generic;
using Core.Application.Common;
using Core.Application.Database;
using Core.Application.Repositories;
using Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Persistence.Repositories
{
    public class TranslationRepository : RepositoryBase<Translation>, ITranslationRepository
    {
        public TranslationRepository(IChatContext context) : base(context)
        {
        }

        public async Task<List<Translation>> GetByLanguage(int languageId)
        {
            return await Context.Translations
                .AsNoTracking()
                .Where(translation => translation.LanguageId == languageId)
                .OrderBy(translation => translation.Key)
                .ToListAsync();
        }

        public async Task<List<Translation>> GetByLanguage(int languageId, string pattern)
        {
            return await Context.Translations
                .AsNoTracking()
                .Where(translation => translation.LanguageId == languageId && EF.Functions.Like(translation.Key, pattern))
                .OrderBy(translation => translation.Key)
                .ToListAsync();
        }
    }
}
