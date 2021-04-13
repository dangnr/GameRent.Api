using FluentValidation.Results;
using GameRent.Application.Shared;
using MediatR;
using System;

namespace GameRent.Application.Commands.Game.DeleteGame
{
    public class DeleteGameCommandRequest : IRequest<BaseResponse>
    {
        public DeleteGameCommandRequest(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; set; }

        public ValidationResult Validate() => new DeleteGameCommandRequestValidator().Validate(this);
    }
}