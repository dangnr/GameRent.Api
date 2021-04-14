using GameRent.Application.Helpers;
using GameRent.Application.Interfaces.Services;
using GameRent.Application.Shared;
using GameRent.Domain.Interfaces.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Net;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace GameRent.Application.Commands.Client.UpdateClient
{
    public class UpdateClientCommandHandler : IRequestHandler<UpdateClientCommandRequest, BaseResponse>
    {
        private readonly IClientCommandRepository _repository;
        private readonly IClientService _clientService;
        private readonly ILogger<UpdateClientCommandHandler> _logger;

        public UpdateClientCommandHandler(IClientCommandRepository repository, IClientService clientService, ILoggerFactory loggerFactory)
        {
            _repository = repository;
            _clientService = clientService;
            _logger = loggerFactory.CreateLogger<UpdateClientCommandHandler>();
        }

        public async Task<BaseResponse> Handle(UpdateClientCommandRequest request, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"[Begin] - Updating client : { JsonSerializer.Serialize(request) }");

            try
            {
                var validationResult = request.Validate();

                if (!validationResult.IsValid)
                {
                    var validationResultErrors = ValidationResultHelper.GetValidationResultErrors(validationResult);

                    _logger.LogInformation($"[Error] - Request not valid: { validationResultErrors }");

                    return new BaseResponse(false, "Ocorreu um problema ao atualizar o cliente!", HttpStatusCode.BadRequest, validationResultErrors);
                }

                if (await _clientService.CheckIfClientExists(request.Id))
                {
                    _logger.LogInformation($"[Error] - Request not valid: { request }");

                    return new BaseResponse(false, "Cliente não existe na base de dados!", HttpStatusCode.NotFound, request);
                }

                if (!await _clientService.CheckIfClientCpfIsUnique(request.Cpf, request.Id))
                {
                    _logger.LogInformation($"[Error] - Request not valid: { request }");

                    return new BaseResponse(false, "Já existe um cliente cadastrado com o mesmo cpf!", HttpStatusCode.BadRequest, request);
                }

                var client = new Domain.Entities.Client(request.Id, request.FirstName, request.LastName, request.Cpf, request.Email, request.Username, request.Password, request.Role, request.IsActive);

                await _repository.Update(client);

                _logger.LogInformation($"[End] - Client successfully updated: { JsonSerializer.Serialize(client) }");

                return new BaseResponse(true, "Cliente atualizado com sucesso!", HttpStatusCode.OK, client);
            }
            catch (Exception ex)
            {
                _logger.LogInformation($"[Error] - An error occurred while updating the client: { JsonSerializer.Serialize(ex) }");

                return new BaseResponse(false, "Ocorreu um problema ao atualizar o cliente!", HttpStatusCode.InternalServerError, ex.InnerException.ToString());
            }
        }
    }
}