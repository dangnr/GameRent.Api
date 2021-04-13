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

namespace GameRent.Application.Commands.Rent.FinishRent
{
    public class FinishRentCommandHandler : IRequestHandler<FinishRentCommandRequest, BaseResponse>
    {
        private readonly IRentCommandRepository _repository;
        private readonly IRentService _rentService;
        private readonly ILogger<FinishRentCommandHandler> _logger;

        public FinishRentCommandHandler(IRentCommandRepository repository, IRentService rentService, ILoggerFactory iLoggerFactory)
        {
            _repository = repository;
            _rentService = rentService;
            _logger = iLoggerFactory.CreateLogger<FinishRentCommandHandler>();
        }

        public async Task<BaseResponse> Handle(FinishRentCommandRequest request, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"[Begin] - Finishing rent : { JsonSerializer.Serialize(request) }");

            try
            {
                var validationResult = request.Validate();

                if (!validationResult.IsValid)
                {
                    var validationResultErrors = ValidationResultHelper.GetValidationResultErrors(validationResult);

                    _logger.LogInformation($"[Error] - Request not valid: { validationResultErrors }");

                    return new BaseResponse(false, "Ocorreu um problema ao encerrar o empréstimo!", HttpStatusCode.BadRequest, validationResultErrors);
                }

                if (!await _rentService.CheckIfRentExists(request.Id))
                {
                    _logger.LogInformation($"[Error] - Request not valid: { request }");

                    return new BaseResponse(false, "Empréstimo não existe na base de dados!", HttpStatusCode.NotFound, request);
                }

                await _repository.Finish(request.Id);

                _logger.LogInformation($"[End] - Rent successfully finished: { JsonSerializer.Serialize(request) }");

                return new BaseResponse(true, "Empréstimo encerrado com sucesso!", HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                _logger.LogInformation($"[Error] - An error occurred while finishing the rent: { JsonSerializer.Serialize(ex) }");

                return new BaseResponse(false, "Ocorreu um problema ao encerrar o empréstimo!", HttpStatusCode.InternalServerError, ex.InnerException.ToString());
            }
        }
    }
}