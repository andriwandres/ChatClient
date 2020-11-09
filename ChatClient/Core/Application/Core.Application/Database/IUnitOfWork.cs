using System;
using System.Threading;
using System.Threading.Tasks;
using Core.Application.Repositories;

namespace Core.Application.Database
{
    public interface IUnitOfWork : IDisposable
    {
        ILanguageRepository Languages { get; }
        ITranslationRepository Translations { get; }
        IUserRepository Users { get; }


        int Commit();
        Task<int> CommitAsync(CancellationToken cancellationToken = default);
    }
}
