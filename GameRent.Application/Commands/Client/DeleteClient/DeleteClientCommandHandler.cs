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

namespace GameRent.Application.Commands.Client.DeleteClient
{
    public class DeleteClientCommandHandler : IRequestHandler<DeleteClientCommandRequest, BaseResponse>
    {
        private readonly IClientCommandRepository _repository;
        private readonly IClientService _clientService;
        private readonly ILogger<DeleteClientCommandHandler> _logger;

        public DeleteClientCommandHandler(IClientCommandRepository repository, IClientService clientService, ILoggerFactory loggerFactory)
        {
            _repository = repository;
            _clientService = clientService;
            _logger = loggerFactory.CreateLogger<DeleteClientCommandHandler>();
        }

        public async Task<BaseResponse> Handle(DeleteClientCommandRequest request, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"[Begin] - Deleting client : { JsonSerializer.Serialize(request) }");

            try
            {
                var validationResult = request.Validate();

                if (!validationResult.IsValid)
                {
                    var validationResultErrors = ValidationResultHelper.GetValidationResultErrors(validationResult);

                    _logger.LogInformation($"[Error] - Request not valid: { validationResultErrors }");

                    return new BaseResponse(false, "Ocorreu um problema ao deletar o cliente!", HttpStatusCode.BadRequest, validationResultErrors);
                }

                if (await _clientService.CheckIfClientExists(request.Id))
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
    }
}