using Presentation.Api.Validation.Languages;
using Presentation.Api.Validation.Session;
using Presentation.Api.Validation.Users;
using Xunit;

namespace Presentation.Api.Test.Validation
{
    public class ValidatorTests
    {
        [Fact]
        public void Validators_DoNotThrow()
        {
            // Languages
            Assert.NotNull(new GetTranslationByLanguageDtoValidator());
            Assert.NotNull(new LanguageResourceValidator());

            // Session
            Assert.NotNull(new LoginUserDtoValidator());

            // Users
            Assert.NotNull(new AuthenticatedUserResourceValidator());
            Assert.NotNull(new EmailExistsDtoValidator());
            Assert.NotNull(new RegisterUserDtoValidator());
            Assert.NotNull(new UserNameExistsDtoValidator());
            Assert.NotNull(new UserProfileResourceValidator());
        }
    }
}
