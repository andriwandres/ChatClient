using AutoMapper;
using Presentation.Api.Mapping;
using Xunit;

namespace Presentation.Api.Test.Mapping
{
    public class SessionMappingProfileTests
    {
        [Fact]
        public void SessionMappingProfile_ShouldHaveValidMappings()
        {
            MapperConfiguration configuration = new MapperConfiguration(config =>
            {
                config.AddProfile(new SessionMappingProfile());
            });

            configuration.AssertConfigurationIsValid();
        }
    }
}
