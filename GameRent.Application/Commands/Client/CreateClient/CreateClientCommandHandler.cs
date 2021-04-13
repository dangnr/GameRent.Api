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

namespace GameRent.Application.Commands.Client.CreateClient
{
    public class CreateClientCommandHandler : IRequestHandler<CreateClientCommandRequest, BaseResponse>
    {
        private readonly IClientCommandRepository _repository;
        private readonly IClientService _clientService;
        private readonly ILogger<CreateClientCommandHandler> _logger;

        public CreateClientCommandHandler(IClientCommandRepository repository, IClientService clientService, ILoggerFactory loggerFactory)
        {
            _repository = repository;
            _clientService = clientService;
            _logger = loggerFactory.CreateLogger<CreateClientCommandHandler>();
        }

        public async Task<BaseResponse> Handle(CreateClientCommandRequest request, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"[Begin] - Creating client: { JsonSerializer.Serialize(request) }");

            try
            {
                var validationResult = request.Validate();

                if (!validationResult.IsValid)
                {
                    var validationResultErrors = ValidationResultHelper.GetValidationResultErrors(validationResult);

                    _logger.LogInformation($"[Error] - Request not valid: { validationResultErrors }");

                    return new BaseResponse(false, "Ocorreu um problema ao criar o cliente!", HttpStatusCode.BadRequest, validationResultErrors);
                }

                if (!await _clientService.CheckIfClientCpfIsUnique(request.Cpf))
                {
                    _logger.LogInformation($"[Error] - Request not valid: { request }");

                    return new BaseResponse(false, "Já existe um cliente cadastrado com o mesmo cpf!", HttpStatusCode.BadRequest,  request);
                }

                var client = new Domain.Entities.Client(request.FirstName, request.LastName, request.Cpf, request.Email, request.Username, request.Password, request.Role);

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
    }
}