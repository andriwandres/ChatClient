using Core.Application.Database;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Core.Application.Requests.Users.Queries
{
    public class EmailExistsQuery : IRequest<bool>
    {
        public string Email { get; set; }

        public class Handler : IRequestHandler<EmailExistsQuery, bool>
        {
            private readonly IUnitOfWork _unitOfWork;

            public Handler(IUnitOfWork unitOfWork)
            {
                _unitOfWork = unitOfWork;
            }

            public async Task<bool> Handle(EmailExistsQuery request, CancellationToken cancellationToken = default)
            {
                bool exists = await _unitOfWork.Users.EmailExists(request.Email, cancellationToken);

                return exists;
            }
        }
    }
}
