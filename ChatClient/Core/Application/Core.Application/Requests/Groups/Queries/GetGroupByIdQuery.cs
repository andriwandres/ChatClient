using AutoMapper;
using Core.Application.Database;
using Core.Domain.Entities;
using Core.Domain.Resources.Groups;
using MediatR;
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
                Group group = await _unitOfWork.Groups.GetByIdAsync(request.GroupId);

                return _mapper.Map<Group, GroupResource>(group);
            }
        }
    }
}
