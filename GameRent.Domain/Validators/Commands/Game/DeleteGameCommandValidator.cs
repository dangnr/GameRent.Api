using FluentValidation;
using GameRent.Domain.Commands.Game;

namespace GameRent.Domain.Validators.Commands.Game
{
    public class DeleteGameCommandValidator : AbstractValidator<DeleteGameCommand>
    {
        public DeleteGameCommandValidator()
        {
            RuleFor(game => game.Id).NotEmpty();
        }
    }
}