using Core.Application.Database;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Core.Application.Requests.Users.Queries
{
    public class UserExistsQuery : IRequest<bool>
    {
        public int UserId { get; set; }

        public class Handler : IRequestHandler<UserExistsQuery, bool>
        {
            private readonly IUnitOfWork _unitOfWork;

            public Handler(IUnitOfWork unitOfWork)
            {
                _unitOfWork = unitOfWork;
            }

            public async Task<bool> Handle(UserExistsQuery request, CancellationToken cancellationToken = default)
            {
                bool exists = await _unitOfWork.Users.Exists(request.UserId, cancellationToken);

                return exists;
            }
        }
    }
}
