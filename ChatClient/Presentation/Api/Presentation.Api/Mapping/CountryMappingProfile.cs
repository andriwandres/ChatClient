using AutoMapper;
using Core.Domain.Entities;
using Core.Domain.Resources;

namespace Presentation.Api.Mapping
{
    public class CountryMappingProfile : Profile
    {
        public CountryMappingProfile()
        {
            CreateMap<Country, CountryResource>();
        }
    }
}
