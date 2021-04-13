using FluentValidation.Results;
using GameRent.Application.Shared;
using MediatR;
using System;

namespace GameRent.Application.Commands.Game.CreateGame
{
    public class CreateGameCommandRequest : IRequest<BaseResponse>
    {
        public string Name { get; set; }
        public string Genre { get; set; }
        public string Synopsis { get; set; }
        public string Platform { get; set; }
        public DateTime LaunchDate { get; set; }

        public ValidationResult Validate() => new CreateGameCommandRequestValidator().Validate(this);
    }
}