using GameRent.Domain.Entities;
using System;
using System.Collections.Generic;

namespace GameRent.Domain.Aggregates
{
    public class Rent : EntityBase
    {
        public DateTime StartDate { get; private set; }
        public DateTime EndDate { get; private set; }
        public DateTime? ReturnedDate { get; private set; }
        public Client Client { get; private set; }
        public List<GameRent> GamesRent { get; private set; }

        public Rent(DateTime endDate)
        {
            Id = Guid.NewGuid();
            StartDate = DateTime.Now;
            EndDate = endDate;
            IsActive = true;
            CreatedOn = DateTime.Now;
        }

        public Rent(Guid id, DateTime endDate, bool isActive)
        {
            Id = id;
            EndDate = endDate;
            IsActive = isActive;
        }

        public void AddGamesRent(List<Game> games)
        {
            GamesRent = new List<GameRent>();

            games.ForEach(game =>
            {
                var gameRent = new GameRent();

                gameRent.AddGameToRent(game);
                GamesRent.Add(gameRent); 
            });
        }

        public void FinishRent()
        {
            GamesRent.ForEach(gameRented =>
            {
                gameRented.UpdateGameRentToFinishRent();
            });

            IsActive = false;
            ReturnedDate = DateTime.Now;
            UpdatedOn = ReturnedDate;
        }

        public void SetClient(Client client) => Client = client;
    }
}