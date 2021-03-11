using FluentValidation;
using GameRent.Domain.Commands.Rent;

namespace GameRent.Domain.Validators.Commands.Rent
{
    public class FinishRentCommandValidator : AbstractValidator<FinishRentCommand>
    {
        public FinishRentCommandValidator()
        {
            RuleFor(rent => rent.Id)
               .NotEmpty();
        }
    }
}