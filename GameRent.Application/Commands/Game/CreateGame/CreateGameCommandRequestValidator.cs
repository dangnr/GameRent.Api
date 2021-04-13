using FluentValidation;
using System;

namespace GameRent.Application.Commands.Game.CreateGame
{
    public class CreateGameCommandRequestValidator : AbstractValidator<CreateGameCommandRequest>
    {
        public CreateGameCommandRequestValidator()
        {
            RuleFor(game => game.Name)
               .NotNull()
               .NotEmpty()
               .MaximumLength(100);

            RuleFor(game => game.Genre)
               .NotNull()
               .NotEmpty()
               .MaximumLength(100);

            RuleFor(game => game.Synopsis)
               .NotNull()
               .NotEmpty()
               .MaximumLength(500);

            RuleFor(game => game.Platform)
               .NotNull()
               .NotEmpty();

            RuleFor(game => game.LaunchDate)
               .GreaterThan(DateTime.MinValue);
        }
    }
}