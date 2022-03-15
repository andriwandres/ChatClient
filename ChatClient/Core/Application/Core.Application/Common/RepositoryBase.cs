using Core.Application.Database;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core.Application.Common;

public abstract class RepositoryBase<TEntity> : IRepository<TEntity> where TEntity : class
{
    protected readonly IChatContext Context;

    protected RepositoryBase(IChatContext context)
    {
        Context = context;
    }

    public async Task<List<TEntity>> GetAllAsync()
    {
        return await Context.Set<TEntity>().ToListAsync();
    }

    public async Task<TEntity> GetByIdAsync(params object[] keyValues)
    {
        return await Context.Set<TEntity>().FindAsync(keyValues);
    }
}