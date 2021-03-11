using FluentValidation.Results;
using GameRent.Domain.Interfaces.Command;
using GameRent.Domain.Validators.Commands.Game;
using System;

namespace GameRent.Domain.Commands.Game
{
    public class CreateGameCommand : ICommand
    {
        public string Name { get; set; }
        public string Genre { get; set; }
        public string Synopsis { get; set; }
        public string Platform { get; set; }
        public DateTime LaunchDate { get; set; }

        public ValidationResult Validate() => new CreateGameCommandValidator().Validate(this);
    }
}