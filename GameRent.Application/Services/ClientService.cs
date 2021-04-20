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
            _logger.LogInformation(id.HasValue ? $"Checking if client CPF is unique. Cpf: {cpf} - Id: {id}" : $"Checking if client CPF is unique: {cpf}");

            return !id.HasValue ?
                await _repository.IsUniqueCpf(cpf) :
                await _repository.IsUniqueCpf(id.Value, cpf);
        }

        public async Task<bool> CheckIfClientExists(Guid id)
        {
            _logger.LogInformation($"Checking if client exists by Id: {id}");

            return (await _repository.GetById(id) == null);
        } 
    }
}