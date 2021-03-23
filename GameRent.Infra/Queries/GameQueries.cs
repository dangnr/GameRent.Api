using Dapper;
using GameRent.Application.Interfaces.Queries;
using GameRent.Application.ViewModels;
using GameRent.Domain.Shared;
using GameRent.Infra.Data.Context;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;

namespace GameRent.Infra.Queries
{
    public class GameQueries : IGameQueries
    {
        private readonly SqlConnection _sqlConnection;
        private readonly ILogger<GameQueries> _logger;

        public GameQueries(GameRentContext context, ILoggerFactory loggerFactory)
        {
            _sqlConnection = new SqlConnection(context.Database.GetConnectionString());
            _logger = loggerFactory.CreateLogger<GameQueries>();
        }

        public async Task<BaseResponse> GetAll()
        {
            _logger.LogInformation($"[Begin] - Getting all games");

            try
            {
                var query = @"SELECT 
                                Id,
                    	        Name,
	                            Genre,
                                Synopsis,
	                            Platform,
	                            LaunchDate,
                                IsAvailable,
	                            IsActive
                              FROM Game
                              ORDER BY Name";

                var result = (await _sqlConnection.QueryAsync<GameViewModel>(query)).AsList();

                if (result is null || result.Count == 0)
                {
                    _logger.LogInformation($"[Warning] - No records found!");

                    return new BaseResponse(false, "Nenhum registro encontrado!", HttpStatusCode.NotFound);
                }

                _logger.LogInformation($"[End] - Querie successfully executed!");

                return new BaseResponse(true, "Consulta realizada com sucesso!", HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                _logger.LogInformation($"[Error] - An error occurred while trying to execute the querie: { JsonSerializer.Serialize(ex) }");

                return new BaseResponse(false, "Ocorreu um problema ao realizar a consulta!", HttpStatusCode.InternalServerError, ex.InnerException.ToString());
            }
        }

        public async Task<BaseResponse> GetAllAvailable()
        {
            _logger.LogInformation($"[Begin] - Getting all available games");

            try
            {
                var query = @"SELECT 
                                Id,
                    	        Name,
	                            Genre,
                                Synopsis,
	                            Platform,
	                            LaunchDate,
                                IsAvailable,
	                            IsActive
                              FROM Game
                              WHERE IsAvailable = 'true'
                              ORDER BY Name";

                var result = (await _sqlConnection.QueryAsync<GameViewModel>(query)).AsList();

                if (result is null || result.Count == 0)
                {
                    _logger.LogInformation($"[Warning] - No records found!");

                    return new BaseResponse(false, "Nenhum registro encontrado!", HttpStatusCode.NotFound);
                }

                _logger.LogInformation($"[End] - Querie successfully executed!");

                return new BaseResponse(true, "Consulta realizada com sucesso!", HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                _logger.LogInformation($"[Error] - An error occurred while trying to execute the querie: { JsonSerializer.Serialize(ex) }");

                return new BaseResponse(false, "Ocorreu um problema ao realizar a consulta!", HttpStatusCode.InternalServerError, ex.InnerException.ToString());
            }
        }

        public async Task<BaseResponse> GetAllRented()
        {
            _logger.LogInformation($"[Begin] - Getting all rented games");

            try
            {
                var query = @"SELECT 
                                Id,
                    	        Name,
	                            Genre,
                                Synopsis,
	                            Platform,
	                            LaunchDate,
                                IsAvailable,
	                            IsActive
                              FROM Game
                              WHERE IsAvailable = 'false'
                              ORDER BY Name";

                var result = (await _sqlConnection.QueryAsync<GameViewModel>(query)).AsList();

                if (result is null || result.Count == 0)
                {
                    _logger.LogInformation($"[Warning] - No records found!");

                    return new BaseResponse(false, "Nenhum registro encontrado!", HttpStatusCode.NotFound);
                }

                _logger.LogInformation($"[End] - Querie successfully executed!");

                return new BaseResponse(true, "Consulta realizada com sucesso!", HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                _logger.LogInformation($"[Error] - An error occurred while trying to execute the querie: { JsonSerializer.Serialize(ex) }");

                return new BaseResponse(false, "Ocorreu um problema ao realizar a consulta!", HttpStatusCode.InternalServerError, ex.InnerException.ToString());
            }
        }

        public async Task<BaseResponse> GetById(Guid id)
        {
            _logger.LogInformation($"[Begin] - Getting game by id: {id}");

            try
            {
                var queryArgs = new DynamicParameters();

                queryArgs.Add("Id", id);

                var query = @"SELECT 
                                Id,
                                Name,
                                Genre,
                                Synopsis,
                                Platform,
                                LaunchDate,
                                IsAvailable,
                                IsActive
                              FROM Game
                              Where Id = @id";

                var result = (await _sqlConnection.QueryAsync<GameViewModel>(query, queryArgs)).FirstOrDefault();

                if (result is null)
                {
                    _logger.LogInformation($"[Warning] - No records found!");

                    return new BaseResponse(false, "Nenhum registro encontrado!", HttpStatusCode.NotFound);
                }

                _logger.LogInformation($"[End] - Querie successfully executed!");

                return new BaseResponse(true, "Consulta realizada com sucesso!", HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                _logger.LogInformation($"[Error] - An error occurred while trying to execute the querie: { JsonSerializer.Serialize(ex) }");

                return new BaseResponse(false, "Ocorreu um problema ao realizar a consulta!", HttpStatusCode.InternalServerError, ex.InnerException.ToString());
            }
        }
    }
}