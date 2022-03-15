using AutoMapper;
using Presentation.Api.Mapping;
using Xunit;

namespace Presentation.Api.Test.Mapping;

public class GroupMembershipMappingProfileTests
{
    [Fact]
    public void GroupMembershipMappingProfile_ShouldHaveValidMappings()
    {
        MapperConfiguration configuration = new MapperConfiguration(config =>
        {
            config.AddProfile(new GroupMembershipMappingProfile());
        });

        configuration.AssertConfigurationIsValid();
    }
}