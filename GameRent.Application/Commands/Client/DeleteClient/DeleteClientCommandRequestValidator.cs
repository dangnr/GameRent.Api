using FluentValidation;

namespace GameRent.Application.Commands.Client.DeleteClient
{
    public class DeleteClientCommandRequestValidator : AbstractValidator<DeleteClientCommandRequest>
    {
        public DeleteClientCommandRequestValidator()
        {
            RuleFor(client => client.Id)
              .NotEmpty();
        }
    }
}