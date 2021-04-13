using FluentValidation.Results;
using GameRent.Application.Shared;
using MediatR;

namespace GameRent.Application.Commands.Login
{
    public class LoginCommandRequest : IRequest<BaseResponse>
    {
        public string Username { get; set; }
        public string Password { get; set; }

        public ValidationResult Validate() => new LoginCommandRequestValidator().Validate(this);
    }
}