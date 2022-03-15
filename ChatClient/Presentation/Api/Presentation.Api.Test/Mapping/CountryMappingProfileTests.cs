using AutoMapper;
using Presentation.Api.Mapping;
using Xunit;

namespace Presentation.Api.Test.Mapping;

public class CountryMappingProfileTests
{
    [Fact]
    public void CountryMappingProfile_ShouldHaveValidMappings()
    {
        MapperConfiguration configuration = new MapperConfiguration(config =>
        {
            config.AddProfile(new CountryMappingProfile());
        });

        configuration.AssertConfigurationIsValid();
    }
}