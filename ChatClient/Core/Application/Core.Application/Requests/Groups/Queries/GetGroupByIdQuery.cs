using AutoMapper;
using AutoMapper.QueryableExtensions;
using Core.Application.Database;
using Core.Domain.Resources.Groups;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace Core.Application.Requests.Groups.Queries
{
    public class GetGroupByIdQuery : IRequest<GroupResource>
    {
        public int GroupId { get; set; }

        public class Handler : IRequestHandler<GetGroupByIdQuery, GroupResource>
        {
            private readonly IMapper _mapper;
            private readonly IUnitOfWork _unitOfWork;

            public Handler(IMapper mapper, IUnitOfWork unitOfWork)
            {
                _mapper = mapper;
                _unitOfWork = unitOfWork;
            }

            public async Task<GroupResource> Handle(GetGroupByIdQuery request, CancellationToken cancellationToken = default)
            {
                GroupResource group = await _unitOfWork.Groups
                    .GetById(request.GroupId)
                    .ProjectTo<GroupResource>(_mapper.ConfigurationProvider)
                    .SingleOrDefaultAsync(cancellationToken);

                return group;
            }
        }
    }
}
