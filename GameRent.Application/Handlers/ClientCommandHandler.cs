using GameRent.Domain.Commands.Client;
using GameRent.Domain.Entities;
using GameRent.Domain.Interfaces.Command;
using GameRent.Domain.Interfaces.Repositories;
using GameRent.Domain.Shared;
using GameRent.Domain.Util;
using Microsoft.Extensions.Logging;
using System;
using System.Net;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace GameRent.Application.Handlers
{
    public class ClientCommandHandler : IClientCommandHandler
    {
        private readonly IClientRepository _repository;
        private readonly ILogger<ClientCommandHandler> _logger;

        public ClientCommandHandler(IClientRepository repository, ILoggerFactory loggerFactory)
        {
            _repository = repository;
            _logger = loggerFactory.CreateLogger<ClientCommandHandler>();
        }

        public async Task<BaseResponse> Handle(CreateClientCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"[Begin] - Creating client: { JsonSerializer.Serialize(request) }");

            try
            {
                var validationResult = request.Validate();

                if (!validationResult.IsValid)
                {
                    var validationResultErrors = ValidationResultUtil.GetValidationResultErrors(validationResult);

                    _logger.LogInformation($"[Error] - Request not valid: { validationResultErrors }");

                    return new BaseResponse(false, "Ocorreu um problema ao criar o cliente!", HttpStatusCode.BadRequest, validationResultErrors);
                }

                if (!await CheckIfClientCpfIsUnique(request.Cpf))
                {
                    _logger.LogInformation($"[Error] - Request not valid: { request }");

                    return new BaseResponse(false, "Já existe um cliente cadastrado com o mesmo cpf!", HttpStatusCode.BadRequest,  request);
                }

                var client = new Client(request.FirstName, request.LastName, request.Cpf, request.Email, request.Username, request.Password, request.Role);

                await _repository.Create(client);

                _logger.LogInformation($"[End] - Client successfully created: { JsonSerializer.Serialize(client) }");

                return new BaseResponse(true, "Cliente criado com sucesso!", HttpStatusCode.OK, client);
            }
            catch (Exception ex)
            {
                _logger.LogInformation($"[Error] - An error occurred while creating the client: { JsonSerializer.Serialize(ex) }");

                return new BaseResponse(false, "Ocorreu um problema ao criar o cliente!", HttpStatusCode.InternalServerError, ex.InnerException.ToString());
            }
        }

        public async Task<BaseResponse> Handle(UpdateClientCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"[Begin] - Updating client : { JsonSerializer.Serialize(request) }");

            try
            {
                var validationResult = request.Validate();

                if (!validationResult.IsValid)
                {
                    var validationResultErrors = ValidationResultUtil.GetValidationResultErrors(validationResult);

                    _logger.LogInformation($"[Error] - Request not valid: { validationResultErrors }");

                    return new BaseResponse(false, "Ocorreu um problema ao atualizar o cliente!", HttpStatusCode.BadRequest, validationResultErrors);
                }

                if (await CheckIfClientExists(request.Id))
                {
                    _logger.LogInformation($"[Error] - Request not valid: { request }");

                    return new BaseResponse(false, "Cliente não existe na base de dados!", HttpStatusCode.NotFound, request);
                }

                if (!await CheckIfClientCpfIsUnique(request.Cpf, request.Id))
                {
                    _logger.LogInformation($"[Error] - Request not valid: { request }");

                    return new BaseResponse(false, "Já existe um cliente cadastrado com o mesmo cpf!", HttpStatusCode.BadRequest, request);
                }

                var client = new Client(request.Id, request.FirstName, request.LastName, request.Cpf, request.Email, request.Username, request.Password, request.Role, request.IsActive);

                await _repository.Create(client);

                _logger.LogInformation($"[End] - Client successfully updated: { JsonSerializer.Serialize(client) }");

                return new BaseResponse(true, "Cliente atualizado com sucesso!", HttpStatusCode.OK, client);
            }
            catch (Exception ex)
            {
                _logger.LogInformation($"[Error] - An error occurred while updating the client: { JsonSerializer.Serialize(ex) }");

                return new BaseResponse(false, "Ocorreu um problema ao atualizar o cliente!", HttpStatusCode.InternalServerError, ex.InnerException.ToString());
            }
        }

        public async Task<BaseResponse> Handle(DeleteClientCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"[Begin] - Deleting client : { JsonSerializer.Serialize(request) }");

            try
            {
                var validationResult = request.Validate();

                if (!validationResult.IsValid)
                {
                    var validationResultErrors = ValidationResultUtil.GetValidationResultErrors(validationResult);

                    _logger.LogInformation($"[Error] - Request not valid: { validationResultErrors }");

                    return new BaseResponse(false, "Ocorreu um problema ao deletar o cliente!", HttpStatusCode.BadRequest, validationResultErrors);
                }

                if (await CheckIfClientExists(request.Id))
                {
                    _logger.LogInformation($"[Error] - Request not valid: { request }");

                    return new BaseResponse(false, "Cliente não existe na base de dados!", HttpStatusCode.NotFound, request);
                }

                await _repository.Delete(request.Id);

                _logger.LogInformation($"[End] - Client successfully deleted: { JsonSerializer.Serialize(request) }");

                return new BaseResponse(true, "Cliente deletado com sucesso!", HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                _logger.LogInformation($"[Error] - An error occurred while deleting the client: { JsonSerializer.Serialize(ex) }");

                return new BaseResponse(false, "Ocorreu um problema ao deletar o cliente!", HttpStatusCode.InternalServerError, ex.InnerException.ToString());
            }
        }

        private async Task<bool> CheckIfClientCpfIsUnique(string cpf, Guid? id = null)
        {
            return !id.HasValue ?
                await _repository.IsUniqueCpf(cpf) :
                await _repository.IsUniqueCpf(id.Value, cpf);
        }

        private async Task<bool> CheckIfClientExists(Guid id) => (await _repository.GetById(id) == null);
    }
}