using GameRent.Application.Interfaces.Services;
using GameRent.Domain.Entities;
using GameRent.Domain.Helpers;
using GameRent.Domain.Shared;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using static GameRent.Domain.Shared.AppSettings;

namespace GameRent.Services.Services
{
    public class TokenService : ITokenService
    {
        private readonly IOptions<TokenSettings> _tokenSettings;
        private readonly ILogger<TokenService> _logger;

        public TokenService(IOptions<TokenSettings> tokenSettings, ILoggerFactory loggerFactory)
        {
            _tokenSettings = tokenSettings;
            _logger = loggerFactory.CreateLogger<TokenService>();
        }

        public async Task<BaseResponse> GenerateToken(Client client)
        {
            _logger.LogInformation($"[Begin] - Generating token for client: { JsonSerializer.Serialize(client) }");

            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();

                var key = Encoding.ASCII.GetBytes(_tokenSettings.Value.TokenSecret);

                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                        new Claim(ClaimTypes.Name, client.Username.ToString()),
                        new Claim(ClaimTypes.Role, client.Role.GetDescription())
                    }),

                    Expires = DateTime.UtcNow.AddMinutes(_tokenSettings.Value.ExpirationMinutes),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };

                var result = tokenHandler.CreateToken(tokenDescriptor);
                var token = tokenHandler.WriteToken(result);

                _logger.LogInformation($"[End] - Token successfully generated: { JsonSerializer.Serialize(token) }");

                return await Task.FromResult(new BaseResponse(true, "Token gerado com sucesso!", token));
            }
            catch (Exception ex)
            {
                _logger.LogInformation($"[Error] - An error occurred while generating the token for client: { JsonSerializer.Serialize(ex) }");

                return new BaseResponse(false, "Ocorreu um problema ao gerar o token de acesso!", ex.InnerException.ToString());
            }
        }
    }
}