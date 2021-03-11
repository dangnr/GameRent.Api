using FluentValidation;
using GameRent.Domain.Commands.Login;

namespace GameRent.Domain.Validators.Commands.Login
{
    public class LoginCommandValidator : AbstractValidator<LoginCommand>
    {
        public LoginCommandValidator()
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