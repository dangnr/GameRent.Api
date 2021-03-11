using FluentValidation.Results;
using GameRent.Domain.Interfaces.Command;
using GameRent.Domain.Validators.Commands.Rent;
using System;
using System.Collections.Generic;

namespace GameRent.Domain.Commands.Rent
{
    public class CreateRentCommand : ICommand
    {
        private GameRent.Domain.Entities.Client Client;
        private List<GameRent.Domain.Entities.Game> Games;

        public Guid ClientId { get; set; }
        public List<Guid> GameIds { get; set; }
        public DateTime EndDate { get; set; }

        public ValidationResult Validate() => new CreateRentCommandValidator().Validate(this);

        public GameRent.Domain.Entities.Client GetClient() => Client;

        public void SetClient(GameRent.Domain.Entities.Client client) => Client = client;

        public List<GameRent.Domain.Entities.Game> GetGames() => Games;

        public void SetGames(List<GameRent.Domain.Entities.Game> games) => Games = games;
    }
}