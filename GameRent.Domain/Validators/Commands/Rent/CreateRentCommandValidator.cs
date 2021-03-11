using FluentValidation;
using GameRent.Domain.Commands.Rent;
using System;

namespace GameRent.Domain.Validators.Commands.Rent
{
    public class CreateRentCommandValidator : AbstractValidator<CreateRentCommand>
    {
        public CreateRentCommandValidator()
        {
            RuleFor(rent => rent.ClientId)
               .NotEmpty();

            RuleFor(game => game.GameIds)
               .NotNull()
               .NotEmpty();

            RuleFor(game => game.EndDate)
               .GreaterThan(DateTime.MinValue);
        }
    }
}