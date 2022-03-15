using Core.Application.Database;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Core.Application.Requests.Users.Queries;

public class UserNameOrEmailExistsQuery : IRequest<bool>
{
    public string UserName { get; set; }
    public string Email { get; set; }

    public class Handler : IRequestHandler<UserNameOrEmailExistsQuery, bool>
    {
        private readonly IUnitOfWork _unitOfWork;

        public Handler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(UserNameOrEmailExistsQuery request, CancellationToken cancellationToken = default)
        {
            bool exists = await _unitOfWork.Users.UserNameOrEmailExists(request.UserName, request.Email, cancellationToken);

            return exists;
        }
    }
}