using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core.Application.Common;

public interface IRepository<TEntity> where TEntity : class
{
    Task<List<TEntity>> GetAllAsync();
    Task<TEntity> GetByIdAsync(params object[] keyValues);
}