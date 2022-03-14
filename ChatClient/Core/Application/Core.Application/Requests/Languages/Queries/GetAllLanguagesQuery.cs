using AutoMapper;
using Core.Application.Database;
using Core.Domain.Entities;
using Core.Domain.Resources.Languages;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Core.Application.Requests.Languages.Queries
{
    public class GetAllLanguagesQuery : IRequest<IEnumerable<LanguageResource>>
    {
        public class Handler : IRequestHandler<GetAllLanguagesQuery, IEnumerable<LanguageResource>>
        {
            private readonly IMapper _mapper;
            private readonly IUnitOfWork _unitOfWork;

            public Handler(IUnitOfWork unitOfWork, IMapper mapper)
            {
                _mapper = mapper;
                _unitOfWork = unitOfWork;
            }

            public async Task<IEnumerable<LanguageResource>> Handle(GetAllLanguagesQuery request, CancellationToken cancellationToken = default)
            {
                List<Language> languages = await _unitOfWork.Languages.GetAllAsync();

                return _mapper.Map<List<Language>, List<LanguageResource>>(languages);
            }
        }
    }
}
