using Core.Application.Database;
using Core.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace Core.Application.Requests.Groups.Commands
{
    public class UpdateGroupCommand : IRequest
    {
        public int GroupId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public class Handler : IRequestHandler<UpdateGroupCommand, Unit>
        {
            private readonly IUnitOfWork _unitOfWork;

            public Handler(IUnitOfWork unitOfWork)
            {
                _unitOfWork = unitOfWork;
            }

            public async Task<Unit> Handle(UpdateGroupCommand request, CancellationToken cancellationToken = default)
            {
                Group group = await _unitOfWork.Groups
                    .GetById(request.GroupId)
                    .SingleOrDefaultAsync(cancellationToken);

                group.Name = request.Name;
                group.Description = request.Description;

                _unitOfWork.Groups.Update(group);
                await _unitOfWork.CommitAsync(cancellationToken);

                return Unit.Value;
            }
        }
    }
}
