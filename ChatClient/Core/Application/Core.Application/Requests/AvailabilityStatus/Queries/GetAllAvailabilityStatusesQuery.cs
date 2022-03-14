using AutoMapper;
using Core.Application.Database;
using Core.Domain.Resources.AvailabilityStatuses;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Core.Application.Requests.AvailabilityStatus.Queries
{
    public class GetAllAvailabilityStatusesQuery : IRequest<IEnumerable<AvailabilityStatusResource>>
    {
        public class Handler : IRequestHandler<GetAllAvailabilityStatusesQuery, IEnumerable<AvailabilityStatusResource>>
        {
            private readonly IMapper _mapper;
            private readonly IUnitOfWork _unitOfWork;

            public Handler(IMapper mapper, IUnitOfWork unitOfWork)
            {
                _mapper = mapper;
                _unitOfWork = unitOfWork;
            }

            public async Task<IEnumerable<AvailabilityStatusResource>> Handle(GetAllAvailabilityStatusesQuery request, CancellationToken cancellationToken = default)
            {
                List<Domain.Entities.AvailabilityStatus> availabilityStatus = await _unitOfWork.AvailabilityStatuses.GetAllAsync();

                return _mapper.Map<List<Domain.Entities.AvailabilityStatus>, List<AvailabilityStatusResource>>(availabilityStatus);
            }
        }
    }
}
