using AutoMapper;
using Core.Application.Database;
using Core.Application.Services;
using Core.Domain.Entities;
using Core.Domain.Resources.Groups;
using MediatR;
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
            
            public Handler(IMapper mapper, IUnitOfWork unitOfWork, IDateProvider dateProvider)
            {
                _mapper = mapper;
                _unitOfWork = unitOfWork;
                _dateProvider = dateProvider;
            }

            public async Task<GroupResource> Handle(CreateGroupCommand request, CancellationToken cancellationToken = default)
            {
                Group group = new Group
                {
                    Name = request.Name,
                    Description = request.Description,
                    Created = _dateProvider.UtcNow()
                };

                await _unitOfWork.Groups.Add(group, cancellationToken);
                await _unitOfWork.CommitAsync(cancellationToken);

                return _mapper.Map<Group, GroupResource>(group);
            }
        }
    }
}
