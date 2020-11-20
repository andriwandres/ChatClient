using AutoMapper;
using Core.Application.Database;
using Core.Application.Services;
using Core.Domain.Entities;
using Core.Domain.Resources.Groups;
using MediatR;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;

namespace Core.Application.Requests.Groups.Commands
{
    public class CreateGroupCommand : IRequest<GroupResource>
    {
        public string Name { get; set; }
        public string Description { get; set; }

        public class Handler : IRequestHandler<CreateGroupCommand, GroupResource>
        {
            private readonly IMapper _mapper;
            private readonly IUnitOfWork _unitOfWork;
            private readonly IDateProvider _dateProvider;
            private readonly IHttpContextAccessor _httpContextAccessor;

            public Handler(IMapper mapper, IUnitOfWork unitOfWork, IDateProvider dateProvider, IHttpContextAccessor httpContextAccessor)
            {
                _mapper = mapper;
                _unitOfWork = unitOfWork;
                _dateProvider = dateProvider;
                _httpContextAccessor = httpContextAccessor;
            }

            public async Task<GroupResource> Handle(CreateGroupCommand request, CancellationToken cancellationToken = default)
            {
                int userId = int.Parse(_httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);

                Group group = new Group
                {
                    Name = request.Name,
                    Description = request.Description,
                    Created = _dateProvider.UtcNow(),
                };

                GroupMembership membership = new GroupMembership
                {
                    Group = group,
                    UserId = userId,
                    IsAdmin = true,
                    Created = _dateProvider.UtcNow()
                };

                Recipient recipient = new Recipient
                {
                    GroupMembership = membership
                };

                await _unitOfWork.Groups.Add(group, cancellationToken);
                await _unitOfWork.GroupMemberships.Add(membership, cancellationToken);
                await _unitOfWork.Recipients.Add(recipient, cancellationToken);

                await _unitOfWork.CommitAsync(cancellationToken);

                return _mapper.Map<Group, GroupResource>(group);
            }
        }
    }
}
