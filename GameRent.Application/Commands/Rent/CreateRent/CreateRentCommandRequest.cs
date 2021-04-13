using FluentValidation.Results;
using GameRent.Application.Shared;
using MediatR;
using System;
using System.Collections.Generic;

namespace GameRent.Application.Commands.Rent.CreateRent
{
    public class CreateRentCommandRequest : IRequest<BaseResponse>
    {
        private GameRent.Domain.Entities.Client Client;
        private List<GameRent.Domain.Entities.Game> Games;

        public Guid ClientId { get; set; }
        public List<Guid> GameIds { get; set; }
        public DateTime EndDate { get; set; }

        public ValidationResult Validate() => new CreateRentCommandRequestValidator().Validate(this);

        public GameRent.Domain.Entities.Client GetClient() => Client;

        public void SetClient(GameRent.Domain.Entities.Client client) => Client = client;

        public List<GameRent.Domain.Entities.Game> GetGames() => Games;

        public void SetGames(List<GameRent.Domain.Entities.Game> games) => Games = games;
    }
}