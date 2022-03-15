using AutoMapper;
using Core.Application.Database;
using Core.Domain.Entities;
using Core.Domain.Resources.GroupMemberships;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Core.Application.Requests.GroupMemberships.Queries;

public class GetMembershipsByGroupQuery : IRequest<IEnumerable<GroupMembershipResource>>
{
    public int GroupId { get; set; }

    public class Handler : IRequestHandler<GetMembershipsByGroupQuery, IEnumerable<GroupMembershipResource>>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public Handler(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<GroupMembershipResource>> Handle(GetMembershipsByGroupQuery request, CancellationToken cancellationToken = default)
        {
            List<GroupMembership> memberships = await _unitOfWork.GroupMemberships.GetByGroup(request.GroupId, cancellationToken);
            return _mapper.Map<List<GroupMembership>, List<GroupMembershipResource>>(memberships);
        }
    }
}