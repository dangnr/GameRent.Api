using FluentValidation.Results;
using GameRent.Domain.Interfaces.Command;
using GameRent.Domain.Validators.Commands.Client;
using System;

namespace GameRent.Domain.Commands.Client
{
    public class DeleteClientCommand : ICommand
    {
        public Guid Id { get; set; }

        public ValidationResult Validate() => new DeleteClientCommandValidator().Validate(this);
    }
}