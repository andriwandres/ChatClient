using Core.Application.Database;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Core.Application.Requests.Groups.Queries;

public class GroupExistsQuery : IRequest<bool>
{
    public int GroupId { get; set; }

    public class Handler : IRequestHandler<GroupExistsQuery, bool>
    {
        private readonly IUnitOfWork _unitOfWork;

        public Handler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(GroupExistsQuery request, CancellationToken cancellationToken = default)
        {
            bool exists = await _unitOfWork.Groups.Exists(request.GroupId, cancellationToken);

            return exists;
        }
    }
}