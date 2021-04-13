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

namespace GameRent.Application.Commands.Game.UpdateGame
{
    public class UpdateGameCommandHandler : IRequestHandler<UpdateGameCommandRequest, BaseResponse>
    {
        private readonly IGameCommandRepository _repository;
        private readonly IGameService _gameService;
        private readonly ILogger<UpdateGameCommandHandler> _logger;

        public UpdateGameCommandHandler(IGameCommandRepository repository, IGameService gameService, ILoggerFactory iLoggerFactory)
        {
            _repository = repository;
            _gameService = gameService;
            _logger = iLoggerFactory.CreateLogger<UpdateGameCommandHandler>();
        }

        public async Task<BaseResponse> Handle(UpdateGameCommandRequest request, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"[Begin] - Updating game : { JsonSerializer.Serialize(request) }");

            try
            {
                var validationResult = request.Validate();

                if (!validationResult.IsValid)
                {
                    var validationResultErrors = ValidationResultHelper.GetValidationResultErrors(validationResult);

                    _logger.LogInformation($"[Error] - Request not valid: { validationResultErrors }");

                    return new BaseResponse(false, "Ocorreu um problema ao atualizar o jogo!", HttpStatusCode.BadRequest, validationResultErrors);
                }

                if (await _gameService.CheckIfGameExists(request.Id))
                {
                    _logger.LogInformation($"[Error] - Request not valid: { request }");

                    return new BaseResponse(false, "Jogo não existe na base de dados!", HttpStatusCode.NotFound, request);
                }

                if (!await _gameService.CheckIfGameNameIsUnique(request.Name, request.Id))
                {
                    _logger.LogInformation($"[Error] - Request not valid: { request }");

                    return new BaseResponse(false, "Já existe um jogo cadastrado com o mesmo nome!", HttpStatusCode.BadRequest, request);
                }

                var game = new Domain.Entities.Game(request.Id, request.Name, request.Genre, request.Synopsis, request.Platform, request.LaunchDate, request.IsAvailable, request.IsActive);

                await _repository.Update(game);

                _logger.LogInformation($"[End] - Game successfully updated: { JsonSerializer.Serialize(game) }");

                return new BaseResponse(true, "Jogo atualizado com sucesso!", HttpStatusCode.OK, game);
            }
            catch (Exception ex)
            {
                _logger.LogInformation($"[Error] - An error occurred while updating the game: { JsonSerializer.Serialize(ex) }");

                return new BaseResponse(false, "Ocorreu um problema ao atualizar o jogo!", HttpStatusCode.InternalServerError, ex.InnerException.ToString());
            }
        }
    }
}