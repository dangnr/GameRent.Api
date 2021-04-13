using Dapper;
using GameRent.Application.Interfaces.Queries;
using GameRent.Application.Queries.Client;
using GameRent.Application.ViewModels;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using static GameRent.Application.Shared.AppSettings;

namespace GameRent.Infra.Queries
{
    public class ClientQueryRepository : IClientQueryRepository
    {
        private readonly SqlConnection _sqlConnection;

        public ClientQueryRepository(IOptions<ConnectionStrings> connectionStrings)
        {
            _sqlConnection = new SqlConnection(connectionStrings.Value.DefaultConnection);
        }

        public async Task<List<ClientViewModel>> FilteredWhereAsync(ClientQueryRequest request)
        {
            var queryArgs = new DynamicParameters();

            var query = $@"SELECT 
                            Id,
                    	    FirstName,
	                        LastName,
                            Cpf,
	                        Email,
	                        IsActive
                           {FormatQueryFilter(request, ref queryArgs)}
                           ORDER BY FirstName";

            return (await _sqlConnection.QueryAsync<ClientViewModel>(query)).AsList();
        }

        public async Task<ClientViewModel> GetById(ClientQueryRequest request)
        {
            var queryArgs = new DynamicParameters();

            var query = $@"SELECT 
                            Id,
                    	    FirstName,
	                        LastName,
                            Cpf,
	                        Email,
	                        IsActive
                           {FormatQueryFilter(request, ref queryArgs)}
                           ORDER BY FirstName";

            return (await _sqlConnection.QueryAsync<ClientViewModel>(query, queryArgs)).FirstOrDefault();
        }

        private static string FormatQueryFilter(ClientQueryRequest request, ref DynamicParameters queryArgs)
        {
            var filter = string.Empty;

            if (request.Id != null)
            {
                queryArgs.Add("Id", request.Id);

                filter += filter.Length > 0
                    ? "AND Id = @Id "
                    : "WHERE Id = @Id ";
            }

            if (!string.IsNullOrWhiteSpace(request.Username))
            {
                queryArgs.Add("Username", request.Username);

                filter += filter.Length > 0
                    ? "AND Username = @Username "
                    : "WHERE Username = @Username";
            }

            if (request.Role.HasValue)
            {
                var role = (int)request.Role;

                queryArgs.Add("Role", role);

                filter += filter.Length > 0
                    ? "AND Role = @Role "
                    : "WHERE Role = @Role";
            }
            return filter;
        }
    }
}