using FluentValidation.Results;
using GameRent.Domain.Enums;
using GameRent.Domain.Interfaces.Command;
using GameRent.Domain.Validators.Commands.Client;
using System;

namespace GameRent.Domain.Commands.Client
{
    public class UpdateClientCommand : ICommand
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Cpf { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public UserRoleType Role { get; set; }
        public bool IsActive { get; set; }

        public ValidationResult Validate() => new UpdateClientCommandValidator().Validate(this);
    }
}