using Core.Application.Database;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Core.Application.Requests.Messages.Queries
{
    public class MessageExistsQuery : IRequest<bool>
    {
        public int MessageId { get; set; }

        public class Handler : IRequestHandler<MessageExistsQuery, bool>
        {
            private readonly IUnitOfWork _unitOfWork;

            public Handler(IUnitOfWork unitOfWork)
            {
                _unitOfWork = unitOfWork;
            }

            public async Task<bool> Handle(MessageExistsQuery request, CancellationToken cancellationToken = default)
            {
                bool exists = await _unitOfWork.Messages.Exists(request.MessageId, cancellationToken);

                return exists;
            }
        }
    }
}
