using GameRent.Domain.Entities;
using System;

namespace GameRent.Domain.Aggregates
{
    public class GameRent : EntityBase
    {
        public Game Game { get; private set; }

        public GameRent()
        {
            Id = Guid.NewGuid();
            CreatedOn = DateTime.Now;
            IsActive = true;
        }

        public GameRent(Guid id, bool isActive)
        {
            Id = id;
            IsActive = isActive;
        }

        public void SetGame(Game game) => Game = game;

        public void AddGameToRent(Game game)
        {
            game.UpdateGameToRent();
            SetGame(game);
        }

        public void UpdateGameRented(GameRent gameRented)
        {
            Id = gameRented.Id;
            Game = gameRented.Game;
            IsActive = gameRented.IsActive;
            UpdatedOn = DateTime.Now;
        }

        public void UpdateGameRentToFinishRent()
        {
            IsActive = false;
            UpdatedOn = DateTime.Now;
            Game.UpdateGameToFinishRent();
        }
    }
}