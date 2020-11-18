using Core.Domain.Entities;
using System.Linq;

namespace Core.Application.Repositories
{
    public interface ICountryRepository
    {
        IQueryable<Country> GetAll();
    }
}
