using FluentValidation.Results;
using GameRent.Application.Shared;
using MediatR;
using System;

namespace GameRent.Application.Commands.Client.DeleteClient
{
    public class DeleteClientCommandRequest : IRequest<BaseResponse>
    {
        public DeleteClientCommandRequest(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; set; }

        public ValidationResult Validate() => new DeleteClientCommandRequestValidator().Validate(this);
    }
}