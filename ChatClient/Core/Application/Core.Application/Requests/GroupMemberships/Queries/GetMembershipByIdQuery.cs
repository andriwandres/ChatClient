using AutoMapper;
using AutoMapper.QueryableExtensions;
using Core.Application.Database;
using Core.Domain.Resources.GroupMemberships;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace Core.Application.Requests.GroupMemberships.Queries
{
    public class GetMembershipByIdQuery : IRequest<GroupMembershipResource>
    {
        public int GroupMembershipId { get; set; }

        public class Handler : IRequestHandler<GetMembershipByIdQuery, GroupMembershipResource>
        {
            private readonly IMapper _mapper;
            private readonly IUnitOfWork _unitOfWork;

            public Handler(IMapper mapper, IUnitOfWork unitOfWork)
            {
                _mapper = mapper;
                _unitOfWork = unitOfWork;
            }

            public async Task<GroupMembershipResource> Handle(GetMembershipByIdQuery request, CancellationToken cancellationToken = default)
            {
                GroupMembershipResource membership = await _unitOfWork.GroupMemberships
                    .GetById(request.GroupMembershipId)
                    .ProjectTo<GroupMembershipResource>(_mapper.ConfigurationProvider)
                    .SingleOrDefaultAsync(cancellationToken);

                return membership;
            }
        }
    }
}
