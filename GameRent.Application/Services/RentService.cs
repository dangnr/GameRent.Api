using GameRent.Application.Interfaces.Services;
using GameRent.Domain.Interfaces.Repositories;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace GameRent.Application.Services
{
    public class RentService : IRentService
    {
        private readonly IRentCommandRepository _repository;
        private readonly ILogger<RentService> _logger;

        public RentService(IRentCommandRepository rentRepository, ILoggerFactory loggerFactory)
        {
            _repository = rentRepository;
            _logger = loggerFactory.CreateLogger<RentService>();
        }

        public async Task<bool> CheckIfRentExists(Guid id)
        {
            _logger.LogInformation($"Checking if rent exists by Id: {id}");

            return (await _repository.GetById(id) != null);
        }

        public async Task<bool> CheckIfClientExists(Guid id)
        {
            _logger.LogInformation($"Checking if client exists by Id: {id}");

            return (await _repository.GetClientById(id) != null);
        }

        public bool CheckIfGamesAreValid(List<Domain.Entities.Game> games)
        {
            _logger.LogInformation($"Checking if games are valid: { JsonSerializer.Serialize(games) }");

            return games.All(x => x.IsAvailable);
        }
    }
}