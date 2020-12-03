using Core.Application.Database;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Core.Application.Requests.Languages.Queries
{
    public class LanguageExistsQuery : IRequest<bool>
    {
        public int LanguageId { get; set; }

        public class Handler : IRequestHandler<LanguageExistsQuery, bool>
        {
            private readonly IUnitOfWork _unitOfWork;

            public Handler(IUnitOfWork unitOfWork)
            {
                _unitOfWork = unitOfWork;
            }

            public async Task<bool> Handle(LanguageExistsQuery request, CancellationToken cancellationToken = default)
            {
                return await _unitOfWork.Languages.Exists(request.LanguageId, cancellationToken);
            }
        }
    }
}
