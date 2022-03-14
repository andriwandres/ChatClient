using Core.Application.Database;
using Core.Domain.Entities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Core.Application.Requests.Messages.Commands
{
    public class DeleteMessageCommand : IRequest
    {
        public int MessageId { get; set; }

        public class Handler : IRequestHandler<DeleteMessageCommand, Unit>
        {
            private readonly IUnitOfWork _unitOfWork;

            public Handler(IUnitOfWork unitOfWork)
            {
                _unitOfWork = unitOfWork;
            }

            public async Task<Unit> Handle(DeleteMessageCommand request, CancellationToken cancellationToken = default)
            {
                Message message = await _unitOfWork.Messages.GetByIdAsync(request.MessageId);

                message.IsDeleted = true;

                _unitOfWork.Messages.Update(message);
                await _unitOfWork.CommitAsync(cancellationToken);

                return Unit.Value;
            }
        }
    }
}
