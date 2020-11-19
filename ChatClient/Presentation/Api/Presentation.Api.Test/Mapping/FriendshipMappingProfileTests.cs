using AutoMapper;
using Presentation.Api.Mapping;
using Xunit;

namespace Presentation.Api.Test.Mapping
{
    public class FriendshipMappingProfileTests
    {
        [Fact]
        public void FriendshipMappingProfile_ShouldHaveValidMappings()
        {
            MapperConfiguration configuration = new MapperConfiguration(config =>
            {
                config.AddProfile(new FriendshipMappingProfile());
            });

            configuration.AssertConfigurationIsValid();
        }
    }
}
