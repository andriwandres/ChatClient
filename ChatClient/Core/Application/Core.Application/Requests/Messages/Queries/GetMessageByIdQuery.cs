using AutoMapper;
using AutoMapper.QueryableExtensions;
using Core.Application.Database;
using Core.Application.Services;
using Core.Domain.Resources.Messages;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;
using Core.Domain.Entities;
using Microsoft.EntityFrameworkCore.Query.Internal;

namespace Core.Application.Requests.Messages.Queries;

public class GetMessageByIdQuery : IRequest<MessageResource>
{
    public int MessageId { get; set; }

    public class Handler : IRequestHandler<GetMessageByIdQuery, MessageResource>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserProvider _userProvider;

        public Handler(IMapper mapper, IUnitOfWork unitOfWork, IUserProvider userProvider)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _userProvider = userProvider;
        }

        public async Task<MessageResource> Handle(GetMessageByIdQuery request, CancellationToken cancellationToken = default)
        {
            int userId = _userProvider.GetCurrentUserId();

            Message message = await _unitOfWork.Messages.GetByIdAsync(request.MessageId);

            return _mapper.Map<Message, MessageResource>(message, options =>
            {
                options.Items["userId"] = userId;
            });
        }
    }
}