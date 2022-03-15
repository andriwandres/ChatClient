using AutoMapper;
using Presentation.Api.Mapping;
using Xunit;

namespace Presentation.Api.Test.Mapping;

public class MessageMappingProfileTests
{
    [Fact]
    public void MessageMappingProfile_ShouldHaveValidMappings()
    {
        MapperConfiguration configuration = new MapperConfiguration(config =>
        {
            config.AddProfile(new MessageMappingProfile());
        });

        configuration.AssertConfigurationIsValid();
    }
}