using Domain.Dto.CreatedRequest;
using FluentValidation;

namespace Infrastructure.Validations;

public class UserCreateValidation : AbstractValidator<UserCreate>
{
    public UserCreateValidation()
    {
        this.RuleFor(c => c.Email).NotEmpty().MinimumLength(5).MaximumLength(50).EmailAddress();

        this.RuleFor(c => c.Password).NotEmpty().NotNull().MinimumLength(4).MaximumLength(16);
    }
}