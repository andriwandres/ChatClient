using Core.Application.Database;
using Core.Domain.Entities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Core.Application.Requests.Groups.Commands;

public class DeleteGroupCommand : IRequest
{
    public int GroupId { get; set; }

    public class Handler : IRequestHandler<DeleteGroupCommand, Unit>
    {
        private readonly IUnitOfWork _unitOfWork;

        public Handler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Unit> Handle(DeleteGroupCommand request, CancellationToken cancellationToken = default)
        {
            Group group = await _unitOfWork.Groups.GetByIdAsync(request.GroupId);

            group.IsDeleted = true;

            _unitOfWork.Groups.Update(group);
            await _unitOfWork.CommitAsync(cancellationToken);

            return Unit.Value;
        }
    }
}