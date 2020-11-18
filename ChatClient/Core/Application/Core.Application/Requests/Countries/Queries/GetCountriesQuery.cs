using AutoMapper;
using AutoMapper.QueryableExtensions;
using Core.Application.Database;
using Core.Domain.Resources;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Core.Application.Requests.Countries.Queries
{
    public class GetCountriesQuery : IRequest<IEnumerable<CountryResource>>
    {
        public class Handler : IRequestHandler<GetCountriesQuery, IEnumerable<CountryResource>>
        {
            private readonly IMapper _mapper;
            private readonly IUnitOfWork _unitOfWork;

            public Handler(IMapper mapper, IUnitOfWork unitOfWork)
            {
                _mapper = mapper;
                _unitOfWork = unitOfWork;
            }

            public async Task<IEnumerable<CountryResource>> Handle(GetCountriesQuery request, CancellationToken cancellationToken = default)
            {
                IEnumerable<CountryResource> countries = await _unitOfWork.Countries
                    .GetAll()
                    .ProjectTo<CountryResource>(_mapper.ConfigurationProvider)
                    .ToListAsync(cancellationToken);

                return countries;
            }
        }
    }
}
