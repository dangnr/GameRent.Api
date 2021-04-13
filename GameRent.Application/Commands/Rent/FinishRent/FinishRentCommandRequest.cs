using FluentValidation.Results;
using GameRent.Application.Shared;
using MediatR;
using System;

namespace GameRent.Application.Commands.Rent.FinishRent
{
    public class FinishRentCommandRequest : IRequest<BaseResponse>
    {
        public FinishRentCommandRequest(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; set; }

        public ValidationResult Validate() => new FinishRentCommandRequestValidator().Validate(this);
    }
}