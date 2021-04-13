using GameRent.Application.Shared;
using MediatR;
using System;

namespace GameRent.Application.Queries.Game
{
    public class GameQueryRequest : IRequest<BaseResponse>
    {
        public GameQueryRequest()
        { }

        public GameQueryRequest(Guid? id)
        {
            Id = id;
        }

        public GameQueryRequest(bool? isAvailable)
        {
            IsAvailable = isAvailable;
        }

        public Guid? Id { get; set; }

        public bool? IsAvailable { get; set; }
    }
}