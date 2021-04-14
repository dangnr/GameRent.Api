using Dapper;
using GameRent.Application.Interfaces.Queries;
using GameRent.Application.Queries.Rent;
using GameRent.Application.ViewModels;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using static GameRent.Application.Shared.AppSettings;

namespace GameRent.Infra.Queries
{
    public class RentQueryRepository : IRentQueryRepository
    {
        private readonly SqlConnection _sqlConnection;

        public RentQueryRepository(IOptions<ConnectionStrings> connectionStrings)
        {
            _sqlConnection = new SqlConnection(connectionStrings.Value.DefaultConnection);
        }

        public async Task<List<RentViewModel>> GetFilteredItemsAsync(RentQueryRequest request)
        {
            var queryArgs = new DynamicParameters();

            var query = $@"SELECT 
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
                           FROM [Rent] r
                            INNER JOIN Client c ON c.Id = r.ClientId
                            INNER JOIN GameRent gr ON gr.RentId = r.Id
                            INNER JOIN Game g ON g.Id = gr.GameId
                           {FormatQueryFilter(request, ref queryArgs)}
                           ORDER BY r.StartDate";

            return (await _sqlConnection.QueryAsync<RentViewModel>(query)).AsList();
        }

        public async Task<RentViewModel> GetFilteredItemAsync(RentQueryRequest request)
        {
            var queryArgs = new DynamicParameters();

            var query = $@"SELECT 
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
                           FROM [Rent] r
                            INNER JOIN Client c ON c.Id = r.ClientId
                            INNER JOIN GameRent gr ON gr.RentId = r.Id
                            INNER JOIN Game g ON g.Id = gr.GameId
                           {FormatQueryFilter(request, ref queryArgs)}
                           ORDER BY r.StartDate";

            return GetResult(await _sqlConnection.QueryAsync<dynamic>(query, queryArgs)).FirstOrDefault();
        }

        private static string FormatQueryFilter(RentQueryRequest request, ref DynamicParameters queryArgs)
        {
            var filter = string.Empty;

            if (request.Id != null)
            {
                queryArgs.Add("Id", request.Id);

                filter += filter.Length > 0
                    ? "AND r.Id = @Id "
                    : "WHERE r.Id = @Id ";
            }

            if (request.ClientId != null)
            {
                queryArgs.Add("Id", request.ClientId);

                filter += filter.Length > 0
                    ? "AND c.Id = @Id "
                    : "WHERE c.Id = @Id ";
            }

            if (request.IsActive.HasValue)
            {
                if (request.IsActive.Value is true)
                {
                    queryArgs.Add("IsActive", request.IsActive.Value);

                    filter += filter.Length > 0
                        ? "AND r.IsActive = @IsActive AND r.ReturnedDate IS NOT NULL "
                        : "WHERE r.IsActive = @IsActive AND r.ReturnedDate IS NOT NULL";
                }
                else
                {
                    queryArgs.Add("IsActive", request.IsActive.Value);

                    filter += filter.Length > 0
                        ? "AND r.IsActive = @IsActive "
                        : "WHERE r.IsActive = @IsActive";
                }
            }

            return filter;
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