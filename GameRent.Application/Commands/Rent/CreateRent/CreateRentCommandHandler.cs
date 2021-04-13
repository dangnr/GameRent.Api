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

namespace GameRent.Application.Commands.Rent.CreateRent
{
    public class CreateRentCommandHandler : IRequestHandler<CreateRentCommandRequest, BaseResponse>
    {
        private readonly IRentCommandRepository _repository;
        private readonly IRentService _rentService;
        private readonly ILogger<CreateRentCommandHandler> _logger;

        public CreateRentCommandHandler(IRentCommandRepository repository, IRentService rentService, ILoggerFactory iLoggerFactory)
        {
            _repository = repository;
            _rentService = rentService;
            _logger = iLoggerFactory.CreateLogger<CreateRentCommandHandler>();
        }

        public async Task<BaseResponse> Handle(CreateRentCommandRequest request, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"[Begin] - Creating rent : { JsonSerializer.Serialize(request) }");

            try
            {
                var validationResult = request.Validate();

                if (!validationResult.IsValid)
                {
                    var validationResultErrors = ValidationResultHelper.GetValidationResultErrors(validationResult);

                    _logger.LogInformation($"[Error] - Request not valid: { validationResultErrors }");

                    return new BaseResponse(false, "Ocorreu um problema ao criar o empréstimo!", HttpStatusCode.BadRequest, validationResultErrors);
                }

                request.SetGames(await _repository.GetGamesFromIds(request.GameIds));

                if (!_rentService.CheckIfGamesAreValid(request.GetGames()))
                {
                    _logger.LogInformation($"[Error] - Request not valid: { request }");

                    return new BaseResponse(false, "Um ou mais jogos não estão disponíveis para empréstimo!", HttpStatusCode.BadRequest, request);
                }

                if (!await _rentService.CheckIfClientExists(request.ClientId))
                {
                    _logger.LogInformation($"[Error] - Request not valid: { request }");

                    return new BaseResponse(false, "Cliente não encontrado na base dados!", HttpStatusCode.NotFound, request);
                }

                request.SetClient(await _repository.GetClientById(request.ClientId));

                var rent = new Domain.Aggregates.Rent(request.EndDate);

                rent.AddGamesRent(request.GetGames());
                rent.SetClient(request.GetClient());

                await _repository.Create(rent);

                _logger.LogInformation($"[End] - Rent successfully created: { JsonSerializer.Serialize(rent) }");

                return new BaseResponse(true, "Empréstimo criado com sucesso!", HttpStatusCode.OK, rent);
            }
            catch (Exception ex)
            {
                _logger.LogInformation($"[Error] - An error occurred while creating the rent: { JsonSerializer.Serialize(ex) }");

                return new BaseResponse(false, "Ocorreu um problema ao criar o empréstimo!", HttpStatusCode.InternalServerError, ex.InnerException.ToString());
            }
        }
    }
}