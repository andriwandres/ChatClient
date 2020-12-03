using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Core.Application.Database;
using Core.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Core.Application.Requests.Translations.Queries
{
    public class GetTranslationsByLanguageQuery : IRequest<IDictionary<string, string>>
    {
        public int LanguageId { get; set; }
        public string Pattern { get; set; }

        public class Handler : IRequestHandler<GetTranslationsByLanguageQuery, IDictionary<string, string>>
        {
            private readonly IUnitOfWork _unitOfWork;

            public Handler(IUnitOfWork unitOfWork)
            {
                _unitOfWork = unitOfWork;
            }

            public async Task<IDictionary<string, string>> Handle(GetTranslationsByLanguageQuery request, CancellationToken cancellationToken = default)
            {
                if (request.Pattern != null)
                {
                    request.Pattern = request.Pattern.Trim().ToLower().Replace('*', '%');
                }

                IQueryable<Translation> translations = string.IsNullOrEmpty(request.Pattern)
                    ? _unitOfWork.Translations.GetByLanguage(request.LanguageId)
                    : _unitOfWork.Translations.GetByLanguage(request.LanguageId, request.Pattern);

                return await translations.ToDictionaryAsync(
                    translation => translation.Key,
                    translation => translation.Value,
                    cancellationToken
                );
            }
        }
    }
}
