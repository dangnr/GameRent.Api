using GameRent.Application.Interfaces.Services;
using GameRent.Domain.Interfaces.Repositories;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
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
            // TO-DO - implementar logs
            return (await _repository.GetById(id) != null);
        }

        public async Task<bool> CheckIfClientExists(Guid id)
        {
            // TO-DO - implementar logs
            return (await _repository.GetClientById(id) != null);
        }

        public bool CheckIfGamesAreValid(List<Domain.Entities.Game> games)
        {
            // TO-DO - implementar logs
            return games.All(x => x.IsAvailable);
        }
    }
}