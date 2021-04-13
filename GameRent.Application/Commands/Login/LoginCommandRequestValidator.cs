using FluentValidation;

namespace GameRent.Application.Commands.Login
{
    public class LoginCommandRequestValidator : AbstractValidator<LoginCommandRequest>
    {
        public LoginCommandRequestValidator()
        {
            RuleFor(login => login.Username)
                .NotNull()
                .NotEmpty();

            RuleFor(login => login.Password)
                .NotNull()
                .NotEmpty();
        }
    }
}