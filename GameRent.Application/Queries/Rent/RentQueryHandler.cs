using GameRent.Application.Interfaces.Queries;
using GameRent.Application.Shared;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Net;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace GameRent.Application.Queries.Rent
{
    public class RentQueryHandler: IRequestHandler<RentQueryRequest, BaseResponse>
    {
        private readonly IRentQueryRepository _rentQueryRepository;
        private readonly ILogger<RentQueryHandler> _logger;

        public RentQueryHandler(IRentQueryRepository rentQueryRepository, ILoggerFactory loggerFactory)
        {
            _rentQueryRepository = rentQueryRepository;
            _logger = loggerFactory.CreateLogger<RentQueryHandler>();
        }
        public async Task<BaseResponse> Handle(RentQueryRequest request, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"[Begin] - Getting rent information.");

            try
            {
                var result = await _rentQueryRepository.FilteredWhereAsync(request);

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