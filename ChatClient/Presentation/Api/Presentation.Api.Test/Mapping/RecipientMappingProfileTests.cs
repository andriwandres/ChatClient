using AutoMapper;
using Presentation.Api.Mapping;
using Xunit;

namespace Presentation.Api.Test.Mapping
{
    public class RecipientMappingProfileTests
    {
        [Fact]
        public void RecipientMappingProfile_ShouldHaveValidMappings()
        {
            MapperConfiguration configuration = new MapperConfiguration(config =>
            {
                config.AddProfile(new RecipientMappingProfile());
            });

            configuration.AssertConfigurationIsValid();
        }
    }
}
