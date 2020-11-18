using Core.Domain.Dtos.Friendships;
using FluentValidation;

namespace Presentation.Api.Validation.Friendships
{
    public class RequestFriendshipDtoValidator : AbstractValidator<RequestFriendshipDto>
    {
        public RequestFriendshipDtoValidator()
        {
            const string addresseeIdName = nameof(RequestFriendshipDto.AddresseeId);
            RuleFor(request => request.AddresseeId)
                .NotEmpty()
                .WithMessage($"'{addresseeIdName}' must contain a value")
                .GreaterThan(0)
                .WithMessage($"'{addresseeIdName}' must be greater than 0");
        }
    }
}
