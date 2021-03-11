using FluentValidation.Results;
using GameRent.Domain.Interfaces.Command;
using GameRent.Domain.Validators.Commands.Rent;
using System;
using System.Collections.Generic;

namespace GameRent.Domain.Commands.Rent
{
    public class FinishRentCommand : ICommand
    {
        public Guid Id { get; set; }

        public ValidationResult Validate() => new FinishRentCommandValidator().Validate(this);
    }
}