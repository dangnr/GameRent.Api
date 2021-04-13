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

namespace GameRent.Application.Commands.Login
{
    public class LoginCommandHandler : IRequestHandler<LoginCommandRequest, BaseResponse>
    {
        private readonly IClientCommandRepository _repository;
        private readonly ITokenService _tokenService;
        private readonly ILogger<LoginCommandHandler> _logger;

        public LoginCommandHandler(IClientCommandRepository clientRepository, ITokenService tokenService, ILoggerFactory loggerFactory)
        {
            _repository = clientRepository;
            _tokenService = tokenService;
            _logger = loggerFactory.CreateLogger<LoginCommandHandler>();
        }

        public async Task<BaseResponse> Handle(LoginCommandRequest request, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"[Begin] - Authenticating user : { JsonSerializer.Serialize(request) }");

            try
            {
                var validationResult = request.Validate();

                if (!validationResult.IsValid)
                {
                    var validationResultErrors = ValidationResultHelper.GetValidationResultErrors(validationResult);

                    _logger.LogInformation($"[Error] - Request not valid: { validationResultErrors }");

                    return new BaseResponse(false, "Ocorreu um problema ao autenticar o usuário!", HttpStatusCode.BadRequest, validationResultErrors);
                }

                var user = await _repository.GetByUsernameAndPassword(request.Username, request.Password);

                if (user == null)
                {
                    _logger.LogInformation($"[Error] - Request not valid: { JsonSerializer.Serialize(request) }");

                    return new BaseResponse(false, "Usuário não encontrado na base de dados!", HttpStatusCode.NotFound, request);
                }

                var tokenResponse = await _tokenService.GenerateToken(user);

                if(!tokenResponse.Success)
                    return new BaseResponse(false, "Ocorreu um problema ao gerar o token para o usuário!", tokenResponse.Data);

                return new BaseResponse(true, "Usuário autenticado com sucesso!", HttpStatusCode.OK, new { request.Username, Token = tokenResponse.Data });
            }
            catch (Exception ex)
            {
                _logger.LogInformation($"[Error] - An error occurred while authenticating the user: { JsonSerializer.Serialize(ex) }");

                return new BaseResponse(false, "Ocorreu um problema ao autenticar o usuário!", HttpStatusCode.InternalServerError, ex.InnerException.ToString());
            }
        }
    }
}