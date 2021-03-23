using Dapper;
using GameRent.Application.Interfaces.Queries;
using GameRent.Application.ViewModels;
using GameRent.Domain.Shared;
using GameRent.Infra.Data.Context;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;

namespace GameRent.Infra.Queries
{
    public class RentQueries : IRentQueries
    {
        private readonly SqlConnection _sqlConnection;
        private readonly ILogger<RentQueries> _logger;

        public RentQueries(GameRentContext context, ILoggerFactory loggerFactory)
        {
            _sqlConnection = new SqlConnection(context.Database.GetConnectionString());
            _logger = loggerFactory.CreateLogger<RentQueries>();
        }

        public async Task<BaseResponse> GetAll()
        {
            _logger.LogInformation($"[Begin] - Getting all rents");

            try
            {
                var query = @"SELECT 
                                r.Id,
                    	        r.StartDate,
	                            r.EndDate,
                                r.ReturnedDate,
                                r.IsActive,
                                c.Id AS Client_Id,
	                            c.FirstName AS Client_FirstName,
	                            c.LastName AS Client_LastName,
	                            c.Cpf AS Client_Cpf,
                                c.Email AS Client_Email,
                                c.IsActive AS Client_IsActive,
                                g.Id AS Games_Id,
                                g.Name AS Games_Name,
                                g.IsActive AS Games_IsActive
                              FROM Rent r
                                INNER JOIN Client c ON c.Id = r.ClientId
                                INNER JOIN GameRent gr ON gr.RentId = r.Id
                                INNER JOIN Game g ON g.Id = gr.GameId
                              ORDER BY r.StartDate";

                var result = GetResult(await _sqlConnection.QueryAsync<dynamic>(query));

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
            _logger.LogInformation($"[Begin] - Getting rent by id: {id}");

            try
            {
                var queryArgs = new DynamicParameters();

                queryArgs.Add("Id", id);

                var query = @"SELECT 
                                r.Id,
                    	        r.StartDate,
	                            r.EndDate,
                                r.ReturnedDate,
                                r.IsActive,
                                c.Id AS Client_Id,
	                            c.FirstName AS Client_FirstName,
	                            c.LastName AS Client_LastName,
	                            c.Cpf AS Client_Cpf,
                                c.Email AS Client_Email,
                                c.IsActive AS Client_IsActive,
                                g.Id AS Games_Id,
                                g.Name AS Games_Name,
                                g.IsActive AS Games_IsActive
                              FROM Rent r
                                INNER JOIN Client c ON c.Id = r.ClientId
                                INNER JOIN GameRent gr ON gr.RentId = r.Id
                                INNER JOIN Game g ON g.Id = gr.GameId
                              Where r.Id = @id
                              ORDER BY r.StartDate";

                var result = GetResult(await _sqlConnection.QueryAsync<dynamic>(query, queryArgs)).FirstOrDefault();

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

        public async Task<BaseResponse> GetAllFinished()
        {
            _logger.LogInformation($"[Begin] - Getting all finished rents");

            try
            {
                var query = @"SELECT 
                                r.Id,
                    	        r.StartDate,
	                            r.EndDate,
                                r.ReturnedDate,
                                r.IsActive,
                                c.Id AS Client_Id,
	                            c.FirstName AS Client_FirstName,
	                            c.LastName AS Client_LastName,
	                            c.Cpf AS Client_Cpf,
                                c.Email AS Client_Email,
                                c.IsActive AS Client_IsActive,
                                g.Id AS Games_Id,
                                g.Name AS Games_Name,
                                g.IsActive AS Games_IsActive
                              FROM Rent r
                                INNER JOIN Client c ON c.Id = r.ClientId
                                INNER JOIN GameRent gr ON gr.RentId = r.Id
                                INNER JOIN Game g ON g.Id = gr.GameId
                              Where r.IsActive = 'false' AND r.ReturnedDate IS NOT NULL
                              ORDER BY r.StartDate";

                var result = GetResult(await _sqlConnection.QueryAsync<dynamic>(query));

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

        public async Task<BaseResponse> GetByClientId(Guid id)
        {
            _logger.LogInformation($"[Begin] - Getting all rents by clientId: {id}");

            try
            {
                var queryArgs = new DynamicParameters();

                queryArgs.Add("Id", id);

                var query = @"SELECT 
                                r.Id,
                    	        r.StartDate,
	                            r.EndDate,
                                r.ReturnedDate,
                                r.IsActive,
                                c.Id AS Client_Id,
	                            c.FirstName AS Client_FirstName,
	                            c.LastName AS Client_LastName,
	                            c.Cpf AS Client_Cpf,
                                c.Email AS Client_Email,
                                c.IsActive AS Client_IsActive,
                                g.Id AS Games_Id,
                                g.Name AS Games_Name,
                                g.IsActive AS Games_IsActive
                              FROM Rent r
                                INNER JOIN Client c ON c.Id = r.ClientId
                                INNER JOIN GameRent gr ON gr.RentId = r.Id
                                INNER JOIN Game g ON g.Id = gr.GameId
                              Where c.Id = @id
                              ORDER BY r.StartDate";

                var result = GetResult(await _sqlConnection.QueryAsync<dynamic>(query, queryArgs));

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

        private static List<RentViewModel> GetResult(IEnumerable<dynamic> result)
        {
            Slapper.AutoMapper.Configuration.AddIdentifier(typeof(RentViewModel), "Id");
            Slapper.AutoMapper.Configuration.AddIdentifier(typeof(ClientViewModel), "Id");
            Slapper.AutoMapper.Configuration.AddIdentifier(typeof(List<GameViewModel>), "Id");

            var rentViewModels = Slapper.AutoMapper.MapDynamic<RentViewModel>(result).ToList();

            // Group and set Games with the same RentId
            if (rentViewModels?.Count > 1)
            {
                var group = rentViewModels?.GroupBy(r => r.Id);

                rentViewModels?.ForEach(rentViewModel =>
                {
                    rentViewModel.Games = group?.SelectMany(g => g.Where(g => g.Id == rentViewModel.Id))
                        ?.SelectMany(x => x.Games)
                        ?.ToList();
                });
            }

            return rentViewModels;
        }
    }
}