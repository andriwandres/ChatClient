using Core.Domain.Entities;
using System.Linq;

namespace Core.Application.Repositories
{
    public interface ILanguageRepository
    {
        IQueryable<Language> GetAll();
    }
}
