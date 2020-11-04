using Core.Application.Database;

namespace Core.Application.Common
{
    public abstract class RepositoryBase
    {
        protected readonly IChatContext Context;

        protected RepositoryBase(IChatContext context)
        {
            Context = context;
        }
    }
}
