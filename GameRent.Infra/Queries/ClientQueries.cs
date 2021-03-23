using Dapper;
using GameRent.Application.Interfaces.Queries;
using GameRent.Application.ViewModels;
using GameRent.Domain.Enums;
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
    public class ClientQueries : IClientQueries
    {
        private readonly SqlConnection _sqlConnection;
        private readonly ILogger<ClientQueries> _logger;

        public ClientQueries(GameRentContext context, ILoggerFactory loggerFactory)
        {
            _sqlConnection = new SqlConnection(context.Database.GetConnectionString());
            _logger = loggerFactory.CreateLogger<ClientQueries>();
        }

        public async Task<BaseResponse> GetAll()
        {
            _logger.LogInformation($"[Begin] - Getting all clients");

            try
            {
                var query = @"SELECT 
                                Id,
                    	        FirstName,
	                            LastName,
                                Cpf,
	                            Email,
	                            IsActive
                              FROM Client
                              ORDER BY FirstName";

                var result = (await _sqlConnection.QueryAsync<ClientViewModel>(query)).AsList();

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
            _logger.LogInformation($"[Begin] - Getting client by id: {id}");

            try
            {
                var queryArgs = new DynamicParameters();

                queryArgs.Add("Id", id);

                var query = @"SELECT 
                                Id,
                    	        FirstName,
	                            LastName,
                                Cpf,
	                            Email,
	                            IsActive
                              FROM Client
                              Where Id = @id";

                var result = (await _sqlConnection.QueryAsync<ClientViewModel>(query, queryArgs)).FirstOrDefault();

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

        public async Task<BaseResponse> GetByUsername(string username)
        {
            _logger.LogInformation($"[Begin] - Getting client by username: {username}");

            try
            {
                var queryArgs = new DynamicParameters();

                queryArgs.Add("Username", username);

                var query = @"SELECT 
                                Id,
                    	        FirstName,
	                            LastName,
                                Cpf,
	                            Email,
	                            IsActive
                              FROM Client
                              Where Username = @username";

                var result = (await _sqlConnection.QueryAsync<ClientViewModel>(query, queryArgs)).FirstOrDefault();

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

        public async Task<BaseResponse> GetByRole(UserRoleType role)
        {
            _logger.LogInformation($"[Begin] - Getting client by role: {(int)role}");

            try
            {
                var queryArgs = new DynamicParameters();

                queryArgs.Add("Role", (int)role);

                var query = @"SELECT 
                                Id,
                    	        FirstName,
	                            LastName,
                                Cpf,
	                            Email,
	                            IsActive
                              FROM Client
                              Where Role = @role
                              ORDER BY FirstName";

                var result = (await _sqlConnection.QueryAsync<ClientViewModel>(query, queryArgs)).AsList();

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
    }
}