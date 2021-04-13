using FluentValidation;

namespace GameRent.Application.Commands.Rent.FinishRent
{
    public class FinishRentCommandRequestValidator : AbstractValidator<FinishRentCommandRequest>
    {
        public FinishRentCommandRequestValidator()
        {
            RuleFor(rent => rent.Id)
               .NotEmpty();
        }
    }
}