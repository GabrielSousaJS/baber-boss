using BarberBoss.Communication.User.Requests;
using BarberBoss.Exception;
using FluentValidation;

namespace BarberBoss.Application.UseCases.User;

public class RegisterUserValidator : AbstractValidator<RequestRegisterUserJson>
{
    public RegisterUserValidator()
    {
        RuleFor(user => user.Name).NotEmpty().WithMessage(ResourceErrorMessages.NAME_EMPTY);
        RuleFor(user => user.BirthDate).LessThanOrEqualTo(DateTime.Now.AddYears(-18)).WithMessage(ResourceErrorMessages.BIRTH_DATE_INVALID);
        RuleFor(user => user.Email)
            .NotEmpty().WithMessage(ResourceErrorMessages.EMAIL_EMPTY)
            .When(user => !string.IsNullOrEmpty(user.Email), ApplyConditionTo.CurrentValidator)
            .EmailAddress().WithMessage(ResourceErrorMessages.EMAIL_INVALID);

        RuleFor(user => user.Password).SetValidator(new PasswordValidator<RequestRegisterUserJson>());
    }
}
