using FluentValidation;
using GameRent.Domain.Commands.Game;
using System;

namespace GameRent.Domain.Validators.Commands.Game
{
    public class UpdateGameCommandValidator : AbstractValidator<UpdateGameCommand>
    {
        public UpdateGameCommandValidator()
        {
            RuleFor(game => game.Id)
               .NotEmpty();

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