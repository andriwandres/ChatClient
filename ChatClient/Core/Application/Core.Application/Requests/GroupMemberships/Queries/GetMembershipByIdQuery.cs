using AutoMapper;
using Core.Application.Database;
using Core.Domain.Entities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Core.Domain.ViewModels.GroupMemberships;

namespace Core.Application.Requests.GroupMemberships.Queries;

public class GetMembershipByIdQuery : IRequest<GroupMembershipViewModel>
{
    public int GroupMembershipId { get; set; }

    public class Handler : IRequestHandler<GetMembershipByIdQuery, GroupMembershipViewModel>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public Handler(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<GroupMembershipViewModel> Handle(GetMembershipByIdQuery request, CancellationToken cancellationToken = default)
        {
            GroupMembership membership = await _unitOfWork.GroupMemberships.GetByIdAsync(request.GroupMembershipId);

            return _mapper.Map<GroupMembership, GroupMembershipViewModel>(membership);
        }
    }
}