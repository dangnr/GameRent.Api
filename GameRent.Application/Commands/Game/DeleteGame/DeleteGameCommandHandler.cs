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

namespace GameRent.Application.Commands.Game.DeleteGame
{
    public class DeleteGameCommandHandler : IRequestHandler<DeleteGameCommandRequest, BaseResponse>
    {
        private readonly IGameCommandRepository _repository;
        private readonly IGameService _gameService;
        private readonly ILogger<DeleteGameCommandHandler> _logger;

        public DeleteGameCommandHandler(IGameCommandRepository repository, IGameService gameService, ILoggerFactory iLoggerFactory)
        {
            _repository = repository;
            _gameService = gameService;
            _logger = iLoggerFactory.CreateLogger<DeleteGameCommandHandler>();
        }

        public async Task<BaseResponse> Handle(DeleteGameCommandRequest request, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"[Begin] - Deleting game : { JsonSerializer.Serialize(request) }");

            try
            {
                var validationResult = request.Validate();

                if (!validationResult.IsValid)
                {
                    var validationResultErrors = ValidationResultHelper.GetValidationResultErrors(validationResult);

                    _logger.LogInformation($"[Error] - Request not valid: { validationResultErrors }");

                    return new BaseResponse(false, "Ocorreu um problema ao deletar o jogo!", HttpStatusCode.BadRequest, validationResultErrors);
                }

                if (await _gameService.CheckIfGameExists(request.Id))
                {
                    _logger.LogInformation($"[Error] - Request not valid: { request }");

                    return new BaseResponse(false, "Jogo não existe na base de dados!", HttpStatusCode.NotFound, request);
                }

                await _repository.Delete(request.Id);

                _logger.LogInformation($"[End] - Game successfully deleted: { JsonSerializer.Serialize(request) }");

                return new BaseResponse(true, "Jogo deletado com sucesso!", HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                _logger.LogInformation($"[Error] - An error occurred while deleting the game: { JsonSerializer.Serialize(ex) }");

                return new BaseResponse(false, "Ocorreu um problema ao deletar o jogo!", HttpStatusCode.InternalServerError, ex.InnerException.ToString());
            }
        }
    }
}