﻿using AutoMapper;
using Core.Application.Database;
using Core.Domain.Entities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Core.Domain.ViewModels.Users;

namespace Core.Application.Requests.Users.Queries;

public class GetUserProfileQuery : IRequest<UserProfileViewModel>
{
    public int UserId { get; set; }

    public class Handler : IRequestHandler<GetUserProfileQuery, UserProfileViewModel>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public Handler(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<UserProfileViewModel> Handle(GetUserProfileQuery request, CancellationToken cancellationToken = default)
        {
            User user = await _unitOfWork.Users.GetByIdAsync(request.UserId);

            return _mapper.Map<User, UserProfileViewModel>(user);
        }
    }
}