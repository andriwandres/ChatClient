using AutoMapper;
using AutoMapper.QueryableExtensions;
using Core.Application.Database;
using Core.Domain.Resources.Messages;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;

namespace Core.Application.Requests.Messages.Queries
{
    public class GetMessageByIdQuery : IRequest<MessageResource>
    {
        public int MessageId { get; set; }

        public class Handler : IRequestHandler<GetMessageByIdQuery, MessageResource>
        {
            private readonly IMapper _mapper;
            private readonly IUnitOfWork _unitOfWork;
            private readonly IHttpContextAccessor _httpContextAccessor;

            public Handler(IMapper mapper, IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor)
            {
                _mapper = mapper;
                _unitOfWork = unitOfWork;
                _httpContextAccessor = httpContextAccessor;
            }

            public async Task<MessageResource> Handle(GetMessageByIdQuery request, CancellationToken cancellationToken = default)
            {
                int userId = int.Parse(_httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);

                MessageResource message = await _unitOfWork.Messages
                    .GetById(request.MessageId)
                    .ProjectTo<MessageResource>(_mapper.ConfigurationProvider, new { userId })
                    .SingleOrDefaultAsync(cancellationToken);

                return message;
            }
        }
    }
}
