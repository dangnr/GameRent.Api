using FluentValidation.Results;
using GameRent.Application.Shared;
using GameRent.Domain.Enums;
using MediatR;

namespace GameRent.Application.Commands.Client.CreateClient
{
    public class CreateClientCommandRequest : IRequest<BaseResponse>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Cpf { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public UserRoleType Role { get; set; }

        public ValidationResult Validate() => new CreateClientCommandRequestValidator().Validate(this);
    }
}