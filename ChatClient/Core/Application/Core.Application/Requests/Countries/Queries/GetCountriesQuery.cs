using AutoMapper;
using Core.Application.Database;
using Core.Domain.Entities;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Core.Domain.ViewModels;

namespace Core.Application.Requests.Countries.Queries;

public class GetCountriesQuery : IRequest<IEnumerable<CountryViewModel>>
{
    public class Handler : IRequestHandler<GetCountriesQuery, IEnumerable<CountryViewModel>>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public Handler(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<CountryViewModel>> Handle(GetCountriesQuery request, CancellationToken cancellationToken = default)
        {
            List<Country> countries = await _unitOfWork.Countries.GetAllAsync();
                
            return _mapper.Map<List<Country>, List<CountryViewModel>>(countries);
        }
    }
}