using System;
using System.ComponentModel.DataAnnotations;

namespace GameRent.Domain.Entities
{
    public class Game : EntityBase
    {
        [MaxLength(100)]
        public string Name { get; private set; }

        [MaxLength(100)]
        public string Genre { get; private set; }

        [MaxLength(500)]
        public string Synopsis { get; private set; }

        [MaxLength(100)]
        public string Platform { get; private set; }

        public DateTime LaunchDate { get; private set; }

        public bool IsAvailable { get; private set; }

        public Game(string name, string genre, string synopsis, string platform, DateTime launchDate)
        {
            Id = Guid.NewGuid();
            Name = name;
            Genre = genre;
            Synopsis = synopsis;
            Platform = platform;
            LaunchDate = launchDate;
            IsAvailable = true;
            IsActive = true;
            CreatedOn = DateTime.Now;
        }

        public Game(Guid id, string name, string genre, string synopsis, string platform, DateTime launchDate, bool isAvailable, bool isActive)
        {
            Id = id;
            Name = name;
            Genre = genre;
            Synopsis = synopsis;
            Platform = platform;
            LaunchDate = launchDate;
            IsAvailable = isAvailable;
            IsActive = isActive;
        }

        public void UpdateGame(Game game)
        {
            Id = game.Id;
            Name = game.Name;
            Genre = game.Genre;
            Synopsis = game.Synopsis;
            Platform = game.Platform;
            LaunchDate = game.LaunchDate;
            IsAvailable = game.IsAvailable;
            IsActive = game.IsActive;
            UpdatedOn = DateTime.Now;
        }

        public void UpdateGameToRent()
        {
            IsAvailable = false;
            UpdatedOn = DateTime.Now;
        }

        public void UpdateGameToFinishRent()
        {
            IsAvailable = true;
            UpdatedOn = DateTime.Now;
        }
    }
}