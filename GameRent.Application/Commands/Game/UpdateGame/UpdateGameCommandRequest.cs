using FluentValidation.Results;
using GameRent.Application.Shared;
using MediatR;
using System;

namespace GameRent.Application.Commands.Game.UpdateGame
{
    public class UpdateGameCommandRequest : IRequest<BaseResponse>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Genre { get; set; }
        public string Synopsis { get; set; }
        public string Platform { get; set; }
        public DateTime LaunchDate { get; set; }
        public bool IsAvailable { get; set; }
        public bool IsActive { get; set; }

        public ValidationResult Validate() => new UpdateGameCommandRequestValidator().Validate(this);
    }
}