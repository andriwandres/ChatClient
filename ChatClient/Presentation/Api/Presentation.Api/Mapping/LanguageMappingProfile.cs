using AutoMapper;
using Core.Domain.Entities;
using Core.Domain.Resources.Languages;

namespace Presentation.Api.Mapping
{
    public class LanguageMappingProfile : Profile
    {
        public LanguageMappingProfile()
        {
            CreateMap<Language, LanguageResource>();
        }
    }
}
