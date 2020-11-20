using Presentation.Api.Validation.Countries;
using Presentation.Api.Validation.Errors;
using Presentation.Api.Validation.Friendships;
using Presentation.Api.Validation.Groups;
using Presentation.Api.Validation.Languages;
using Presentation.Api.Validation.Session;
using Presentation.Api.Validation.Translations;
using Presentation.Api.Validation.Users;
using Xunit;

namespace Presentation.Api.Test.Validation
{
    public class ValidatorTests
    {
        [Fact]
        public void Validators_DoNotThrow()
        {
            // Countries
            Assert.NotNull(new CountryResourceValidator());

            // Errors
            Assert.NotNull(new ErrorResourceValidator());
            Assert.NotNull(new ValidationErrorResourceValidator());

            // Friendships
            Assert.NotNull(new FriendshipResourceValidator());
            Assert.NotNull(new RequestFriendshipDtoValidator());
            Assert.NotNull(new UpdateFriendshipStatusDtoValidator());

            // Groups
            Assert.NotNull(new CreateGroupDtoValidator());
            Assert.NotNull(new UpdateGroupDtoValidator());

            // Languages
            Assert.NotNull(new LanguageResourceValidator());

            // Session
            Assert.NotNull(new LoginUserDtoValidator());

            // Translations
            Assert.NotNull(new GetTranslationByLanguageDtoValidator());

            // Users
            Assert.NotNull(new AuthenticatedUserResourceValidator());
            Assert.NotNull(new EmailExistsDtoValidator());
            Assert.NotNull(new RegisterUserDtoValidator());
            Assert.NotNull(new UserNameExistsDtoValidator());
            Assert.NotNull(new UserProfileResourceValidator());
        }
    }
}
