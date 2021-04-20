using GameRent.Application.Interfaces.Services;
using GameRent.Domain.Interfaces.Repositories;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace GameRent.Application.Services
{
    public class GameService : IGameService
    {
        private readonly IGameCommandRepository _repository;
        private readonly ILogger<GameService> _logger;

        public GameService(IGameCommandRepository gameRepository, ILoggerFactory loggerFactory)
        {
            _repository = gameRepository;
            _logger = loggerFactory.CreateLogger<GameService>();
        }

        public async Task<bool> CheckIfGameNameIsUnique(string name, Guid? id = null)
        {
            _logger.LogInformation(id.HasValue ? $"Checking if game name is unique. Name: {name} - Id: {id}" : $"Checking if game name is unique: {name}");

            return !id.HasValue ?
                await _repository.IsUniqueGameName(name) :
                await _repository.IsUniqueGameName(id.Value, name);
        }

        public async Task<bool> CheckIfGameExists(Guid id)
        {
            _logger.LogInformation($"Checking if game exists by Id: {id}");

            return (await _repository.GetById(id) == null);
        }
    }
}