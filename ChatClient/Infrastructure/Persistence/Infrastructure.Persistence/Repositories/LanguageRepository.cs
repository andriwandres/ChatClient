using Core.Application.Common;
using Core.Application.Database;
using Core.Application.Repositories;
using Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Infrastructure.Persistence.Repositories
{
    public class LanguageRepository : RepositoryBase, ILanguageRepository
    {
        public LanguageRepository(IChatContext context) : base(context)
        {
        }

        public IQueryable<Language> GetAll()
        {
            return Context.Languages.AsNoTracking();
        }

        public async Task<bool> Exists(int languageId, CancellationToken cancellationToken = default)
        {
            return await Context.Languages
                .AsNoTracking()
                .AnyAsync(language => language.LanguageId == languageId, cancellationToken);
        }
    }
}
