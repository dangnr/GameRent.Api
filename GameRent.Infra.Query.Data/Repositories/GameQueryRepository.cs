using Dapper;
using GameRent.Application.Interfaces.Queries;
using GameRent.Application.Queries.Game;
using GameRent.Application.ViewModels;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using static GameRent.Application.Shared.AppSettings;

namespace GameRent.Infra.Queries
{
    public class GameQueryRepository : IGameQueryRepository
    {
        private readonly SqlConnection _sqlConnection;

        public GameQueryRepository(IOptions<ConnectionStrings> connectionStrings)
        {
            _sqlConnection = new SqlConnection(connectionStrings.Value.DefaultConnection);
        }

        public async Task<List<GameViewModel>> GetFilteredItemsAsync(GameQueryRequest request)
        {
            var queryArgs = new DynamicParameters();

            var query = $@"SELECT 
                            Id,
                    	    Name,
	                        Genre,
                            Synopsis,
	                        Platform,
	                        LaunchDate,
                            IsAvailable,
	                        IsActive
                           FROM [Game]
                           {FormatQueryFilter(request, ref queryArgs)}
                           ORDER BY Name";

            return (await _sqlConnection.QueryAsync<GameViewModel>(query)).AsList();
        }

        public async Task<GameViewModel> GetFilteredItemAsync(GameQueryRequest request)
        {
            var queryArgs = new DynamicParameters();

            var query = $@"SELECT 
                            Id,
                            Name,
                            Genre,
                            Synopsis,
                            Platform,
                            LaunchDate,
                            IsAvailable,
                            IsActive
                           FROM [Game]
                           {FormatQueryFilter(request, ref queryArgs)}
                           ORDER BY Name";

            return (await _sqlConnection.QueryAsync<GameViewModel>(query, queryArgs)).FirstOrDefault();
        }

        private static string FormatQueryFilter(GameQueryRequest request, ref DynamicParameters queryArgs)
        {
            var filter = string.Empty;

            if (request.Id.HasValue)
            {
                queryArgs.Add("Id", request.Id);

                filter += filter.Length > 0
                    ? "AND Id = @Id "
                    : "WHERE Id = @Id ";
            }

            if (request.IsAvailable.HasValue)
            {
                queryArgs.Add("IsAvailable", request.IsAvailable.Value);

                filter += filter.Length > 0
                    ? "AND IsAvailable = @IsAvailable "
                    : "WHERE IsAvailable = @IsAvailable";
            }

            return filter;
        }
    }
}