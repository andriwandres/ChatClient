using Core.Application.Database;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Core.Application.Requests.Recipients.Queries
{
    public class RecipientExistsQuery : IRequest<bool>
    {
        public int RecipientId { get; set; }

        public class Handler : IRequestHandler<RecipientExistsQuery, bool>
        {
            private readonly IUnitOfWork _unitOfWork;

            public Handler(IUnitOfWork unitOfWork)
            {
                _unitOfWork = unitOfWork;
            }

            public async Task<bool> Handle(RecipientExistsQuery request, CancellationToken cancellationToken = default)
            {
                bool exists = await _unitOfWork.Recipients.Exists(request.RecipientId, cancellationToken);

                return exists;
            }
        }
    }
}
