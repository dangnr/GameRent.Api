using FluentValidation;
using GameRent.Domain.Commands.Client;

namespace GameRent.Domain.Validators.Commands.Client
{
    public class DeleteClientCommandValidator : AbstractValidator<DeleteClientCommand>
    {
        public DeleteClientCommandValidator()
        {
            RuleFor(client => client.Id)
              .NotEmpty();
        }
    }
}