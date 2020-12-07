using AutoMapper;
using Presentation.Api.Mapping;
using Xunit;

namespace Presentation.Api.Test.Mapping
{
    public class AvailabilityStatusMappingProfileTests
    {
        [Fact]
        public void AvailabilityStatusMappingProfile_ShouldHaveValidMappings()
        {
            MapperConfiguration configuration = new MapperConfiguration(config =>
            {
                config.AddProfile(new AvailabilityStatusMappingProfile());
            });

            configuration.AssertConfigurationIsValid();
        }
    }
}
