using Dapper;
using GameRent.Application.Interfaces.Queries;
using GameRent.Application.ViewModels;
using GameRent.Infra.Data.Context;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameRent.Infra.Queries
{
    public class RentQueries : IRentQueries
    {
        private readonly SqlConnection _sqlConnection;

        public RentQueries(GameRentContext context)
        {
            _sqlConnection = new SqlConnection(context.Database.GetConnectionString());
        }

        public async Task<List<RentViewModel>> GetAll()
        {
            var query = @"SELECT 
                          r.Id,
                    	  r.StartDate,
	                      r.EndDate,
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

            var result = await _sqlConnection.QueryAsync<dynamic>(query);

            return GetResult(result);
        }

        public async Task<RentViewModel> GetById(Guid id)
        {
            var queryArgs = new DynamicParameters();

            queryArgs.Add("Id", id);

            var query = @"SELECT 
                          r.Id,
                    	  r.StartDate,
	                      r.EndDate,
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

            var result = await _sqlConnection.QueryAsync<dynamic>(query, queryArgs);

            return GetResult(result).FirstOrDefault();
        }

        public async Task<List<RentViewModel>> GetByClientId(Guid id)
        {
            var queryArgs = new DynamicParameters();

            queryArgs.Add("Id", id);

            var query = @"SELECT 
                          r.Id,
                    	  r.StartDate,
	                      r.EndDate,
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

            var result = await _sqlConnection.QueryAsync<dynamic>(query, queryArgs);

            return GetResult(result);
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