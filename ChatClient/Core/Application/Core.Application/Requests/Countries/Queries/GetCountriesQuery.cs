using AutoMapper;
using Core.Application.Database;
using Core.Domain.Entities;
using Core.Domain.Resources;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Core.Application.Requests.Countries.Queries;

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
            List<Country> countries = await _unitOfWork.Countries.GetAllAsync();
                
            return _mapper.Map<List<Country>, List<CountryResource>>(countries);
        }
    }
}