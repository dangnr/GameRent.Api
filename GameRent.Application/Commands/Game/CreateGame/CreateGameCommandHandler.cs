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

namespace GameRent.Application.Commands.Game.CreateGame
{
    public class CreateGameCommandHandler : IRequestHandler<CreateGameCommandRequest, BaseResponse>
    {
        private readonly IGameCommandRepository _repository;
        private readonly IGameService _gameService;
        private readonly ILogger<CreateGameCommandHandler> _logger;

        public CreateGameCommandHandler(IGameCommandRepository repository, IGameService gameService, ILoggerFactory iLoggerFactory)
        {
            _repository = repository;
            _gameService = gameService;
            _logger = iLoggerFactory.CreateLogger<CreateGameCommandHandler>();
        }

        public async Task<BaseResponse> Handle(CreateGameCommandRequest request, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"[Begin] - Creating game : { JsonSerializer.Serialize(request) }");

            try
            {
                var validationResult = request.Validate();

                if (!validationResult.IsValid)
                {
                    var validationResultErrors = ValidationResultHelper.GetValidationResultErrors(validationResult);

                    _logger.LogInformation($"[Error] - Request not valid: { validationResultErrors }");

                    return new BaseResponse(false, "Ocorreu um problema ao criar o jogo!", HttpStatusCode.BadRequest, validationResultErrors);
                }

                if (!await _gameService.CheckIfGameNameIsUnique(request.Name))
                {
                    _logger.LogInformation($"[Error] - Request not valid: { request }");

                    return new BaseResponse(false, "Já existe um jogo cadastrado com o mesmo nome!", HttpStatusCode.BadRequest, request);
                }

                var game = new Domain.Entities.Game(request.Name, request.Genre, request.Synopsis, request.Platform, request.LaunchDate);

                await _repository.Create(game);

                _logger.LogInformation($"[End] - Game successfully created: { JsonSerializer.Serialize(game) }");

                return new BaseResponse(true, "Jogo criado com sucesso!", HttpStatusCode.OK, game);
            }
            catch (Exception ex)
            {
                _logger.LogInformation($"[Error] - An error occurred while creating the game: { JsonSerializer.Serialize(ex) }");

                return new BaseResponse(false, "Ocorreu um problema ao criar o jogo!", HttpStatusCode.InternalServerError, ex.InnerException.ToString());
            }
        }
    }
}