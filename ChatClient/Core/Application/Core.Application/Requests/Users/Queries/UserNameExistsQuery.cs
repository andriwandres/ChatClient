using Core.Application.Database;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Core.Application.Requests.Users.Queries
{
    public class UserNameExistsQuery : IRequest<bool>
    {
        public string UserName { get; set; }

        public class Handler : IRequestHandler<UserNameExistsQuery, bool>
        {
            private readonly IUnitOfWork _unitOfWork;

            public Handler(IUnitOfWork unitOfWork)
            {
                _unitOfWork = unitOfWork;
            }

            public async Task<bool> Handle(UserNameExistsQuery request, CancellationToken cancellationToken = default)
            {
                bool exists = await _unitOfWork.Users.UserNameExists(request.UserName, cancellationToken);

                return exists;
            }
        }
    }
}
