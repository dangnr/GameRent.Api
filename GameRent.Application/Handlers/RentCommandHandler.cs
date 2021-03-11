using GameRent.Domain.Aggregates;
using GameRent.Domain.Commands.Rent;
using GameRent.Domain.Interfaces.Command;
using GameRent.Domain.Interfaces.Repositories;
using GameRent.Domain.Shared;
using GameRent.Domain.Util;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace GameRent.Application.Handlers
{
    public class RentCommandHandler : IRentCommandHandler
    {
        private readonly IRentRepository _repository;
        private readonly ILogger<GameCommandHandler> _logger;

        public RentCommandHandler(IRentRepository repository, ILoggerFactory iLoggerFactory)
        {
            _repository = repository;
            _logger = iLoggerFactory.CreateLogger<GameCommandHandler>();
        }

        public async Task<BaseResponse> Handle(CreateRentCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"[Begin] - Creating rent : { JsonSerializer.Serialize(request) }");

            try
            {
                var validationResult = request.Validate();

                if (!validationResult.IsValid)
                {
                    var validationResultErrors = ValidationResultUtil.GetValidationResultErrors(validationResult);

                    _logger.LogInformation($"[Error] - Request not valid: { validationResultErrors }");

                    return new BaseResponse(false, "Ocorreu um problema ao criar o empréstimo!", validationResultErrors);
                }

                request.SetGames(await _repository.GetGamesFromIds(request.GameIds));

                if (!CheckIfGamesAreValid(request.GetGames()))
                {
                    _logger.LogInformation($"[Error] - Request not valid: { request }");

                    return new BaseResponse(false, "Um ou mais jogos não estão disponíveis para empréstimo!", request);
                }

                request.SetClient(await _repository.GetClientFromId(request.ClientId));

                var rent = new Rent(request.EndDate);

                rent.AddGamesRent(request.GetGames());
                rent.SetClient(request.GetClient());

                await _repository.Create(rent);

                _logger.LogInformation($"[End] - Rent successfully created: { JsonSerializer.Serialize(rent) }");

                return new BaseResponse(true, "Empréstimo criado com sucesso!", rent);
            }
            catch (Exception ex)
            {
                _logger.LogInformation($"[Error] - An error occurred while creating the rent: { JsonSerializer.Serialize(ex) }");

                return new BaseResponse(false, "Ocorreu um problema ao criar o empréstimo!", ex.InnerException.ToString());
            }
        }

        public async Task<BaseResponse> Handle(FinishRentCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"[Begin] - Finishing rent : { JsonSerializer.Serialize(request) }");

            try
            {
                var validationResult = request.Validate();

                if (!validationResult.IsValid)
                {
                    var validationResultErrors = ValidationResultUtil.GetValidationResultErrors(validationResult);

                    _logger.LogInformation($"[Error] - Request not valid: { validationResultErrors }");

                    return new BaseResponse(false, "Ocorreu um problema ao encerrar o empréstimo!", validationResultErrors);
                }

                if (!await CheckIfRentExists(request.Id))
                {
                    _logger.LogInformation($"[Error] - Request not valid: { request }");

                    return new BaseResponse(false, "Empréstimo não existe na base de dados!", request);
                }

                await _repository.Finish(request.Id);

                _logger.LogInformation($"[End] - Rent successfully finished: { JsonSerializer.Serialize(request) }");

                return new BaseResponse(true, "Empréstimo encerrado com sucesso!");
            }
            catch (Exception ex)
            {
                _logger.LogInformation($"[Error] - An error occurred while finishing the rent: { JsonSerializer.Serialize(ex) }");

                return new BaseResponse(false, "Ocorreu um problema ao encerrar o empréstimo!", ex.InnerException.ToString());
            }
        }

        private async Task<bool> CheckIfRentExists(Guid id) => (await _repository.GetById(id) != null);

        private bool CheckIfGamesAreValid(List<Domain.Entities.Game> games) => games.All(x => x.IsAvailable);
    }
}