using AutoMapper;
using Presentation.Api.Mapping;
using Xunit;

namespace Presentation.Api.Test.Mapping
{
    public class LanguageMappingProfileTests
    {
        [Fact]
        public void UserMappingProfile_ShouldHaveValidMappings()
        {
            MapperConfiguration configuration = new MapperConfiguration(config =>
            {
                config.AddProfile(new LanguageMappingProfile());
            });

            configuration.AssertConfigurationIsValid();
        }
    }
}
