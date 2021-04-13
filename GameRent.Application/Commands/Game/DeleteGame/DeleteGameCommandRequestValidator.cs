using FluentValidation;

namespace GameRent.Application.Commands.Game.DeleteGame
{
    public class DeleteGameCommandRequestValidator : AbstractValidator<DeleteGameCommandRequest>
    {
        public DeleteGameCommandRequestValidator()
        {
            RuleFor(game => game.Id).NotEmpty();
        }
    }
}