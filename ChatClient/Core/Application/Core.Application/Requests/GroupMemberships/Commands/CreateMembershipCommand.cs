using AutoMapper;
using Core.Application.Database;
using Core.Application.Services;
using Core.Domain.Entities;
using Core.Domain.Resources.GroupMemberships;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Core.Application.Requests.GroupMemberships.Commands
{
    public class CreateMembershipCommand : IRequest<GroupMembershipResource>
    {
        public int UserId { get; set; }
        public int GroupId { get; set; }
        public bool IsAdmin { get; set; }

        public class Handler : IRequestHandler<CreateMembershipCommand, GroupMembershipResource>
        {
            private readonly IMapper _mapper;
            private readonly IUnitOfWork _unitOfWork;
            private readonly IDateProvider _dateProvider;

            public Handler(IDateProvider dateProvider, IUnitOfWork unitOfWork, IMapper mapper)
            {
                _mapper = mapper;
                _unitOfWork = unitOfWork;
                _dateProvider = dateProvider;
            }

            public async Task<GroupMembershipResource> Handle(CreateMembershipCommand request, CancellationToken cancellationToken = default)
            {
                GroupMembership membership = new GroupMembership
                {
                    UserId = request.UserId,
                    GroupId = request.GroupId,
                    IsAdmin = request.IsAdmin,
                    Created = _dateProvider.UtcNow(),
                };

                Recipient recipient = new Recipient
                {
                    GroupMembership = membership,
                };

                await _unitOfWork.GroupMemberships.Add(membership, cancellationToken);
                await _unitOfWork.Recipients.Add(recipient, cancellationToken);

                await _unitOfWork.CommitAsync(cancellationToken);

                return _mapper.Map<GroupMembership, GroupMembershipResource>(membership);
            }
        }
    }
}
