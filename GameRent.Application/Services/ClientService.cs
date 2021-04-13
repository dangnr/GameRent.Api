using GameRent.Application.Interfaces.Services;
using GameRent.Domain.Interfaces.Repositories;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace GameRent.Application.Services
{
    public class ClientService : IClientService
    {
        private readonly IClientCommandRepository _repository;
        private readonly ILogger<ClientService> _logger;

        public ClientService(IClientCommandRepository repository, ILoggerFactory loggerFactory)
        {
            _repository = repository;
            _logger = loggerFactory.CreateLogger<ClientService>();
        }

        public async Task<bool> CheckIfClientCpfIsUnique(string cpf, Guid? id = null)
        {
            // TO-DO -- implementar log

            return !id.HasValue ?
                await _repository.IsUniqueCpf(cpf) :
                await _repository.IsUniqueCpf(id.Value, cpf);
        }

        public async Task<bool> CheckIfClientExists(Guid id)
        {
            // TO-DO -- implementar log

            return (await _repository.GetById(id) == null);
        } 
    }
}