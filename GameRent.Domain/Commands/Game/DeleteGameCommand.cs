using FluentValidation.Results;
using GameRent.Domain.Interfaces.Command;
using GameRent.Domain.Validators.Commands.Game;
using System;

namespace GameRent.Domain.Commands.Game
{
    public class DeleteGameCommand : ICommand
    {
        public Guid Id { get; set; }

        public ValidationResult Validate() => new DeleteGameCommandValidator().Validate(this);
    }
}