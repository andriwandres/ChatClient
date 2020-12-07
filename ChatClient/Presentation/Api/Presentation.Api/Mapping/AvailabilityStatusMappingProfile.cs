using AutoMapper;
using Core.Domain.Entities;
using Core.Domain.Resources.AvailabilityStatuses;

namespace Presentation.Api.Mapping
{
    public class AvailabilityStatusMappingProfile : Profile
    {
        public AvailabilityStatusMappingProfile()
        {
            CreateMap<AvailabilityStatus, AvailabilityStatusResource>();
        }
    }
}
