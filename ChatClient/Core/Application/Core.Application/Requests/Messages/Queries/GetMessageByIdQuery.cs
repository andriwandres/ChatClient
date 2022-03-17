﻿using AutoMapper;
using AutoMapper.QueryableExtensions;
using Core.Application.Database;
using Core.Application.Services;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;
using Core.Domain.Entities;
using Core.Domain.ViewModels.Messages;
using Microsoft.EntityFrameworkCore.Query.Internal;

namespace Core.Application.Requests.Messages.Queries;

public class GetMessageByIdQuery : IRequest<MessageViewModel>
{
    public int MessageId { get; set; }

    public class Handler : IRequestHandler<GetMessageByIdQuery, MessageViewModel>
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

        public async Task<MessageViewModel> Handle(GetMessageByIdQuery request, CancellationToken cancellationToken = default)
        {
            int userId = _userProvider.GetCurrentUserId();

            Message message = await _unitOfWork.Messages.GetByIdAsync(request.MessageId);

            return _mapper.Map<Message, MessageViewModel>(message, options =>
            {
                options.Items["userId"] = userId;
            });
        }
    }
}