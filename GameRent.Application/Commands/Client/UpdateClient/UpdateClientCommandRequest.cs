using FluentValidation.Results;
using GameRent.Application.Shared;
using GameRent.Domain.Enums;
using MediatR;
using System;

namespace GameRent.Application.Commands.Client.UpdateClient
{
    public class UpdateClientCommandRequest : IRequest<BaseResponse>
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

        public ValidationResult Validate() => new UpdateClientCommandRequestValidator().Validate(this);
    }
}