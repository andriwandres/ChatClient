using AutoMapper;
using Presentation.Api.Mapping;
using Xunit;

namespace Presentation.Api.Test.Mapping
{
    public class MappingProfileTests
    {
        [Fact]
        public void UserMappingProfile_ShouldHaveValidMappings() =>
            AssertProfileIsValid<UserMappingProfile>();

        [Fact]
        public void UserMappingProfile_ShouldNotThrowOnExecution()
        {
            UserMappingProfile profile = new UserMappingProfile();
            Assert.NotNull(profile);
        }

        private static void AssertProfileIsValid<TProfile>() where TProfile : Profile, new()
        {
            MapperConfiguration configuration = new MapperConfiguration(config => 
                config.AddProfile<TProfile>());

            configuration.AssertConfigurationIsValid();
        }
    }
}
