using FluentValidation.Results;
using GameRent.Domain.Interfaces.Command;
using GameRent.Domain.Validators.Commands.Login;

namespace GameRent.Domain.Commands.Login
{
    public class LoginCommand : ICommand
    {
        public string Username { get; set; }
        public string Password { get; set; }

        public ValidationResult Validate() => new LoginCommandValidator().Validate(this);
    }
}