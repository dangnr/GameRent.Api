using GameRent.Domain.Commands.Game;
using GameRent.Domain.Entities;
using GameRent.Domain.Interfaces.Command;
using GameRent.Domain.Interfaces.Repositories;
using GameRent.Domain.Shared;
using GameRent.Domain.Util;
using Microsoft.Extensions.Logging;
using System;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace GameRent.Application.Handlers
{
    public class GameCommandHandler : IGameCommandHandler
    {
        private readonly IGameRepository _repository;
        private readonly ILogger<GameCommandHandler> _logger;

        public GameCommandHandler(IGameRepository repository, ILoggerFactory iLoggerFactory)
        {
            _repository = repository;
            _logger = iLoggerFactory.CreateLogger<GameCommandHandler>();
        }

        public async Task<BaseResponse> Handle(CreateGameCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"[Begin] - Creating game : { JsonSerializer.Serialize(request) }");

            try
            {
                var validationResult = request.Validate();

                if (!validationResult.IsValid)
                {
                    var validationResultErrors = ValidationResultUtil.GetValidationResultErrors(validationResult);

                    _logger.LogInformation($"[Error] - Request not valid: { validationResultErrors }");

                    return new BaseResponse(false, "Ocorreu um problema ao criar o jogo!", validationResultErrors);
                }

                if (!await CheckIfGameNameIsUnique(request.Name))
                {
                    _logger.LogInformation($"[Error] - Request not valid: { request }");

                    return new BaseResponse(false, "Já existe um jogo cadastrado com o mesmo nome!", request);
                }

                var game = new Game(request.Name, request.Genre, request.Synopsis, request.Platform, request.LaunchDate);

                await _repository.Create(game);

                _logger.LogInformation($"[End] - Game successfully created: { JsonSerializer.Serialize(game) }");

                return new BaseResponse(true, "Jogo criado com sucesso!", game);
            }
            catch (Exception ex)
            {
                _logger.LogInformation($"[Error] - An error occurred while creating the game: { JsonSerializer.Serialize(ex) }");

                return new BaseResponse(false, "Ocorreu um problema ao criar o jogo!", ex.InnerException.ToString());
            }
        }

        public async Task<BaseResponse> Handle(UpdateGameCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"[Begin] - Updating game : { JsonSerializer.Serialize(request) }");

            try
            {
                var validationResult = request.Validate();

                if (!validationResult.IsValid)
                {
                    var validationResultErrors = ValidationResultUtil.GetValidationResultErrors(validationResult);

                    _logger.LogInformation($"[Error] - Request not valid: { validationResultErrors }");

                    return new BaseResponse(false, "Ocorreu um problema ao atualizar o jogo!", validationResultErrors);
                }

                if (await CheckIfGameExists(request.Id))
                {
                    _logger.LogInformation($"[Error] - Request not valid: { request }");

                    return new BaseResponse(false, "Jogo não existe na base de dados!", request);
                }

                if (!await CheckIfGameNameIsUnique(request.Name, request.Id))
                {
                    _logger.LogInformation($"[Error] - Request not valid: { request }");

                    return new BaseResponse(false, "Já existe um jogo cadastrado com o mesmo nome!", request);
                }

                var game = new Game(request.Id, request.Name, request.Genre, request.Synopsis, request.Platform, request.LaunchDate, request.IsAvailable, request.IsActive);

                await _repository.Update(game);

                _logger.LogInformation($"[End] - Game successfully updated: { JsonSerializer.Serialize(game) }");

                return new BaseResponse(true, "Jogo atualizado com sucesso!", game);
            }
            catch (Exception ex)
            {
                _logger.LogInformation($"[Error] - An error occurred while updating the game: { JsonSerializer.Serialize(ex) }");

                return new BaseResponse(false, "Ocorreu um problema ao atualizar o jogo!", ex.InnerException.ToString());
            }
        }

        public async Task<BaseResponse> Handle(DeleteGameCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"[Begin] - Deleting game : { JsonSerializer.Serialize(request) }");

            try
            {
                var validationResult = request.Validate();

                if (!validationResult.IsValid)
                {
                    var validationResultErrors = ValidationResultUtil.GetValidationResultErrors(validationResult);

                    _logger.LogInformation($"[Error] - Request not valid: { validationResultErrors }");

                    return new BaseResponse(false, "Ocorreu um problema ao deletar o jogo!", validationResultErrors);
                }

                if (await CheckIfGameExists(request.Id))
                {
                    _logger.LogInformation($"[Error] - Request not valid: { request }");

                    return new BaseResponse(false, "Jogo não existe na base de dados!", request);
                }

                await _repository.Delete(request.Id);

                _logger.LogInformation($"[End] - Game successfully deleted: { JsonSerializer.Serialize(request) }");

                return new BaseResponse(true, "Jogo deletado com sucesso!");
            }
            catch (Exception ex)
            {
                _logger.LogInformation($"[Error] - An error occurred while deleting the game: { JsonSerializer.Serialize(ex) }");

                return new BaseResponse(false, "Ocorreu um problema ao deletar o jogo!", ex.InnerException.ToString());
            }
        }

        private async Task<bool> CheckIfGameNameIsUnique(string name, Guid? id = null) =>
             !id.HasValue ?
                await _repository.IsUniqueGameName(name) :
                await _repository.IsUniqueGameName(id.Value, name);

        private async Task<bool> CheckIfGameExists(Guid id) => (await _repository.GetById(id) == null);
    }
}