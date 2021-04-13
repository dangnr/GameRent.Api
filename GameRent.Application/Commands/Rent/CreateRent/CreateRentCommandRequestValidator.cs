using FluentValidation;
using System;

namespace GameRent.Application.Commands.Rent.CreateRent
{
    public class CreateRentCommandRequestValidator : AbstractValidator<CreateRentCommandRequest>
    {
        public CreateRentCommandRequestValidator()
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