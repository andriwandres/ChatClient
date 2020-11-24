using Presentation.Api.Validation.Countries;
using Presentation.Api.Validation.Errors;
using Presentation.Api.Validation.Friendships;
using Presentation.Api.Validation.GroupMemberships;
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
            Assert.NotNull(new RequestFriendshipBodyValidator());
            Assert.NotNull(new UpdateFriendshipStatusBodyValidator());

            // GroupMemberships
            Assert.NotNull(new GroupMembershipResourceValidator());
            Assert.NotNull(new CreateMembershipBodyValidator());

            // Groups
            Assert.NotNull(new CreateGroupBodyValidator());
            Assert.NotNull(new UpdateGroupBodyValidator());
            Assert.NotNull(new GroupResourceValidator());

            // Languages
            Assert.NotNull(new LanguageResourceValidator());

            // Session
            Assert.NotNull(new LoginBodyValidator());

            // Translations
            Assert.NotNull(new GetTranslationsByLanguageQueryParamsValidator());

            // Users
            Assert.NotNull(new AuthenticatedUserResourceValidator());
            Assert.NotNull(new EmailExistsQueryParamsValidator());
            Assert.NotNull(new CreateAccountBodyValidator());
            Assert.NotNull(new UserNameExistsQueryParamsValidator());
            Assert.NotNull(new UserProfileResourceValidator());
        }
    }
}
