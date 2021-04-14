using GameRent.Application.Interfaces.Queries;
using GameRent.Application.Shared;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Net;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace GameRent.Application.Queries.Client
{
    public class ClientQueryHandler : IRequestHandler<ClientQueryRequest, BaseResponse>
    {
        private readonly IClientQueryRepository _clientQueryRepository;
        private readonly ILogger<ClientQueryHandler> _logger;

        public ClientQueryHandler(IClientQueryRepository clientQueryRepository, ILoggerFactory loggerFactory)
        {
            _clientQueryRepository = clientQueryRepository;
            _logger = loggerFactory.CreateLogger<ClientQueryHandler>();
        }

        public async Task<BaseResponse> Handle(ClientQueryRequest request, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"[Begin] - Getting client information.");

            try
            {
                object result = (request.Id.HasValue || !string.IsNullOrEmpty(request.Username) ?
                    await _clientQueryRepository.GetFilteredItemAsync(request) :
                    await _clientQueryRepository.GetFilteredItemsAsync(request));

                if (result is null)
                {
                    _logger.LogInformation($"[Warning] - No records found!");

                    return new BaseResponse(false, "Nenhum registro encontrado!", HttpStatusCode.NotFound);
                }

                _logger.LogInformation($"[End] - Query successfully executed!");

                return new BaseResponse(true, "Consulta realizada com sucesso!", HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                _logger.LogInformation($"[Error] - An error occurred while trying to execute the query: { JsonSerializer.Serialize(ex) }");

                return new BaseResponse(false, "Ocorreu um problema ao realizar a consulta!", HttpStatusCode.InternalServerError, ex.InnerException.ToString());
            }
        }
    }
}