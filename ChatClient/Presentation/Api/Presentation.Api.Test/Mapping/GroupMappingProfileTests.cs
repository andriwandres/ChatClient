using AutoMapper;
using Presentation.Api.Mapping;
using Xunit;

namespace Presentation.Api.Test.Mapping;

public class GroupMappingProfileTests
{
    [Fact]
    public void GroupMappingProfile_ShouldHaveValidMappings()
    {
        MapperConfiguration configuration = new MapperConfiguration(config =>
        {
            config.AddProfile(new GroupMappingProfile());
        });

        configuration.AssertConfigurationIsValid();
    }
}