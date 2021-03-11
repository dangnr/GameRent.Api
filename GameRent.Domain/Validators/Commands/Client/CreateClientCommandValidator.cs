using FluentValidation;
using GameRent.Domain.Commands.Client;

namespace GameRent.Domain.Validators.Commands.Client
{
    public class CreateClientCommandValidator : AbstractValidator<CreateClientCommand>
    {
        public CreateClientCommandValidator()
        {
            RuleFor(client => client.FirstName)
               .NotNull()
               .NotEmpty()
               .MaximumLength(100);

            RuleFor(client => client.LastName)
               .NotNull()
               .NotEmpty()
               .MaximumLength(100);

            RuleFor(client => client.Cpf)
               .NotNull()
               .NotEmpty()
               .MaximumLength(11);

            RuleFor(client => client.Email)
               .NotNull()
               .NotEmpty()
               .MaximumLength(100);

            RuleFor(client => client.Username)
              .NotNull()
              .NotEmpty()
              .MaximumLength(100);

            RuleFor(client => client.Password)
              .NotNull()
              .NotEmpty()
              .MaximumLength(100);

            RuleFor(client => client.Role)
              .NotNull();
        }
    }
}